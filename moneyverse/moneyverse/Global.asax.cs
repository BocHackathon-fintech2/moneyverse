using moneyverse.Helpers;
using System.Runtime.Caching;
using System.Web.Http;
namespace moneyverse
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            });
            //MyCache.cache = new FileCache(new ObjectBinder());
            MyCache.cache = MemoryCache.Default;

            //MyCache.cache["Boc.access_token"] = BocHelper.GetTokenFromBoc();
            //MyCache.cache["Boc.subscriptionId"] = BocHelper.CreateSubscription((string)MyCache.cache["Boc.access_token"]);

            MyCache.cache["Boc.access_token"] = BocHelper.GetTokenFromBoc();
            MyCache.cache["Boc.subscriptionId"] = BocHelper.CreateSubscription((string)MyCache.cache["Boc.access_token"]);
        }
    }
}