using moneyverse.Models.Boc;
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
    public static class BocHelper
    {
        public static Func<string> GetToken = () => (string) MyCache.cache["TrueLayer.access_token"];

        public static string GetTokenFromBoc()
        {
            var client = new RestClient("https://sandbox-apis.bankofcyprus.com/df-boc-org-sb/sb/psd2/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "client_id=f4e3ccf4-53e8-420e-90e2-fd30b39b3813&client_secret=wE1tT2oQ1mL1jP8eS5fI6jX2xJ8xN8wH3bS1yU5bD5oV8oH8vP&grant_type=client_credentials&scope=TPPOAuth2Security", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (string)((dynamic)JsonConvert.DeserializeObject(response.Content)).access_token;
        }

        internal static void UpdateSubscription(object token)
        {
            var client = new RestClient($@"https://sandbox-apis.bankofcyprus.com/df-boc-org-sb/sb/psd2/v1/subscriptions/{MyCache.cache["Boc.subscriptionId"]}?client_id=f4e3ccf4-53e8-420e-90e2-fd30b39b3813&client_secret=wE1tT2oQ1mL1jP8eS5fI6jX2xJ8xN8wH3bS1yU5bD5oV8oH8vP");
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("Postman-Token", "622fa2bb-e5a9-486b-9d06-1465f49a7cc1");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("journeyId", "abc");
            request.AddHeader("timeStamp", DateTime.Now.Ticks.ToString());
            request.AddHeader("originUserId", "abc");
            request.AddHeader("tppid", "abc");
            request.AddHeader("app_name", "SomeApp");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $@"Bearer {token}");
            request.AddParameter("undefined", "{\"selectedAccounts\": [ {\"accountId\": \"351012345671\"},  {\"accountId\": \"351092345672\"},  {\"accountId\": \"351012345673\"},  {\"accountId\": \"351012345674\"}],\"accounts\":{\"transactionHistory\":false,\"balance\":false,\"details\":true,\"checkFundsAvailability\":true},\"payments\":{ \"limit\":89.95580255,\"currency\":\"EUR\",\"amount\":65.90993593}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var a = (dynamic)JsonConvert.DeserializeObject(response.Content);
        }

        internal static object GetTokenFromBoc2(string code)
        {
            var client = new RestClient("https://sandbox-apis.bankofcyprus.com/df-boc-org-sb/sb/psd2/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", $@"client_id=f4e3ccf4-53e8-420e-90e2-fd30b39b3813&client_secret=wE1tT2oQ1mL1jP8eS5fI6jX2xJ8xN8wH3bS1yU5bD5oV8oH8vP&grant_type=authorization_code&scope=UserOAuth2Security&code={code}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (string)((dynamic)JsonConvert.DeserializeObject(response.Content)).access_token;
        }

        public static string CreateSubscription(string token)
        {
            var client = new RestClient("https://sandbox-apis.bankofcyprus.com/df-boc-org-sb/sb/psd2/v1/subscriptions?client_id=f4e3ccf4-53e8-420e-90e2-fd30b39b3813&client_secret=wE1tT2oQ1mL1jP8eS5fI6jX2xJ8xN8wH3bS1yU5bD5oV8oH8vP");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "649246f1-aa9e-4421-bc74-107694d702b0");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("journeyId", "abc");
            request.AddHeader("timeStamp", DateTime.Now.Ticks.ToString());
            request.AddHeader("originUserId", "abc");
            request.AddHeader("tppid", "abc");
            request.AddHeader("app_name", "SomeApp");
            request.AddHeader("APIm-Debug-Trans-Id", "true");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $@"Bearer {token}");
            request.AddParameter("undefined", "{\r\n \"accounts\": {\r\n    \"transactionHistory\": true,\r\n    \"balance\": true,\r\n    \"details\": true,\r\n    \"checkFundsAvailability\": true\r\n  },\r\n  \"payments\": {\r\n    \"limit\": 99999999,\r\n    \"currency\": \"EUR\",\r\n    \"amount\": 999999999\r\n  }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (string)((dynamic)JsonConvert.DeserializeObject(response.Content)).subscriptionId;
        }

        public static List<BocAccount> GetAccounts()
        {
            var client = new RestClient("https://sandbox-apis.bankofcyprus.com/df-boc-org-sb/sb/psd2/v1/accounts?client_id=f4e3ccf4-53e8-420e-90e2-fd30b39b3813&client_secret=wE1tT2oQ1mL1jP8eS5fI6jX2xJ8xN8wH3bS1yU5bD5oV8oH8vP");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "105680e3-7fdb-43eb-b2d5-b442202a0376");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("timeStamp", DateTime.Now.Ticks.ToString());
            request.AddHeader("journeyId", "abc");
            request.AddHeader("tppId", "abc");
            request.AddHeader("originUserId", "abc");
            request.AddHeader("subscriptionId", (string)MyCache.cache["Boc.subscriptionId"]);
            request.AddHeader("Authorization", $@"Bearer {MyCache.cache["Boc.access_token"]}");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<BocAccount>>(response.Content);
        }
    }
}