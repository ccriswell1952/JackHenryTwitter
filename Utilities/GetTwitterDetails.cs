// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-05-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="Utilities.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using JackHenryTwitter.Models;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace JackHenryTwitter.Utilities
{
    /// <summary>
    /// Class Utilities.
    /// </summary>
    public static partial class GetTwitterDetails
    {

        #region Public Methods

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>System.String.</returns>
        public static string GetFilePath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        /// <summary>
        /// Gets the tweeter json file path.
        /// </summary>
        /// <param name="isStatsFile">true if writing to the Tweet Statistics file else false if writing to the data file</param>
        /// <returns>System.String.</returns>
        public static string GetTweetJsonFilePath(bool isStatsFile)
        {
            string relativePath = isStatsFile ? ConfigurationManager.AppSettings["TweetStatsJsonFilePath"] : ConfigurationManager.AppSettings["TweetJsonFilePath"];
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        /// <summary>
        /// Gets the tweet stream from twitter.
        /// </summary>
        /// <param name="secondsToRun">The seconds to run.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static async Task<bool> GetTweetStreamFromTwitter(int secondsToRun)
        {
            var dataSource = new SetTwitterDataToJsonFile();
            ReadOnlyTwitterCredentials twitterCredentials = GetTwitterCredentials();
            TwitterClient twitterClient = new TwitterClient(twitterCredentials);
            GetTweetsFromTwitter getDataFromTwitter = new GetTweetsFromTwitter(secondsToRun, dataSource, twitterClient);
            return await getDataFromTwitter.GetSampleTweetsFromTwitter();
        }

        /// <summary>
        /// Gets the twitter credentials.
        /// </summary>
        /// <returns>ReadOnlyTwitterCredentials.</returns>
        public static ReadOnlyTwitterCredentials GetTwitterCredentials()
        {
            ReadOnlyConsumerCredentials basicCredentials =
                new ReadOnlyConsumerCredentials(ConfigurationManager.AppSettings["CONSUMER_KEY"],
                ConfigurationManager.AppSettings["CONSUMER_SECRET"],
                ConfigurationManager.AppSettings["BEARER_TOKEN"]);
            return new ReadOnlyTwitterCredentials(basicCredentials);
        }

        #endregion Public Methods

    }
}