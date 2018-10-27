using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moneyverse.Models.TrueLayer
{
    public class TrueLayerMeta
    {
        public string bank_transaction_id { get; set; }
        public string provider_transaction_category { get; set; }
    }

    public class TrueLayerTransaction
    {
        public string transaction_id { get; set; }
        public DateTime timestamp { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string transaction_type { get; set; }
        public string transaction_category { get; set; }
        public List<string> transaction_classification { get; set; }
        public string merchant_name { get; set; }
        public TrueLayerMeta meta { get; set; }
    }
}