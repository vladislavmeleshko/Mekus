using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mekus.classes
{
    public class Database
    {
        public static string str_connect = @"Data Source=.\SQLEXPRESS;Database=Mekus;AttachDbFilename=|DataDirectory|\Mekus.mdf;Integrated Security=True;Connect Timeout=30";

        public List<Gas> gases = new List<Gas>();
        public List<Model> models = new List<Model>();
        public List<Car> cars = new List<Car>();
        public List<Courier> couriers= new List<Courier>();
        public List<Gasstation> gasstations = new List<Gasstation>();
        public List<Traveling> travelings = new List<Traveling>();

        public Database()
        {
            get_all_data();
        }

        public List<Gas> get_gases()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    gases.Clear();
                    string query = string.Format("select * from Gases");
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            gases.Add(new Gas((int)reader["id"], (string)reader["gas"], (decimal)reader["price"]));
                    reader.Close();
                    connect.Close();
                    return gases;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (получение списка видов топлива)! " + ex.Message);
                    connect.Close();
                    return gases;
                }
            }
        }

        public List<Model> get_models()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    models.Clear();
                    string query = string.Format("select * from Models");
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            models.Add(new Model((int)reader["id"], (string)reader["model"], (decimal)reader["rasxod"], gases.Find(x => x.id == (int)reader["id_gas"])));
                    reader.Close();
                    connect.Close();
                    return models;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (получение списка моделей)! " + ex.Message);
                    connect.Close();
                    return models;
                }
            }
        }

        public List<Car> get_cars()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    cars.Clear();
                    string query = string.Format("select * from Cars");
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            cars.Add(new Car((int)reader["id"], (string)reader["car"], (int)reader["probeg"], (decimal)reader["gas"], models.Find(x => x.id == (int)reader["id_model"]), (int)reader["nav_id_object"]));
                    reader.Close();
                    connect.Close();
                    return cars;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (получение списка автомобилей)! " + ex.Message);
                    connect.Close();
                    return cars;
                }
            }
        }

        public List<Courier> get_couriers()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    couriers.Clear();
                    string query = string.Format("select * from Couriers");
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            couriers.Add(new Courier((int)reader["id"], (string)reader["courier"], (string)reader["prava"], cars.Find(x => x.id == (int)reader["id_car"])));
                    reader.Close();
                    connect.Close();
                    return couriers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (получение списка курьеров)! " + ex.Message);
                    connect.Close();
                    return couriers;
                }
            }
        }

        public List<Gasstation> get_gasstations()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    gasstations.Clear();
                    string query = string.Format("select * from Gasstations");
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            gasstations.Add(new Gasstation((int)reader["id"], (decimal)reader["enter_gas"], (decimal)reader["really_gas"], (decimal)reader["price"], cars.Find(x => x.id == (int)reader["id_car"])));
                    reader.Close();
                    connect.Close();
                    return gasstations;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (получение списка заправок)! " + ex.Message);
                    connect.Close();
                    return gasstations;
                }
            }
        }

        public List<Traveling> get_travelings()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    travelings.Clear();
                    string query = string.Format("select * from Travelings order by date_traveling desc");
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            travelings.Add(new Traveling((int)reader["id"], (int)reader["number"], (DateTime)reader["date_traveling"], cars.Find(x => x.id == (int)reader["id_car"]), 
                                            couriers.Find(x => x.id == (int)reader["id_courier"]), gasstations.Find(x => x.id == (int)reader["id_gasstation"]),
                                            (int)reader["s_probeg_1"], (int)reader["e_probeg_1"], (int)reader["t_probeg_1"],
                                            (int)reader["s_probeg_2"], (int)reader["e_probeg_2"], (int)reader["t_probeg_2"], (int)reader["t_probeg_all"],
                                            (decimal)reader["s_gas_1"], (decimal)reader["e_gas_1"], (decimal)reader["t_gas_1"], (decimal)reader["r_gas_1"], (decimal)reader["z_gas_1"], (decimal)reader["p_gas_1"],
                                            (decimal)reader["s_gas_2"], (decimal)reader["e_gas_2"], (decimal)reader["t_gas_2"], (decimal)reader["r_gas_2"], (decimal)reader["z_gas_2"], (decimal)reader["p_gas_2"],
                                            (decimal)reader["t_gas_all"], (decimal)reader["p_traveling_1"], (decimal)reader["p_traveling_2"], (decimal)reader["p_traveling_all"], (int)reader["status_traveling"],
                                            (int)reader["status_inRf"]));
                    reader.Close();
                    connect.Close();
                    return travelings;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (получение списка путевых листов)! " + ex.Message);
                    connect.Close();
                    return travelings;
                }
            }
        }

        public void get_all_data()
        {
            get_gases();
            get_models();
            get_cars();
            get_couriers();
            get_gasstations();
            get_travelings();
        }
    }
}
