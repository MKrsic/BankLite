using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("UserData")]
    public class UserData
    {
        [Key]
        [ForeignKey("User")]
        public int User_ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Ime može sadržavati maksimalno 100 znakova.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Prezime može sadržavati maksimalno 100 znakova.")]
        public string LastName { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "OIB mora sadržavati točno 11 znakova.", MinimumLength = 11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "OIB može sadržavati samo znamenke od 0 do 9.")]
        public string OIB { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
