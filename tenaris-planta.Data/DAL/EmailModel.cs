using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        /*
        public EmailModel()
        {
            // See https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest-getting-started.html
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;

            var uris = new[]
            {
                new Uri("https://54.162.155.36:9200/"),
            };
            // new Uri("https://54.162.155.36:9200/emails"),

            var connectionPool = new SniffingConnectionPool(uris);
            var connectionConfiguration = new ConnectionConfiguration()
                .DisableAutomaticProxyDetection()
                .EnableHttpCompression()
                .DisableDirectStreaming()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            var lowLevelClient = new ElasticLowLevelClient(connectionConfiguration);

            // var searchResponseLow = lowLevelClient.Search<JObject>("emails", null);

            var settings = new ConnectionSettings(connectionPool)
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                .DefaultIndex("emails")
                .BasicAuthentication("admin", "admin");
            
            var client = new ElasticClient(settings);

            var searchResponse = client.Search<object>(s => s
                                    .AllIndices()
                                    .Query(q => q));

            var emails = searchResponse.Documents;
        }
        */

        public EmailModel() { }
        public static object Get(string id)
        {
            string rtn = string.Empty;
            string url = @"https://54.162.155.36:9200/emails/_search";
            if (!string.IsNullOrEmpty(id))
            {
                url += "?q=_id: " + id;
            }

            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Basic YWRtaW46YWRtaW4=");

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
            string url = @"https://54.162.155.36:9200/emails/_update/" + id;

            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic YWRtaW46YWRtaW4=");

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
