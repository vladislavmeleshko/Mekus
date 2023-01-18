using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.nav
{
    public class JSONParser
    {
        public Root root { get; set; }

        public class Root
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public Item[] items { get; set; }
        }

        public class Item
        {
            public string object_id { get; set; }
            public string object_name { get; set; }
            public string object_uid { get; set; }
            public double distance_gps { get; set; }
            public int distance_can { get; set; }
            public int run_time { get; set; }
            public string run_time_str { get; set; }
            public int stop_time { get; set; }
            public string stop_time_str { get; set; }
            public int max_speed { get; set; }
            public float fuel_can { get; set; }
            public int fuel_dut { get; set; }
            public object fuel_flow { get; set; }
            public object[] fuel_diffs { get; set; }
            public Fuel_In_List[] fuel_in_list { get; set; }
            public object[] fuel_card_in { get; set; }
            public int fuel_dut_start { get; set; }
            public int fuel_dut_finish { get; set; }
            public int odom_start { get; set; }
            public int odom_finish { get; set; }
            public float avg_speed_gps { get; set; }
            public float avg_speed_can { get; set; }
        }
    }

    public class Fuel_In_List
    {
        public string dt { get; set; }
        public int value { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public object address { get; set; }
        public object countryCode { get; set; }
    }
}
