using System;
using System.Collections;
using System.IO;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace twilio.clr
{
    public class StoredProcedures
    {
        const String AccountSid = "fake_sid";
        const String AuthToken = "fake_auth_token";

        private readonly IAccount _account;

        public StoredProcedures()
            : this(new Account(AccountSid, AuthToken))
        {
        }

        public StoredProcedures(IAccount account) {
            _account = account;
        }

        [SqlProcedure]
        //public static void SendSMSMessage(String apiVersion, String accountSid, String authToken, String from, String to, String body, out String sid)
        public void SendSMSMessage(String apiVersion, String accountSid, String authToken, String from, String to, String body, out String sid)
        {
            if (String.IsNullOrEmpty(apiVersion) || String.IsNullOrEmpty(authToken) || String.IsNullOrEmpty(from) || String.IsNullOrEmpty(to) || String.IsNullOrEmpty(body)) {
                throw new ArgumentNullException();
            }

            var parameters = new Hashtable {{"From", from}, {"To", to}, {"Body", body}};

            //Havent figured out how to use unit testing with static methods so for now, just toggle the comments for the method signature
            //and the account instantiation below

            //var account = new Account(accountSid, authToken);
            var twilioResponse = _account.Request(String.Format("/{0}/Accounts/{1}/SMS/Messages", apiVersion, accountSid), "POST", parameters);

            using (var stream = new StringReader(twilioResponse))
            {
                var doc = new XmlDocument();
                doc.Load(stream);
                var nodes = doc.GetElementsByTagName("Sid");
                sid = nodes[0].InnerText;
            }
        }
    }
}
