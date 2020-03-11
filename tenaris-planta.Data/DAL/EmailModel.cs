using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenaris_planta.Core.DTO;

namespace tenaris_planta.Data.DAL
{
    public class EmailModel
    {
        public EmailModel()
        {
            // See https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest-getting-started.html

            var uris = new[]
            {
                new Uri("https://54.162.155.36:9200/"),
            };

            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool)
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                .DefaultIndex("emails")
                .BasicAuthentication("admin", "admin");

            var client = new ElasticClient(settings);

            var searchResponse = client.Search<JObject>(s => s
                                    .Index("emails")
                                    .MatchAll());

            var emails = searchResponse.Documents;
        }
        public List<object> Get(Nullable<int> id = null)
        {
            return new List<object>();
        }
    }
}
