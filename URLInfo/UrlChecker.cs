using BrightsTestTask.Models;
using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BrightsTestTask.URLInfo
{
    public class UrlChecker
    {
        public async Task<Statistic> GetUrlInfoAsync(Url url)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(url.UrlName);
            var rezalt = new Statistic();
            try
            {
                rezalt.Url = url;
                rezalt.RequestDate = DateTime.Now;
                using (var response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader streadReader = new StreamReader(response.GetResponseStream());
                    var htmlContent = streadReader.ReadToEnd();
                    rezalt.ResponseCode = response.StatusCode;
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);
                    var title_node = doc.DocumentNode.SelectSingleNode("//title"); ;
                    if (title_node == null)
                    {
                        rezalt.Title = "Can`t find title";
                    }
                    else
                    {
                        rezalt.Title = title_node.InnerText;
                    }
                }
                return rezalt;
            }
            catch (WebException ex)
            {
                rezalt.ResponseCode = HttpStatusCode.BadRequest;
                return rezalt;
            }
            catch(Exception ex)
            {
                rezalt.ResponseCode = HttpStatusCode.BadRequest;
                return rezalt;
            }
        }
    }
}
