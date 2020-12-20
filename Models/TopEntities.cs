// *********************************************************************** Assembly :
// JackHenryTwitter Author : charl Created : 12-18-2020
//
// Last Modified By : charl Last Modified On : 12-18-2020 ***********************************************************************
// <copyright file="ITopEntities.cs" company="HP Inc.">
//     Copyright © 2020
// </copyright>
// <summary>
// </summary>
// ***********************************************************************

namespace JackHenryTwitter.Models
{
    public partial class KeyValuePair<TKey, TValue>
    {
        #region Public Properties

        public TValue Count { get; set; }
        public TKey Value { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// class TopEntities
    /// </summary>
    public partial class TopEntities
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        #endregion Public Properties
    }
}