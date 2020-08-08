using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightsTestTask.Models
{
    public class Url
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UrlName { get; set; }
        [JsonIgnore]
        public List<Statistic> Statistics { get; set; }
    }
}
