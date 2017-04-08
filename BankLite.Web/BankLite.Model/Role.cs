using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("Role")]
    public class Role : EntityBase
    {
        [Required]
        [StringLength(50, ErrorMessage = "Naziv može sadržavati maksimalno 50 znakova.")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
