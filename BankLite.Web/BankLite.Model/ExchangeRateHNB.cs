using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Model
{
    public class ExchangeRateHNB
    {
        public double median_rate { get; set; }
        public string unit_value { get; set; }
        public double selling_rate { get; set; }
        public string currency_code { get; set; }
        public double buying_rate { get; set; }
    }
}
