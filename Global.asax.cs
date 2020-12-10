// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-06-2020
// ***********************************************************************
// <copyright file="Global.asax.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JackHenryTwitter
{
    /// <summary>
    /// Class MvcApplication.
    /// Implements the <see cref="System.Web.HttpApplication" />
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}