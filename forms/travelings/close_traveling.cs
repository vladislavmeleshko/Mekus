using Mekus.classes;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;
using Mekus.nav;
using System.Net;
using System.IO;
using System.Text.Json;

namespace Mekus.forms.travelings
{
    public partial class close_traveling : Form
    {
        Database db = null;
        Main main = null;
        Traveling traveling = null;

        public close_traveling()
        {
            InitializeComponent();
        }

        public close_traveling(Database db, Main main, Traveling traveling)
        {
            InitializeComponent();
            this.db = db;
            this.main = main;
            this.traveling = traveling;

            textBox1.Text = Convert.ToString(traveling.number);
            textBox2.Text = Convert.ToString(traveling.date_traveling.Date.ToString("dd MMMM yyyy"));
            textBox3.Text = Convert.ToString(traveling.id_courier.courier);
            textBox4.Text = Convert.ToString(traveling.id_car.car);
            textBox5.Text = Convert.ToString(traveling.id_car.probeg);
            traveling.s_probeg_1 = traveling.id_car.probeg;
            textBox8.Text = Convert.ToString(traveling.id_car.Gas);
            traveling.S_gas_1 = traveling.id_car.Gas;
            textBox11.Text = Convert.ToString(traveling.id_car.id_model.Rasxod);
            textBox12.Text = Convert.ToString(traveling.Z_gas_1);
            textBox13.Text = Convert.ToString(traveling.id_car.id_model.id_gas.Price);

            GetRequestInNavBy(traveling.date_traveling, traveling.date_traveling, traveling.id_car.nav_id_object);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text.Length != 0)
                {
                    traveling.e_probeg_1 = Convert.ToInt32(textBox6.Text);
                    traveling.t_probeg_1 = traveling.e_probeg_1 - traveling.s_probeg_1;
                    traveling.t_probeg_all = traveling.t_probeg_1;
                    textBox7.Text = Convert.ToString(traveling.t_probeg_all);

                    traveling.T_gas_1 = traveling.t_probeg_1 * traveling.R_gas_1 / 100;
                    traveling.T_gas_all = traveling.T_gas_1;
                    textBox10.Text = Convert.ToString(traveling.T_gas_all);

                    traveling.E_gas_1 = traveling.S_gas_1 - traveling.T_gas_all + traveling.Z_gas_1;
                    textBox9.Text = Convert.ToString(traveling.E_gas_1);

                    traveling.id_car.probeg = traveling.e_probeg_1;
                    traveling.id_car.Gas = traveling.E_gas_1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.R_gas_1 = Convert.ToDecimal(textBox11.Text);
                if (textBox6.Text.Length != 0)
                    textBox6_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.Z_gas_1 = Convert.ToDecimal(textBox12.Text);
                if (textBox6.Text.Length != 0)
                    textBox6_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.P_gas_1 = Convert.ToDecimal(textBox13.Text);
                if (textBox6.Text.Length != 0)
                    textBox6_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (traveling.Z_gas_1 != 0)
                    traveling.id_gasstation.addGasstation(db, traveling.Z_gas_1, traveling.P_gas_1, traveling);
                traveling.P_traveling_1 = traveling.id_gasstation.get_price_traveling(db, traveling, traveling.T_gas_1);
                traveling.P_traveling_all = traveling.P_traveling_1;
                traveling.id_car.editCar();
                traveling.closeTraveling();
                main.set_values_table();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetRequestInNavBy(DateTime from, DateTime to, int nav_id_object)
        {
            string url = @"https://api.nav.by/info/integration.php?type=OBJECT_STAT_DATA&token=613ce8ea-8506-49a6-bf76-279a635601ce&from="
                            + from.Date.ToString("yyyy-MM-dd") + " 00:00:00&to=" + to.Date.ToString("yyyy-MM-dd") + " 23:59:00&object_id="+ nav_id_object;
            WebRequest request = WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            JSONParser parser = new JSONParser();
            parser = JsonSerializer.Deserialize<JSONParser>(responseFromServer);

            if(parser.root.result.items[0].distance_can != 0)
                textBox14.Text = Convert.ToString(Convert.ToDecimal(decimal.Round((decimal)parser.root.result.items[0].distance_can, 3, MidpointRounding.AwayFromZero) / 1000));
            else
                textBox14.Text = Convert.ToString(Convert.ToDecimal(decimal.Round((decimal)parser.root.result.items[0].distance_gps, 3, MidpointRounding.AwayFromZero) / 1000));

            if (parser.root.result.items[0].fuel_in_list.Length > 0)
                textBox15.Text = Convert.ToString(parser.root.result.items[0].fuel_in_list[0].value);
            else
                textBox15.Text = "0";

            if (parser.root.result.items[0].odom_start != 0)
                textBox16.Text = Convert.ToString(parser.root.result.items[0].odom_start);
            else
                textBox16.Text = "0";

            if (parser.root.result.items[0].odom_finish != 0)
                textBox17.Text = Convert.ToString(parser.root.result.items[0].odom_finish);
            else
                textBox17.Text = "0";
        }
    }
}
