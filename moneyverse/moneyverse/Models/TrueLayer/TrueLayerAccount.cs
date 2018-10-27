using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moneyverse.Models.TrueLayer
{
    public class TrueLayerAccountNumber
    {
        public string iban { get; set; }
        public string number { get; set; }
        public string sort_code { get; set; }
        public string swift_bic { get; set; }
    }

    public class TrueLayerAccountProvider
    {
        public string display_name { get; set; }
        public string logo_uri { get; set; }
        public string provider_id { get; set; }
    }

    public class TrueLayerAccount
    {
        public DateTime update_timestamp { get; set; }
        public string account_id { get; set; }
        public string account_type { get; set; }
        public string display_name { get; set; }
        public string currency { get; set; }
        public TrueLayerAccountNumber account_number { get; set; }
        public TrueLayerAccountProvider provider { get; set; }
    }
}