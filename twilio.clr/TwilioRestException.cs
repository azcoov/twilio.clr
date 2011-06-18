

using System;

[Serializable()]
public class TwilioRestException : System.Exception
{

    public TwilioRestException()
        : base()
    {

    }

    public TwilioRestException(string message)
        : base(message)
    {

    }

    public TwilioRestException(string message,
                               System.Exception innerException)
        : base(message, innerException)
    {

    }

    protected TwilioRestException(System.Runtime.Serialization.SerializationInfo info,
                                  System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {

    }

}
