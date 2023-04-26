using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.classes
{
    public class ReportGases
    {
        public int id { get; set; }
        public string car { get; set; }
        public string name_gas { get; set; }
        public decimal Price_gas { get => price_gas; set => price_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Amount_really_gas { get => amount_really_gas; set => amount_really_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Amount_enter_gas { get => amount_enter_gas; set => amount_enter_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Price_really_gas { get => price_really_gas; set => price_really_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Price_enter_gas { get => price_enter_gas; set => price_enter_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Price_really_gasNDS { get => price_really_gasNDS; set => price_really_gasNDS = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Price_enter_gasNDS { get => price_enter_gasNDS; set => price_enter_gasNDS = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal S_gas { get => s_gas; set => s_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal E_gas { get => e_gas; set => e_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }

        private decimal price_gas;
        private decimal amount_really_gas;
        private decimal amount_enter_gas;
        private decimal price_really_gas;
        private decimal price_enter_gas;
        private decimal price_really_gasNDS;
        private decimal price_enter_gasNDS;
        private decimal s_gas;
        private decimal e_gas;

        public ReportGases(string car, string name_gas, decimal price_gas, decimal amount_gas, decimal price_gasNDS, decimal price_gas_2, int n = 0)
        {
            if(n == 0)
            {
                this.car = car;
                this.name_gas = name_gas;
                Price_gas = price_gas;
                Amount_really_gas = amount_gas;
                Price_really_gasNDS = price_gasNDS;
                Price_really_gas = price_gas_2;
                Amount_enter_gas = 0.00m;
                Price_enter_gasNDS = 0.00m;
                Price_enter_gas = 0.00m;
                E_gas = 0.00m;
                S_gas = 0.00m;
    }
            else
            {
                this.car = car;
                this.name_gas = name_gas;
                Price_gas = price_gas;
                Amount_enter_gas = amount_gas;
                Price_enter_gasNDS = price_gasNDS;
                Price_enter_gas = price_gas_2;
                Amount_really_gas = 0.00m;
                Price_really_gasNDS = 0.00m;
                Price_really_gas = 0.00m;
                E_gas = 0.00m;
                S_gas = 0.00m;
            }
        }
    }
}
