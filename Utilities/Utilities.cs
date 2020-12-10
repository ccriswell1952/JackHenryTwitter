// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-05-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="Utilities.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using JackHenryTwitter.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace JackHenryTwitter.Utilities
{
    /// <summary>
    /// Class Utilities.
    /// </summary>
    public static partial class Utilities
    {
        /// <summary>
        /// Gets the deserialized file json data.
        /// </summary>
        /// <returns>Root.</returns>
        public static Root GetDeserializedFileJsonData()
        {
            string combinedPath = GetTweetJsonFilePath(false);
            string fileContent = "";
            using (FileStream stream = System.IO.File.Open(combinedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        fileContent += reader.ReadToEnd();
                    }
                }
            }
            //dynamic array = JsonConvert.DeserializeObject(fileContent);
            Root deserializedClass = JsonConvert.DeserializeObject<Root>(fileContent);
            return deserializedClass;
        }

        /// <summary>
        /// Gets the deserialized file json statisitcs data.
        /// </summary>
        /// <returns>TweetStats.</returns>
        public static TweetStats GetDeserializedFileJsonStatisitcsData()
        {
            TweetStats deserializedClass = new TweetStats();
            string combinedPath = GetTweetJsonFilePath(true);
            if (File.Exists(combinedPath))
            {
                string fileContent = "";
                using (FileStream stream = System.IO.File.Open(combinedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            fileContent += reader.ReadToEnd();
                        }
                    }
                }
                deserializedClass = JsonConvert.DeserializeObject<TweetStats>(fileContent);
                deserializedClass.TopUrlDomainList = deserializedClass.TopUrlDomainList.Distinct().OrderByDescending(o => o.DomainCount).ThenBy(t => t.Domain).Take(100).ToList();
                deserializedClass.TopHashtagList = deserializedClass.TopHashtagList.Distinct().OrderByDescending(o => o.HashtagCount).ThenBy(t => t.Hashtag).Take(100).ToList();
                deserializedClass.TopEmojis = deserializedClass.TopEmojis.Distinct().OrderByDescending(o => o.EmojiCount).Take(100).ToList();
            }

            return deserializedClass;
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>System.String.</returns>
        public static string GetFilePath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        /// <summary>
        /// Gets the host from URL string.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>System.String.</returns>
        public static string GetHostFromUrlString(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return String.Empty;
            }
            if (url.Contains(@"://"))
            {
                url = url.Split(new string[] { "://" }, 2, StringSplitOptions.None)[1];
            }
            return url.Split('/')[0];
        }

        /// <summary>
        /// Gets the tweeter json file path.
        /// </summary>
        /// <param name="isStatsFile">true if writing to the Tweet Statistics file else false if writing to the data file</param>
        /// <returns>System.String.</returns>
        public static string GetTweetJsonFilePath(bool isStatsFile)
        {
            string relativePath = isStatsFile ? ConfigurationManager.AppSettings["TweetStatsJsonFilePath"] : ConfigurationManager.AppSettings["TweetJsonFilePath"];
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }
        /// <summary>
        /// Gets the tweet stream from twitter.
        /// </summary>
        /// <param name="secondsToRun">The seconds to run.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static async Task<bool> GetTweetStreamFromTwitter(int secondsToRun)
        {
            var appVarDataSrc = new AppVariableDataSource(secondsToRun);
            return await appVarDataSrc.GetSampleTweetsFromTwitter();
        }

        /// <summary>
        /// Gets the twitter credentials.
        /// </summary>
        /// <returns>ReadOnlyTwitterCredentials.</returns>
        public static ReadOnlyTwitterCredentials GetTwitterCredentials()
        {
            ReadOnlyConsumerCredentials basicCredentials =
                new ReadOnlyConsumerCredentials(ConfigurationManager.AppSettings["CONSUMER_KEY"],
                ConfigurationManager.AppSettings["CONSUMER_SECRET"],
                ConfigurationManager.AppSettings["BEARER_TOKEN"]);
            return new ReadOnlyTwitterCredentials(basicCredentials);
        }
    }
}