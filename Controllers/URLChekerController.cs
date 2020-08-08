using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BrightsTestTask.Models;
using BrightsTestTask.URLInfo;
using Microsoft.AspNetCore.Mvc;

namespace BrightsTestTask.Controllers
{
    public class URLChekerController : ControllerBase
    {
        private StatisticContext db;
        public URLChekerController(StatisticContext context):base()
        {
            db = context;
        }
        //[HttpGet("{urlsString}"), Route("GetStatistic")]
        public async Task<ActionResult<string>> GetStatisticAsync(string urlsString)
        {
            Parser parser = new Parser();
            List<string> BadUrl = new List<string>();
            var urlsList = parser.GetUrlList(urlsString,out BadUrl);
            UrlChecker urlChecker = new UrlChecker();
            List<Statistic> url_statistic = new List<Statistic>();

            foreach (var urlString in urlsList)
            {
                var url  = db.GetUrl(urlString, true);
                if (url != null)
                {
                    url_statistic.Add(await urlChecker.GetUrlInfoAsync(url));
                    
                }
            }
            await db.Statistics.AddRangeAsync(url_statistic);
            db.SaveChangesAsync();
            return Newtonsoft.Json.JsonConvert.SerializeObject(url_statistic);
        }
    }
}