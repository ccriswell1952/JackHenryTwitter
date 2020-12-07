// ***********************************************************************
// Assembly         : JackHenryTwitter
// Author           : Chuck
// Created          : 12-05-2020
//
// Last Modified By : Chuck
// Last Modified On : 12-05-2020
// ***********************************************************************
// <copyright file="ExtensionMethods.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JackHenryTwitter.Utilities
{
    /// <summary>
    /// Class ExtensionMethods.
    /// </summary>
    public static partial class ExtensionMethods
    {
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