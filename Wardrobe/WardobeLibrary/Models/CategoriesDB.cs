using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WardobeLibrary.Models
{
    [Table("tblCategoriesDB")]
    public class CategoriesDB
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double TemperatureMin { get; set; }

        public double TemperatureMax { get; set; }

        [ForeignKey("ClothesOf")]
        public int IdClothes { get; set; }


        public virtual ICollection<ClothesDB> ClothesOf { get; set; }
    }
}
