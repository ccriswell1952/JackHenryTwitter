// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-05-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="ExtensionMethods.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace JackHenryTwitter.Utilities
{
    /// <summary>
    /// Class ExtensionMethods.
    /// </summary>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Adds an item to a dictionary only if there isn't an already existing key.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddSafe(this Dictionary<object, object> dictionary, object key, object value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
        }

        /// <summary>
        /// Replaces the first instance of a string with another.
        /// </summary>
        /// <param name="text">The text you want to search and replace.</param>
        /// <param name="searchFor">The string to search for.</param>
        /// <param name="replaceWith">The string  that will be used to replace with.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceFirst(this string text, string searchFor, string replaceWith)
        {
            int pos = text.IndexOf(searchFor);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replaceWith + text.Substring(pos + searchFor.Length);
        }

        /// <summary>
        /// Replaces the last instance of a string with another.
        /// </summary>
        /// <param name="text">The text you want to search and replace.</param>
        /// <param name="searchFor">The string to search for.</param>
        /// <param name="replaceWith">The string  that will be used to replace with.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceLast(this string text, string searchFor, string replaceWith)
        {
            int pos = text.LastIndexOf(searchFor);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replaceWith + text.Substring(pos + searchFor.Length);
        }
    }
}