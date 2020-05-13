using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WardobeLibrary.WCFClasses.Classes.ResponseFromWeatherSite;

namespace WardobeLibrary.Models
{
    [Table("tblWeatherDB")]
    public class WeatherDB
    {
        [Key]
        public int Id { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public DateTime TimeOfInfo { get; set; }

        public Daily[] Weather { get; set; }
    }
}
