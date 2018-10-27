using moneyverse.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;
using System.Runtime.Caching;
using moneyverse.Models.TrueLayer;
using moneyverse.Models.Transferwise;

namespace moneyverse.Controllers
{
    public class TransferwiseController : ApiController
    {
        [HttpGet]
        [Route("Transferwise/VerifyCode")]
        public IHttpActionResult Get(string code)
        {
            ObjectCache cache = MemoryCache.Default;
            MyCache.cache["Transferwise.access_token"] = TransferwiseHelper.GetTokenFromTransferwise(code);
            return Redirect("http://localhost:50033/tabclose.html");
        }

        [HttpGet]
        [Route("Transferwise/GetTokenActive")]
        public bool GetTokenActive()
        {
            var token = MyCache.cache["Transferwise.access_token"];
            return (token == null) ? false : true;
        }

        [HttpGet]
        [Route("Transferwise/Quote")]
        public TransferwiseQuote GetQuote(double amount)
        {
            var profile = TransferwiseHelper.GetProfile();
            TransferwiseQuote transferwiseQuote = TransferwiseHelper.CreateQuote(profile.id, amount);
            MyCache.cache["Transferwise.quote_id"] = transferwiseQuote.id;
            return transferwiseQuote;
        }

        [HttpGet]
        [Route("Transferwise/CompleteTransfer")]
        public void CompleteTransfer(string sortCode, string accountNumber)
        {
            var profile = TransferwiseHelper.GetProfile();
            var recipient = TransferwiseHelper.CreateRecipient(profile.id, sortCode, accountNumber);
            var id = TransferwiseHelper.CreateTransfer(recipient.id, (string)MyCache.cache["Transferwise.quote_id"]);
            TransferwiseHelper.CompleteTransfer(id);
        }
    }
}