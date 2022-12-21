using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.classes
{
    public class Car
    {
        public int id { get; set; }
        public string car { get; set; }
        public int probeg { get; set; }
        public decimal Gas { get => decimal.Round(gas, 2, MidpointRounding.AwayFromZero); set => gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }

        private decimal gas;

        public Model id_model { get; set; }

        public Car()
        {

        }

        public Car(int id, string car, int probeg, decimal gas, Model id_model)
        {
            this.id = id;
            this.car = car;
            this.probeg = probeg;
            Gas = gas;
            this.id_model = id_model;
        }

        public Car(string car, int probeg, decimal gas, Model id_model)
        {
            this.car = car;
            this.probeg = probeg;
            Gas = gas;
            this.id_model = id_model;
        }
    }
}
