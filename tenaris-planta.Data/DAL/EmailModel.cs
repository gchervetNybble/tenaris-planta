using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using tenaris_planta.Core.DTO;

namespace tenaris_planta.Data.DAL
{
    public class EmailModel
    {
        public EmailModel() { }
        public static object Get(string id)
        {
            string rtn = string.Empty;
            string url = ConfigurationManager.AppSettings["ElasticsearchServer"] + @"/emails/_search";
            if (!string.IsNullOrEmpty(id))
            {
                url += "?q=_id: " + id;
            }

            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );

            // Request data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", ConfigurationManager.AppSettings["ElasticsearchBasicAuthBase64"]);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                rtn = reader.ReadToEnd();
            }

            return JObject.Parse(rtn);
        }

        public static object Update(string id, JObject updateObject)
        {
            string html = string.Empty;
            string url = ConfigurationManager.AppSettings["ElasticsearchServer"] + @"/emails/_update/" + id;

            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );

            // Request data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", ConfigurationManager.AppSettings["ElasticsearchBasicAuthBase64"]);

            // POST data
            byte[] data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(updateObject));
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.Default);

            string rtn = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();

            response.Close();

            return JObject.Parse(rtn);
        }
    }
}
