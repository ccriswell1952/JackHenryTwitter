// *********************************************************************** Assembly :
// JackHenryTwitter Author : Chuck Created : 12-04-2020
//
// Last Modified By : Chuck Last Modified On : 12-09-2020 ***********************************************************************
// <copyright file="AppVariableDataSource.cs" company="">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using JackHenryTwitter.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class AppVariableDataSource. Implements the <see cref="JackHenryTwitter.Models.ISetTwitterData"/>
    /// </summary>
    /// <seealso cref="JackHenryTwitter.Models.ISetTwitterData"/>
    public partial class SetTwitterDataToJsonFile : ISetTwitterData
    {
        #region Private Fields

        private static ReaderWriterLockSlim tweetDataFileWriterLock = new ReaderWriterLockSlim();
        private static ReaderWriterLockSlim tweetStatsFileWriterLock = new ReaderWriterLockSlim();
        private static ReaderWriterLockSlim tweetStreamWriterLock = new ReaderWriterLockSlim();

        /// <summary>
        /// The tweet data
        /// </summary>
        private TweetData streamingTweetData = new TweetData();

        private TweetDownloadProperties streamingTweetDownloadProperties = new TweetDownloadProperties();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTwitterDataToJsonFile"/> class.
        /// </summary>
        public SetTwitterDataToJsonFile()
        {
            streamingTweetData.Tweets = new List<Tweet>();
            this.startTime = DateTime.Now;
            CombinedFilePathForData = GetTwitterDetails.GetTweetJsonFilePath(false);
            CombinedFilePathForStats = GetTwitterDetails.GetTweetJsonFilePath(true);
        }

        #endregion Public Constructors

        #region Private Properties

        /// <summary>
        /// Gets or sets the combined file path.
        /// </summary>
        /// <value>The combined file path.</value>
        private string CombinedFilePathForData { get; set; }

        /// <summary>
        /// Gets or sets the combined file path for stats.
        /// </summary>
        /// <value>The combined file path for stats.</value>
        private string CombinedFilePathForStats { get; set; }

        private DateTime startTime { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Adds the tweet data to data source.
        /// </summary>
        /// <param name="jsonData">The Json data as a string as it comes from the feed.</param>
        public void AddStreamingTweetToTempDataset(string jsonData, bool isFinishedDownloading)
        {
            if (!string.IsNullOrEmpty(jsonData))
            {
                Tweet deserializedData = JsonConvert.DeserializeObject<Tweet>(jsonData);
                streamingTweetData.Tweets.Add(deserializedData);
            }
            if (isFinishedDownloading || streamingTweetData.Tweets.Count >= 200)
            {
                DateTime passedStartTime = this.startTime;
                this.startTime = DateTime.Now;
                double theseTweetsTimeSpan = GetTimespanInMilliseconds(passedStartTime);
                List<Tweet> tweetsToInsert = streamingTweetData.Tweets;
                streamingTweetData.Tweets = new List<Tweet>();
                if (WriteTweetDataToDataSet(tweetsToInsert, theseTweetsTimeSpan))
                {
                    WriteTweetStatsToDataSet(theseTweetsTimeSpan);
                }
            }
        }

        /// <summary>
        /// Updates the tweet statistics.
        /// </summary>
        /// <param name="newTweetStats">The new tweet stats.</param>
        /// <returns>TweetStats.</returns>
        public TweetStats GetStreamingAndExistingTweetStatistics()
        {
            GetTwitterDataFromJsonFile getTwitterDataFromJsonFile = new GetTwitterDataFromJsonFile();
            TweetStats existingTweetStats = getTwitterDataFromJsonFile.GetTwitterStatisitcsData();
            DateTime passedStartTime = this.startTime;
            this.startTime = DateTime.Now;
            double theseTweetsTimeSpan = GetTimespanInMilliseconds(passedStartTime);

            try
            {
                Root existingRootData = getTwitterDataFromJsonFile.GetTweeterRootData();
                var currentStreamingTweetStats = GetStreamingTweetStats.GetStreamingStats(existingRootData, this.streamingTweetData, theseTweetsTimeSpan, existingTweetStats);

                return currentStreamingTweetStats;
            }
            catch
            {
                return existingTweetStats;
            }
        }

        /// <summary>
        /// Gets a timespan in milliseconds.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <returns>System.Double.</returns>
        public double GetTimespanInMilliseconds(DateTime startTime)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan span = endTime.Subtract(startTime);
            return span.TotalMilliseconds;
        }

        /// <summary>
        /// Writes the tweet data to data set.
        /// </summary>
        /// <param name="tweets">The list of tweets.</param>
        /// <param name="theseTweetsTimeSpan">These tweets time span.</param>
        /// <returns>
        /// <c>true</c> if Tweets successfully were written to their final destination, <c>false</c> otherwise.
        /// </returns>
        public bool WriteTweetDataToDataSet(List<Tweet> tweets, double theseTweetsTimeSpan)
        {
            IGetTwitterData getExistingData = new GetTwitterDataFromJsonFile();
            Root existingRootData = getExistingData.GetTweeterRootData();
            double existingTotalRunningTime = 0;
            List<Tweet> combinedTweets = new List<Tweet>();
            if (existingRootData != null)
            {
                existingTotalRunningTime = existingRootData.TweetData.TotalRunningTime;
                combinedTweets = existingRootData.TweetData.Tweets;
                combinedTweets.AddRange(tweets);
            }
            else
            {
                combinedTweets = tweets;
            }

            double totalRunningTime = theseTweetsTimeSpan + existingTotalRunningTime;

            TweetData combinedTweetData = new TweetData();
            combinedTweetData.TotalRunningTime = totalRunningTime;
            combinedTweetData.Tweets = new List<Tweet>();
            combinedTweetData.Tweets.AddRange(combinedTweets);
            Root newRoot = new Root();
            newRoot.TweetData = combinedTweetData;
            var content = JsonConvert.SerializeObject(newRoot);
            tweetDataFileWriterLock.EnterWriteLock();
            using (StreamWriter writer = new StreamWriter(CombinedFilePathForData))
            {
                writer.Write(content);
                writer.Close();
            }
            tweetDataFileWriterLock.ExitWriteLock();

            return true;
        }

        /// <summary>
        /// Writes the tweet stats to data set.
        /// </summary>
        /// <param name="theseTweetsTimeSpan">These tweets time span.</param>
        /// <returns><c>true</c> if records were added to the data set, <c>false</c> otherwise.</returns>
        public bool WriteTweetStatsToDataSet(double theseTweetsTimeSpan)
        {
            GetTwitterDataFromJsonFile tweetData = new GetTwitterDataFromJsonFile();
            TweetStats existingTweetStats = tweetData.GetTwitterStatisitcsData();
            Root root = tweetData.GetTweeterRootData();
            List<Emoji> emojiBaseList = new List<Emoji>();
            RunningTotals runningTotals = TweetParser.ParseTweetDataForDataStatistics(root.TweetData, theseTweetsTimeSpan, out emojiBaseList);

            // set the stats for the newly downloaded stream of tweets
            TweetStats tweetStats = new TweetStats(root, emojiBaseList, runningTotals);
            tweetStats.SetAllTweetStatsProperties();

            var serializedTweetStats = JsonConvert.SerializeObject(tweetStats);
            tweetStatsFileWriterLock.EnterWriteLock();
            using (StreamWriter writer = new StreamWriter(CombinedFilePathForStats))
            {
                writer.Write(serializedTweetStats);
                writer.Close();
            }
            tweetStatsFileWriterLock.ExitWriteLock();

            return true;
        }

        #endregion Public Methods
    }
}