// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-08-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="EmojiBase.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Class EmojiBase.
    /// </summary>
    public partial class EmojiBase
    {
        /// <summary>
        /// Gets or sets the emoji HTML encode.
        /// </summary>
        /// <value>The emoji HTML encode.</value>
        public string EmojiHtmlEncode { get; set; }

        /// <summary>
        /// Gets or sets the emoji image.
        /// </summary>
        /// <value>The emoji image.</value>
        public string EmojiImage { get; set; }
    }
}