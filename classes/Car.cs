using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mekus.classes
{
    public class Car
    {
        public static string str_connect = @"Data Source=.\SQLEXPRESS;Database=Mekus;AttachDbFilename=|DataDirectory|\Mekus.mdf;Integrated Security=True;Connect Timeout=30";
        public int id { get; set; }
        public string car { get; set; }
        public int probeg { get; set; }
        public decimal Gas { get => decimal.Round(gas, 2, MidpointRounding.AwayFromZero); set => gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }

        private decimal gas;

        public Model id_model { get; set; }

        public int nav_id_object { get; set; }

        public Car()
        {

        }

        public Car(int id, string car, int probeg, decimal gas, Model id_model, int nav_id_object)
        {
            this.id = id;
            this.car = car;
            this.probeg = probeg;
            Gas = gas;
            this.id_model = id_model;
            this.nav_id_object = nav_id_object;
        }

        public Car(string car, int probeg, decimal gas, Model id_model)
        {
            this.car = car;
            this.probeg = probeg;
            Gas = gas;
            this.id_model = id_model;
        }

        public void addCar()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("insert into Cars (car, probeg, gas, id_model) values ('{0}', {1}, @gas, {2})", car, probeg, id_model.id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@gas", Gas);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (добавление модели)! " + ex.Message);
                    connect.Close();
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
                    string query = string.Format("update Cars set car='{0}', probeg={1}, gas=@gas, id_model={2} where id={3}", car, probeg, id_model.id, id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@gas", Gas);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (обновление модели)! " + ex.Message);
                    connect.Close();
                }
            }
        }

        public void deleteCar()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("delete Cars where id={0}", id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (удаление модели)! " + ex.Message);
                    connect.Close();
                }
            }
        }
    }
}
