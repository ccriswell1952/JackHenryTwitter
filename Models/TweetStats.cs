// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-05-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="TweetStats.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class TweetStats.
    /// </summary>
    public partial class TweetStats
    {
        /// <summary>
        /// The emoji list
        /// </summary>
        private List<EmojiBase> emojiList = new List<EmojiBase>();

        /// <summary>
        /// The top emojies
        /// </summary>
        private List<TopEmojies> topEmojies = new List<TopEmojies>();

        /// <summary>
        /// The tweet root
        /// </summary>
        private Root tweetRoot = new Root();

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetStats" /> class.
        /// </summary>
        public TweetStats()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetStats" /> class.
        /// </summary>
        /// <param name="root">A Tweet Root object.</param>
        /// <param name="emojiList">The emoji list.</param>
        /// <param name="tweetsWithEmojiCount">The tweets with emoji count.</param>
        public TweetStats(Root root, List<EmojiBase> emojiList, int tweetsWithEmojiCount = 0)
        {
            this.tweetRoot = root;
            this.emojiList = emojiList;
            this.TweetsWithEmojiCount = tweetsWithEmojiCount;
        }

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
        /// Gets or sets the top emojis.
        /// </summary>
        /// <value>The top emojis.</value>
        public List<TopEmojies> TopEmojis { get; set; }

        /// <summary>
        /// Gets or sets the top hashtag list.
        /// </summary>
        /// <value>The top hashtag list.</value>
        public List<TopHashtags> TopHashtagList { get; set; }

        /// <summary>
        /// Gets or sets the top URL domain list.
        /// </summary>
        /// <value>The top URL domain list.</value>
        public List<TopDomains> TopUrlDomainList { get; set; }

        /// <summary>
        /// Gets or sets the total download time in mili seconds.
        /// </summary>
        /// <value>The total download time in mili seconds.</value>
        public int TotalDownloadTimeInMiliSeconds { get; set; }

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
        public int TweetsWithEmojiCount { get; set; }

        /// <summary>
        /// Sets all tweet stats properties.
        /// </summary>
        public void SetAllTweetStatsProperties()
        {
            this.TotalTweetsReceived = tweetRoot.TweetData.Tweets.Count;
            SetAverageTimes();
            SetPctTweetsWithPhoto();
            SetPctTweetsWithEmoji(0);
            SetPctTweetsWithUrl();
            SetTopUrlDomains();
            SetTopHashtags();
            SetTopEmojies();
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
        public void SetPctTweetsWithEmoji(int totalEmojis)
        {
            if (totalEmojis > 0)
            {
                this.TweetsWithEmojiCount = totalEmojis;
                this.PctTweetsWithEmojis = ((decimal)this.TweetsWithEmojiCount / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithEmojis = 0;
            }
        }

        /// <summary>
        /// Sets the PCT tweets with emoji.
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
        /// Sets the percentage of tweets with photo.
        /// </summary>
        public void SetPctTweetsWithPhoto()
        {
            this.TotalTweetsWithPhoto = tweetRoot.TweetData.Tweets.Where(t => t.includes.users.Any(a => a.profile_image_url != null)).Count();
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
            this.TotalUrlsInTweets = tweetRoot.TweetData.Tweets.Where(t => t.includes.users.Any(a => a.url != null)).Count();
            if (this.TotalTweetsReceived > 0)
            {
                this.PctTweetsWithUrl = ((decimal)this.TotalUrlsInTweets / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithUrl = 0;
            }
        }

        /// <summary>
        /// Sets the percentage of tweets with URL.
        /// </summary>
        /// <param name="totalUrlsInTweets">The total urls in tweets.</param>
        public void SetPctTweetsWithUrl(double totalUrlsInTweets)
        {
            this.TotalUrlsInTweets = totalUrlsInTweets;
            if (this.TotalTweetsReceived > 0)
            {
                this.PctTweetsWithUrl = ((decimal)this.TotalUrlsInTweets / (decimal)this.TotalTweetsReceived) * 100;
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
            TopEmojis = new List<TopEmojies>();
            var distinctList = emojiList.Select(s => new { s.EmojiHtmlEncode, s.EmojiImage }).Distinct();
            foreach (var image in distinctList)
            {
                int emojiCount = 0;
                TopEmojies topEmoji = new TopEmojies();
                EmojiBase emoji = new EmojiBase();
                emoji.EmojiHtmlEncode = image.EmojiHtmlEncode;
                emoji.EmojiImage = image.EmojiImage;
                topEmoji.Emoji = emoji;
                emojiCount = emojiList.Where(w => w.EmojiHtmlEncode == image.EmojiHtmlEncode).Count();
                if (emojiCount > 1)
                {
                    topEmoji.EmojiCount = emojiCount;
                    TopEmojis.Add(topEmoji);
                }
            }
            TopEmojis = TopEmojis.Take(100).OrderByDescending(o => o.EmojiCount).ToList();
            return TopEmojis;
        }

        /// <summary>
        /// Sets the top hashtags.
        /// </summary>
        public void SetTopHashtags()
        {
            var regex = new Regex(@"#\w+");
            List<string> hashTagList = new List<string>();
            List<string> uniqueHashTagList = new List<string>();
            foreach (var tweet in tweetRoot.TweetData.Tweets)
            {
                var hashTags = regex.Matches(tweet.data.text);
                if (hashTags != null)
                {
                    foreach (var hashTag in hashTags)
                    {
                        hashTagList.Add(hashTag.ToString().ToLower());
                    }
                }
                foreach (var user in tweet.includes.users)
                {
                    hashTags = regex.Matches(user.description);
                    if (hashTags != null)
                    {
                        foreach (var hashTag in hashTags)
                        {
                            hashTagList.Add(hashTag.ToString().ToLower());
                        }
                    }
                }
            }
            uniqueHashTagList = hashTagList.Select(s => s.Trim().ToLower()).Distinct().ToList();
            List<TopHashtags> topHashtagList = new List<TopHashtags>();
            foreach (var uniqueHashtag in uniqueHashTagList)
            {
                TopHashtags topHashtags = new TopHashtags();
                var hashtagCount = hashTagList.Where(w => w.ToLower().Trim() == uniqueHashtag.ToLower().Trim()).Count();
                topHashtags.Hashtag = uniqueHashtag;
                topHashtags.HashtagCount = hashtagCount;
                if (hashtagCount > 1)
                {
                    topHashtagList.Add(topHashtags);
                }
            }
            this.TopHashtagList = topHashtagList.OrderByDescending(o => o.HashtagCount).ToList();
        }

        /// <summary>
        /// Sets the top URL domains.
        /// </summary>
        public void SetTopUrlDomains()
        {
            List<string> urlList = new List<string>();
            List<string> hostList = new List<string>();
            List<string> uniqueHostList = new List<string>();
            foreach (var tweet in tweetRoot.TweetData.Tweets)
            {
                foreach (var u in tweet.includes.users)
                {
                    if (!string.IsNullOrEmpty(u.url))
                    {
                        urlList.Add(u.url);
                    }
                }
                Entities ent = new Entities();
                if (tweet.data.entities != null)
                {
                    ent = tweet.data.entities;
                    List<Url> us = new List<Url>();
                    if (ent.urls != null)
                    {
                        us = ent.urls;
                        foreach (var e in us)
                        {
                            urlList.Add(e.expanded_url);
                        }
                    }
                }
            }

            foreach (var url in urlList)
            {
                var host = Utilities.Utilities.GetHostFromUrlString(url);
                hostList.Add(host);
            }
            uniqueHostList = hostList.Select(s => s.Trim()).Distinct().ToList();
            List<TopDomains> topDomainList = new List<TopDomains>();
            foreach (var uniqueHost in uniqueHostList)
            {
                TopDomains topDomains = new TopDomains();
                var hostCount = hostList.Where(w => w.ToLower().Trim() == uniqueHost.ToLower().Trim()).Count();
                topDomains.Domain = uniqueHost;
                topDomains.DomainCount = hostCount;
                if (hostCount > 1)
                {
                    topDomainList.Add(topDomains);
                }
            }
            this.TopUrlDomainList = topDomainList.OrderByDescending(o => o.DomainCount).ToList();
        }

        /// <summary>
        /// Class TopDomains.
        /// </summary>
        public class TopDomains
        {
            /// <summary>
            /// Gets or sets the domain name.
            /// </summary>
            /// <value>The domain name.</value>
            public string Domain { get; set; }

            /// <summary>
            /// Gets or sets the domain count.
            /// </summary>
            /// <value>The domain count.</value>
            public int DomainCount { get; set; }
        }

        /// <summary>
        /// Class TopEmojies.
        /// </summary>
        public class TopEmojies
        {
            /// <summary>
            /// Gets or sets the emoji.
            /// </summary>
            /// <value>The emoji.</value>
            public EmojiBase Emoji { get; set; }

            /// <summary>
            /// Gets or sets the emoji count.
            /// </summary>
            /// <value>The emoji count.</value>
            public int EmojiCount { get; set; }
        }

        /// <summary>
        /// Class TopHashtags.
        /// </summary>
        public class TopHashtags
        {
            /// <summary>
            /// Gets or sets the hashtag.
            /// </summary>
            /// <value>The hashtag.</value>
            public string Hashtag { get; set; }

            /// <summary>
            /// Gets or sets the hashtag count.
            /// </summary>
            /// <value>The hashtag count.</value>
            public int HashtagCount { get; set; }
        }
    }
}