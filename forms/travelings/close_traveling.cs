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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                traveling.P_traveling_1 = traveling.id_gasstation.get_price_traveling(db, traveling, traveling.T_gas_all);
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
    }
}
