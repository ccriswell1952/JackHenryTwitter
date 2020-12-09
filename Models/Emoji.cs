// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-08-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-08-2020
// ***********************************************************************
// <copyright file="Emoji.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary>
// Emoji myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
//</summary>
// ***********************************************************************
using System.Collections.Generic;

namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class Emoji.
    /// </summary>
    public partial class Emoji: EmojiBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the unified.
        /// </summary>
        /// <value>The unified.</value>
        public string unified { get; set; }
        /// <summary>
        /// Gets or sets the non qualified.
        /// </summary>
        /// <value>The non qualified.</value>
        public string non_qualified { get; set; }
        /// <summary>
        /// Gets or sets the docomo.
        /// </summary>
        /// <value>The docomo.</value>
        public string docomo { get; set; }
        /// <summary>
        /// Gets or sets the au.
        /// </summary>
        /// <value>The au.</value>
        public string au { get; set; }
        /// <summary>
        /// Gets or sets the softbank.
        /// </summary>
        /// <value>The softbank.</value>
        public string softbank { get; set; }
        /// <summary>
        /// Gets or sets the google.
        /// </summary>
        /// <value>The google.</value>
        public string google { get; set; }
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public string image { get; set; }
        /// <summary>
        /// Gets or sets the sheet x.
        /// </summary>
        /// <value>The sheet x.</value>
        public int sheet_x { get; set; }
        /// <summary>
        /// Gets or sets the sheet y.
        /// </summary>
        /// <value>The sheet y.</value>
        public int sheet_y { get; set; }
        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public string short_name { get; set; }
        /// <summary>
        /// Gets or sets the short names.
        /// </summary>
        /// <value>The short names.</value>
        public List<string> short_names { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public object text { get; set; }
        /// <summary>
        /// Gets or sets the texts.
        /// </summary>
        /// <value>The texts.</value>
        public object texts { get; set; }
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string category { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        public int sort_order { get; set; }
        /// <summary>
        /// Gets or sets the added in.
        /// </summary>
        /// <value>The added in.</value>
        public string added_in { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has img apple.
        /// </summary>
        /// <value><c>true</c> if this instance has img apple; otherwise, <c>false</c>.</value>
        public bool has_img_apple { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has img google.
        /// </summary>
        /// <value><c>true</c> if this instance has img google; otherwise, <c>false</c>.</value>
        public bool has_img_google { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has img twitter.
        /// </summary>
        /// <value><c>true</c> if this instance has img twitter; otherwise, <c>false</c>.</value>
        public bool has_img_twitter { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has img facebook.
        /// </summary>
        /// <value><c>true</c> if this instance has img facebook; otherwise, <c>false</c>.</value>
        public bool has_img_facebook { get; set; }


    }
}