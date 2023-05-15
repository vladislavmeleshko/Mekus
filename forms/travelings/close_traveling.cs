using Mekus.classes;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Http;
using Mekus.berlio;
using System.Text.Json;
using Mekus.belarusneft;
using System.Linq;
using Newtonsoft.Json;

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
            this.db = db;
            this.main = main;

            db.travelings = db.get_travelings();
            this.traveling = db.travelings.Find(x => x.id == traveling.id);

            db.cars = db.get_cars();
            this.traveling.id_car = db.cars.Find(x => x.id == traveling.id_car.id);

            InitializeComponent();

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
                textBox11.Text = Convert.ToString(traveling.R_gas_1);

                for (int i = 0; i < traveling.listGasstations.Count; i++)
                { 
                    dataGridView2.Rows.Add(
                            traveling.listGasstations[i].date_gas.Value.ToString("dd MMMM yyyy"),
                            traveling.listGasstations[i].name_gas,
                            traveling.listGasstations[i].Enter_gas,
                            traveling.listGasstations[i].Price
                        );
                }

                button1.Enabled = false;
                button2.Enabled = false;
                comboBox1.Enabled = false;
            }

            dateTimePicker3.Value = traveling.date_traveling;
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
                        raschetGas(true);
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

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = new DialogResult();

                if (traveling.E_gas_1 <= 0)
                    dialogResult = MessageBox.Show("Остаток топлива по пути туда получается отрицательный, вы желаете продолжить?", "Отрицательный остаток топлива", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                if(dialogResult == DialogResult.OK || traveling.E_gas_1 > 0)
                {
                    if (editcar == true)
                    {
                        traveling.id_gasstation = traveling.id_gasstation.get_gasstation(db, traveling);
                        traveling.editCar();
                    }

                    traveling.id_car.probeg = traveling.e_probeg_1;
                    traveling.id_car.Gas = traveling.E_gas_1;

                    if (raschetGas(true) == -1) throw new Exception("Проверьте таблицу заправок!");
                    if (traveling.Z_gas_1 != 0)
                    {
                        for (int i = 0; i < traveling.listGasstations.Count; i++)
                        {
                            traveling.id_gasstation.addGasstation(
                                        db,
                                        traveling.listGasstations[i].Enter_gas,
                                        traveling.listGasstations[i].Price,
                                        traveling,
                                        traveling.listGasstations[i].date_gas,
                                        traveling.listGasstations[i].name_gas
                                    );
                            traveling.listGasstations[i].id = db.gasstations.FindLast(x => x.id_car == traveling.id_car).id;
                        }
                    }

                    traveling.P_traveling_1 = traveling.id_gasstation.get_price_traveling_test_2(db, traveling, traveling.T_gas_1, 0.00m);

                    if (traveling.P_traveling_1 != -1)
                    {
                        db.gasstations = db.get_gasstations();

                        traveling.P_traveling_all = traveling.P_traveling_1;
                        traveling.id_car.editCar();
                        traveling.closeTraveling();

                        main.set_value_table(traveling, 0);

                        Close();
                    }
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

        public async void get_gas_berlio()
        {
            HttpClient httpClient = new HttpClient();
            string card = Convert.ToString(traveling.id_car.cardCode);
            string dateFrom = traveling.date_traveling.Date.ToString("yyyy-MM-dd");
            string dateTo = traveling.date_traveling.Date.AddDays(1).ToString("yyyy-MM-dd");
            string url = "https://api.cardcenter.by/api/Cards/GetCardRealisations?cardNumber="+card+"&dateFrom="+dateFrom+"&dateTo="+dateTo;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            if (Convert.ToInt32(response.StatusCode) == 200)
            {
                Berlio[] berlio = System.Text.Json.JsonSerializer.Deserialize<Berlio[]>(content);
                if (berlio.Length > 0)
                {
                    for (int i = 0; i < berlio.Length; i++)
                    {
                        if (Convert.ToDateTime(berlio[i].RealisationDate).Date.ToString() == traveling.date_traveling.ToString())
                        {
                            dataGridView1.Rows.Add(Convert.ToDateTime(berlio[i].RealisationDate).ToString("dd MMMM yyyy"), berlio[i].RealisationProductName,
                                                    berlio[i].RealisationQuantity, berlio[i].RealisationRoznPrice, berlio[i].RealisationProductName);
                            if (traveling.status_traveling == 0)
                                dataGridView2.Rows.Add(Convert.ToDateTime(berlio[i].RealisationDate).ToString("dd MMMM yyyy"), berlio[i].RealisationProductName,
                                                    berlio[i].RealisationQuantity, berlio[i].RealisationRoznPrice);
                        }
                    }
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                EnterAPI enter = null;
                if(traveling.id_car.id != 11)
                    enter = await Belarusneft.auth("https://belorusneft.by/identity/connect/token", 0);
                else enter = await Belarusneft.auth("https://belorusneft.by/identity/connect/token", 1);


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
                            dataGridView1.Rows.Add(api.cardList[0].issueRows[i].dateTimeIssue, api.cardList[0].issueRows[i].productName, api.cardList[0].issueRows[i].productQuantity, 
                                                    api.cardList[0].issueRows[i].productUnitPrice);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void close_traveling_Load(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Value = traveling.date_traveling.Date;
                dateTimePicker2.Value = traveling.date_traveling.Date;
                button3_Click(sender, e);
                get_gas_berlio();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Add(
                        dateTimePicker3.Value.Date.ToString("dd.MM.yyyy"), "", "", traveling.id_car.id_model.id_gas.Price
                    );
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int raschetGas(bool isClose = false)
        {
            try
            {
                traveling.listGasstations = new List<Gasstation>();
                for(int i = 0; i <  dataGridView2.Rows.Count; i++)
                {
                    if (isClose != false)
                    {
                        if (dataGridView2.Rows[i].Cells[0].Value.ToString() == "") throw new Exception("Укажите дату заправки");
                        if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "") throw new Exception("Укажите количество заправленного топлива");
                        if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "") throw new Exception("Укажите количество стоимость топлива");
                    }
                    traveling.listGasstations.Add(new Gasstation(
                            Convert.ToDateTime(dataGridView2.Rows[i].Cells[0].Value.ToString()),
                            Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()),
                            Convert.ToDecimal(dataGridView2.Rows[i].Cells[3].Value.ToString()),
                            dataGridView2.Rows[i].Cells[1].Value.ToString()
                        ));
                }
                traveling.Z_gas_1 = traveling.listGasstations.Sum(x => x.Enter_gas);
                traveling.E_gas_1 = traveling.S_gas_1 - traveling.T_gas_1 + traveling.Z_gas_1;
                textBox9.Text = Convert.ToString(traveling.E_gas_1);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            raschetGas();
        }

        private void dataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            raschetGas();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dateTimePicker3.Value = dateTimePicker3.Value.Date.AddDays(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dateTimePicker3.Value = dateTimePicker3.Value.Date.AddDays(-1);
        }

        private void close_traveling_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(traveling.editTypeTraveling(1) == 1)
                {
                    main.set_values_table();
                    Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
