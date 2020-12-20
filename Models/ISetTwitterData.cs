// *********************************************************************** Assembly :
// JackHenryTwitter Author : Chuck Created : 12-04-2020
//
// Last Modified By : Chuck Last Modified On : 12-13-2020 ***********************************************************************
// <copyright file="ISetTwitterData.cs" company="">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System.Collections.Generic;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Interface ISetTwitterData
    /// </summary>
    public partial interface ISetTwitterData
    {
        #region Public Methods

        /// <summary>
        /// Adds the streaming tweet to temporary dataset.
        /// </summary>
        /// <param name="jsonData">The Json data as a string as it comes from the feed.</param>
        /// <param name="isFinishedDownloading">true if the tweet stream is done, otherwise false</param>
        /// <param name="tweetDownloadProperties">a TweetDownloadProperties object</param>
        void AddStreamingTweetToTempDataset(string jsonData, bool isFinishedDownloading);

        /// <summary>
        /// Updates the tweet statistics.
        /// </summary>
        /// <returns>TweetStats.</returns>
        TweetStats GetStreamingAndExistingTweetStatistics();

        /// <summary>
        /// Writes the tweet data to data set.
        /// </summary>
        /// <param name="tweets">The tweets.</param>
        /// <param name="theseTweetsTimeSpan">The these tweets time span.</param>
        /// <returns><c>true</c> if the tweet data was written to the dataset, <c>false</c> otherwise.</returns>
        bool WriteTweetDataToDataSet(List<Tweet> tweets, double theseTweetsTimeSpan);

        /// <summary>
        /// Writes the tweet stats to data set.
        /// </summary>
        /// <param name="theseTweetsTimeSpan">The these tweets time span.</param>
        /// <returns>
        /// <c>true</c> if the tweet stats were written to the dataset, <c>false</c> otherwise.
        /// </returns>
        bool WriteTweetStatsToDataSet(double theseTweetsTimeSpan);

        #endregion Public Methods
    }
}