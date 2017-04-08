using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    public abstract class EntityBase 
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
    }
}
