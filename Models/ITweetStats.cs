// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-05-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-13-2020
// ***********************************************************************
// <copyright file="ITweetStats.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Interface ITweetStats
    /// </summary>
    public interface ITweetStats
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the average tweets received per hour.
        /// </summary>
        /// <value>The average tweets received per hour.</value>
        double AverageTweetsReceivedPerHour { get; set; }

        /// <summary>
        /// Gets or sets the average tweets received per minute.
        /// </summary>
        /// <value>The average tweets received per minute.</value>
        double AverageTweetsReceivedPerMinute { get; set; }

        /// <summary>
        /// Gets or sets the average tweets received per second.
        /// </summary>
        /// <value>The average tweets received per second.</value>
        double AverageTweetsReceivedPerSecond { get; set; }

        /// <summary>
        /// Gets or sets the PCT tweets with emojis.
        /// </summary>
        /// <value>The PCT tweets with emojis.</value>
        decimal PctTweetsWithEmojis { get; set; }

        /// <summary>
        /// Gets or sets the PCT tweets with hash tags.
        /// </summary>
        /// <value>The PCT tweets with hash tags.</value>
        decimal PctTweetsWithHashTags { get; set; }

        /// <summary>
        /// Gets or sets the PCT tweets with photo.
        /// </summary>
        /// <value>The PCT tweets with photo.</value>
        decimal PctTweetsWithPhoto { get; set; }

        /// <summary>
        /// Gets or sets the PCT tweets with URL.
        /// </summary>
        /// <value>The PCT tweets with URL.</value>
        decimal PctTweetsWithUrl { get; set; }

        /// <summary>
        /// Gets or sets the top emojis list.
        /// </summary>
        /// <value>The top emojis list.</value>
        List<TweetStats.TopEmojies> TopEmojisList { get; set; }

        /// <summary>
        /// Gets or sets the top hashtag list.
        /// </summary>
        /// <value>The top hashtag list.</value>
        List<TweetStats.TopHashtags> TopHashtagList { get; set; }

        /// <summary>
        /// Gets or sets the top URL domain list.
        /// </summary>
        /// <value>The top URL domain list.</value>
        List<TweetStats.TopDomains> TopUrlDomainList { get; set; }

        /// <summary>
        /// Gets or sets the total download time in mili seconds.
        /// </summary>
        /// <value>The total download time in mili seconds.</value>
        double TotalDownloadTimeInMiliSeconds { get; set; }

        /// <summary>
        /// Gets or sets the total emojis in tweets.
        /// </summary>
        /// <value>The total emojis in tweets.</value>
        double TotalEmojisInTweets { get; set; }

        /// <summary>
        /// Gets or sets the total hash tags in tweets.
        /// </summary>
        /// <value>The total hash tags in tweets.</value>
        double TotalHashTagsInTweets { get; set; }

        /// <summary>
        /// Gets or sets the total tweets received.
        /// </summary>
        /// <value>The total tweets received.</value>
        double TotalTweetsReceived { get; set; }

        /// <summary>
        /// Gets or sets the total tweets with photo.
        /// </summary>
        /// <value>The total tweets with photo.</value>
        double TotalTweetsWithPhoto { get; set; }

        /// <summary>
        /// Gets or sets the total urls in tweets.
        /// </summary>
        /// <value>The total urls in tweets.</value>
        double TotalUrlsInTweets { get; set; }

        /// <summary>
        /// Gets or sets the tweets with emoji count.
        /// </summary>
        /// <value>The tweets with emoji count.</value>
        double TweetsWithEmojiCount { get; set; }

        /// <summary>
        /// Gets or sets the tweets with hash tags count.
        /// </summary>
        /// <value>The tweets with hash tags count.</value>
        double TweetsWithHashTagsCount { get; set; }

        /// <summary>
        /// Gets or sets the tweets with urls count.
        /// </summary>
        /// <value>The tweets with urls count.</value>
        double TweetsWithUrlsCount { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Sets all tweet stats properties.
        /// </summary>
        void SetAllTweetStatsProperties();

        /// <summary>
        /// Sets the average times.
        /// </summary>
        void SetAverageTimes();

        /// <summary>
        /// Sets the PCT tweets with emoji.
        /// </summary>
        void SetPctTweetsWithEmoji();

        /// <summary>
        /// Sets the PCT tweets with emoji.
        /// </summary>
        /// <param name="totalEmojis">The total emojis.</param>
        void SetPctTweetsWithEmoji(double totalEmojis);

        /// <summary>
        /// Sets the PCT tweets with hash tags.
        /// </summary>
        void SetPctTweetsWithHashTags();

        /// <summary>
        /// Sets the PCT tweets with hash tags.
        /// </summary>
        /// <param name="tweetsWithHashtagCount">The tweets with hashtag count.</param>
        void SetPctTweetsWithHashTags(double tweetsWithHashtagCount);

        /// <summary>
        /// Sets the PCT tweets with hash tags.
        /// </summary>
        /// <param name="tweetsWithHashtagCount">The tweets with hashtag count.</param>
        void SetPctTweetsWithHashTags(int tweetsWithHashtagCount);

        /// <summary>
        /// Sets the PCT tweets with photo.
        /// </summary>
        void SetPctTweetsWithPhoto();

        /// <summary>
        /// Sets the PCT tweets with photo.
        /// </summary>
        /// <param name="totalTweetsWithPhoto">The total tweets with photo.</param>
        void SetPctTweetsWithPhoto(double totalTweetsWithPhoto);

        /// <summary>
        /// Sets the PCT tweets with URL.
        /// </summary>
        void SetPctTweetsWithUrl();

        /// <summary>
        /// Sets the PCT tweets with URL.
        /// </summary>
        /// <param name="tweetsWithUrlCount">The tweets with URL count.</param>
        void SetPctTweetsWithUrl(double tweetsWithUrlCount);

        /// <summary>
        /// Sets the top emojies.
        /// </summary>
        /// <returns>List&lt;TweetStats.TopEmojies&gt;.</returns>
        List<TweetStats.TopEmojies> SetTopEmojies();

        /// <summary>
        /// Sets the top hashtags.
        /// </summary>
        void SetTopHashtags();

        /// <summary>
        /// Sets the top URL domains.
        /// </summary>
        void SetTopUrlDomains();

        #endregion Public Methods
    }
}