using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace JackHenryTwitter.Models
{
    public partial class GetDataFromTwitter : IGetDataFromTwitter
    {
        DataSource datasource = new DataSource();
        TwitterClient twitterCredentials;
        public TweetDownloadProperties TweetDownloadProperties { get; set; }
        public GetDataFromTwitter(int secondsToRun, DataSource passedDataSource, TwitterClient passedTwitterCredentials)
        {
            this.TweetDownloadProperties = new TweetDownloadProperties();
            this.TweetDownloadProperties.TimeWantedToDownloadTweets = secondsToRun;
            this.datasource = passedDataSource;
            this.twitterCredentials = passedTwitterCredentials;
        }


        public async Task<bool> GetSampleTweetsFromTwitter()
        {
            this.TweetDownloadProperties.StartTimeForDownload = DateTime.Now;
            this.TweetDownloadProperties.IsFinishedLoadingTweets = false;
            var timer = Stopwatch.StartNew();
            var sampleStream = twitterCredentials.StreamsV2.CreateSampleStream();
            try
            {
                sampleStream.TweetReceived += (sender, eventArgs) =>
                {
                    var json = eventArgs.Json;
                    this.datasource.AddStreamingTweetToTempDataset(json);
                    if (timer.ElapsedMilliseconds >= this.TweetDownloadProperties.TimeWantedToDownloadTweets*1000)
                    {
                        sampleStream.StopStream();
                    }
                };
                await sampleStream.StartAsync();
                return HandleTweetDownloadCompletion();
            }
            catch
            {
                // we have received all the records twitter will send us.
                // since we aren't getting any more we write what we already have to the data set.
                sampleStream.StopStream();
                return HandleTweetDownloadCompletion();
            }
        }

        public bool HandleTweetDownloadCompletion()
        {
            this.TweetDownloadProperties.EndTimeForDownload = DateTime.Now;
            TimeSpan span = (this.TweetDownloadProperties.EndTimeForDownload.Subtract(this.TweetDownloadProperties.StartTimeForDownload));
            this.TweetDownloadProperties.TimeSpentDownloading = span.Seconds;
            this.TweetDownloadProperties.IsFinishedLoadingTweets = true;
            return this.datasource.WriteTweetStatsToDataSet(this.TweetDownloadProperties);
        }
    }
}