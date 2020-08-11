using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace BrightsTestTask.Models
{
    public class StatisticContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public StatisticContext(DbContextOptions<StatisticContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Statistic>().Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Statistic>().HasOne(i => i.Url).WithMany(w => w.Statistics);
        }

        public Url GetUrl(string url,bool create_if_notset=true)
        {
            if (url == string.Empty)
                return null;
            var rezalt = Urls.Where(x => x.UrlName == url).FirstOrDefault();
            if(rezalt==null && create_if_notset)
            {
                rezalt = new Url();
                rezalt.UrlName = url;
                Urls.Add(rezalt);
                SaveChanges();
            }
            return rezalt;
        }
    }
}
