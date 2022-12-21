using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mekus.classes
{
    public class Model
    {
        public static string str_connect = @"Data Source=.\SQLEXPRESS;Database=Mekus;AttachDbFilename=|DataDirectory|\Mekus.mdf;Integrated Security=True;Connect Timeout=30";
        public int id { get; set; }
        public string model { get; set; }
        public decimal Rasxod { get => decimal.Round(rasxod, 2, MidpointRounding.AwayFromZero); set => rasxod = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }

        private decimal rasxod;
        
        public Gas id_gas { get; set; }

        public Model()
        {

        }

        public Model(int id, string model, decimal rasxod, Gas id_gas)
        {
            this.id = id;
            this.model = model;
            Rasxod = rasxod;
            this.id_gas = id_gas;
        }

        public Model(string model, decimal rasxod, Gas id_gas)
        {
            this.model = model;
            Rasxod = rasxod;
            this.id_gas = id_gas;
        }

        public void addModel()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("insert into Models (model, rasxod, id_gas) values ('{0}', @rasxod, {1})", model, id_gas.id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@rasxod", Rasxod);
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

        public void editModel()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("update Models set model='{0}', rasxod=@rasxod, id_gas={1} where id={2}", model, id_gas.id, id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@rasxod", Rasxod);
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

        public void deleteModel()
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("delete Models where id={0}", id);
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
