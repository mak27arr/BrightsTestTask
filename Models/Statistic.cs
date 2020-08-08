using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace BrightsTestTask.Models
{
    public class Statistic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(Url))]
        public int UrlId { get; set; } //site id
        public Url Url { get; set; } // site url
        public DateTime RequestDate { get; set; } // request time
        public HttpStatusCode ResponseCode { get; set; } //http code
        public string Title { get; set; } // response title
    }
}
