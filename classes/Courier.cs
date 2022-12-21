using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.classes
{
    public class Courier
    {
        public int id { get; set; }
        public string courier { get; set; }
        public string prava { get; set; }
        public Car id_car { get; set; }

        public Courier()
        {

        }

        public Courier(int id, string courier, string prava, Car id_car)
        {
            this.id = id;
            this.courier = courier;
            this.prava = prava;
            this.id_car = id_car;
        }

        public Courier(string courier, string prava, Car id_car)
        {
            this.courier = courier;
            this.prava = prava;
            this.id_car = id_car;
        }
    }
}
