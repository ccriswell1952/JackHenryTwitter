using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace JackHenryTwitter.Utilities
{
    public static class EmojiParser
    {
        static readonly Dictionary<string, string> emojisAsColons;
        static readonly Regex regexColons;
        static EmojiParser()
        {
            string path = ConfigurationManager.AppSettings["EmojiJsonFilePath"];
            string filePath = Utilities.GetFilePath(path);
            // load mentioned json from somewhere
            var data = JArray.Parse(File.ReadAllText(filePath));
            emojisAsColons = data.OfType<JObject>().ToDictionary(
                // key dictionary by coloned short names
                c => ":" + ((JValue)c["short_name"]).Value.ToString() + ":",
                c => {
                    var unicodeRaw = ((JValue)c["unified"]).Value.ToString();
                    var chars = new List<char>();
                // some characters are multibyte in UTF32, split them
                foreach (var point in unicodeRaw.Split('-'))
                    {
                    // parse hex to 32-bit unsigned integer (UTF32)
                    uint unicodeInt = uint.Parse(point, System.Globalization.NumberStyles.HexNumber);
                    // convert to bytes and get chars with UTF32 encoding
                    chars.AddRange(Encoding.UTF32.GetChars(BitConverter.GetBytes(unicodeInt)));
                    }
                // this is resulting emoji
                return new string(chars.ToArray());
                });
            // build huge regex (all 1500 emojies combined) by join all names with OR ("|")
            regexColons = new Regex(String.Join("|", emojisAsColons.Keys.Select(Regex.Escape)));
        }

        public static string ReplaceColonNames(string input)
        {
            // replace match using dictionary
            return regexColons.Replace(input, match => emojisAsColons[match.Value]);
        }

        /*
byte[] utfBytes = System.Text.Encoding.UTF32.GetBytes("👱");
print(utfBytes.Length);
for (int i = 0; i < utfBytes.Length; i += 4)
{
     if (i != 0) result += '-';
     result += System.BitConverter.ToInt32(utfBytes, i).ToString("x2").ToUpper();
}         
* */
    }
}