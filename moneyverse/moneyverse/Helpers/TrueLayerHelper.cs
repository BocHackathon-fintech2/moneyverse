using moneyverse.Models.TrueLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace moneyverse.Helpers
{
    public static class TrueLayerHelper
    {
        private static ObjectCache cache = MemoryCache.Default;
        public static Func<string> GetToken = () => (string) MyCache.cache["TrueLayer.access_token"];

        public static string GetTokenFromTrueLayer(string code)
        {
            var client = new RestClient("https://auth.truelayer.com/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", $@"grant_type=authorization_code&client_id={Constants.Constants.TrueLayer_Client_Id}&client_secret={Constants.Constants.TrueLayer_Client_Secret}&redirect_uri={Constants.Constants.TrueLayer_RedirectUri}&code={code}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (string)((dynamic)JsonConvert.DeserializeObject(response.Content)).access_token;
        }

        public static List<TrueLayerAccount> GetAccounts()
        {
            var client = new RestClient("https://api.truelayer.com/data/v1/accounts");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<TrueLayerAccount>>((((dynamic)JsonConvert.DeserializeObject(response.Content)).results.ToString()));
        }
    }
}