// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-12-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-12-2020
// ***********************************************************************
// <copyright file="GetDataFromTwitter.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Tweetinvi;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class GetDataFromTwitter.
    /// Implements the <see cref="JackHenryTwitter.Models.IGetTweetsFromTwitter" />
    /// </summary>
    /// <seealso cref="JackHenryTwitter.Models.IGetTweetsFromTwitter" />
    public partial class GetTweetsFromTwitter : IGetTweetsFromTwitter
    {

        #region Private Fields

        /// <summary>
        /// The datasource
        /// </summary>
        private SetTwitterDataToJsonFile datasource = new SetTwitterDataToJsonFile();
        /// <summary>
        /// The twitter credentials
        /// </summary>
        private TwitterClient twitterCredentials;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTweetsFromTwitter"/> class.
        /// </summary>
        /// <param name="secondsToRun">The seconds to run.</param>
        /// <param name="passedDataSource">The passed data source.</param>
        /// <param name="passedTwitterCredentials">The passed twitter credentials.</param>
        public GetTweetsFromTwitter(int secondsToRun, SetTwitterDataToJsonFile passedDataSource, TwitterClient passedTwitterCredentials)
        {
            this.TweetDownloadProperties = new TweetDownloadProperties();
            this.TweetDownloadProperties.TimeWantedToDownloadTweets = secondsToRun;
            this.datasource = passedDataSource;
            this.twitterCredentials = passedTwitterCredentials;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the tweet download properties.
        /// </summary>
        /// <value>The tweet download properties.</value>
        public TweetDownloadProperties TweetDownloadProperties { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the sample tweets from twitter.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> GetSampleTweetsFromTwitter()
        {
            this.TweetDownloadProperties.StartTimeForDownload = DateTime.Now;
            this.TweetDownloadProperties.IsFinishedLoadingTweets = false;
            var timer = Stopwatch.StartNew();
            var sampleStream = twitterCredentials.StreamsV2.CreateSampleStream();
            try
            {
                sampleStream.TweetReceived += (sender, eventArgs) =>
                {
                    var json = eventArgs.Json;
                    this.datasource.AddStreamingTweetToTempDataset(json);
                    if (timer.ElapsedMilliseconds >= this.TweetDownloadProperties.TimeWantedToDownloadTweets * 1000)
                    {
                        sampleStream.StopStream();
                    }
                };
                await sampleStream.StartAsync();
                return HandleTweetDownloadCompletion();
            }
            catch
            {
                // we have received all the records twitter will send us.
                // since we aren't getting any more we write what we already have to the data set.
                sampleStream.StopStream();
                return HandleTweetDownloadCompletion();
            }
        }

        /// <summary>
        /// Handles the tweet download completion.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool HandleTweetDownloadCompletion()
        {
            this.TweetDownloadProperties.EndTimeForDownload = DateTime.Now;
            TimeSpan span = (this.TweetDownloadProperties.EndTimeForDownload.Subtract(this.TweetDownloadProperties.StartTimeForDownload));
            this.TweetDownloadProperties.TimeSpentDownloading = span.Seconds;
            this.TweetDownloadProperties.IsFinishedLoadingTweets = true;
            return this.datasource.WriteTweetStatsToDataSet(this.TweetDownloadProperties);
        }

        #endregion Public Methods

    }
}