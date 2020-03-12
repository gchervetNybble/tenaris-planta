using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenaris_planta.Core
{
    public class EmailFilterDTO
    {
        public Query query { get; set; }
        public EmailFilterDTO(string queryMatch, string operation)
        {
            this.query = new Query(queryMatch, operation);
        }
    }

    public class Query
    {
        public Match match { get; set; }
        public Query(string queryMatch, string operation)
        {
            this.match = new Match(queryMatch, operation);
        }
    }

    public class Match
    {
        public Tags tags { get; set; }

        public Match(string queryMatch, string operation)
        {
            this.tags = new Tags(queryMatch, operation);
        }
    }

    public class Tags
    {
        public string query { get; set; }
        public string _operator { get; set; }

        public Tags(string queryMatch, string operation)
        {
            this.query = queryMatch;
            this._operator = operation;
        }
    }

}
