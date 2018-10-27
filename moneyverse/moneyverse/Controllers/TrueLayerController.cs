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
using System.IO;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("TrueLayer/GetTransactions")]
        public object GetTransactions()
        {
            return JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\micha\source\repos\BocHackathon-fintech2\moneyverse\moneyverse\moneyverse\App_Data\data.json"));

        }
    }
}