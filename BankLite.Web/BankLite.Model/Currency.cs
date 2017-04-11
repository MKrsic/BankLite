using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    [Table("Currency")]
    public class Currency : EntityBase
    {
        [Required]
        [StringLength(5, ErrorMessage = "Valuta može sadržavati maksimalno 5 znakova.")]
        [Display(Name = "Naziv")]
        public string Name { get; set; }

        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
    }
}
