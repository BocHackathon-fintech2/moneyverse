using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace moneyverse.Helpers
{
    public static class CurlHelper
    {
        public static string GetAccessToken(string code)
        {
            var psi = new ProcessStartInfo(@"C:\Users\micha\Downloads\curl-7.61.1-win64-mingw\bin\curl.exe")
            {
                Arguments = $@"curl https://api.sandbox.transferwise.tech/oauth/token -u {Constants.Constants.Transferwise_Client_Id}:{Constants.Constants.Transferwise_Client_Secret} -d ""grant_type=authorization_code"" -d ""client_id={Constants.Constants.Transferwise_Client_Id}"" -d ""code={code}"" -d ""redirect_uri=http://localhost:50033/Transferwise/VerifyCode""",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            var ps = Process.Start(psi);
            var result = ps.StandardOutput.ReadToEnd();
            return ((dynamic)JsonConvert.DeserializeObject(result)).access_token;
        }
    }
}