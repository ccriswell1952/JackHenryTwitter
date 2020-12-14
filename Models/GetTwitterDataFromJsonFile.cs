// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-12-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-12-2020
// ***********************************************************************
// <copyright file="GetData.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class GetData.
    /// </summary>
    public partial class GetTwitterDataFromJsonFile : IGetTwitterData
    {

        #region Public Methods

        /// <summary>
        /// Gets the deserialized file json data.
        /// </summary>
        /// <returns>Root.</returns>
        public Root GetTweeterRootData()
        {
            string combinedPath = Utilities.GetTwitterDetails.GetTweetJsonFilePath(false);
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
        public TweetStats GetTwitterStatisitcsData()
        {
            TweetStats deserializedClass = new TweetStats();
            string combinedPath = Utilities.GetTwitterDetails.GetTweetJsonFilePath(true);
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
                deserializedClass.TopEmojisList = deserializedClass.TopEmojisList.Distinct().OrderByDescending(o => o.EmojiCount).Take(100).ToList();
            }

            return deserializedClass;
        }

        #endregion Public Methods

    }
}