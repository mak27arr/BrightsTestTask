using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BrightsTestTask.URLInfo
{
    public class Validator
    {
        /// <summary>
        /// Get url list from string
        /// </summary>
        /// <param name="url">url for cheak</param>
        /// <returns>If its url retur url else return null</returns>
        public string CheakURL(string url)
        {
            if (IsValidURL(url))
            {
                if (url.Contains("http"))
                {
                    return url;
                }
                else
                {
                    return "http://" + url;
                }
            }
            else
            {
                return null;
            }
        }


        bool IsValidURL(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }
    }
}
