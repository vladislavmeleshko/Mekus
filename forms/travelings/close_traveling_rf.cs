using Mekus.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mekus.forms.travelings
{
    public partial class close_traveling_rf : Form
    {
        Database db = null;
        Main main = null;
        Traveling traveling = null;

        public close_traveling_rf()
        {
            InitializeComponent();
        }

        public close_traveling_rf(Database db, Main main, Traveling traveling)
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
            textBox18.Text = Convert.ToString(traveling.id_car.id_model.Rasxod);
            textBox19.Text = Convert.ToString(traveling.Z_gas_2);
            textBox20.Text = Convert.ToString(traveling.id_car.id_model.id_gas.Price);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text.Length != 0)
                {
                    traveling.e_probeg_1 = Convert.ToInt32(textBox6.Text);
                    traveling.t_probeg_1 = traveling.e_probeg_1 - traveling.s_probeg_1;
                    textBox7.Text = Convert.ToString(traveling.t_probeg_1);

                    traveling.T_gas_1 = traveling.t_probeg_1 * traveling.R_gas_1 / 100;
                    textBox10.Text = Convert.ToString(traveling.T_gas_1);

                    traveling.E_gas_1 = traveling.S_gas_1 - traveling.T_gas_1 + traveling.Z_gas_1;
                    textBox9.Text = Convert.ToString(traveling.E_gas_1);

                    traveling.id_car.probeg = traveling.e_probeg_1;
                    traveling.id_car.Gas = traveling.E_gas_1;

                    traveling.s_probeg_2 = traveling.e_probeg_1;
                    textBox14.Text = Convert.ToString(traveling.s_probeg_2);

                    traveling.S_gas_2 = traveling.E_gas_1;
                    textBox17.Text = Convert.ToString(traveling.S_gas_2);

                    if(textBox15.Text.Length != 0)
                        textBox15_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
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

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox15.Text.Length != 0)
                {
                    traveling.e_probeg_2 = Convert.ToInt32(textBox15.Text);
                    traveling.t_probeg_2 = traveling.e_probeg_2 - traveling.s_probeg_2;
                    textBox16.Text = Convert.ToString(traveling.t_probeg_2);

                    traveling.T_gas_2 = traveling.t_probeg_2 * traveling.R_gas_2 / 100;
                    textBox22.Text = Convert.ToString(traveling.T_gas_2);

                    traveling.E_gas_2 = traveling.S_gas_2 - traveling.T_gas_2 + traveling.Z_gas_2;
                    textBox21.Text = Convert.ToString(traveling.E_gas_2);

                    traveling.t_probeg_all = traveling.t_probeg_1 + traveling.t_probeg_2;
                    textBox23.Text = Convert.ToString(traveling.t_probeg_all);

                    traveling.T_gas_all = traveling.T_gas_1 + traveling.T_gas_2;
                    textBox24.Text = Convert.ToString(traveling.T_gas_all);

                    traveling.id_car.probeg = traveling.e_probeg_2;
                    traveling.id_car.Gas = traveling.E_gas_2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.R_gas_2 = Convert.ToDecimal(textBox18.Text);
                if (textBox15.Text.Length != 0)
                    textBox15_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.Z_gas_2 = Convert.ToDecimal(textBox19.Text);
                if (textBox15.Text.Length != 0)
                    textBox15_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.P_gas_2 = Convert.ToDecimal(textBox20.Text);
                if (textBox15.Text.Length != 0)
                    textBox15_TextChanged(sender, e);
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
                if (traveling.Z_gas_2 != 0)
                    traveling.id_gasstation.addGasstation(db, traveling.Z_gas_2, traveling.P_gas_2, traveling);

                traveling.P_traveling_1 = traveling.id_gasstation.get_price_traveling(db, traveling, traveling.T_gas_1);
                traveling.P_traveling_2 = traveling.id_gasstation.get_price_traveling(db, traveling, traveling.T_gas_2);
                
                traveling.P_traveling_all = traveling.P_traveling_1 + traveling.P_traveling_2;
                traveling.id_car.editCar();
                traveling.closeTraveling();
                main.set_values_table();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
