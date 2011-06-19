using System;
using System.Collections;
using System.IO;
using System.Xml;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    const String IsoCountryCode = "US";
    const String AccountSid = "fake_sid";
    const String AuthToken = "fake_auth_token";

    private IAccount account;

    public StoredProcedures()
        : this(new Account(AccountSid, AuthToken))
    {
    }

    public StoredProcedures(IAccount account) {
        this.account = account;
    }

    [SqlProcedure]
    //public static void SendSMSMessage(String apiVersion, String accountSid, String authToken, String from, String to, String body, out String sid)
    public void SendSMSMessage(String apiVersion, String accountSid, String authToken, String from, String to, String body, out String sid)
    {
        if (String.IsNullOrEmpty(apiVersion) || String.IsNullOrEmpty(authToken)) {
            throw new ArgumentNullException("api version and auth token required");
        }
        if (String.IsNullOrEmpty(from) || String.IsNullOrEmpty(to) || String.IsNullOrEmpty(body)) {
            throw new ArgumentNullException("missing from, to, or body");
        }

        Hashtable parameters = new Hashtable();
        parameters.Add("From", from);
        parameters.Add("To", to);
        parameters.Add("Body", body);

        //Havent figured out how to use unit testing with static methods so for now, just toggle the comments for the method signature
        //and the account instantiation below

        //var account = new Account(accountSid, authToken);
        var twilioResponse = account.request(String.Format("/{0}/Accounts/{1}/SMS/Messages", apiVersion, accountSid), "POST", parameters);

        using (StringReader stream = new StringReader(twilioResponse))
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            XmlNodeList nodes = doc.GetElementsByTagName("Sid");
            sid = nodes[0].InnerText;
        }
    }
}
