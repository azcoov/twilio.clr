using System;
using System.Collections;
using System.IO;
using System.Xml;
using Microsoft.SqlServer.Server;
using System.Web;

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

            // Replace plus values in to and from numbers
            from = from.Replace("+", "%2B");
            to = to.Replace("+", "%2B");

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

        [SqlProcedure]
        //public static void makeOutgoingCall(String apiVersion, String accountSid, String authToken, String from, String to, String url, out String sid)
        public void makeOutgoingCall(String apiVersion, String accountSid, String authToken, String from, String to, String url, out String sid)
        {
            if (String.IsNullOrEmpty(apiVersion) || String.IsNullOrEmpty(authToken) || String.IsNullOrEmpty(from) || String.IsNullOrEmpty(to) || String.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException();
            }

            // Replace plus values in to and from numbers and encode the url so you can pass the url
            from = from.Replace("+", "%2B");
            to = to.Replace("+", "%2B");
            url = HttpUtility.UrlEncode(url);

            var parameters = new Hashtable { { "From", from }, { "To", to }, { "Url", url } };

            //Havent figured out how to use unit testing with static methods so for now, just toggle the comments for the method signature
            //and the account instantiation below

            //var account = new Account(accountSid, authToken);
            var twilioResponse = _account.Request(String.Format("/{0}/Accounts/{1}/Calls", apiVersion, accountSid), "POST", parameters);

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
