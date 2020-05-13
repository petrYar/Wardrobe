using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.Models
{
    public class EFContext : DbContext
    {
        public EFContext() : base("DbConnection")
        {

        }

        public DbSet<ClothesDB> Clothes { get; set; }
        public AccountDB Account { get; set; }
        public WeatherDB Weather { get; set; }
    }
}
