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
    public class TrueLayerController : ApiController
    {
        [HttpGet]
        [Route("TrueLayer/VerifyCode")]
        public IHttpActionResult Get(string code, string state, string scope)
        {
            ObjectCache cache = MemoryCache.Default;
            MyCache.cache["TrueLayer.access_token"] = TrueLayerHelper.GetTokenFromTrueLayer(code);
            return Redirect("http://localhost:50033/tabclose.html");
        }

        [HttpGet]
        [Route("TrueLayer/GetAccounts")]
        public List<TrueLayerAccount> GetAccounts()
        {
            var token = MyCache.cache["TrueLayer.access_token"];
            if(token != null)
            {
                var a = TrueLayerHelper.GetAccounts();
                return a;
            }
            return null;    
        }
    }
}