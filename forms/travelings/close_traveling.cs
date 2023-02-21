using Mekus.classes;
using System;
using System.Windows.Forms;
using Mekus.nav;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Reflection.Emit;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using Mekus.belarusneft;

namespace Mekus.forms.travelings
{
    public partial class close_traveling : Form
    {
        Database db = null;
        Main main = null;
        Traveling traveling = null;
        bool editcar = false;
        bool edit_traveling = false;
        bool read_traveling = false;

        public close_traveling()
        {
            InitializeComponent();
        }

        public close_traveling(Database db, Main main, Traveling traveling)
        {
            InitializeComponent();

            this.db = db;
            this.main = main;

            db.travelings = db.get_travelings();
            this.traveling = db.travelings.Find(x => x.id == traveling.id);

            db.cars = db.get_cars();
            this.traveling.id_car = db.cars.Find(x => x.id == traveling.id_car.id);

            for (int i = 0; i < db.cars.Count; i++)
                comboBox1.Items.Add(db.cars[i].car);

            if (this.traveling.status_traveling == 0)
            {
                textBox1.Text = Convert.ToString(traveling.number);
                textBox2.Text = Convert.ToString(traveling.date_traveling.Date.ToString("dd MMMM yyyy"));
                textBox3.Text = Convert.ToString(traveling.id_courier.courier);
                comboBox1.Text = Convert.ToString(traveling.id_car.car);
                textBox5.Text = Convert.ToString(traveling.id_car.probeg);
                this.traveling.s_probeg_1 = traveling.id_car.probeg;
                textBox8.Text = Convert.ToString(traveling.id_car.Gas);
                this.traveling.S_gas_1 = traveling.id_car.Gas;
                textBox11.Text = Convert.ToString(traveling.id_car.id_model.Rasxod);
                textBox12.Text = Convert.ToString(traveling.Z_gas_1);
                textBox13.Text = Convert.ToString(traveling.id_car.id_model.id_gas.Price);

                GetRequestInNavBy(traveling.date_traveling, traveling.date_traveling, traveling.id_car.nav_id_object);

                edit_traveling = true;
            }
            else
            {
                read_traveling = true;
                textBox1.Text = Convert.ToString(traveling.number);
                textBox2.Text = Convert.ToString(traveling.date_traveling.Date.ToString("dd MMMM yyyy"));
                textBox3.Text = Convert.ToString(traveling.id_courier.courier);
                comboBox1.Text = Convert.ToString(traveling.id_car.car);
                textBox5.Text = Convert.ToString(traveling.s_probeg_1);
                textBox6.Text = Convert.ToString(traveling.e_probeg_1);
                textBox7.Text = Convert.ToString(traveling.t_probeg_1);
                textBox8.Text = Convert.ToString(traveling.S_gas_1);
                textBox9.Text = Convert.ToString(traveling.E_gas_1);
                textBox10.Text = Convert.ToString(traveling.T_gas_1);
                textBox14.Text = Convert.ToString(traveling.t_probeg_all);
                textBox15.Text = Convert.ToString(traveling.Z_gas_1);
                textBox11.Text = Convert.ToString(traveling.R_gas_1);
                textBox12.Text = Convert.ToString(traveling.Z_gas_1);
                textBox13.Text = Convert.ToString(traveling.P_gas_1);

                GetRequestInNavBy(traveling.date_traveling, traveling.date_traveling, traveling.id_car.nav_id_object);

                button1.Enabled = false;
                button2.Enabled = false;
                comboBox1.Enabled = false;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(read_traveling == false)
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
                    }
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
                if (read_traveling == false)
                {
                    traveling.R_gas_1 = Convert.ToDecimal(textBox11.Text);
                    if (textBox6.Text.Length != 0 && read_traveling == false)
                        textBox6_TextChanged(sender, e);
                }
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
                if(read_traveling == false)
                {
                    traveling.Z_gas_1 = Convert.ToDecimal(textBox12.Text);
                    if (textBox6.Text.Length != 0)
                        textBox6_TextChanged(sender, e);
                }
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
                if (read_traveling == false)
                {
                    traveling.P_gas_1 = Convert.ToDecimal(textBox13.Text);
                    if (textBox6.Text.Length != 0 && read_traveling == false)
                        textBox6_TextChanged(sender, e);
                }
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
                DialogResult dialogResult = new DialogResult();

                if (traveling.E_gas_1 < 0)
                    dialogResult = MessageBox.Show("Остаток топлива по пути туда получается отрицательный, вы желаете продолжить?", "Отрицательный остаток топлива", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                if(dialogResult == DialogResult.OK || traveling.E_gas_1 >= 0)
                {
                    if (editcar == true)
                    {
                        traveling.id_gasstation = traveling.id_gasstation.get_gasstation(db, traveling);
                        traveling.editCar();
                    }

                    traveling.id_car.probeg = traveling.e_probeg_1;
                    traveling.id_car.Gas = traveling.E_gas_1;

                    if (traveling.Z_gas_1 != 0)
                        traveling.id_gasstation.addGasstation(db, traveling.Z_gas_1, traveling.P_gas_1, traveling, false);

                    traveling.P_traveling_1 = traveling.id_gasstation.get_price_traveling_test_2(db, traveling, traveling.T_gas_1, 0.00m);

                    if (traveling.P_traveling_1 != -1)
                    {
                        db.gasstations = db.get_gasstations();

                        traveling.P_traveling_all = traveling.P_traveling_1;
                        traveling.id_car.editCar();
                        traveling.closeTraveling();

                        main.set_value_table(traveling, 0);
                        //main.set_values_table();

                        Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetRequestInNavBy(DateTime from, DateTime to, int nav_id_object)
        {
            try
            {
                if (nav_id_object != 5716630 && nav_id_object != 5431279)
                {
                    string url = @"https://api.nav.by/info/integration.php?type=OBJECT_STAT_DATA&token=613ce8ea-8506-49a6-bf76-279a635601ce&from="
                            + from.Date.ToString("yyyy-MM-dd") + " 00:00:00&to=" + to.Date.ToString("yyyy-MM-dd") + " 23:59:00&object_id=" + nav_id_object;

                    WebRequest request = WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    JSONParser parser = new JSONParser();
                    parser = JsonSerializer.Deserialize<JSONParser>(responseFromServer);


                    if (parser.root.result.items[0].distance_can != 0)
                        textBox14.Text = Convert.ToString(Convert.ToDecimal(decimal.Round((decimal)parser.root.result.items[0].distance_can, 3, MidpointRounding.AwayFromZero) / 1000));
                    else
                        textBox14.Text = Convert.ToString(Convert.ToDecimal(decimal.Round((decimal)parser.root.result.items[0].distance_gps, 3, MidpointRounding.AwayFromZero) / 1000));

                    if (parser.root.result.items[0].fuel_in_list.Length > 0)
                    {
                        if (parser.root.result.items[0].fuel_in_list.Length == 1)
                            if (parser.root.result.items[0].fuel_in_list[0].value > 0)
                                textBox15.Text += Convert.ToString(parser.root.result.items[0].fuel_in_list[0].value);
                            else
                                textBox15.Text += "0";
                    }

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                traveling.id_gasstation = traveling.id_gasstation.get_gasstation(db, traveling);
                traveling.editCar();
                main.set_value_table(traveling, 0);
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(edit_traveling == true)
                {
                    editcar = true; // проверка на кривые ручки
                    traveling.id_car = db.cars.Find(x => x.car == comboBox1.Text);
                    textBox5.Text = Convert.ToString(traveling.id_car.probeg);
                    this.traveling.s_probeg_1 = traveling.id_car.probeg;
                    textBox8.Text = Convert.ToString(traveling.id_car.Gas);
                    this.traveling.S_gas_1 = traveling.id_car.Gas;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void close_traveling_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.get_all_data();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                EnterAPI enter = await Belarusneft.auth("https://belorusneft.by/identity/connect/token");

                Test_1 test = new Test_1
                {
                    startDate = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"),
                    endDate = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"),
                    cardNumber = traveling.id_car.cardCode,
                    subDivisnNumber = -1,
                    flChoice = 1
                };

                API api = await Belarusneft.getAPI("https://ssl.beloil.by/rcp/i/api/v2/Contract/operational", enter.access_token, test);
                if(api.cardList.Length > 0)
                {
                    if (api.cardList[0].issueRows.Length > 0)
                    {
                        dataGridView1.Rows.Clear();
                        for (int i = 0; i < api.cardList[0].issueRows.Length; i++)
                        {
                            dataGridView1.Rows.Add(api.cardList[0].issueRows[i].dateTimeIssue, api.cardList[0].issueRows[i].productQuantity, 
                                                    api.cardList[0].issueRows[i].productUnitPrice, api.cardList[0].issueRows[i].productName);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
