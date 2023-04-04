using System;
using System.Collections;
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

        public DateTime? date_gas { get; set; }

        private decimal enter_gas;

        private decimal really_gas;

        private decimal price;
        public Car id_car { get; set; }

        public Gasstation()
        {

        }

        public Gasstation(int id, decimal enter_gas, decimal really_gas, decimal price, Car id_car, DateTime date_gas)
        {
            this.id = id;
            Enter_gas = enter_gas;
            Really_gas = really_gas;
            Price = price;
            this.id_car = id_car;
            this.date_gas = date_gas;
        }

        public Gasstation(decimal enter_gas, decimal really_gas, decimal price, Car id_car, DateTime date_gas)
        {
            Enter_gas = enter_gas;
            Really_gas = really_gas;
            Price = price;
            this.id_car = id_car;
            this.date_gas = date_gas;
        }

        public Gasstation get_gasstation(Database db, Traveling traveling)
        {
            Gasstation gasstation = null;
            using(SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    gasstation = db.gasstations.Find(x => x.id_car.id == traveling.id_car.id && x.Enter_gas > x.Really_gas);
                    if (gasstation == null)
                    {
                        gasstation = new Gasstation();
                        string query = string.Format("insert into Gasstations (enter_gas, really_gas, price, id_car, date_gas) values (@enter_gas, 0, @price, {0}, '{1}')", traveling.id_car.id, traveling.date_traveling.Date);
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

        public void addGasstation(Database db, decimal enter_gas, decimal price, Traveling traveling, bool next_day)
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    connect.Open();
                    string query;
                    if(next_day == false)
                        query = string.Format("insert into Gasstations (enter_gas, really_gas, id_car, price, date_gas) values (@enter_gas, 0, {0}, @price, '{1}')", traveling.id_car.id, traveling.date_traveling);
                    else
                        query = string.Format("insert into Gasstations (enter_gas, really_gas, id_car, price, date_gas) values (@enter_gas, 0, {0}, @price, '{1}')", traveling.id_car.id, traveling.date_traveling.AddDays(1));
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

        public decimal get_price_traveling_test_2(Database db, Traveling traveling, decimal t_gas_all, decimal price_test)
        {
            using (SqlConnection connect = new SqlConnection(str_connect))
            {
                try
                {
                    db.gasstations = db.get_gasstations();
                    if (t_gas_all == 0.00m)
                        return price_test;
                    else
                    {
                        if(t_gas_all + Really_gas <= Enter_gas)
                        {
                            connect.Open();
                            Really_gas += t_gas_all;
                            string query = string.Format("update Gasstations set really_gas = {0} where id = {1}", Really_gas.ToString().Replace(",", "."), id);
                            SqlCommand cmd = new SqlCommand(query, connect);
                            cmd.ExecuteNonQuery();
                            query = string.Format("insert into History_gas (id_traveling, prev_t_gas, one_to_many, id_car) values ({0}, {1}, '{2}', {3})", traveling.id, t_gas_all.ToString().Replace(",", "."), id, id_car.id);
                            cmd = new SqlCommand(query, connect);
                            cmd.ExecuteNonQuery();
                            price_test += t_gas_all * Price;
                            t_gas_all -= t_gas_all;
                            connect.Close();
                            return get_price_traveling_test_2(db, traveling, t_gas_all, price_test);
                        }
                        else
                        {
                            Gasstation gasstation = db.gasstations.Find(x => x.id > id && x.Enter_gas != x.Really_gas && x.id_car.id == id_car.id);
                            if(gasstation != null)
                            {
                                connect.Open();
                                price_test += (Enter_gas - Really_gas) * Price;
                                string query = string.Format("update Gasstations set really_gas = {0} where id = {1}", Enter_gas.ToString().Replace(",", "."), id);
                                SqlCommand cmd = new SqlCommand(query, connect);
                                cmd.ExecuteNonQuery();
                                query = string.Format("insert into History_gas (id_traveling, prev_t_gas, one_to_many, id_car) values ({0}, {1}, '{2}', {3})", traveling.id, (Enter_gas - Really_gas).ToString().Replace(",", "."), id, id_car.id);
                                cmd = new SqlCommand(query, connect);
                                cmd.ExecuteNonQuery();
                                t_gas_all -= Enter_gas - Really_gas;
                                traveling.id_gasstation = gasstation;
                                if (t_gas_all + gasstation.Really_gas <= gasstation.Enter_gas)
                                {
                                    query = string.Format("update Travelings set id_gasstation = {0} where id >= {1} and id_car = {2}", gasstation.id, traveling.id, traveling.id_car.id);
                                    cmd = new SqlCommand(query, connect);
                                    cmd.ExecuteNonQuery();
                                    connect.Close();
                                    return traveling.id_gasstation.get_price_traveling_test_2(db, traveling, t_gas_all, price_test);
                                }
                                else
                                {
                                    connect.Close();
                                    return traveling.id_gasstation.get_price_traveling_test_2(db, traveling, t_gas_all, price_test);
                                }
                            }
                            else
                            {
                                connect.Open();
                                string message = string.Format("Вам необходимо искусственно добавить заправку на {0} литров, вы желаете продолжить?", t_gas_all - (Enter_gas - Really_gas));
                                DialogResult dialogResult = MessageBox.Show(message, "Искусственное добавление заправки", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                if (dialogResult == DialogResult.OK)
                                {
                                    string query = string.Format("insert into Gasstations (enter_gas, price, id_car, date_gas) values ({0}, {1}, {2}, '{3}')", (t_gas_all - (Enter_gas - Really_gas)).ToString().Replace(",", "."), traveling.P_gas_1.ToString().Replace(",", "."), id_car, date_gas);
                                    SqlCommand cmd = new SqlCommand(query, connect);
                                    cmd.ExecuteNonQuery();
                                    db.gasstations = db.get_gasstations();
                                    gasstation = db.gasstations.Find(x => x.id > id && x.Enter_gas != x.Really_gas && x.id_car.id == id_car.id);
                                    traveling.id_gasstation = gasstation;
                                    connect.Close();
                                    return traveling.id_gasstation.get_price_traveling_test_2(db, traveling, t_gas_all, price_test);
                                }
                                else
                                {
                                    connect.Close();
                                    return -1;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connect.Close();
                    return -1;
                }
            }
        }

        public int get_last_gasstations(int id_car, DateTime date, List<Gasstation> gasstations)
        {
            try
            {
                return gasstations.FindLast(x => x.id_car.id == id_car && x.date_gas <= date.Date).id;
            }
            catch(Exception)
            {
                return -1;
            }
        }

        public void deleteGasstations(int id_gasstation, int id_car, int id_gasstation2)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(str_connect))
                {
                    try
                    {
                        connect.Open();
                        string query = string.Format("delete History_gas where one_to_many >= {0} and id_traveling >= (select top 1 id from travelings where status_traveling=0 and id_car={1})" +
                            " and id_car = {2}", id_gasstation2, id_car, id_car);
                        SqlCommand cmd = new SqlCommand(query, connect);
                        cmd.ExecuteNonQuery();
                        query = string.Format("delete Gasstations where id > {0} and id_car = {1}", id_gasstation, id_car);
                        cmd = new SqlCommand(query, connect);
                        cmd.ExecuteNonQuery();
                        connect.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connect.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void get_and_set_value_in_gastation(int id_gasstation, int id_car)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(str_connect))
                {
                    try
                    {
                        connect.Open();
                        decimal sum_t_gas = 0.00m;
                        string query = string.Format("select sum(prev_t_gas) from History_gas where one_to_many = {0}", id_gasstation);
                        SqlCommand cmd = new SqlCommand(query, connect);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                            sum_t_gas = (decimal)reader[0];
                        reader.Close();
                        query = string.Format("update Gasstations set really_gas = {0} where id = {1}", sum_t_gas.ToString().Replace(",", "."), id_gasstation);
                        cmd = new SqlCommand(query, connect);
                        cmd.ExecuteNonQuery();
                        query = string.Format("update Gasstations set really_gas = 0.00 where id > {0} and id_car={1}", id_gasstation, id_car);
                        cmd = new SqlCommand(query, connect);
                        cmd.ExecuteNonQuery();
                        connect.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connect.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
