using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("User")]
    public class User : EntityBase
    {
        [Required]
        [StringLength(100, ErrorMessage = "Korisničko ime može sadržavati maksimalno 100 znakova.")]
        public string UserName { get; set; }
        [ForeignKey("Role")]
        public int Role_ID { get; set; }
        [Required]
        public byte PasswordHash { get; set; }
        [Required]
        public bool Active { get; set; }

        public virtual Role Role { get; set; }

        public virtual UserData UserData { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
