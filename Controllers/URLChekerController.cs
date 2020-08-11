using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BrightsTestTask.Models;
using BrightsTestTask.URLInfo;
using Microsoft.AspNetCore.Mvc;

namespace BrightsTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URLChekerController : ControllerBase
    {
        private StatisticContext db;
        public URLChekerController(StatisticContext context):base()
        {
            db = context;
        }
        [HttpGet, Route("GetStatisticAsync")]
        public async Task<JsonResult> GetStatisticAsync(string urlsString)
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
            return new JsonResult(url_statistic);
        }
        [HttpGet, Route("GetStatisticSingleAsync")]
        public async Task<JsonResult> GetStatisticSingleAsync(string urlsString)
        {
            Validator validator = new Validator();
            List<string> BadUrl = new List<string>();
            var url_cheked = validator.CheakURL(urlsString);
            UrlChecker urlChecker = new UrlChecker();
            Statistic rezalt = new Statistic();
            if (url_cheked != null)
            {
                var url = db.GetUrl(url_cheked, true);
                rezalt = await urlChecker.GetUrlInfoAsync(url);
            }

            await db.Statistics.AddAsync(rezalt);
            db.SaveChangesAsync();
            return new JsonResult(rezalt);
        }
    }
}