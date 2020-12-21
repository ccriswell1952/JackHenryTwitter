// *********************************************************************** Assembly :
// JackHenryTwitter Author : charl Created : 12-19-2020
//
// Last Modified By : charl
// Last Modified On : 12-19-2020
//***********************************************************************
// <copyright file="GetStreamingTweetStats.cs" company="HP Inc.">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System.Collections.Generic;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class GetStreamingTweetStats.
    /// </summary>
    public static class GetStreamingTweetStats
    {
        #region Public Methods

        /// <summary>
        /// Gets the streaming stats.
        /// </summary>
        /// <param name="existingRootData">The existing root data.</param>
        /// <param name="streamingTweetData">The streaming tweet data.</param>
        /// <param name="streamingTweetDownloadProperties">The streaming tweet download properties.</param>
        /// <returns>TweetStats.</returns>
        public static TweetStats GetStreamingStats(Root existingRootData, TweetData streamingTweetData, double theseTweetsTimeSpan, TweetStats existingTweetStats)
        {
            try
            {
                existingRootData.TweetData.Tweets.AddRange(streamingTweetData.Tweets);
                List<Emoji> emojiBaseList = new List<Emoji>();
                RunningTotals runningTotals = TweetParser.ParseTweetDataForDataStatistics(existingRootData.TweetData, theseTweetsTimeSpan, out emojiBaseList);

                // set the stats for the newly downloaded stream of tweets
                TweetStats tweetStats = new TweetStats(existingRootData, emojiBaseList, runningTotals);
                tweetStats.SetAllTweetStatsProperties();
                return tweetStats;
            }
            catch
            {
                return existingTweetStats;
            }
        }

        #endregion Public Methods
    }
}