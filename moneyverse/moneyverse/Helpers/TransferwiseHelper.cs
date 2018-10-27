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
    }
}