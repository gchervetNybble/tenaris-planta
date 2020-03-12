using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenaris_planta.Core.DTO
{
    public class EmailDTO
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public float _score { get; set; }
        public _Source _source { get; set; }
    }

    public class _Source
    {
        public string deliveredto { get; set; }
        public string messageid { get; set; }
        public string xgooglesmtpsource { get; set; }
        public string receivedspf { get; set; }
        public string arcmessagesignature { get; set; }
        public string from { get; set; }
        public string contenttype { get; set; }
        public string xmsexchangecalendarseriesinstanceid { get; set; }
        public string mimeversion { get; set; }
        public string xproofpointvirusversion { get; set; }
        public string threadindex { get; set; }
        public string contentlanguage { get; set; }
        public string xoriginatingip { get; set; }
        public string subject { get; set; }
        public string arcauthenticationresults { get; set; }
        public string xmstnefcorrelator { get; set; }
        public string returnpath { get; set; }
        public string[] received { get; set; }
        public string xproofpointspamdetails { get; set; }
        public string cc { get; set; }
        public string arcseal { get; set; }
        public string message { get; set; }
        public string version { get; set; }
        public string dkimsignature { get; set; }
        public string threadtopic { get; set; }
        public string to { get; set; }
        public string xmshasattach { get; set; }
        public string authenticationresults { get; set; }
        public string xreceived { get; set; }
        public DateTime timestamp { get; set; }
        public string date { get; set; }
        public string[] tags { get; set; }
        public string acceptlanguage { get; set; }
    }

}
