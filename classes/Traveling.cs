using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.classes
{
    public class Traveling
    {
        public int id { get; set; }
        public int number { get; set; }
        public DateTime date_traveling { get; set; }
        public Car id_car { get; set; }
        public Courier id_courier { get; set; }
        public Gasstation id_gasstation { get; set; }
        public int s_probeg_1 { get; set; }
        public int e_probeg_1 { get; set; }
        public int t_probeg_1 { get; set; }
        public int s_probeg_2 { get; set; }
        public int e_probeg_2 { get; set; }
        public int t_probeg_2 { get; set; }
        public int t_probeg_all { get; set; }
        public decimal S_gas_1 { get => decimal.Round(s_gas_1, 2, MidpointRounding.AwayFromZero); set => s_gas_1 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal E_gas_1 { get => decimal.Round(e_gas_1, 2, MidpointRounding.AwayFromZero); set => e_gas_1 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal T_gas_1 { get => decimal.Round(t_gas_1, 2, MidpointRounding.AwayFromZero); set => t_gas_1 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal R_gas_1 { get => decimal.Round(r_gas_1, 2, MidpointRounding.AwayFromZero); set => r_gas_1 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Z_gas_1 { get => decimal.Round(z_gas_1, 2, MidpointRounding.AwayFromZero); set => z_gas_1 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal P_gas_1 { get => decimal.Round(p_gas_1, 2, MidpointRounding.AwayFromZero); set => p_gas_1 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal S_gas_2 { get => decimal.Round(s_gas_2, 2, MidpointRounding.AwayFromZero); set => s_gas_2 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal E_gas_2 { get => decimal.Round(e_gas_2, 2, MidpointRounding.AwayFromZero); set => e_gas_2 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal T_gas_2 { get => decimal.Round(t_gas_2, 2, MidpointRounding.AwayFromZero); set => t_gas_2 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal R_gas_2 { get => decimal.Round(r_gas_2, 2, MidpointRounding.AwayFromZero); set => r_gas_2 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Z_gas_2 { get => decimal.Round(z_gas_2, 2, MidpointRounding.AwayFromZero); set => z_gas_2 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal P_gas_2 { get => decimal.Round(p_gas_2, 2, MidpointRounding.AwayFromZero); set => p_gas_2 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal T_gas_all { get => decimal.Round(t_gas_all, 2, MidpointRounding.AwayFromZero); set => t_gas_all = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal P_traveling_1 { get => decimal.Round(p_traveling_1, 2, MidpointRounding.AwayFromZero); set => p_traveling_1 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal P_traveling_2 { get => decimal.Round(p_traveling_2, 2, MidpointRounding.AwayFromZero); set => p_traveling_2 = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal P_traveling_all { get => decimal.Round(p_traveling_all, 2, MidpointRounding.AwayFromZero); set => p_traveling_all = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public int status_traveling { get; set; }
        public int status_inRf { get; set; }

        private decimal s_gas_1;
        private decimal e_gas_1;
        private decimal t_gas_1;
        private decimal r_gas_1;
        private decimal z_gas_1;
        private decimal p_gas_1;

        private decimal s_gas_2;
        private decimal e_gas_2;
        private decimal t_gas_2;
        private decimal r_gas_2;
        private decimal z_gas_2;
        private decimal p_gas_2;

        private decimal t_gas_all;

        private decimal p_traveling_1;
        private decimal p_traveling_2;
        private decimal p_traveling_all;

        public Traveling()
        {

        }

        public Traveling(int id, int number, DateTime date_traveling, Car id_car, Courier id_courier, Gasstation id_gasstation, int s_probeg_1, int e_probeg_1, int t_probeg_1, int s_probeg_2, int e_probeg_2, int t_probeg_2, int t_probeg_all, decimal s_gas_1, decimal e_gas_1, decimal t_gas_1, decimal r_gas_1, decimal z_gas_1, decimal p_gas_1, decimal s_gas_2, decimal e_gas_2, decimal t_gas_2, decimal r_gas_2, decimal z_gas_2, decimal p_gas_2, decimal t_gas_all, decimal p_traveling_1, decimal p_traveling_2, decimal p_traveling_all, int status_traveling, int status_inRf)
        {
            this.id = id;
            this.number = number;
            this.date_traveling = date_traveling;
            this.id_car = id_car;
            this.id_courier = id_courier;
            this.id_gasstation = id_gasstation;
            this.s_probeg_1 = s_probeg_1;
            this.e_probeg_1 = e_probeg_1;
            this.t_probeg_1 = t_probeg_1;
            this.s_probeg_2 = s_probeg_2;
            this.e_probeg_2 = e_probeg_2;
            this.t_probeg_2 = t_probeg_2;
            this.t_probeg_all = t_probeg_all;
            S_gas_1 = s_gas_1;
            E_gas_1 = e_gas_1;
            T_gas_1 = t_gas_1;
            R_gas_1 = r_gas_1;
            Z_gas_1 = z_gas_1;
            P_gas_1 = p_gas_1;
            S_gas_2 = s_gas_2;
            E_gas_2 = e_gas_2;
            T_gas_2 = t_gas_2;
            R_gas_2 = r_gas_2;
            Z_gas_2 = z_gas_2;
            P_gas_2 = p_gas_2;
            T_gas_all = t_gas_all;
            P_traveling_1 = p_traveling_1;
            P_traveling_2 = p_traveling_2;
            P_traveling_all = p_traveling_all;
            this.status_traveling = status_traveling;
            this.status_inRf = status_inRf;
        }
    }
}
