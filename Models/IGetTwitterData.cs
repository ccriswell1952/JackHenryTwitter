// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-12-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-12-2020
// ***********************************************************************
// <copyright file="IGetData.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JackHenryTwitter.Models
{
    /// <summary>
    /// Interface IGetData
    /// </summary>
    public interface IGetTwitterData
    {
        #region Public Methods

        /// <summary>
        /// Gets the tweeter root data.
        /// </summary>
        /// <returns>Root.</returns>
        Root GetTweeterRootData();

        /// <summary>
        /// Gets the twitter statisitcs data.
        /// </summary>
        /// <returns>TweetStats.</returns>
        TweetStats GetTwitterStatisitcsData();

        #endregion Public Methods
    }
}