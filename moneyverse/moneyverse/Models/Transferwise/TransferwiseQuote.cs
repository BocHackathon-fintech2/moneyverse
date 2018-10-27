using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moneyverse.Models.Transferwise
{
    public class TransferwiseQuote
    {
        public string id { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public double sourceAmount { get; set; }
        public double targetAmount { get; set; }
        public string type { get; set; }
        public double rate { get; set; }
        public DateTime createdTime { get; set; }
        public string createdByUserId { get; set; }
        public string profile { get; set; }
        public string rateType { get; set; }
        public DateTime deliveryEstimate { get; set; }
        public double fee { get; set; }
        public List<string> allowedProfileTypes { get; set; }
        public bool guaranteedTargetAmount { get; set; }
        public bool ofSourceAmount { get; set; }
    }
}