// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="HomeController.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using JackHenryTwitter.Models;
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
        /// Returns the About view.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// returns the Contact view.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Gets the tweet statistics.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult GetTweetStatistics()
        {
            var model = new GetTwitterDataFromJsonFile().GetTwitterStatisitcsData();
            return PartialView(model);
        }

        /// <summary>
        /// Gets the tweet stream from twitter.
        /// </summary>
        /// <param name="secondsToRun">The seconds to run.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public async Task<bool> GetTweetStreamFromTwitter(int secondsToRun)
        {
            await Utilities.GetTwitterDetails.GetTweetStreamFromTwitter(secondsToRun);
            return true;
        }

        /// <summary>
        /// Returns the Index view.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Used for development reasons only.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult sample()
        {
            return null;
        }

        /// <summary>
        /// Returns the TestTweet view.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult TweetTest()
        {
            var model = new GetTwitterDataFromJsonFile().GetTwitterStatisitcsData();
            return View(model);
        }
    }
}