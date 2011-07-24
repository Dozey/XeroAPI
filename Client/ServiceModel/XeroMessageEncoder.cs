using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace XeroAPI.Client.ServiceModel
{
    class XeroMessageEncoder : MessageEncoder
    {
        private MessageEncoder _encoder;

        public XeroMessageEncoder(MessageEncoder innerEncoder)
        {
            if (innerEncoder == null)
                throw new ArgumentNullException("innerEncoder");

            _encoder = innerEncoder;
        }

        public override string ContentType
        {
            get { return "text/xml"; }
        }

        public override string MediaType
        {
            get { return _encoder.MediaType; }
        }

        public override bool IsContentTypeSupported(string contentType)
        {
            return (contentType == "text/xml" || base.IsContentTypeSupported(contentType));
        }

        public override MessageVersion MessageVersion
        {
            get { return _encoder.MessageVersion; }
        }

        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            Message response = null;

            using (MemoryStream ms1 = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count))
            using (MemoryStream ms2 = new MemoryStream())
            using (XmlTextWriter xml1 = new XmlTextWriter(ms2, Encoding.UTF8))
            {
                bufferManager.ReturnBuffer(buffer.Array);
                XDocument.Load(ms1).Root.LastNode.WriteTo(xml1);
                xml1.Flush();

                BufferManager bm = BufferManager.CreateBufferManager(3, (int)ms2.Length);
                byte[] newBuffer = bm.TakeBuffer((int)ms2.Length);
                Array.Copy(ms2.ToArray(), newBuffer, ms2.Length);

                response = _encoder.ReadMessage(new ArraySegment<byte>(newBuffer, 0, (int)ms2.Length), bm, contentType);
            }

            return response;
        }

        public override Message ReadMessage(System.IO.Stream stream, int maxSizeOfHeaders, string contentType)
        {
            var response = _encoder.ReadMessage(stream, maxSizeOfHeaders, contentType);
            return response;
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            if (message.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                HttpRequestMessageProperty httpMessage = message.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;

                if (httpMessage != null && httpMessage.Method == "POST")
                {
                    ArraySegment<byte> innerMessage = _encoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);

                    byte[] encodedMessage = Encoding.UTF8.GetBytes(String.Concat("xml=", HttpUtility.UrlEncode(Encoding.UTF8.GetString(innerMessage.Array, 0, innerMessage.Count))));

                    bufferManager.ReturnBuffer(innerMessage.Array);

                    byte[] buffer = bufferManager.TakeBuffer(encodedMessage.Length);

                    Array.Copy(encodedMessage, buffer, encodedMessage.Length);

                    bufferManager.ReturnBuffer(buffer);

                    return new ArraySegment<byte>(buffer, messageOffset, encodedMessage.Length);
                }
                else
                {
                    return _encoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
                }
            }
            else
            {
                return _encoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
            }
        }

        public override void WriteMessage(Message message, System.IO.Stream stream)
        {
            HttpRequestMessageProperty httpMessage = message.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;


            if (httpMessage != null && httpMessage.Method == "POST")
            {
                byte[] buffer;

                using (MemoryStream ms = new MemoryStream())
                {
                    _encoder.WriteMessage(message, ms);
                    buffer = Encoding.UTF8.GetBytes(String.Concat("xml=", HttpUtility.UrlEncode(Encoding.UTF8.GetString(ms.ToArray()))));
                }

                stream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                _encoder.WriteMessage(message, stream);
            }
        }
    }
}
