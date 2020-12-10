﻿// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-04-2020
// ***********************************************************************
// <copyright file="RouteConfig.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using System.Web.Routing;

namespace JackHenryTwitter
{
    /// <summary>
    /// Class RouteConfig.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}