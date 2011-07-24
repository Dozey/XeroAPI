using System;
using System.ServiceModel.Channels;

namespace XeroAPI.Client.ServiceModel
{
    class XeroMessageEncodingBindingElement : MessageEncodingBindingElement
    {
        private MessageEncodingBindingElement _innerEncodingBindingElement;

        public XeroMessageEncodingBindingElement()
            : this(new TextMessageEncodingBindingElement())
        {
        }

        public XeroMessageEncodingBindingElement(MessageEncodingBindingElement messageEncodingBindingElement)
        {
            _innerEncodingBindingElement = messageEncodingBindingElement;
        }

        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new XeroMessageEncoderFactory(_innerEncodingBindingElement.CreateMessageEncoderFactory());
        }

        public override MessageVersion MessageVersion
        {
            get { return _innerEncodingBindingElement.MessageVersion; }
            set { _innerEncodingBindingElement.MessageVersion = value; }
        }

        public override BindingElement Clone()
        {
            return new XeroMessageEncodingBindingElement(_innerEncodingBindingElement);
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }
    }
}
