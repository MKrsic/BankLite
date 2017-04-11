using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("BankAccount")]
    public class BankAccount : EntityBase
    {
        [Required]
        [ForeignKey("AccountType")]
        public int AccountType_ID { get; set; }
        [Required]
        [ForeignKey("User")]
        public int User_ID { get; set; }
        [Display(Name = "Iznos")]
        public decimal MoneyAmount { get; set; }

        public virtual AccountType AccountType { get; set; }
        public virtual User User { get; set; }
    }
}
