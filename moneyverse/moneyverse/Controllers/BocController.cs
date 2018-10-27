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
using moneyverse.Models.Boc;

namespace moneyverse.Controllers
{
    public class BocController : ApiController
    {
        [HttpGet]
        [Route("Boc/VerifyCode")]
        public IHttpActionResult VerifyCode(string code)
        {
            var token = BocHelper.GetTokenFromBoc2(code);
            BocHelper.UpdateSubscription(token);
            MyCache.cache["Boc.access_token"] = BocHelper.GetTokenFromBoc();
            return Redirect("http://localhost:50033/tabclose.html");
        }

        [HttpGet]
        [Route("Boc/AuthUrl")]
        public UrlContainer AuthUrl()
        {
            return new UrlContainer { url = $@"https://sandbox-apis.bankofcyprus.com/df-boc-org-sb/sb/psd2/oauth2/authorize?response_type=code&redirect_uri=http://localhost:50033/Boc/VerifyCode&scope=UserOAuth2Security&client_id={Constants.Constants.Boc_Client_Id}&subscriptionid={MyCache.cache["Boc.subscriptionId"]}" };
        }

        [HttpGet]
        [Route("Boc/GetAccounts")]
        public List<BocAccount> GetAccounts()
        {
            var token = MyCache.cache["Boc.access_token"];
            if(token != null)
            {
                var a = BocHelper.GetAccounts();
                return a;
            }
            return null;    
        }
    }

    public class UrlContainer
    {
        public string url { get; set; }
    }
}