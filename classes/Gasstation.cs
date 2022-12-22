using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mekus.classes
{
    public class Gasstation
    {
        public static string str_connect = @"Data Source=.\SQLEXPRESS;Database=Mekus;AttachDbFilename=|DataDirectory|\Mekus.mdf;Integrated Security=True;Connect Timeout=30";
        public int id { get; set; }
        public decimal Enter_gas { get => decimal.Round(enter_gas, 2, MidpointRounding.AwayFromZero); set => enter_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Really_gas { get => decimal.Round(really_gas, 2, MidpointRounding.AwayFromZero); set => really_gas = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }
        public decimal Price { get => decimal.Round(price, 2, MidpointRounding.AwayFromZero); set => price = decimal.Round(value, 2, MidpointRounding.AwayFromZero); }

        private decimal enter_gas;

        private decimal really_gas;

        private decimal price;
        public Car id_car { get; set; }

        public Gasstation()
        {

        }

        public Gasstation(int id, decimal enter_gas, decimal really_gas, decimal price, Car id_car)
        {
            this.id = id;
            Enter_gas = enter_gas;
            Really_gas = really_gas;
            Price = price;
            this.id_car = id_car;
        }

        public Gasstation(decimal enter_gas, decimal really_gas, decimal price, Car id_car)
        {
            Enter_gas = enter_gas;
            Really_gas = really_gas;
            Price = price;
            this.id_car = id_car;
        }

        public Gasstation get_gasstation(Database db, Traveling traveling)
        {
            Gasstation gasstation = new Gasstation();
            using(SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    gasstation = db.gasstations.Find(x => x.id_car == traveling.id_car && x.Enter_gas != x.Really_gas);
                    if (gasstation == null)
                    {
                        gasstation = new Gasstation();
                        string query = string.Format("insert into Gasstations (enter_gas, really_gas, price, id_car) values (@enter_gas, 0, @price, {0})", traveling.id_car.id);
                        SqlCommand cmd = new SqlCommand(query, connect);
                        SqlParameter param = new SqlParameter("@enter_gas", traveling.id_car.Gas);
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("@price", traveling.id_car.id_model.id_gas.Price);
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        db.gasstations = db.get_gasstations();
                        connect.Close();
                        return db.gasstations[db.gasstations.Count - 1];
                    }
                    else
                    { connect.Close(); return gasstation; }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connect.Close();
                    return gasstation;
                }
            }
        }

        public void addGasstation(Database db, decimal enter_gas, decimal price, Traveling traveling)
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query = string.Format("insert into Gasstations (enter_gas, really_gas, id_car, price) values (@enter_gas, 0, {0}, @price)", traveling.id_car.id);
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlParameter param = new SqlParameter("@enter_gas", enter_gas);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@price", price);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    db.gasstations = db.get_gasstations();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connect.Close();
                }
            }
        }

        public decimal get_price_traveling(Database db, Traveling traveling, decimal t_gas_all)
        {
            decimal price = 0;
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    if (Really_gas + t_gas_all > Enter_gas)
                    {
                        string query = string.Format("update Gasstations set really_gas=@really_gas where id={0}", id);
                        SqlCommand cmd = new SqlCommand(query, connect);
                        SqlParameter param = new SqlParameter("@really_gas", Enter_gas);
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        Gasstation gasstation = db.gasstations.Find(x => x.id > traveling.id_gasstation.id && x.id_car.id == traveling.id_car.id);
                        query = string.Format("update Gasstations set really_gas=@really_gas where id={0}", gasstation.id);
                        cmd = new SqlCommand(query, connect);
                        param = new SqlParameter("@really_gas", Really_gas + t_gas_all - Enter_gas);
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        query = string.Format("update Travelings set id_gasstation={0} where id>{1}", gasstation.id, traveling.id);
                        cmd = new SqlCommand(query, connect);
                        cmd.ExecuteNonQuery();
                        connect.Close();
                        return (Enter_gas - Really_gas) * Price + (Really_gas + t_gas_all - Enter_gas) * gasstation.Price;
                    }
                    else if (Really_gas + t_gas_all == Enter_gas)
                    {
                        string query = string.Format("update Gasstations set really_gas=@really_gas where id={0}", id);
                        SqlCommand cmd = new SqlCommand(query, connect);
                        SqlParameter param = new SqlParameter("@really_gas", Really_gas + t_gas_all);
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        Gasstation gasstation = db.gasstations.Find(x => x.id > traveling.id_gasstation.id && x.id_car.id == traveling.id_car.id);
                        query = string.Format("update Travelings set id_gasstation={0} where id>{1}", gasstation.id, traveling.id);
                        cmd = new SqlCommand(query, connect);
                        cmd.ExecuteNonQuery();
                        connect.Close();
                        return t_gas_all * Price;
                    }
                    else
                    {
                        string query = string.Format("update Gasstations set really_gas=@really_gas where id={0}", id);
                        SqlCommand cmd = new SqlCommand(query, connect);
                        SqlParameter param = new SqlParameter("@really_gas", Really_gas + t_gas_all);
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        connect.Close();
                        return t_gas_all * Price;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connect.Close();
                    return price;
                }
            }
        }
    }
}
