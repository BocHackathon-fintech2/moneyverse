using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moneyverse.Models.Transferwise
{
    public class TransferwiseRecipientDetails
    {
        public string legalType { get; set; }
        public string accountNumber { get; set; }
        public string sortCode { get; set; }
    }

    public class TransferwiseRecipient
    {
        public string id { get; set; }
        public string profile { get; set; }
        public string accountHolderName { get; set; }
        public string type { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public TransferwiseRecipientDetails details { get; set; }
    }
}