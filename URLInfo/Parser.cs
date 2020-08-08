using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BrightsTestTask.URLInfo
{
    public class Parser
    {
        /// <summary>
        /// Get url list from string
        /// </summary>
        /// <param name="urlsString">string with urls</param>
        /// <param name="badUrl">ulr separator</param>
        /// <param name="separator">list of not url in input string</param>
        /// <returns></returns>
        public List<string> GetUrlList(string urlsString,out List<string> badUrl, string separator = "\n")
        {
            List<string> rez = new List<string>();
            badUrl = new List<string>();
            if (urlsString == null || urlsString == string.Empty)
                return rez;
            string[] result = Regex.Split(urlsString, separator,
                                   RegexOptions.IgnoreCase,
                                   TimeSpan.FromMilliseconds(500));
            foreach(var urlstring in result)
            {
                if(IsValidURL(urlstring))
                {
                    if (urlstring.Contains("http")) {
                        rez.Add(urlstring);
                    }
                    else
                    {
                        rez.Add("http://"+urlstring);
                        rez.Add("https://" + urlstring);
                    }
                }
                else
                {
                    badUrl.Add(urlstring);
                }
                
            }
            return rez;
        }
        bool IsValidURL(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }
    }
}
