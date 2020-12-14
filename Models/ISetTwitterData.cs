// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-04-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-13-2020
// ***********************************************************************
// <copyright file="ISetTwitterData.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Interface ISetTwitterData
    /// </summary>
    public interface ISetTwitterData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the tweet download properties.
        /// </summary>
        /// <value>The tweet download properties.</value>
        TweetDownloadProperties tweetDownloadProperties { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds the streaming tweet to temporary dataset.
        /// </summary>
        /// <param name="data">The data.</param>
        void AddStreamingTweetToTempDataset(string data);

        /// <summary>
        /// Formats the tweet data for data insert.
        /// </summary>
        /// <param name="tweetJsonData">The tweet json data.</param>
        /// <returns>System.String.</returns>
        string FormatTweetDataForDataInsert(string tweetJsonData);

        /// <summary>
        /// Updates the top emojies.
        /// </summary>
        /// <param name="newEmojiList">The new emoji list.</param>
        /// <param name="existingList">The existing list.</param>
        /// <returns>List&lt;TweetStats.TopEmojies&gt;.</returns>
        List<TweetStats.TopEmojies> UpdateTopEmojies(List<TweetStats.TopEmojies> newEmojiList, List<TweetStats.TopEmojies> existingList);

        /// <summary>
        /// Updates the top hashtags.
        /// </summary>
        /// <param name="existingTopHashtags">The existing top hashtags.</param>
        /// <param name="newTopHashtags">The new top hashtags.</param>
        /// <returns>List&lt;TweetStats.TopHashtags&gt;.</returns>
        List<TweetStats.TopHashtags> UpdateTopHashtags(List<TweetStats.TopHashtags> existingTopHashtags, List<TweetStats.TopHashtags> newTopHashtags);

        /// <summary>
        /// Updates the top urls.
        /// </summary>
        /// <param name="existingTopDomains">The existing top domains.</param>
        /// <param name="newTopDomains">The new top domains.</param>
        /// <returns>List&lt;TweetStats.TopDomains&gt;.</returns>
        List<TweetStats.TopDomains> UpdateTopUrls(List<TweetStats.TopDomains> existingTopDomains, List<TweetStats.TopDomains> newTopDomains);

        /// <summary>
        /// Updates the tweet statistics.
        /// </summary>
        /// <param name="newTweetStats">The new tweet stats.</param>
        /// <returns>TweetStats.</returns>
        TweetStats UpdateTweetStatistics(TweetStats newTweetStats);

        /// <summary>
        /// Writes the tweet stats to data set.
        /// </summary>
        /// <param name="passedTweetDownloadProperties">The passed tweet download properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WriteTweetStatsToDataSet(TweetDownloadProperties passedTweetDownloadProperties);

        #endregion Public Methods
    }
}