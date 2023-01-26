using Mekus.classes;
using Mekus.forms.cars;
using Mekus.forms.couriers;
using Mekus.forms.gases;
using Mekus.forms.models;
using Mekus.forms.travelings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Mekus
{
    public partial class Main : Form
    {
        Database db = new Database();
        public Main()
        {
            InitializeComponent();
            set_values_table();
        }

        public void update_gridviewtravelingslist(int status_inRf)
        {
            if (status_inRf == 0) 
            {
                dataGridView5.Rows.Clear();
                for (int i = 0; i < db.travelings.Count; i++)
                {
                    if (db.travelings[i].status_inRf == status_inRf && db.travelings[i].date_traveling.Date >= dateTimePicker1.Value.Date && db.travelings[i].date_traveling.Date <= dateTimePicker2.Value.Date)
                    {
                        dataGridView5.Rows.Add(db.travelings[i].number, db.travelings[i].date_traveling.ToString("dd MMMM yyyy"), db.travelings[i].id_courier.courier, db.travelings[i].id_car.id_model.model + " " + db.travelings[i].id_car.car,
                                            db.travelings[i].s_probeg_1, db.travelings[i].e_probeg_1, db.travelings[i].t_probeg_all, db.travelings[i].S_gas_1, db.travelings[i].E_gas_1,
                                            db.travelings[i].T_gas_all, db.travelings[i].R_gas_1, db.travelings[i].Z_gas_1, db.travelings[i].P_gas_1, db.travelings[i].P_traveling_all);
                    }
                }
            }
            else
            {
                dataGridView6.Rows.Clear();
                for (int i = 0; i < db.travelings.Count; i++)
                {
                    if (db.travelings[i].status_inRf == status_inRf && db.travelings[i].date_traveling.Date >= dateTimePicker4.Value.Date && db.travelings[i].date_traveling.Date <= dateTimePicker3.Value.Date)
                    {
                        dataGridView6.Rows.Add(db.travelings[i].number, db.travelings[i].date_traveling.ToString("dd MMMM yyyy"), db.travelings[i].id_courier.courier, db.travelings[i].id_car.id_model.model + " " + db.travelings[i].id_car.car,
                                        db.travelings[i].s_probeg_1, db.travelings[i].e_probeg_1, db.travelings[i].S_gas_1, db.travelings[i].E_gas_1, db.travelings[i].R_gas_1, db.travelings[i].Z_gas_1,
                                        db.travelings[i].s_probeg_2, db.travelings[i].e_probeg_2, db.travelings[i].S_gas_2, db.travelings[i].E_gas_2, db.travelings[i].R_gas_2, db.travelings[i].Z_gas_2,
                                        db.travelings[i].t_probeg_all, db.travelings[i].T_gas_all, db.travelings[i].P_traveling_all);
                    }
                }
            }
        }

        // Пробная функция

        public void set_value_table(Traveling traveling, int id)
        {
            try
            {
                db.get_all_data();
                if(id == 0)
                {
                    for (int i = 0; i < dataGridView5.Rows.Count; i++)
                    {
                        if (dataGridView5.Rows[i].Cells[0].Value.ToString() == Convert.ToString(traveling.number))
                        {
                            dataGridView5.Rows[i].Cells[3].Value = Convert.ToString(traveling.id_car.id_model.model + " " + traveling.id_car.car);
                            dataGridView5.Rows[i].Cells[4].Value = Convert.ToString(traveling.s_probeg_1);
                            dataGridView5.Rows[i].Cells[5].Value = Convert.ToString(traveling.e_probeg_1);
                            dataGridView5.Rows[i].Cells[6].Value = Convert.ToString(traveling.t_probeg_1);
                            dataGridView5.Rows[i].Cells[7].Value = Convert.ToString(traveling.S_gas_1);
                            dataGridView5.Rows[i].Cells[8].Value = Convert.ToString(traveling.E_gas_1);
                            dataGridView5.Rows[i].Cells[9].Value = Convert.ToString(traveling.T_gas_1);
                            dataGridView5.Rows[i].Cells[10].Value = Convert.ToString(traveling.R_gas_1);
                            dataGridView5.Rows[i].Cells[11].Value = Convert.ToString(traveling.Z_gas_1);
                            dataGridView5.Rows[i].Cells[12].Value = Convert.ToString(traveling.id_gasstation.Price);
                            dataGridView5.Rows[i].Cells[13].Value = Convert.ToString(traveling.P_traveling_all);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dataGridView6.Rows.Count; i++)
                    {
                        if (dataGridView6.Rows[i].Cells[0].Value.ToString() == Convert.ToString(traveling.number))
                        {
                            dataGridView6.Rows[i].Cells[4].Value = Convert.ToString(traveling.s_probeg_1);
                            dataGridView6.Rows[i].Cells[5].Value = Convert.ToString(traveling.e_probeg_1);
                            dataGridView6.Rows[i].Cells[6].Value = Convert.ToString(traveling.S_gas_1);
                            dataGridView6.Rows[i].Cells[7].Value = Convert.ToString(traveling.E_gas_1);
                            dataGridView6.Rows[i].Cells[8].Value = Convert.ToString(traveling.R_gas_1);
                            dataGridView6.Rows[i].Cells[9].Value = Convert.ToString(traveling.Z_gas_1);
                            dataGridView6.Rows[i].Cells[10].Value = Convert.ToString(traveling.s_probeg_2);
                            dataGridView6.Rows[i].Cells[11].Value = Convert.ToString(traveling.e_probeg_2);
                            dataGridView6.Rows[i].Cells[12].Value = Convert.ToString(traveling.S_gas_2);
                            dataGridView6.Rows[i].Cells[13].Value = Convert.ToString(traveling.E_gas_2);
                            dataGridView6.Rows[i].Cells[14].Value = Convert.ToString(traveling.R_gas_2);
                            dataGridView6.Rows[i].Cells[15].Value = Convert.ToString(traveling.Z_gas_2);
                            dataGridView6.Rows[i].Cells[16].Value = Convert.ToString(traveling.t_probeg_all);
                            dataGridView6.Rows[i].Cells[17].Value = Convert.ToString(traveling.T_gas_all);
                            dataGridView6.Rows[i].Cells[18].Value = Convert.ToString(traveling.P_traveling_all);
                        }
                    }
                }
            }
            catch(Exception e) 
            {
                MessageBox.Show(e.Message); ;
            }
        }

        // Пробная функция


        public void set_values_table()
        {
            db.get_all_data();
            dataGridView1.Rows.Clear();
            for (int i = 0; i < db.gases.Count; i++)
                dataGridView1.Rows.Add(db.gases[i].id, db.gases[i].gas, db.gases[i].Price);
            dataGridView2.Rows.Clear();
            for (int i = 0; i < db.models.Count; i++)
                dataGridView2.Rows.Add(db.models[i].id, db.models[i].model, db.models[i].Rasxod, db.models[i].id_gas.gas);
            dataGridView3.Rows.Clear();
            for (int i = 0; i < db.cars.Count; i++)
                dataGridView3.Rows.Add(db.cars[i].id, db.cars[i].id_model.model + " " + db.cars[i].car, db.cars[i].probeg, db.cars[i].Gas);
            dataGridView4.Rows.Clear();
            for (int i = 0; i < db.couriers.Count; i++)
                dataGridView4.Rows.Add(db.couriers[i].id, db.couriers[i].courier, db.couriers[i].prava, db.couriers[i].id_car.id_model.model + " " + db.couriers[i].id_car.car);
            dataGridView5.Rows.Clear();
            for (int i = 0; i < db.travelings.Count; i++)
            {
                if (db.travelings[i].status_inRf == 0 && db.travelings[i].date_traveling.Date >= dateTimePicker1.Value.Date && db.travelings[i].date_traveling.Date <= dateTimePicker2.Value.Date)
                {
                    dataGridView5.Rows.Add(db.travelings[i].number, db.travelings[i].date_traveling.ToString("dd MMMM yyyy"), db.travelings[i].id_courier.courier, db.travelings[i].id_car.id_model.model + " " + db.travelings[i].id_car.car,
                                        db.travelings[i].s_probeg_1, db.travelings[i].e_probeg_1, db.travelings[i].t_probeg_all, db.travelings[i].S_gas_1, db.travelings[i].E_gas_1,
                                        db.travelings[i].T_gas_all, db.travelings[i].R_gas_1, db.travelings[i].Z_gas_1, db.travelings[i].P_gas_1, db.travelings[i].P_traveling_all);
                }
            }
            dataGridView6.Rows.Clear();
            for (int i = 0; i < db.travelings.Count; i++)
            {
                if (db.travelings[i].status_inRf == 1 && db.travelings[i].date_traveling.Date >= dateTimePicker4.Value.Date && db.travelings[i].date_traveling.Date <= dateTimePicker3.Value.Date)
                { 
                    dataGridView6.Rows.Add(db.travelings[i].number, db.travelings[i].date_traveling.ToString("dd MMMM yyyy"), db.travelings[i].id_courier.courier, db.travelings[i].id_car.id_model.model + " " + db.travelings[i].id_car.car,
                                        db.travelings[i].s_probeg_1, db.travelings[i].e_probeg_1, db.travelings[i].S_gas_1, db.travelings[i].E_gas_1, db.travelings[i].R_gas_1, db.travelings[i].Z_gas_1,
                                        db.travelings[i].s_probeg_2, db.travelings[i].e_probeg_2, db.travelings[i].S_gas_2, db.travelings[i].E_gas_2, db.travelings[i].R_gas_2, db.travelings[i].Z_gas_2,
                                        db.travelings[i].t_probeg_all, db.travelings[i].T_gas_all, db.travelings[i].P_traveling_all);
                
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crud_gases form = new crud_gases(db, this);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            crud_models form = new crud_models(db, this);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            crud_cars form = new crud_cars(db, this);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            crud_courier form = new crud_courier(db, this);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            create_traveling form = new create_traveling(db, this);
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            create_traveling form = new create_traveling(db, this);
            form.Show();
        }

        private void dataGridView5_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Traveling traveling = db.travelings.Find(x => x.number == (int)dataGridView5.CurrentRow.Cells[0].Value);
                close_traveling form = new close_traveling(db, this, traveling);
                form.Show();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView6_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Traveling traveling = db.travelings.Find(x => x.number == (int)dataGridView6.CurrentRow.Cells[0].Value);
                close_traveling_rf form = new close_traveling_rf(db, this, traveling);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Traveling traveling = db.travelings.Find(x => x.number == (int)dataGridView5.CurrentRow.Cells[0].Value);

                Excel.Workbook xlWB;
                Excel.Worksheet xlSht;

                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlWB = xlApp.Workbooks.Open(Application.StartupPath + @"\list.xls");
                xlSht = xlWB.Worksheets["Шаблон"];

                xlSht.Cells[7, 10] = traveling.date_traveling.ToString("dd          MMMM          yyyy") + " г.";
                xlSht.Cells[6, 27] = traveling.number;
                xlSht.Cells[10, 1] = traveling.id_car.id_model.model;
                xlSht.Cells[10, "S"] = traveling.id_car.car;
                xlSht.Cells[16, "A"] = traveling.id_courier.courier;
                xlSht.Cells[16, "V"] = traveling.id_courier.prava;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                update_gridviewtravelingslist(0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                update_gridviewtravelingslist(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Traveling traveling = db.travelings.Find(x => x.number == (int)dataGridView6.CurrentRow.Cells[0].Value);

                Excel.Workbook xlWB;
                Excel.Worksheet xlSht;

                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlWB = xlApp.Workbooks.Open(Application.StartupPath + @"\list_rf.xls");
                xlSht = xlWB.Worksheets["Шаблон"];

                xlSht.Cells[7, 10] = traveling.date_traveling.ToString("dd          MMMM          yyyy") + " г.";
                xlSht.Cells[6, 27] = traveling.number;
                xlSht.Cells[10, 1] = traveling.id_car.id_model.model;
                xlSht.Cells[10, "S"] = traveling.id_car.car;
                xlSht.Cells[16, "A"] = traveling.id_courier.courier;
                xlSht.Cells[16, "V"] = traveling.id_courier.prava;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                update_gridviewtravelingslist(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                update_gridviewtravelingslist(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
