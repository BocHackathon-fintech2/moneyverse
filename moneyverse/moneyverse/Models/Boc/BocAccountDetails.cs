using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moneyverse.Models.Boc
{
    public class BocBalance
    {
        public int amount { get; set; }
        public string balanceType { get; set; }
    }

    public class BocAccountDetails
    {
        public string bankId { get; set; }
        public string accountId { get; set; }
        public string accountAlias { get; set; }
        public string accountType { get; set; }
        public string accountName { get; set; }
        public string IBAN { get; set; }
        public string currency { get; set; }
        public string infoTimeStamp { get; set; }
        public int interestRate { get; set; }
        public string maturityDate { get; set; }
        public string lastPaymentDate { get; set; }
        public string nextPaymentDate { get; set; }
        public int remainingInstallments { get; set; }
        public List<BocBalance> balances { get; set; }
    }
}