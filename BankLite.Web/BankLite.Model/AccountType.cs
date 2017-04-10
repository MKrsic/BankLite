using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("AccountType")]
    public class AccountType : EntityBase
    {
        [Required]
        [StringLength(100, ErrorMessage = "Tip računa može sadržavati maksimalno 100 znakova.")]
        [Display(Name = " Tip računa")]
        public string Type { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
