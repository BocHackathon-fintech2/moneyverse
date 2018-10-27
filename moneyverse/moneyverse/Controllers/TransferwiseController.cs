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
    }
}