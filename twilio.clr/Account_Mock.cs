using System;

namespace twilio.clr
{
    public class AccountMock : IAccount
    {
        public string Request(string path, string method)
        {
            throw new NotImplementedException();
        }

        public string Request(string path, string method, System.Collections.Hashtable vars)
        {
            if (path.Contains("/SMS/Messages") && method == "POST")
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

            if (path.Contains("/Calls") && method == "POST")
            {
                return @"<TwilioResponse>
                            <Call>
                                <Sid>CAa346467ca321c71dbd5e12f627deb854</Sid>
                                <DateCreated>Thu, 19 Aug 2010 00:25:48 +0000</DateCreated>
                                <DateUpdated>Thu, 19 Aug 2010 00:25:48 +0000</DateUpdated>
                                <ParentCallSid/>
                                <AccountSid>AC24748d2c33b78629cabdca5a2b47a162</AccountSid>
                                <To>+14155551212</To>
                                <FormattedTo>(415) 555-1212</FormattedTo>
                                <From>+14158675309</From>
                                <FormattedFrom>(415) 867-5309</FormattedFrom>
                                <PhoneNumberSid></PhoneNumberSid>
                                <Status>queued</Status>
                                <StartTime/>
                                <EndTime/>
                                <Duration/>
                                <Price/>
                                <Direction>outbound-api</Direction>
                                <AnsweredBy/>
                                <ApiVersion>2010-04-01</ApiVersion>
                                <ForwardedFrom/>
                                <CallerName/>
                                <Uri>/2010-04-01/Accounts/AC24748d2c33b78629cabdca5a2b47a162/Calls/CAa346467ca321c71dbd5e12f627deb854</Uri>
                                <SubresourceUris>
                                    <Notifications>/2010-04-01/Accounts/AC24748d2c33b78629cabdca5a2b47a162/Calls/CAa346467ca321c71dbd5e12f627deb854/Notifications</Notifications>
                                    <Recordings>/2010-04-01/Accounts/AC24748d2c33b78629cabdca5a2b47a162/Calls/CAa346467ca321c71dbd5e12f627deb854/Recordings</Recordings>
                                </SubresourceUris>
                            </Call>
                        </TwilioResponse>";
            }
            throw new NotImplementedException();
        }
    }
}
