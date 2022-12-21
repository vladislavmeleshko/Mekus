using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.classes
{
    public class Gasstation
    {
        public int id { get; set; }
        public decimal Enter_gas { get => decimal.Round(enter_gas, 2, MidpointRounding.AwayFromZero); set => enter_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Really_gas { get => decimal.Round(really_gas, 2, MidpointRounding.AwayFromZero); set => really_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Price { get => decimal.Round(price, 2, MidpointRounding.AwayFromZero); set => price = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }

        private decimal enter_gas;

        private decimal really_gas;

        private decimal price;
        public Car id_car { get; set; }

        public Gasstation()
        {

        }

        public Gasstation(int id, decimal enter_gas, decimal really_gas, decimal price, Car id_car)
        {
            this.id = id;
            Enter_gas = enter_gas;
            Really_gas = really_gas;
            Price = price;
            this.id_car = id_car;
        }

        public Gasstation(decimal enter_gas, decimal really_gas, decimal price, Car id_car)
        {
            Enter_gas = enter_gas;
            Really_gas = really_gas;
            Price = price;
            this.id_car = id_car;
        }
    }
}
