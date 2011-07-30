using System;

namespace twilio.clr
{
    [Serializable]
    public class TwilioRestException : Exception
    {

        public TwilioRestException()
        {

        }

        public TwilioRestException(string message)
            : base(message)
        {

        }

        public TwilioRestException(string message,
                                   Exception innerException)
            : base(message, innerException)
        {

        }

        protected TwilioRestException(System.Runtime.Serialization.SerializationInfo info,
                                      System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

    }
}
