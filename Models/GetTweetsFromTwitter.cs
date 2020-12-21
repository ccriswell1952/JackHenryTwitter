// *********************************************************************** Assembly :
// JackHenryTwitter Author : Chuck Created : 12-12-2020
//
// Last Modified By : Chuck Last Modified On : 12-13-2020 ***********************************************************************
// <copyright file="GetTweetsFromTwitter.cs" company="">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Tweetinvi;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class GetDataFromTwitter. Implements the <see cref="JackHenryTwitter.Models.IGetTweetsFromTwitter"/>
    /// </summary>
    /// <seealso cref="JackHenryTwitter.Models.IGetTweetsFromTwitter"/>
    public partial class GetTweetsFromTwitter : IGetTweetsFromTwitter
    {
        #region Public Fields

        /// <summary>
        /// The datasource
        /// </summary>
        public ISetTwitterData DataSource;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// The end time
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// The start time
        /// </summary>
        private DateTime startTime;

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
        public GetTweetsFromTwitter(ISetTwitterData passedDataSource, TwitterClient passedTwitterCredentials)
        {
            this.TweetDownloadProperties = new TweetDownloadProperties();
            this.DataSource = passedDataSource;
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
            DateTime startTime = DateTime.Now;
            this.startTime = startTime;
            this.TweetDownloadProperties.IsFinishedLoadingTweets = false;
            var timer = Stopwatch.StartNew();
            var sampleStream = twitterCredentials.StreamsV2.CreateSampleStream();
            TweetDownloadProperties tweetDownloadProperties = new TweetDownloadProperties();
            try
            {
                sampleStream.TweetReceived += (sender, eventArgs) =>
                {
                    var json = eventArgs.Json;
                    this.DataSource.AddStreamingTweetToTempDataset(json, false);
                };
                await sampleStream.StartAsync();
                return true;
            }
            finally
            {
                sampleStream.StopStream();
                this.DataSource.AddStreamingTweetToTempDataset(null, true);
            }
        }

        #endregion Public Methods
    }
}