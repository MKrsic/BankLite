using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("Transaction")]
    public class Transaction : EntityBase
    {
        [Required]
        public int BankAccount_From { get; set; }
        [Required]
        public int BankAccount_To { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [ForeignKey("ExchangeRate")]
        public int ExchangeRate_ID { get; set; }

        public virtual ExchangeRate ExchangeRate { get; set; }
    }
}
