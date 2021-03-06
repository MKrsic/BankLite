﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("ExchangeRate")]
    public class ExchangeRate : EntityBase
    {
        [Required]
        [ForeignKey("Currency")]
        public int Currency_ID { get; set; }
        [Required]
        [Display(Name ="Vrijednost")]
        public decimal Value { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
