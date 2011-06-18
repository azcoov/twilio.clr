using System;

public interface IAccount
{
    string request(string path, string method);
    string request(string path, string method, System.Collections.Hashtable vars);
}
