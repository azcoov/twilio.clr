using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace twilio.clr
{
    public class Account : IAccount
    {
        const string TwilioApiUrl = "https://api.twilio.com";

        private readonly string _id;
        private readonly string _token;

        public Account(string id, string token)
        {
            _id = id;
            _token = token;
        }

        private string Download(string uri, Hashtable vars)
        {
            // 1. format query string
            if (vars != null)
            {
                var query = vars.Cast<DictionaryEntry>().Aggregate("", (current, d) => current + ("&" + d.Key.ToString() + "=" + d.Value.ToString()));
                if (query.Length > 0)
                    uri = uri + "?" + query.Substring(1);
            }

            // 2. setup basic authenication
            var authstring = Convert.ToBase64String(
                Encoding.ASCII.GetBytes(String.Format("{0}:{1}",
                                                      _id, _token)));

            // 3. perform GET using WebClient
            var client = new WebClient();
            client.Headers.Add("Authorization",
                               String.Format("Basic {0}", authstring));
            var resp = client.DownloadData(uri);

            return Encoding.ASCII.GetString(resp);
        }

        private string Upload(string uri, string method, Hashtable vars)
        {
            // 1. format body data
            var data = "";
            if (vars != null)
            {
                data = vars.Cast<DictionaryEntry>().Aggregate(data, (current, d) => current + (d.Key.ToString() + "=" + d.Value.ToString() + "&"));
            }

            // 2. setup basic authenication
            var authstring = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", _id, _token)));

            // 3. perform POST/PUT/DELETE using WebClient
            ServicePointManager.Expect100Continue = false;
            var postbytes = Encoding.ASCII.GetBytes(data);
            var client = new WebClient();

            client.Headers.Add("Authorization", String.Format("Basic {0}", authstring));
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var resp = client.UploadData(uri, method, postbytes);

            return Encoding.ASCII.GetString(resp);
        }

        private string _build_uri(string path)
        {
            if (path[0] == '/')
                return TwilioApiUrl + path;
            return TwilioApiUrl + "/" + path;
        }

        public string Request(string path, string method, Hashtable vars)
        {
            string response;

            if (path == null || path.Length <= 0)
                throw (new ArgumentException("Invalid path parameter"));

            method = method.ToUpper();
            if (method == null || (method != "GET" && method != "POST" && method != "PUT" && method != "DELETE"))
            {
                throw (new ArgumentException("Invalid method parameter"));
            }

            if (method != "GET" && vars.Count <= 0)
            {
                throw (new ArgumentException("No vars parameters"));
            }

            var url = _build_uri(path);
            try
            {
                response = method == "GET" ? Download(url, vars) : Upload(url, method, vars);
            }
            catch (WebException e)
            {
                var message = e.Message;

                switch (e.Status)
                {
                    case WebExceptionStatus.TrustFailure:
                        message = "You do not trust the people who issued the certificate being used by twiliorest.dll.";
                        break;
                }

                var varList = vars.Cast<DictionaryEntry>().Aggregate("", (current, d) => current + ("&" + d.Key.ToString() + "=" + d.Value.ToString()));


                var responseStr = "";
                Stream responseStream = null;
                if (e.Response != null)
                    responseStream = e.Response.GetResponseStream();
                if (responseStream != null)
                    using (var sr = new StreamReader(responseStream))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            responseStr += line;
                        }
                    }


                message = String.Format("TwilioRestException occurred in the request you sent: \n{0}\n\tURLL: {1}\n\tMETHOD:{2}\n\tVARS:{3}\n\tRESPONSE:{4}",
                                        message,
                                        url,
                                        method,
                                        varList,
                                        responseStr);

                throw new TwilioRestException(message, e);
            }

            return response;
        }

        public string Request(string path, string method)
        {
            return Request(path, method, null);
        }
    }
}
