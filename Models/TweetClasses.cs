// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-05-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="TweetClasses.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary>Usage:      Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class Attachments.
    /// </summary>
    public class Attachments
    {
        /// <summary>
        /// Gets or sets the media keys.
        /// </summary>
        /// <value>The media keys.</value>
        public List<string> media_keys { get; set; }
    }

    /// <summary>
    /// Class Data.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>The attachments.</value>
        public Attachments attachments { get; set; }

        /// <summary>
        /// Gets or sets the author identifier.
        /// </summary>
        /// <value>The author identifier.</value>
        public string author_id { get; set; }

        /// <summary>
        /// Gets or sets the conversation identifier.
        /// </summary>
        /// <value>The conversation identifier.</value>
        public string conversation_id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime created_at { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public Entities entities { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string lang { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [possibly sensitive].
        /// </summary>
        /// <value><c>true</c> if [possibly sensitive]; otherwise, <c>false</c>.</value>
        public bool possibly_sensitive { get; set; }

        /// <summary>
        /// Gets or sets the public metrics.
        /// </summary>
        /// <value>The public metrics.</value>
        public PublicMetrics public_metrics { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string source { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string text { get; set; }
    }

    /// <summary>
    /// Class Description.
    /// </summary>
    public class Description
    {
        /// <summary>
        /// Gets or sets the mentions.
        /// </summary>
        /// <value>The mentions.</value>
        public List<Mention> mentions { get; set; }
    }

    /// <summary>
    /// Class Entities.
    /// </summary>
    public class Entities
    {
        /// <summary>
        /// Gets or sets the urls.
        /// </summary>
        /// <value>The urls.</value>
        public List<Url> urls { get; set; }
    }

    /// <summary>
    /// Class Entities2.
    /// </summary>
    public class Entities2
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public Description description { get; set; }
    }

    /// <summary>
    /// Class Includes.
    /// </summary>
    public class Includes
    {
        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public List<Medium> media { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public List<User> users { get; set; }
    }

    /// <summary>
    /// Class Medium.
    /// </summary>
    public class Medium
    {
        /// <summary>
        /// Gets or sets the duration ms.
        /// </summary>
        /// <value>The duration ms.</value>
        public int duration_ms { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int height { get; set; }

        /// <summary>
        /// Gets or sets the media key.
        /// </summary>
        /// <value>The media key.</value>
        public string media_key { get; set; }

        /// <summary>
        /// Gets or sets the preview image URL.
        /// </summary>
        /// <value>The preview image URL.</value>
        public string preview_image_url { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int width { get; set; }
    }

    /// <summary>
    /// Class Mention.
    /// </summary>
    public class Mention
    {
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public int end { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public int start { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string username { get; set; }
    }

    /// <summary>
    /// Class PublicMetrics.
    /// </summary>
    public class PublicMetrics
    {
        /// <summary>
        /// Gets or sets the like count.
        /// </summary>
        /// <value>The like count.</value>
        public int like_count { get; set; }

        /// <summary>
        /// Gets or sets the quote count.
        /// </summary>
        /// <value>The quote count.</value>
        public int quote_count { get; set; }

        /// <summary>
        /// Gets or sets the reply count.
        /// </summary>
        /// <value>The reply count.</value>
        public int reply_count { get; set; }

        /// <summary>
        /// Gets or sets the retweet count.
        /// </summary>
        /// <value>The retweet count.</value>
        public int retweet_count { get; set; }
    }

    /// <summary>
    /// Class PublicMetricsUsers.
    /// </summary>
    public class PublicMetricsUsers
    {
        /// <summary>
        /// Gets or sets the followers count.
        /// </summary>
        /// <value>The followers count.</value>
        public int followers_count { get; set; }

        /// <summary>
        /// Gets or sets the following count.
        /// </summary>
        /// <value>The following count.</value>
        public int following_count { get; set; }

        /// <summary>
        /// Gets or sets the listed count.
        /// </summary>
        /// <value>The listed count.</value>
        public int listed_count { get; set; }

        /// <summary>
        /// Gets or sets the tweet count.
        /// </summary>
        /// <value>The tweet count.</value>
        public int tweet_count { get; set; }
    }

    /// <summary>
    /// Class Root.
    /// </summary>
    public class Root
    {
        /// <summary>
        /// Gets or sets the tweet data.
        /// </summary>
        /// <value>The tweet data.</value>
        public TweetData TweetData { get; set; }
    }

    /// <summary>
    /// Class Tweet.
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public Data data { get; set; }

        /// <summary>
        /// Gets or sets the includes.
        /// </summary>
        /// <value>The includes.</value>
        public Includes includes { get; set; }
    }

    /// <summary>
    /// Class TweetData.
    /// </summary>
    public class TweetData
    {
        /// <summary>
        /// Gets or sets the tweet.
        /// </summary>
        /// <value>The tweet.</value>
        public List<Tweet> Tweets { get; set; }
    }

    /// <summary>
    /// Class Url.
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Gets or sets the display URL.
        /// </summary>
        /// <value>The display URL.</value>
        public string display_url { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public int end { get; set; }

        /// <summary>
        /// Gets or sets the expanded URL.
        /// </summary>
        /// <value>The expanded URL.</value>
        public string expanded_url { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public int start { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string url { get; set; }
    }

    /// <summary>
    /// Class User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User" /> is protected.
        /// </summary>
        /// <value><c>true</c> if protected; otherwise, <c>false</c>.</value>
        public bool @protected { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime created_at { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string description { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public Entities2 entities { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the pinned tweet identifier.
        /// </summary>
        /// <value>The pinned tweet identifier.</value>
        public string pinned_tweet_id { get; set; }

        /// <summary>
        /// Gets or sets the profile image URL.
        /// </summary>
        /// <value>The profile image URL.</value>
        public string profile_image_url { get; set; }

        /// <summary>
        /// Gets or sets the public metrics.
        /// </summary>
        /// <value>The public metrics.</value>
        public PublicMetricsUsers public_metrics { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string url { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string username { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User" /> is verified.
        /// </summary>
        /// <value><c>true</c> if verified; otherwise, <c>false</c>.</value>
        public bool verified { get; set; }
    }
}