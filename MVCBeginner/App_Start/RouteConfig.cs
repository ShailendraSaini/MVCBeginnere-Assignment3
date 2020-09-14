namespace MVCBeginner
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    ///     RouteConfig Class
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        ///     RegisterRoutes method
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(); //Enables Attribute Routing

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
