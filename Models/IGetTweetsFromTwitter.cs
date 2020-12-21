// *********************************************************************** Assembly :
// JackHenryTwitter Author : Chuck Created : 12-12-2020
//
// Last Modified By : Chuck Last Modified On : 12-12-2020 ***********************************************************************
// <copyright file="GetDataFromTwitter.cs" company="">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System.Threading.Tasks;

/// <summary>
/// The Models namespace.
/// </summary>
namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Interface GetDataFromTwitter
    /// </summary>
    internal partial interface IGetTweetsFromTwitter
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the tweet download properties.
        /// </summary>
        /// <value>The tweet download properties.</value>
        TweetDownloadProperties TweetDownloadProperties { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the sample tweets from twitter.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> GetSampleTweetsFromTwitter();

        #endregion Public Methods
    }
}