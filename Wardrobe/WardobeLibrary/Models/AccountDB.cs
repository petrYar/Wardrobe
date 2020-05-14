using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WardobeLibrary.Models
{
    [Table("tblAccountDB")]
    public class AccountDB
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Token { get; set; }

        //[ForeignKey("ClothesOf")]
        //public int IdClothes { get; set; }

        public virtual ICollection<ClothesDB> ClothesOf { get; set; }
    }
}
