// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="AppVariableDataSource.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using JackHenryTwitter.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using static JackHenryTwitter.Models.TweetStats;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class AppVariableDataSource.
    /// Implements the <see cref="JackHenryTwitter.Models.ISetTwitterData" />
    /// </summary>
    /// <seealso cref="JackHenryTwitter.Models.ISetTwitterData" />
    public class SetTwitterDataToJsonFile : ISetTwitterData
    {
        #region Public Fields

        /// <summary>
        /// The emoji base list
        /// </summary>
        public List<EmojiBase> emojiBaseList = new List<EmojiBase>();

        #endregion Public Fields

        #region Private Fields

        private RunningTotals runningTotals;

        /// <summary>
        /// The tweet data
        /// </summary>
        private TweetData tweetData = new TweetData();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTwitterDataToJsonFile" /> class.
        /// </summary>
        public SetTwitterDataToJsonFile()
        {
            tweetData.Tweets = new List<Tweet>();
            runningTotals = new RunningTotals();
            runningTotals.TweetsWithEmojiRunningTotal = 0;
            runningTotals.TweetsWithHashTagRunningTotal = 0;
            runningTotals.TweetsWithUrlRunningTotal = 0;
            runningTotals.RunningTotalEmoji = 0;
            runningTotals.RunningTotalHashtag = 0;
            runningTotals.RunningTotalUrl = 0;
            runningTotals.FullListOfUrls = new List<string>();
            runningTotals.FullListOfHashtags = new List<string>();

            CombinedFilePathForData = GetTwitterDetails.GetTweetJsonFilePath(false);
            CombinedFilePathForStats = GetTwitterDetails.GetTweetJsonFilePath(true);
            var filePath = ConfigurationManager.AppSettings["EmojiStatsJsonFilePath"];
            CombinedFilePathForEmojis = GetTwitterDetails.GetFilePath(filePath);
        }

        #endregion Public Constructors

        #region Public Properties

        public TweetDownloadProperties tweetDownloadProperties { get; set; }

        #endregion Public Properties

        #region Private Properties

        /// <summary>
        /// Gets or sets the combined file path.
        /// </summary>
        /// <value>The combined file path.</value>
        private string CombinedFilePathForData { get; set; }

        /// <summary>
        /// Gets or sets the combined file path for emojis.
        /// </summary>
        /// <value>The combined file path for emojis.</value>
        private string CombinedFilePathForEmojis { get; set; }

        /// <summary>
        /// Gets or sets the combined file path for stats.
        /// </summary>
        /// <value>The combined file path for stats.</value>
        private string CombinedFilePathForStats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is finished loading tweets.
        /// </summary>
        /// <value><c>true</c> if this instance is finished loading tweets; otherwise, <c>false</c>.</value>
        private bool IsFinishedLoadingTweets { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Adds the tweet data to data source.
        /// </summary>
        /// <param name="data">The tweet data.</param>
        public void AddStreamingTweetToTempDataset(string data)
        {
            List<string> linesToCheckForStats = new List<string>();
            Tweet myDeserializedClass = JsonConvert.DeserializeObject<Tweet>(data);
            tweetData.Tweets.Add(myDeserializedClass);
            string lineToAdd = myDeserializedClass.data.text;
            if (!string.IsNullOrEmpty(lineToAdd))
            {
                linesToCheckForStats.Add(lineToAdd);
            }
            if (GetTwitterDetails.SearchTextFieldOnlyForStatTotals() == false)
            {
                foreach (var user in myDeserializedClass.includes.users)
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
                if (myDeserializedClass.data.entities != null)
                {
                    ent = myDeserializedClass.data.entities;
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
                List<EmojiBase> thisEmojiList = LineStatParsers.GetEmojiList(linesToCheckForStats);

                if (thisEmojiList.Count > 0)
                {
                    this.emojiBaseList.AddRange(thisEmojiList);
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

        /// <summary>
        /// Formats the tweet Json data for data insert.
        /// </summary>
        /// <param name="tweetJsonData">The tweets Json data.</param>
        /// <returns>System.String.</returns>
        public string FormatTweetDataForDataInsert(string tweetJsonData)
        {
            string returnValue = tweetJsonData.ReplaceFirst("{", ",");
            returnValue = tweetJsonData.ReplaceLast("}", "");
            return returnValue;
        }

        /// <summary>
        /// Updats the top emojies.
        /// </summary>
        /// <param name="newEmojiList">The new emoji list.</param>
        /// <param name="existingList">The existing list.</param>
        /// <returns>List&lt;TopEmojies&gt;.</returns>
        public List<TopEmojies> UpdateTopEmojies(List<TopEmojies> newEmojiList, List<TopEmojies> existingList)
        {
            List<TopEmojies> topEmojis = new List<TopEmojies>();
            List<TopEmojies> combinedLists = new List<TopEmojies>();
            combinedLists.AddRange(newEmojiList);
            combinedLists.AddRange(existingList);

            var uniqueEmojies = combinedLists.Select(s => new { s.Emoji.EmojiHtmlEncode, s.Emoji.EmojiImage }).Distinct();
            foreach (var item in uniqueEmojies)
            {
                TopEmojies topEmoji = new TopEmojies();
                EmojiBase emoji = new EmojiBase();
                var emojiHtmlEncode = item.EmojiImage;
                emoji.EmojiHtmlEncode = emojiHtmlEncode;
                emoji.EmojiImage = item.EmojiImage;
                topEmoji.Emoji = emoji;
                int emojiCount = 0;
                foreach (var te in combinedLists)
                {
                    if (te.Emoji.EmojiHtmlEncode.Equals(item.EmojiHtmlEncode))
                    {
                        emojiCount += te.EmojiCount;
                    }
                }
                topEmoji.EmojiCount = emojiCount;
                topEmojis.Add(topEmoji);
            }
            topEmojis = topEmojis.OrderByDescending(o => o.EmojiCount).Take(100).ToList();
            return topEmojis;
        }

        /// <summary>
        /// Updates the top hashtags.
        /// </summary>
        /// <param name="existingTopHashtags">The existing top hashtags.</param>
        /// <param name="newTopHashtags">The new top hashtags.</param>
        /// <returns>TweetStats.TopHashtags.</returns>
        public List<TopHashtags> UpdateTopHashtags(List<TopHashtags> existingTopHashtags, List<TweetStats.TopHashtags> newTopHashtags)
        {
            List<TopHashtags> topHashtags = new List<TopHashtags>();
            List<TopHashtags> combinedLists = new List<TopHashtags>();
            combinedLists.AddRange(newTopHashtags);
            combinedLists.AddRange(existingTopHashtags);

            var uniqueHashtag = combinedLists.Select(s => new { s.Hashtag }).Distinct();
            foreach (var item in uniqueHashtag)
            {
                TopHashtags topHashTag = new TopHashtags();
                var hashtag = item.Hashtag;
                topHashTag.Hashtag = hashtag;
                int HashtagCount = 0;
                foreach (var te in combinedLists)
                {
                    if (te.Hashtag.Equals(item.Hashtag))
                    {
                        HashtagCount += te.HashtagCount;
                    }
                }
                topHashTag.HashtagCount = HashtagCount;
                topHashtags.Add(topHashTag);
            }
            topHashtags = topHashtags.OrderByDescending(o => o.HashtagCount).Take(100).ToList();
            return topHashtags;
        }

        /// <summary>
        /// Updates the top domains from the URLs.
        /// </summary>
        /// <param name="existingTopDomains">The existing top domains.</param>
        /// <param name="newTopDomains">The new top domains.</param>
        /// <returns>TweetStats.TopDomains.</returns>
        public List<TweetStats.TopDomains> UpdateTopUrls(List<TweetStats.TopDomains> existingTopDomains, List<TweetStats.TopDomains> newTopDomains)
        {
            List<TopDomains> mergedTopDomains = new List<TopDomains>();
            List<TopDomains> combinedLists = new List<TopDomains>();
            combinedLists.AddRange(newTopDomains);
            combinedLists.AddRange(existingTopDomains);

            var uniqueDomain = combinedLists.Select(s => new { s.Domain }).Distinct();
            foreach (var item in uniqueDomain)
            {
                TopDomains topDomain = new TopDomains();
                var domain = item.Domain;
                topDomain.Domain = domain;
                int domainCount = 0;
                foreach (var te in combinedLists)
                {
                    if (te.Domain.Equals(domain))
                    {
                        domainCount += te.DomainCount;
                    }
                }
                topDomain.DomainCount = domainCount;
                mergedTopDomains.Add(topDomain);
            }
            mergedTopDomains = mergedTopDomains.OrderByDescending(o => o.DomainCount).Take(100).ToList();
            return mergedTopDomains;
        }

        /// <summary>
        /// Updates the tweet statistics.
        /// </summary>
        /// <param name="newTweetStats">The new tweet stats.</param>
        /// <returns>TweetStats.</returns>
        public TweetStats UpdateTweetStatistics(TweetStats newTweetStats)
        {
            TweetStats combinedStats = new TweetStats();
            combinedStats = new GetTwitterDataFromJsonFile().GetTwitterStatisitcsData();
            combinedStats.TotalDownloadTimeInMiliSeconds += newTweetStats.TotalDownloadTimeInMiliSeconds;
            combinedStats.TotalTweetsReceived += newTweetStats.TotalTweetsReceived;
            combinedStats.TotalTweetsWithPhoto += newTweetStats.TotalTweetsWithPhoto;
            combinedStats.TotalUrlsInTweets += newTweetStats.TotalUrlsInTweets;
            combinedStats.TotalHashTagsInTweets += newTweetStats.TotalHashTagsInTweets;
            combinedStats.TweetsWithEmojiCount += newTweetStats.TweetsWithEmojiCount;
            combinedStats.TweetsWithHashTagsCount += newTweetStats.TweetsWithHashTagsCount;
            combinedStats.TweetsWithUrlsCount += newTweetStats.TweetsWithUrlsCount;
            combinedStats.SetAverageTimes();
            combinedStats.TopEmojisList = UpdateTopEmojies(combinedStats.TopEmojisList, newTweetStats.TopEmojisList);
            combinedStats.SetPctTweetsWithPhoto(combinedStats.TotalTweetsWithPhoto);
            combinedStats.SetPctTweetsWithUrl(combinedStats.TweetsWithUrlsCount);
            combinedStats.SetPctTweetsWithEmoji(combinedStats.TweetsWithEmojiCount);
            combinedStats.SetPctTweetsWithHashTags(combinedStats.TweetsWithHashTagsCount);
            combinedStats.TopUrlDomainList = UpdateTopUrls(combinedStats.TopUrlDomainList, newTweetStats.TopUrlDomainList);
            combinedStats.TopHashtagList = UpdateTopHashtags(combinedStats.TopHashtagList, newTweetStats.TopHashtagList);
            return combinedStats;
        }

        /// <summary>
        /// Writes the tweet stats to data set.
        /// </summary>
        /// <returns><c>true</c> if records were added to the data set, <c>false</c> otherwise.</returns>
        public bool WriteTweetStatsToDataSet(TweetDownloadProperties passedTweetDownloadProperties)
        {
            tweetDownloadProperties = passedTweetDownloadProperties;
            Root root = new Root();
            root.TweetData = tweetData;
            var content = JsonConvert.SerializeObject(root);
            runningTotals.TimeToDownloadInMiliSeconds = passedTweetDownloadProperties.TimeSpentDownloadingInSeconds / 1000;
            var emojiContent = JsonConvert.SerializeObject(emojiBaseList);
            if (ConfigurationManager.AppSettings["SaveTweetDataToFile"] == "true")
            {
                using (StreamWriter writer = new StreamWriter(CombinedFilePathForData))
                {
                    writer.Write(content);
                    writer.Close();
                }

                using (StreamWriter writer = new StreamWriter(CombinedFilePathForEmojis))
                {
                    writer.Write(emojiContent);
                    writer.Close();
                }
            }

            // set the stats for the newly downloaded stream of tweets
            TweetStats tweetStats = new TweetStats(root, emojiBaseList, runningTotals);
            tweetStats.SetAllTweetStatsProperties();
            if (File.Exists(CombinedFilePathForStats))
            {
                // combine the new stats with our existing stats
                TweetStats combinedTweetStats = UpdateTweetStatistics(tweetStats);

                // write the combined stats to a json file (a file in this case, can be any time of data storage)
                content = JsonConvert.SerializeObject(combinedTweetStats);
            }
            else
            {
                content = JsonConvert.SerializeObject(tweetStats);
            }

            using (StreamWriter writer = new StreamWriter(CombinedFilePathForStats))
            {
                writer.Write(content);
                writer.Close();
            }
            this.IsFinishedLoadingTweets = true;
            return IsFinishedLoadingTweets;
        }

        #endregion Public Methods
    }
}