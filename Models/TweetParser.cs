// *********************************************************************** Assembly :
// JackHenryTwitter Author : charl Created : 12-19-2020
//
// Last Modified By : charl Last Modified On : 12-19-2020 ***********************************************************************
// <copyright file="TweetParser.cs" company="HP Inc.">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using JackHenryTwitter.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class TweetParser.
    /// </summary>
    public static partial class TweetParser
    {
        #region Public Methods

        /// <summary>
        /// Formats the tweet Json data for data insert.
        /// </summary>
        /// <param name="deserializedTweets">The list of tweets.</param>
        /// <param name="streamingDownloadProperties">The tweet download properties.</param>
        /// <param name="emojiBaseList">The emoji base list.</param>
        /// <returns>System.String.</returns>
        public static RunningTotals ParseTweetDataForDataStatistics(TweetData tweetData, double theseTweetsTimeSpan, out List<Emoji> emojiBaseList)
        {
            List<Tweet> deserializedTweets = tweetData.Tweets;
            emojiBaseList = new List<Emoji>();
            RunningTotals runningTotals = InstanciateRunningTotals();
            foreach (Tweet deserializedTweet in deserializedTweets)
            {
                List<string> linesToCheckForStats = new List<string>();
                string lineToAdd = deserializedTweet.data.text;
                if (!string.IsNullOrEmpty(lineToAdd))
                {
                    linesToCheckForStats.Add(lineToAdd);
                }
                if (GetTwitterDetails.SearchTextFieldOnlyForStatTotals() == false)
                {
                    foreach (var user in deserializedTweet.includes.users)
                    {
                        lineToAdd = null;
                        lineToAdd = user.name;
                        if (!string.IsNullOrEmpty(lineToAdd))
                        {
                            linesToCheckForStats.Add(lineToAdd);
                        }
                        lineToAdd = null;
                        lineToAdd = user.description;
                        if (!string.IsNullOrEmpty(lineToAdd))
                        {
                            linesToCheckForStats.Add(lineToAdd);
                        }
                    }
                    Entities ent = new Entities();
                    if (deserializedTweet.data.entities != null)
                    {
                        ent = deserializedTweet.data.entities;
                        List<Url> us = new List<Url>();
                        if (ent.urls != null)
                        {
                            us = ent.urls;
                            foreach (var e in us)
                            {
                                if (!string.IsNullOrEmpty(e.expanded_url))
                                {
                                    linesToCheckForStats.Add(e.expanded_url);
                                }
                            }
                        }
                    }
                }
                if (linesToCheckForStats.Count > 0)
                {
                    List<Emoji> thisEmojiList = LineStatParsers.GetEmojiList(linesToCheckForStats);

                    if (thisEmojiList.Count > 0)
                    {
                        emojiBaseList.AddRange(thisEmojiList);
                        List<string> emojiNames = new List<string>();
                        emojiNames = thisEmojiList.Select(s => s.EmojiHtmlEncode).Distinct().ToList();
                        runningTotals.RunningTotalEmoji += thisEmojiList.Count;
                        runningTotals.TweetsWithEmojiRunningTotal++;
                    }
                    var thisHashTagList = LineStatParsers.GetHashTagList(linesToCheckForStats);
                    var thisHastTagCount = thisHashTagList.Count;
                    if (thisHastTagCount > 0)
                    {
                        runningTotals.RunningTotalHashtag += thisHastTagCount;
                        runningTotals.TweetsWithHashTagRunningTotal++;
                        runningTotals.FullListOfHashtags.AddRange(thisHashTagList);
                    }
                    var thisUrlList = LineStatParsers.GetUrlList(linesToCheckForStats);
                    var thisUrlCount = thisUrlList.Count;
                    if (thisUrlCount > 0)
                    {
                        runningTotals.RunningTotalUrl += thisUrlCount;
                        runningTotals.TweetsWithUrlRunningTotal++;
                        runningTotals.FullListOfUrls.AddRange(thisUrlList);
                    }
                }
            }
            var runningTotalTime = tweetData.TotalRunningTime + theseTweetsTimeSpan;
            runningTotals.TimeToDownloadInMiliSeconds = runningTotalTime;

            return runningTotals;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Instanciates the running totals.
        /// </summary>
        /// <returns>RunningTotals.</returns>
        private static RunningTotals InstanciateRunningTotals()
        {
            RunningTotals runningTotals = new RunningTotals();
            runningTotals.TweetsWithEmojiRunningTotal = 0;
            runningTotals.TweetsWithHashTagRunningTotal = 0;
            runningTotals.TweetsWithUrlRunningTotal = 0;
            runningTotals.RunningTotalEmoji = 0;
            runningTotals.RunningTotalHashtag = 0;
            runningTotals.RunningTotalUrl = 0;
            runningTotals.FullListOfUrls = new List<string>();
            runningTotals.FullListOfHashtags = new List<string>();
            return runningTotals;
        }

        #endregion Private Methods
    }
}