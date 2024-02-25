using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.classes
{
    public class TypeTraveling
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string prefix { get; set; }

        public TypeTraveling()
        {

        }

        public TypeTraveling(int id, string name, string prefix)
        {
            this.id = id;
            this.name = name;
            this.prefix = prefix;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
