// *********************************************************************** Assembly :
// JackHenryTwitter Author : Chuck Created : 12-05-2020
//
// Last Modified By : Chuck Last Modified On : 12-13-2020 ***********************************************************************
// <copyright file="TweetStats.cs" company="">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class TweetStats.
    /// </summary>
    public partial class TweetStats : ITweetStats
    {
        #region Private Fields

        /// <summary>
        /// The emoji list
        /// </summary>
        private List<Emoji> emojiList = new List<Emoji>();

        /// <summary>
        /// The full list of hashtags
        /// </summary>
        private List<string> fullListOfHashtags = new List<string>();

        /// <summary>
        /// The full list of urls
        /// </summary>
        private List<string> fullListOfUrls = new List<string>();

        /// <summary>
        /// The unique list of emojies
        /// </summary>
        private List<string> uniqueListOfEmojies = new List<string>();

        /// <summary>
        /// The unique list of hashtags
        /// </summary>
        private List<string> uniqueListOfHashtags = new List<string>();

        /// <summary>
        /// The unique list of urls
        /// </summary>
        private List<string> uniqueListOfUrls = new List<string>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetStats"/> class.
        /// </summary>
        public TweetStats()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetStats"/> class.
        /// </summary>
        /// <param name="root">A Tweet Root object.</param>
        /// <param name="emojiList">The emoji list.</param>
        /// <param name="runningTotals">The running totals.</param>
        public TweetStats(Root root, List<Emoji> emojiList, RunningTotals runningTotals)
        {
            this.TotalTweetsReceived = root.TweetData.Tweets.Count;
            this.TotalDownloadTimeInMiliSeconds = root.TweetData.TotalRunningTime;
            this.TotalTweetsWithPhoto = root.TweetData.Tweets.Where(t => t.includes.users.Any(a => a.profile_image_url != null)).Count();
            this.emojiList = emojiList;
            this.TotalHashTagsInTweets = runningTotals.RunningTotalHashtag;
            this.TotalUrlsInTweets = runningTotals.RunningTotalUrl;
            this.TotalEmojisInTweets = runningTotals.RunningTotalEmoji;
            this.TweetsWithEmojiCount = runningTotals.TweetsWithEmojiRunningTotal;
            this.TweetsWithUrlsCount = runningTotals.TweetsWithUrlRunningTotal;
            this.TweetsWithHashTagsCount = runningTotals.TweetsWithHashTagRunningTotal;
            this.fullListOfHashtags = runningTotals.FullListOfHashtags;
            this.fullListOfUrls = runningTotals.FullListOfUrls;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the average tweets received per hour.
        /// </summary>
        /// <value>The average tweets received per hour.</value>
        public double AverageTweetsReceivedPerHour { get; set; }

        /// <summary>
        /// Gets or sets the average tweets received per minute.
        /// </summary>
        /// <value>The average tweets received per minute.</value>
        public double AverageTweetsReceivedPerMinute { get; set; }

        /// <summary>
        /// Gets or sets the average tweets received per second.
        /// </summary>
        /// <value>The average tweets received per second.</value>
        public double AverageTweetsReceivedPerSecond { get; set; }

        /// <summary>
        /// Gets or sets the percentage of tweets with emojis.
        /// </summary>
        /// <value>The percentage of tweets with emojis.</value>
        public decimal PctTweetsWithEmojis { get; set; }

        /// <summary>
        /// Gets or sets the percentage of tweets with hash tags.
        /// </summary>
        /// <value>The percentage tweets with hash tags.</value>
        public decimal PctTweetsWithHashTags { get; set; }

        /// <summary>
        /// Gets or sets the percentage of tweets with photo.
        /// </summary>
        /// <value>The percentage of tweets with photo.</value>
        public decimal PctTweetsWithPhoto { get; set; }

        /// <summary>
        /// Gets or sets the percentage of tweets with URL.
        /// </summary>
        /// <value>The percentage of tweets with URL.</value>
        public decimal PctTweetsWithUrl { get; set; }

        /// <summary>
        /// Gets or sets the top emojis list.
        /// </summary>
        /// <value>The top emojis list.</value>
        public List<TopEmojies> TopEmojisList { get; set; }

        /// <summary>
        /// Gets or sets the top hashtag list.
        /// </summary>
        /// <value>The top hashtag list.</value>
        public List<TopEntities> TopHashtagList { get; set; }

        /// <summary>
        /// Gets or sets the top URL domain list.
        /// </summary>
        /// <value>The top URL domain list.</value>
        public List<TopEntities> TopUrlDomainList { get; set; }

        /// <summary>
        /// Gets or sets the total download time in mili seconds.
        /// </summary>
        /// <value>The total download time in mili seconds.</value>
        public double TotalDownloadTimeInMiliSeconds { get; set; }

        /// <summary>
        /// Gets or sets the total emojis in tweets.
        /// </summary>
        /// <value>The total emojis in tweets.</value>
        public double TotalEmojisInTweets { get; set; }

        /// <summary>
        /// Gets or sets the total hash tags in tweets.
        /// </summary>
        /// <value>The total hash tags in tweets.</value>
        public double TotalHashTagsInTweets { get; set; }

        /// <summary>
        /// Gets or sets the total tweets received.
        /// </summary>
        /// <value>The total tweets received.</value>
        public double TotalTweetsReceived { get; set; }

        /// <summary>
        /// Gets or sets the total tweets with photo.
        /// </summary>
        /// <value>The total tweets with photo.</value>
        public double TotalTweetsWithPhoto { get; set; }

        /// <summary>
        /// Gets or sets the total urls in tweets.
        /// </summary>
        /// <value>The total urls in tweets.</value>
        public double TotalUrlsInTweets { get; set; }

        /// <summary>
        /// Gets or sets the total tweets with emoji count.
        /// </summary>
        /// <value>The tweets with emoji count.</value>
        public double TweetsWithEmojiCount { get; set; }

        /// <summary>
        /// Gets or sets the tweets with hash tags count.
        /// </summary>
        /// <value>The tweets with hash tags count.</value>
        public double TweetsWithHashTagsCount { get; set; }

        /// <summary>
        /// Gets or sets the tweets with urls count.
        /// </summary>
        /// <value>The tweets with urls count.</value>
        public double TweetsWithUrlsCount { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Sets all tweet stats properties.
        /// </summary>
        public void SetAllTweetStatsProperties()
        {
            SetAverageTimes();
            SetPctTweetsWithPhoto();
            SetPctTweetsWithEmoji();
            SetPctTweetsWithUrl();
            SetTopUrlDomains();
            SetTopHashtags();
            SetTopEmojies();
            SetPctTweetsWithHashTags();
        }

        /// <summary>
        /// Sets the average times.
        /// </summary>
        public void SetAverageTimes()
        {
            var averageTweetsPerSecond = this.TotalTweetsReceived / (this.TotalDownloadTimeInMiliSeconds / 1000);
            this.AverageTweetsReceivedPerSecond = averageTweetsPerSecond;
            this.AverageTweetsReceivedPerMinute = averageTweetsPerSecond * 60;
            this.AverageTweetsReceivedPerHour = averageTweetsPerSecond * (60 * 60);
        }

        /// <summary>
        /// Sets the percentage of tweets with emoji.
        /// </summary>
        /// <param name="totalEmojis">The total emojis.</param>
        public void SetPctTweetsWithEmoji(double totalEmojis)
        {
            this.TweetsWithEmojiCount = totalEmojis;
            if (totalEmojis > 0 && this.TotalTweetsReceived > 0)
            {
                this.PctTweetsWithEmojis = ((decimal)this.TweetsWithEmojiCount / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithEmojis = 0;
            }
        }

        /// <summary>
        /// Sets the percentage of tweets with emoji.
        /// </summary>
        public void SetPctTweetsWithEmoji()
        {
            if (this.TweetsWithEmojiCount > 0)
            {
                this.PctTweetsWithEmojis = ((decimal)this.TweetsWithEmojiCount / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithEmojis = 0;
            }
        }

        /// <summary>
        /// Sets the percentage of tweets with hash tags.
        /// </summary>
        public void SetPctTweetsWithHashTags()
        {
            if (this.TotalTweetsReceived > 0 && this.TweetsWithHashTagsCount > 0)
            {
                this.PctTweetsWithHashTags = ((decimal)this.TweetsWithHashTagsCount / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithHashTags = 0;
            }
        }

        /// Sets the percentage of tweets with hash tags. </summary>
        public void SetPctTweetsWithHashTags(double tweetsWithHashtagCount)
        {
            this.TweetsWithHashTagsCount = tweetsWithHashtagCount;
            if (this.TotalTweetsReceived > 0 && this.TweetsWithHashTagsCount > 0)
            {
                this.PctTweetsWithHashTags = ((decimal)this.TweetsWithHashTagsCount / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithHashTags = 0;
            }
        }

        /// <summary>
        /// Sets the percentage tweets with hash tags.
        /// </summary>
        /// <param name="tweetsWithHashtagCount">The tweets with hashtag count.</param>
        public void SetPctTweetsWithHashTags(int tweetsWithHashtagCount)
        {
            this.TweetsWithHashTagsCount = tweetsWithHashtagCount;
            if (tweetsWithHashtagCount > 0 && this.TotalTweetsReceived > 0)
            {
                this.PctTweetsWithHashTags = ((decimal)this.TweetsWithHashTagsCount / (decimal)this.TotalTweetsReceived) * 100; ;
            }
            else
            {
                this.PctTweetsWithHashTags = 0;
            }
        }

        /// <summary>
        /// Sets the percentage of tweets with photo.
        /// </summary>
        public void SetPctTweetsWithPhoto()
        {
            if (this.TotalTweetsReceived > 0)
            {
                this.PctTweetsWithPhoto = ((decimal)this.TotalTweetsWithPhoto / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithPhoto = 0;
            }
        }

        /// <summary>
        /// Sets the percentage of tweets with photo.
        /// </summary>
        /// <param name="totalTweetsWithPhoto">The total tweets with photo.</param>
        public void SetPctTweetsWithPhoto(double totalTweetsWithPhoto)
        {
            this.TotalTweetsWithPhoto = totalTweetsWithPhoto;
            if (this.TotalTweetsReceived > 0)
            {
                this.PctTweetsWithPhoto = ((decimal)this.TotalTweetsWithPhoto / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithPhoto = 0;
            }
        }

        /// <summary>
        /// Sets the percentage of tweets with URL.
        /// </summary>
        public void SetPctTweetsWithUrl()
        {
            if (this.TotalTweetsReceived > 0 && this.TweetsWithUrlsCount > 0)
            {
                this.PctTweetsWithUrl = ((decimal)this.TweetsWithUrlsCount / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithUrl = 0;
            }
        }

        /// <summary>
        /// Sets the percentage of tweets with URL.
        /// </summary>
        /// <param name="tweetsWithUrlCount">The total urls in tweets.</param>
        public void SetPctTweetsWithUrl(double tweetsWithUrlCount)
        {
            this.TweetsWithUrlsCount = tweetsWithUrlCount;
            if (this.TotalTweetsReceived > 0 && this.TweetsWithUrlsCount > 0)
            {
                this.PctTweetsWithUrl = ((decimal)this.TweetsWithUrlsCount / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithUrl = 0;
            }
        }

        /// <summary>
        /// Sets the top emojies.
        /// </summary>
        /// <returns>List&lt;TopEmojies&gt;.</returns>
        public List<TopEmojies> SetTopEmojies()
        {
            TopEmojisList = new List<TopEmojies>();
            this.uniqueListOfEmojies = emojiList.Select(s => s.EmojiHtmlEncode).Distinct().ToList();
            foreach (var htmlEncode in this.uniqueListOfEmojies)
            {
                var distinctList = emojiList.Select(s => new { s.EmojiHtmlEncode, s.EmojiImage }).Where(w => w.EmojiHtmlEncode.Equals(htmlEncode));

                int emojiCount = 0;
                TopEmojies topEmoji = new TopEmojies();
                Emoji emoji = new Emoji();
                emoji.EmojiHtmlEncode = htmlEncode;
                emoji.EmojiImage = distinctList.Select(s => s.EmojiImage).FirstOrDefault();
                topEmoji.Emoji = emoji;
                emojiCount = emojiList.Where(w => w.EmojiHtmlEncode == htmlEncode).Count();
                if (emojiCount > 0)
                {
                    topEmoji.EmojiCount = emojiCount;
                    TopEmojisList.Add(topEmoji);
                }
            }
            TopEmojisList = TopEmojisList.Take(100).OrderByDescending(o => o.EmojiCount).ToList();
            return TopEmojisList;
        }

        /// <summary>
        /// Sets the top hashtags.
        /// </summary>
        public void SetTopHashtags()
        {
            this.uniqueListOfHashtags = this.fullListOfHashtags.Distinct().ToList();
            List<TopEntities> topHashtagList = new List<TopEntities>();
            foreach (var uniqueHashtag in this.uniqueListOfHashtags)
            {
                TopEntities topHashtags = new TopEntities();
                var hashtagCount = this.fullListOfHashtags.Where(w => w.ToLower().Trim() == uniqueHashtag.ToLower().Trim()).Count();
                topHashtags.Value = uniqueHashtag;
                topHashtags.Count = hashtagCount;
                if (hashtagCount > 0)
                {
                    topHashtagList.Add(topHashtags);
                }
            }
            this.TopHashtagList = topHashtagList.OrderByDescending(o => o.Count).ToList();
        }

        /// <summary>
        /// Sets the top URL domains.
        /// </summary>
        public void SetTopUrlDomains()
        {
            this.uniqueListOfUrls = this.fullListOfUrls.Distinct().ToList();
            List<TopEntities> topDomainList = new List<TopEntities>();
            foreach (var uniqueHost in this.uniqueListOfUrls)
            {
                TopEntities topDomains = new TopEntities();
                var hostCount = this.fullListOfUrls.Where(w => w.ToLower().Trim() == uniqueHost.ToLower().Trim()).Count();
                topDomains.Value = uniqueHost;
                topDomains.Count = hostCount;
                if (hostCount > 0)
                {
                    topDomainList.Add(topDomains);
                }
            }
            this.TopUrlDomainList = topDomainList.OrderByDescending(o => o.Count).ToList();
        }

        #endregion Public Methods

        #region Public Classes

        /// <summary>
        /// Class TopEmojies.
        /// </summary>
        public class TopEmojies
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the emoji.
            /// </summary>
            /// <value>The emoji.</value>
            public Emoji Emoji { get; set; }

            /// <summary>
            /// Gets or sets the emoji count.
            /// </summary>
            /// <value>The emoji count.</value>
            public int EmojiCount { get; set; }

            #endregion Public Properties
        }

        #endregion Public Classes
    }
}