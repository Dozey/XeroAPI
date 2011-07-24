using System;
using System.ServiceModel.Channels;

namespace XeroAPI.Client.ServiceModel
{
    class XeroMessageEncoderFactory : MessageEncoderFactory
    {
        private readonly MessageEncoder _encoder;

        public XeroMessageEncoderFactory(MessageEncoderFactory innerEncoderFactory)
        {
            if (innerEncoderFactory == null)
                throw new ArgumentNullException("innerEncoderFactory");

            _encoder = new XeroMessageEncoder(innerEncoderFactory.Encoder);
        }

        public override MessageEncoder Encoder
        {
            get { return _encoder; }
        }

        public override MessageVersion MessageVersion
        {
            get { return _encoder.MessageVersion; }
        }
    }
}
