// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-04-2020
// ***********************************************************************
// <copyright file="HomeController.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using JackHenryTwitter.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JackHenryTwitter.Controllers
{
    /// <summary>
    /// Class HomeController.
    /// Implements the <see cref="System.Web.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Contacts this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Tweets the test.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult TweetTest()
        {
            var model = Utilities.Utilities.GetDeserializedFileJsonStatisitcsData();
            return View(model);
        }

        public ActionResult GetTweetStatistics()
        {
            var model = Utilities.Utilities.GetDeserializedFileJsonStatisitcsData();
            return PartialView(model);
        }

        public async Task<bool> GetTweetStreamFromTwitter(int secondsToRun)
        {
            await Utilities.Utilities.GetTweetStreamFromTwitter(secondsToRun);
            return true;
        }

        public ActionResult sample()
        {
            return null;
        }

    }
}