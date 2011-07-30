namespace twilio.clr
{
    public interface IAccount
    {
        string Request(string path, string method);
        string Request(string path, string method, System.Collections.Hashtable vars);
    }
}
