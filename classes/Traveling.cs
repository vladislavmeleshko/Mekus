using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mekus.classes
{
    public class Traveling
    {
        public static string str_connect = @"Data Source=.\SQLEXPRESS;Database=Mekus;AttachDbFilename=|DataDirectory|\Mekus.mdf;Integrated Security=True;Connect Timeout=30";
        public int id { get; set; }
        public int number { get; set; }
        public DateTime date_traveling { get; set; }
        public Car id_car { get; set; }
        public Courier id_courier { get; set; }
        public Gasstation id_gasstation { get; set; }
        public List<Gasstation> listGasstations { get; set; }
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

        public Traveling(int id, int number, DateTime date_traveling, Car id_car, Courier id_courier, Gasstation id_gasstation, int s_probeg_1, int e_probeg_1, int t_probeg_1, int s_probeg_2, int e_probeg_2, int t_probeg_2, int t_probeg_all, decimal s_gas_1, decimal e_gas_1, decimal t_gas_1, decimal r_gas_1, decimal z_gas_1, decimal p_gas_1, decimal s_gas_2, decimal e_gas_2, decimal t_gas_2, decimal r_gas_2, decimal z_gas_2, decimal p_gas_2, decimal t_gas_all, decimal p_traveling_1, decimal p_traveling_2, decimal p_traveling_all, int status_traveling, int status_inRf, List<Gasstation> list)
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
            this.listGasstations = list;
        }

        public void createTraveling(int status_inRf)
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("insert into Travelings (number, date_traveling, id_courier, id_car, id_gasstation, status_inRf) values ({0}, '{1}', {2}, {3}, {4}, {5})",
                                                    number, date_traveling.Date, id_courier.id, id_car.id, id_gasstation.id, status_inRf);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    connect.Close();
                }
            }
        }

        public int editTypeTraveling(int typeTraveling)
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("update Travelings set status_inRf = {0} where id = {1}", typeTraveling, id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                    return 1;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    connect.Close();
                    return 0;
                }
            }
        }
        public void editCar()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("update Travelings set id_car={0}, id_gasstation={1} where id={2}", id_car.id, id_gasstation.id, id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    connect.Close();
                }
            }
        }

        public void editGasstation(int id_gasstations, int id_car, DateTime date)
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("update Travelings set id_gasstation={0}, " +
                                                    "s_probeg_1=0, e_probeg_1=0, t_probeg_1=0, s_probeg_2=0, e_probeg_2=0, t_probeg_2=0, t_probeg_all=0, " +
                                                    "s_gas_1=0.00, e_gas_1=0.00, t_gas_1=0.00, r_gas_1=0.00, z_gas_1=0.00, p_gas_1=0.00, " +
                                                    "s_gas_2=0.00, e_gas_2=0.00, t_gas_2=0.00, r_gas_2=0.00, z_gas_2=0.00, p_gas_2=0.00, t_gas_all=0.00, " +
                                                    "p_traveling_1=0.00, p_traveling_2=0.00, p_traveling_all=0.00, status_traveling=0 " +
                                                    "where id_car = {1} and date_traveling >= '{2}'", id_gasstations, id_car, date.Date);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    connect.Close();
                }
            }
        }

        public void editCar(DateTime date, int id_car)
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    int probeg = 0;
                    decimal gas = 0.00m;
                    string query = string.Format("select top 1 e_gas_1, e_gas_2, e_probeg_1, e_probeg_2 from Travelings where id_car={0} and date_traveling < '{1}' order by date_traveling desc", id_car, date.Date);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if((decimal)reader[1] != 0.00m)
                            gas = (decimal)reader[1];
                        else gas = (decimal)reader[0];
                        if ((int)reader[3] != 0)
                            probeg = (int)reader[3];
                        else probeg = (int)reader[2];
                    }
                    reader.Close();
                    query = string.Format("update Cars set probeg={0}, gas={1} where id={2}", probeg, gas.ToString().Replace(",", "."), id_car);
                    cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    connect.Close();
                }
            }
        }

        public void closeTraveling()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string list = "";
                    for (int i = 0; i < listGasstations.Count; i++)
                    {
                        if (i != listGasstations.Count - 1)
                            list += listGasstations[i].id + ",";
                        else list += listGasstations[i].id;
                    }
                    string query = string.Format("update Travelings set s_probeg_1={0}, e_probeg_1={1}, t_probeg_1={2}, s_probeg_2={3}, e_probeg_2={4}, t_probeg_2={5}, t_probeg_all={6}," +
                                                    "s_gas_1=@s_gas_1, e_gas_1=@e_gas_1, t_gas_1=@t_gas_1, r_gas_1=@r_gas_1, z_gas_1=@z_gas_1, p_gas_1=@p_gas_1," +
                                                    "s_gas_2=@s_gas_2, e_gas_2=@e_gas_2, t_gas_2=@t_gas_2, r_gas_2=@r_gas_2, z_gas_2=@z_gas_2, p_gas_2=@p_gas_2," +
                                                    "t_gas_all=@t_gas_all, p_traveling_1=@p_traveling_1, p_traveling_2=@p_traveling_2, p_traveling_all=@p_traveling_all," +
                                                    "status_traveling=1, id_car={7}, id_gasstation={8}, list_gasstations='{9}' where id={10}", s_probeg_1, 
                                                    e_probeg_1, t_probeg_1, s_probeg_2, e_probeg_2, t_probeg_2, t_probeg_all, id_car.id, id_gasstation.id,
                                                    list, id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@s_gas_1", S_gas_1);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@e_gas_1", E_gas_1);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@t_gas_1", T_gas_1);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@r_gas_1", R_gas_1);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@z_gas_1", Z_gas_1);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@p_gas_1", P_gas_1);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@s_gas_2", S_gas_2);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@e_gas_2", E_gas_2);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@t_gas_2", T_gas_2);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@r_gas_2", R_gas_2);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@z_gas_2", Z_gas_2);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@p_gas_2", P_gas_2);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@t_gas_all", T_gas_all);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@p_traveling_1", P_traveling_1);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@p_traveling_2", P_traveling_2);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@p_traveling_all", P_traveling_all);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    connect.Close();
                }
            }
        }
    }
}
