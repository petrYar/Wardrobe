using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WardobeLibrary.Models
{
    [Table("tblClothesDB")]
    public class ClothesDB
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        [ForeignKey("AccountOf")]
        public int IdAccount { get; set; }
        [ForeignKey("CategoryOf")]
        public int IdCategory { get; set; }

        public virtual AccountDB AccountOf { get; set; }
        public virtual CategoriesDB CategoryOf { get; set; }
    }
}
