// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-06-2020
// ***********************************************************************
// <copyright file="IDataSource.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading.Tasks;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Interface IDataSource
    /// </summary>
    internal interface IDataSource
    {
        /// <summary>
        /// Adds the tweet data to data source.
        /// </summary>
        /// <param name="tweetData">The tweet data.</param>
        void AddStreamingTweetToTempDataset(string tweetData);

        /// <summary>
        /// Formats the tweet data for data insert.
        /// </summary>
        /// <param name="tweetData">The tweet data.</param>
        /// <returns>System.String.</returns>
        string FormatTweetDataForDataInsert(string tweetData);

        /// <summary>
        /// Gets the sample tweets from twitter.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> GetSampleTweetsFromTwitter();

        /// <summary>
        /// Updates the top domains.
        /// </summary>
        /// <param name="existingTopDomains">The existing top domains.</param>
        /// <param name="newTopDomains">The new top domains.</param>
        /// <returns>TweetStats.TopDomains.</returns>
        List<TweetStats.TopDomains> UpdateTopDomains(List<TweetStats.TopDomains> existingTopDomains, List<TweetStats.TopDomains> newTopDomains);

        /// <summary>
        /// Updates the top hashtags.
        /// </summary>
        /// <param name="existingTopHashtags">The existing top hashtags.</param>
        /// <param name="newTopHashtags">The new top hashtags.</param>
        /// <returns>TweetStats.TopHashtags.</returns>
        List<TweetStats.TopHashtags> UpdateTopHashtags(List<TweetStats.TopHashtags> existingTopHashtags, List<TweetStats.TopHashtags> newTopHashtags);

        /// <summary>
        /// Updates the tweet statistics.
        /// </summary>
        /// <param name="newTweetStats">The new tweet stats.</param>
        /// <returns>TweetStats.</returns>
        TweetStats UpdateTweetStatistics(TweetStats newTweetStats);

        /// <summary>
        /// Writes the tweet stats to data set.
        /// </summary>
        /// <returns><c>true</c> if if the tweet stats were written to the dataset, <c>false</c> otherwise.</returns>
        bool WriteTweetStatsToDataSet();
    }
}