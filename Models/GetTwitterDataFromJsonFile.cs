// *********************************************************************** Assembly :
// JackHenryTwitter Author : Chuck Created : 12-12-2020
//
// Last Modified By : Chuck Last Modified On : 12-12-2020 ***********************************************************************
// <copyright file="GetData.cs" company="">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class GetData.
    /// </summary>
    public partial class GetTwitterDataFromJsonFile : IGetTwitterData
    {
        #region Private Fields

        private static ReaderWriterLockSlim readJsonLock = new ReaderWriterLockSlim();
        private static ReaderWriterLockSlim readStatisticsLock = new ReaderWriterLockSlim();

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Gets the deserialized file json data.
        /// </summary>
        /// <returns>Root.</returns>
        public Root GetTweeterRootData()
        {
            string filePath = Utilities.GetTwitterDetails.GetTweetJsonFilePath(false);
            string fileContent = "";
            if (File.Exists(filePath))
            {
                readJsonLock.EnterReadLock();
                using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            fileContent += reader.ReadToEnd();
                        }
                    }
                }
                readJsonLock.ExitReadLock();
                return JsonConvert.DeserializeObject<Root>(fileContent);
            }
            else return null;
        }

        /// <summary>
        /// Gets the deserialized file json statisitcs data.
        /// </summary>
        /// <returns>TweetStats.</returns>
        public TweetStats GetTwitterStatisitcsData()
        {
            TweetStats deserializedClass = new TweetStats();
            string filePath = Utilities.GetTwitterDetails.GetTweetJsonFilePath(true);

            if (File.Exists(filePath))
            {
                string fileContent = "";
                readStatisticsLock.EnterReadLock();
                using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            fileContent += reader.ReadToEnd();
                        }
                    }
                }
                readStatisticsLock.ExitReadLock();
                deserializedClass = JsonConvert.DeserializeObject<TweetStats>(fileContent);
                deserializedClass.TopUrlDomainList = deserializedClass.TopUrlDomainList.Distinct().OrderByDescending(o => o.Count).ThenBy(t => t.Value).Take(100).ToList();
                deserializedClass.TopHashtagList = deserializedClass.TopHashtagList.Distinct().OrderByDescending(o => o.Count).ThenBy(t => t.Value).Take(100).ToList();
                deserializedClass.TopEmojisList = deserializedClass.TopEmojisList.Distinct().OrderByDescending(o => o.EmojiCount).Take(100).ToList();
            }

            return deserializedClass;
        }

        #endregion Public Methods
    }
}