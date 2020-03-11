using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenaris_planta.Core.DTO
{
    public class EmailDTO
    {
        public string message { get; set; }
        public string contenttype { get; set; }
        public string version { get; set; }
        public string to { get; set; }
        public string mimeversion { get; set; }
        public string parser { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string messageid { get; set; }
        public DateTime timestamp { get; set; }
        public string date { get; set; }
    }
}
