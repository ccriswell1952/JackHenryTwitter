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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using static JackHenryTwitter.Models.TweetStats;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class AppVariableDataSource.
    /// Implements the <see cref="JackHenryTwitter.Models.IDataSource" />
    /// </summary>
    /// <seealso cref="JackHenryTwitter.Models.IDataSource" />
    public class DataSource : IDataSource
    {

        #region Public Fields

        /// <summary>
        /// The emoji base list
        /// </summary>
        public List<EmojiBase> emojiBaseList = new List<EmojiBase>();

        /// <summary>
        /// The emoji in file running count
        /// </summary>
        public int EmojiInFileRunningCount = 0;

        /// <summary>
        /// The tweet data
        /// </summary>
        public TweetData tweetData = new TweetData();

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSource" /> class.
        /// </summary>
        public DataSource()
        {
            tweetData.Tweets = new List<Tweet>();
            CombinedFilePathForData = Utilities.Utilities.GetTweetJsonFilePath(false);
            CombinedFilePathForStats = Utilities.Utilities.GetTweetJsonFilePath(true);
            var filePath = ConfigurationManager.AppSettings["EmojiStatsJsonFilePath"];
            CombinedFilePathForEmojis = Utilities.Utilities.GetFilePath(filePath);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the combined file path.
        /// </summary>
        /// <value>The combined file path.</value>
        public string CombinedFilePathForData { get; set; }

        /// <summary>
        /// Gets or sets the combined file path for emojis.
        /// </summary>
        /// <value>The combined file path for emojis.</value>
        public string CombinedFilePathForEmojis { get; set; }

        /// <summary>
        /// Gets or sets the combined file path for stats.
        /// </summary>
        /// <value>The combined file path for stats.</value>
        public string CombinedFilePathForStats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is finished loading tweets.
        /// </summary>
        /// <value><c>true</c> if this instance is finished loading tweets; otherwise, <c>false</c>.</value>
        public bool IsFinishedLoadingTweets { get; set; }

        /// <summary>
        /// Gets or sets the start time for download.
        /// </summary>
        /// <value>The start time for download.</value>
        public DateTime StartTimeForDownload { get; set; }

        /// <summary>
        /// Gets or sets the time left for tweet download.
        /// </summary>
        /// <value>The time left for tweet download.</value>
        public int TimeLeftForTweetDownload { get; set; }

        public TweetDownloadProperties tweetDownloadProperties { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds the tweet data to data source.
        /// </summary>
        /// <param name="data">The tweet data.</param>
        public void AddStreamingTweetToTempDataset(string data)
        {
            List<string> emojiList = new List<string>();
            Tweet myDeserializedClass = JsonConvert.DeserializeObject<Tweet>(data);
            tweetData.Tweets.Add(myDeserializedClass);
            string lineToAdd = myDeserializedClass.data.text;
            if (!string.IsNullOrEmpty(lineToAdd))
            {
                emojiList.Add(lineToAdd);
            }
            foreach (var user in myDeserializedClass.includes.users)
            {
                lineToAdd = null;
                lineToAdd = user.name;
                if (!string.IsNullOrEmpty(lineToAdd))
                {
                    emojiList.Add(lineToAdd);
                }
                lineToAdd = null;
                lineToAdd = user.description;
                if (!string.IsNullOrEmpty(lineToAdd))
                {
                    emojiList.Add(lineToAdd);
                }
            }
            if (emojiList.Count > 0)
            {
                EmojiInFileRunningCount++;
                List<EmojiBase> thisList = EmojiParser.GetEmojiList(emojiList);
                emojiBaseList.AddRange(thisList);
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
        /// Updates the top domains.
        /// </summary>
        /// <param name="existingTopDomains">The existing top domains.</param>
        /// <param name="newTopDomains">The new top domains.</param>
        /// <returns>TweetStats.TopDomains.</returns>
        public List<TweetStats.TopDomains> UpdateTopDomains(List<TweetStats.TopDomains> existingTopDomains, List<TweetStats.TopDomains> newTopDomains)
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
        /// Updates the tweet statistics.
        /// </summary>
        /// <param name="newTweetStats">The new tweet stats.</param>
        /// <returns>TweetStats.</returns>
        public TweetStats UpdateTweetStatistics(TweetStats newTweetStats)
        {
            TweetStats stats = new TweetStats();
            stats = Utilities.Utilities.GetDeserializedFileJsonStatisitcsData();
            stats.TotalDownloadTimeInMiliSeconds += newTweetStats.TotalDownloadTimeInMiliSeconds;
            stats.TotalTweetsReceived += newTweetStats.TotalTweetsReceived;
            stats.TotalTweetsWithPhoto += newTweetStats.TotalTweetsWithPhoto;
            stats.TotalUrlsInTweets += newTweetStats.TotalUrlsInTweets;
            stats.TweetsWithEmojiCount += newTweetStats.TweetsWithEmojiCount;
            stats.SetAverageTimes();
            var combinedStats = UpdatTopEmojies(stats.TopEmojis, newTweetStats.TopEmojis);
            stats.TopEmojis = combinedStats;
            stats.SetPctTweetsWithPhoto(stats.TotalTweetsWithPhoto);
            stats.SetPctTweetsWithUrl(stats.TotalUrlsInTweets);
            stats.SetPctTweetsWithEmoji(stats.TweetsWithEmojiCount);
            stats.TopUrlDomainList = UpdateTopDomains(stats.TopUrlDomainList, newTweetStats.TopUrlDomainList);
            stats.TopHashtagList = UpdateTopHashtags(stats.TopHashtagList, newTweetStats.TopHashtagList);
            return stats;
        }

        /// <summary>
        /// Updats the top emojies.
        /// </summary>
        /// <param name="newEmojiList">The new emoji list.</param>
        /// <param name="existingList">The existing list.</param>
        /// <returns>List&lt;TopEmojies&gt;.</returns>
        public List<TopEmojies> UpdatTopEmojies(List<TopEmojies> newEmojiList, List<TopEmojies> existingList)
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
        /// Writes the tweet stats to data set.
        /// </summary>
        /// <returns><c>true</c> if records were added to the data set, <c>false</c> otherwise.</returns>
        public bool WriteTweetStatsToDataSet(TweetDownloadProperties passedTweetDownloadProperties)
        {
            tweetDownloadProperties = passedTweetDownloadProperties;
            Root root = new Root();
            root.TweetData = tweetData;
            var content = JsonConvert.SerializeObject(root);

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
            TweetStats tweetStats = new TweetStats(root, emojiBaseList, EmojiInFileRunningCount);
            tweetStats.TotalDownloadTimeInMiliSeconds = tweetDownloadProperties.TimeSpentDownloading * 1000;
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