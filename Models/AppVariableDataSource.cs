// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-05-2020
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
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Tweetinvi;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class AppVariableDataSource.
    /// Implements the <see cref="JackHenryTwitter.Models.IDataSource" />
    /// </summary>
    /// <seealso cref="JackHenryTwitter.Models.IDataSource" />
    public class AppVariableDataSource : IDataSource
    {
        public TweetData tweetData = new TweetData();

        public AppVariableDataSource(int secondsToDownload)
        {
            this.TimeToTweetDownload = secondsToDownload * 1000; // turning the time into miliseconds
            tweetData.Tweets = new System.Collections.Generic.List<Tweet>();
            CombinedFilePathForData = Utilities.Utilities.GetTweetJsonFilePath(false);
            CombinedFilePathForStats = Utilities.Utilities.GetTweetJsonFilePath(true);
        }

        public AppVariableDataSource()
        {
            CombinedFilePathForData = Utilities.Utilities.GetTweetJsonFilePath(false);
            CombinedFilePathForStats = Utilities.Utilities.GetTweetJsonFilePath(true);
        }

        /// <summary>
        /// Gets or sets the combined file path.
        /// </summary>
        /// <value>The combined file path.</value>
        public string CombinedFilePathForData { get; set; }

        /// <summary>Gets or sets the combined file path for stats.</summary>
        /// <value>The combined file path for stats.</value>
        public string CombinedFilePathForStats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is finished loading tweets.
        /// </summary>
        /// <value><c>true</c> if this instance is finished loading tweets; otherwise, <c>false</c>.</value>
        public bool IsFinishedLoadingTweets { get; set; }

        public DateTime StartTimeForDownload { get; set; }

        public DateTime EndTimeForDownload { get; set; }

        public int TimeToTweetDownload { get; set; }

        public int TimeLeftForTweetDownload { get; set; }

        /// <summary>
        /// Adds the tweet data to data source.
        /// </summary>
        /// <param name="data">The tweet data.</param>
        public void AddStreamingTweetToTempDataset(string data)
        {
            Tweet myDeserializedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<Tweet>(data);
            tweetData.Tweets.Add(myDeserializedClass);
        }

        /// <summary>
        /// Formats the tweet data for data insert.
        /// </summary>
        /// <param name="tweetData">The tweet data.</param>
        /// <returns>System.String.</returns>
        public string FormatTweetDataForDataInsert(string tweetData)
        {
            string returnValue = tweetData.ReplaceFirst("{", ",");
            returnValue = tweetData.ReplaceLast("}", "");
            return returnValue;
        }

        /// <summary>
        /// Gets the sample tweets from twitter.
        /// </summary>
        public async Task<bool> GetSampleTweetsFromTwitter()
        {
            this.StartTimeForDownload = DateTime.Now;
            IsFinishedLoadingTweets = false;
            var timer = Stopwatch.StartNew();
            var userClient = new TwitterClient(Utilities.Utilities.GetTwitterCredentials());
            var sampleStream = userClient.StreamsV2.CreateSampleStream();
            try
            {
                sampleStream.TweetReceived += (sender, eventArgs) =>
                {
                    var json = eventArgs.Json;
                    AddStreamingTweetToTempDataset(json);
                    if (timer.ElapsedMilliseconds >= this.TimeToTweetDownload)
                    {
                        sampleStream.StopStream();
                    }
                };
                await sampleStream.StartAsync();
                return WriteTweetStatsToDataSet();
            }
            catch
            {
                // we have received all the records twitter will send us.
                // since we aren't getting any more we write what we already have to the data set.
                sampleStream.StopStream();
                return WriteTweetStatsToDataSet();
            }
        }

        /// <summary>Writes the tweet stats to data set.</summary>
        /// <returns>
        ///   <c>true</c> if records were added to the data set, <c>false</c> otherwise.</returns>
        public bool WriteTweetStatsToDataSet()
        {
            this.EndTimeForDownload = DateTime.Now;
            Root root = new Root();
            root.TweetData = tweetData;
            var content = JsonConvert.SerializeObject(root);

            if(ConfigurationManager.AppSettings["SaveTweetDataToFile"] == "true")
            {
                using (StreamWriter writer = new StreamWriter(CombinedFilePathForData))
                {
                    writer.Write(content);
                    writer.Close();
                }
            }

            // set the stats for the newly downloaded stream of tweets
            TweetStats tweetStats = new TweetStats(root);
            tweetStats.TotalDownloadTimeInMiliSeconds = this.TimeToTweetDownload;
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
            IsFinishedLoadingTweets = true;
            return IsFinishedLoadingTweets;
        }

        public TweetStats UpdateTweetStatistics(TweetStats newTweetStats)
        {
            TweetStats stats = new TweetStats();
            stats = Utilities.Utilities.GetDeserializedFileJsonStatisitcsData();
            stats.TotalDownloadTimeInMiliSeconds += newTweetStats.TotalDownloadTimeInMiliSeconds;
            stats.TotalTweetsReceived += newTweetStats.TotalTweetsReceived;
            stats.TotalTweetsWithPhoto += newTweetStats.TotalTweetsWithPhoto;
            stats.TotalUrlsInTweets += newTweetStats.TotalUrlsInTweets;
            stats.SetAverageTimes();
            stats.SetPctTweetsWithPhoto(stats.TotalTweetsWithPhoto);
            stats.SetPctTweetsWithUrl(stats.TotalUrlsInTweets);
            stats.TopUrlDomainList = UpdateTopDomains(stats.TopUrlDomainList, newTweetStats.TopUrlDomainList);
            stats.TopHashtagList = UpdateTopHashtags(stats.TopHashtagList, newTweetStats.TopHashtagList);
            return stats;
        }

        public List<TweetStats.TopDomains> UpdateTopDomains(List<TweetStats.TopDomains> existingTopDomains, List<TweetStats.TopDomains> newTopDomains)
        {
            List<TweetStats.TopDomains> mergedTopDomains = new List<TweetStats.TopDomains>();

            // we iterate over the new top domains we just received
            List<string> mergedNames = new List<string>();
            foreach (var domain in newTopDomains)
            {
                string newlyAddedDomainName = domain.Domain.ToLower();
                int newlyAddedDomainCount = domain.DomainCount;
                if (!mergedNames.Contains(newlyAddedDomainName))
                {
                    mergedNames.Add(newlyAddedDomainName);
                    foreach (var existingDomain in existingTopDomains)
                    {
                        string existingDomainName = existingDomain.Domain.ToLower();
                        int existingDomainCount = existingDomain.DomainCount;

                        // if both collections contain the same domain name we add it to the collection we return and add the old count to the new count.
                        if (existingDomainName == newlyAddedDomainName)
                        {
                            TweetStats.TopDomains topDomain = new TweetStats.TopDomains();
                            topDomain.Domain = newlyAddedDomainName;
                            topDomain.DomainCount = newlyAddedDomainCount + existingDomainCount;
                            mergedTopDomains.Add(topDomain);
                            break;
                        }
                    }
                }
            }
            foreach(var existingDomain in existingTopDomains)
            {
                string existingDomainName = existingDomain.Domain.ToLower();
                int existingDomainCount = existingDomain.DomainCount;
                if (!mergedNames.Contains(existingDomainName))
                {
                    mergedNames.Add(existingDomainName);
                    TweetStats.TopDomains topDomain = new TweetStats.TopDomains();
                    topDomain.Domain = existingDomainName;
                    topDomain.DomainCount = existingDomainCount;
                    mergedTopDomains.Add(topDomain);
                }
            }

            return mergedTopDomains;
        }

        public List<TweetStats.TopHashtags> UpdateTopHashtags(List<TweetStats.TopHashtags> existingTopHashtags, List<TweetStats.TopHashtags> newTopHashtags)
        {
            List<TweetStats.TopHashtags> mergedTopHashtags = new List<TweetStats.TopHashtags>();
            
            List<string> mergedNames = new List<string>();
            // we iterate over the new top hashtags just downloaded
            foreach (var hashtags in newTopHashtags)
            {
                string newlyAddedHashtagName = hashtags.Hashtag.ToLower();
                int newlyAddedHashtagCount = hashtags.HashtagCount;
                if (!mergedNames.Contains(newlyAddedHashtagName))
                {
                    foreach (var existingHashtag in existingTopHashtags)
                    {
                        string existingHashtagName = existingHashtag.Hashtag.ToLower();
                        int existingHashtagCount = existingHashtag.HashtagCount;

                        // if both collections contain the same hashtags name we add it to the collection we return and add the old count to the new count.
                        if (existingHashtagName == newlyAddedHashtagName)
                        {
                            TweetStats.TopHashtags topHashtag = new TweetStats.TopHashtags();
                            topHashtag.Hashtag = newlyAddedHashtagName;
                            topHashtag.HashtagCount = newlyAddedHashtagCount + existingHashtagCount;
                            mergedTopHashtags.Add(topHashtag);
                            mergedNames.Add(newlyAddedHashtagName);
                            break;
                        }
                    }
                }
            }

            foreach (var existingDomain in existingTopHashtags)
            {
                string existingHashtagName = existingDomain.Hashtag.ToLower();
                int existingHashtagCount = existingDomain.HashtagCount;
                if (!mergedNames.Contains(existingHashtagName))
                {
                    mergedNames.Add(existingHashtagName);
                    TweetStats.TopHashtags topHashtag = new TweetStats.TopHashtags();
                    topHashtag.Hashtag = existingHashtagName;
                    topHashtag.HashtagCount = existingHashtagCount;
                    mergedTopHashtags.Add(topHashtag);
                }
            }

            return mergedTopHashtags;
        }
    }
}