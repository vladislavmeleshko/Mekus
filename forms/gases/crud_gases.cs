using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mekus.classes;

namespace Mekus.forms.gases
{
    public partial class crud_gases : Form
    {
        Database db = null;
        Main main = null;
        Gas gas = new Gas();
        public crud_gases()
        {
            InitializeComponent();
        }

        public crud_gases(Database db, Main main)
        {
            InitializeComponent();
            this.db = db;
            this.main = main;
            for (int i = 0; i < db.gases.Count; i++)
                comboBox1.Items.Add(db.gases[i].gas);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Укажите название топлива!");
                if (textBox2.Text.Length == 0) throw new Exception("Укажите стоимость топлива за 1 литр!");
                gas.addGas();
                main.set_values_table();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении вида топлива! " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Укажите название топлива!");
                if (textBox2.Text.Length == 0) throw new Exception("Укажите стоимость топлива за 1 литр!");
                if(comboBox1.Text.Length == 0) throw new Exception("Выберите вид топлива для дальнейшего его обновления!");
                gas.editGas();
                main.set_values_table();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении вида топлива! " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Length == 0) throw new Exception("Выберите вид топлива для дальнейшего его удаления!");
                gas.deleteGas();
                main.set_values_table();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении вида топлива! " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gas = db.gases.Find(x => x.gas == comboBox1.Text);
                textBox1.Text = gas.gas;
                textBox2.Text = Convert.ToString(gas.Price);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при выборе вида топлива! " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gas.gas = textBox1.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вводе названия вида топлива! " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gas.Price = Convert.ToDecimal(textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вводе стоимости вида топлива! " + ex.Message);
            }
        }
    }
}
