using moneyverse.Models.Transferwise;
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
    public static class TransferwiseHelper
    {
        private static ObjectCache cache = MemoryCache.Default;
        public static Func<string> GetToken = () => (string) MyCache.cache["Transferwise.access_token"];

        public static string GetTokenFromTransferwise(string code)
        {
            return CurlHelper.GetAccessToken(code);
        }

        internal static TransferwiseProfile GetProfile()
        {
            var client = new RestClient("https://api.sandbox.transferwise.tech/v1/profiles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "f607428e-003d-4258-aedf-0ab809eef99b");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<TransferwiseProfile>>(response.Content).First();
        }

        internal static TransferwiseQuote CreateQuote(string profileId, double amount)
        {
            var client = new RestClient("https://api.sandbox.transferwise.tech/v1/quotes");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "269bc0ec-92e7-40b0-b2af-3d381c9b10d1");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            request.AddParameter("undefined", $"{{ \n          \"profile\": \"{profileId}\",\n          \"source\": \"EUR\",\n          \"target\": \"GBP\",\n          \"rateType\": \"FIXED\",\n          \"sourceAmount\": {amount},\n          \"type\": \"REGULAR\"\n        }}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<TransferwiseQuote>(response.Content);
        }

        internal static TransferwiseRecipient CreateRecipient(string profileId, string sortCode, string accountNumber)
        {
            var client = new RestClient("https://api.sandbox.transferwise.tech/v1/accounts");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "daa856df-b8b0-4da5-96f7-4db6957db242");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            request.AddParameter("undefined", $"{{ \n          \"currency\": \"GBP\", \n          \"type\": \"sort_code\", \n          \"profile\": \"{profileId}\", \n          \"accountHolderName\": \"John Doe\",\n           \"details\": {{ \n              \"legalType\": \"PRIVATE\",\n              \"sortCode\": \"40-30-20\", \n              \"accountNumber\": \"12345678\" \n           }} \n         }}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<TransferwiseRecipient>(response.Content);
        }

        internal static int CreateTransfer(string targetAccount, string quoteId)
        {
            var client = new RestClient("https://api.sandbox.transferwise.tech/v1/transfers");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "482bef9b-74f3-4792-9216-77c31dc67d2d");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            request.AddParameter("undefined", $"{{ \n          \"targetAccount\": \"{targetAccount}\",   \n          \"quote\": \"{quoteId}\",\n          \"customerTransactionId\": \"{Guid.NewGuid()}\",\n          \"details\" : {{\n              \"reference\" : \"moneyverse\",\n              \"transferPurpose\": \"verification.transfers.purpose.pay.bills\",\n              \"sourceOfFunds\": \"verification.source.of.funds.other\"\n            }} \n         }}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return ((dynamic)JsonConvert.DeserializeObject(response.Content)).id;
        }

        internal static void CompleteTransfer(int transferId)
        {
            var client = new RestClient($"https://api.sandbox.transferwise.tech/v1/simulation/transfers/{transferId}/processing");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "8e6c2be5-6733-4ad0-9801-60a85cfe9bb6");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            IRestResponse response = client.Execute(request);
            client = new RestClient($"https://api.sandbox.transferwise.tech/v1/simulation/transfers/{transferId}/funds_converted");
            request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "8e6c2be5-6733-4ad0-9801-60a85cfe9bb6");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            response = client.Execute(request);
            client = new RestClient($"https://api.sandbox.transferwise.tech/v1/simulation/transfers/{transferId}/outgoing_payment_sent");
            request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "8e6c2be5-6733-4ad0-9801-60a85cfe9bb6");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", $@"Bearer {GetToken()}");
            response = client.Execute(request);
        }
    }
}