using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace twilio.clr
{
    public class Account_Mock : IAccount
    {
        public string request(string path, string method)
        {
            throw new NotImplementedException();
        }

        public string request(string path, string method, System.Collections.Hashtable vars)
        {
            if (path.Contains("") && method == "POST")
            {
                return @"<TwilioResponse>
                        <SMSMessage>
                            <Sid>SM90c6fc909d8504d45ecdb3a3d5b3556e</Sid>
                            <DateCreated>Wed, 18 Aug 2010 20:01:40 +0000</DateCreated>
                            <DateUpdated>Wed, 18 Aug 2010 20:01:40 +0000</DateUpdated>
                            <DateSent/>
                            <AccountSid>AC5ef872f6da5a21de157d80997a64bd33</AccountSid>
                            <To>+14159352345</To>
                            <From>+14158141829</From>
                            <Body>Jenny please?! I love you &lt;3</Body>
                            <Status>queued</Status>
                            <Direction>outbound-api</Direction>
                            <ApiVersion>2010-04-01</ApiVersion>
                            <Price/>
                            <Uri>/2010-04-01/Accounts/AC5ef872f6da5a21de157d80997a64bd33/SMS/Messages/SM90c6fc909d8504d45ecdb3a3d5b3556e</Uri>
                        </SMSMessage>
                    </TwilioResponse>";
            }
            throw new NotImplementedException();
        }
    }
}
