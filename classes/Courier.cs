using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mekus.classes
{
    public class Courier
    {
        public static string str_connect = @"Data Source=.\SQLEXPRESS;Database=Mekus;AttachDbFilename=|DataDirectory|\Mekus.mdf;Integrated Security=True;Connect Timeout=30";
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

        public void addCourier()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("insert into Couriers (courier, prava, id_car) values ('{0}', '{1}', {2})", courier, prava, id_car.id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connect.Close();
                }
            }
        }

        public void editCourier()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("update Couriers set courier='{0}', prava='{1}', id_car={2} where id={3}", courier, prava, id_car.id, id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connect.Close();
                }
            }
        }

        public void deleteCourier()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("delete Couriers where id={0}", id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connect.Close();
                }
            }
        }
    }
}
