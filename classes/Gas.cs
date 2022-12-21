using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Mekus.classes
{
    public class Gas
    {
        public static string str_connect = @"Data Source=.\SQLEXPRESS;Database=Mekus;AttachDbFilename=|DataDirectory|\Mekus.mdf;Integrated Security=True;Connect Timeout=30";
        public int id { get; set; }
        public string gas { get; set; }
        public decimal Price { get => decimal.Round(price, 2, MidpointRounding.AwayFromZero); set => price = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }

        private decimal price;
        public Gas() { }

        public Gas(int id, string gas, decimal price)
        {
            this.id = id;
            this.gas = gas;
            Price = price;
        }

        public Gas(string gas, decimal price)
        {
            this.gas = gas;
            Price = price;
        }

        public void addGas()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("insert into Gases (gas, price) values ('{0}', @price)", gas);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@price", Price);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (добавление вида топлива)! " + ex.Message);
                    connect.Close();
                }
            }
        }

        public void editGas()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("update Gases set gas='{0}', price=@price where id={1}", gas, id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@price", Price);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (обновление вида топлива)! " + ex.Message);
                    connect.Close();
                }
            }
        }

        public void deleteGas()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("delete Gases where id={0}", id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка БД (удаление вида топлива)! " + ex.Message);
                    connect.Close();
                }
            }
        }
    }
}
