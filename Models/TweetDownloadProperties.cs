// *********************************************************************** Assembly :
// JackHenryTwitter Author : Chuck Created : 12-12-2020
//
// Last Modified By : Chuck Last Modified On : 12-12-2020 ***********************************************************************
// <copyright file="TweedDownloadProperties.cs" company="">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class TweedDownloadProperties.
    /// </summary>
    public partial class TweetDownloadProperties
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the end time for download.
        /// </summary>
        /// <value>The end time for download.</value>
        public DateTime EndTimeForDownload { get; set; }

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
        /// Gets or sets the time actually taken to download.
        /// </summary>
        /// <value>The time taken to download.</value>
        public double TimeSpentDownloadingInMiliSeconds { get; set; }

        /// <summary>
        /// Gets or sets the time user wants to download new tweets.
        /// </summary>
        /// <value>The time to tweet download.</value>
        public int TimeWantedToDownloadTweets { get; set; }

        #endregion Public Properties
    }
}