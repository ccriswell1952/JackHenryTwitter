using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JackHenryTwitter.Models
{
    public partial class TweetStats
    {
        private Root tweetRoot = new Root();

        public TweetStats()
        {
        }

        public TweetStats(Root root)
        {
            this.tweetRoot = root;
        }

        public double AverageTweetsReceivedPerHour { get; set; }
        public double AverageTweetsReceivedPerMinute { get; set; }
        public double AverageTweetsReceivedPerSecond { get; set; }
        public decimal PctTweetsWithEmojis { get; set; }
        public decimal PctTweetsWithPhoto { get; set; }
        public decimal PctTweetsWithUrl { get; set; }
        public List<TopEmojies> TopEmojis { get; set; }
        public List<TopHashtags> TopHashtagList { get; set; }
        public List<TopDomains> TopUrlDomainList { get; set; }
        public int TotalDownloadTimeInMiliSeconds { get; set; }
        public double TotalEmojis { get; set; }
        public double TotalTweetsReceived { get; set; }
        public double TotalTweetsWithPhoto { get; set; }
        public double TotalUrlsInTweets { get; set; }

        public void SetAllTweetStatsProperties()
        {
            this.TotalTweetsReceived = tweetRoot.TweetData.Tweets.Count;
            SetAverageTimes();
            SetPctTweetsWithPhoto();
            SetPctTweetsWithEmoji(0);
            SetPctTweetsWithUrl();
            SetTopUrlDomains();
            SetTopHashtags();
        }

        public void SetAverageTimes()
        {
            var averageTweetsPerSecond = this.TotalTweetsReceived / (this.TotalDownloadTimeInMiliSeconds / 1000);
            this.AverageTweetsReceivedPerSecond = averageTweetsPerSecond;
            this.AverageTweetsReceivedPerMinute = averageTweetsPerSecond * 60;
            this.AverageTweetsReceivedPerHour = averageTweetsPerSecond * (60 * 60);
        }

        public void SetPctTweetsWithEmoji(int totalEmojis)
        {
            this.TotalEmojis = totalEmojis;
            if (this.TotalEmojis > 0)
            {
                this.PctTweetsWithEmojis = ((decimal)this.TotalEmojis / (decimal)this.TotalTweetsReceived) * 100;
            }
            else
            {
                this.PctTweetsWithEmojis = 0;
            }
        }

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

        public class TopDomains
        {
            public string Domain { get; set; }
            public int DomainCount { get; set; }
        }

        public class TopEmojies
        {
            public string Emoji { get; set; }
            public int EmojiCount { get; set; }
        }

        public class TopHashtags
        {
            public string Hashtag { get; set; }
            public int HashtagCount { get; set; }
        }
    }
}