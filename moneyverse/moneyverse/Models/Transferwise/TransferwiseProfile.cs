using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moneyverse.Models.Transferwise
{
    public class TransferwiseDetails
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string phoneNumber { get; set; }
        public string avatar { get; set; }
        public string occupation { get; set; }
        public object primaryAddress { get; set; }
    }

    public class TransferwiseProfile
    {
        public string id { get; set; }
        public string type { get; set; }
        public TransferwiseDetails details { get; set; }
    }
}