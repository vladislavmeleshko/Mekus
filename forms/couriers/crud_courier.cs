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

namespace Mekus.forms.couriers
{
    public partial class crud_courier : Form
    {
        Database db = null;
        Main main = null;
        Courier courier = new Courier();

        public crud_courier()
        {
            InitializeComponent();
        }

        public crud_courier(Database db, Main main)
        {
            InitializeComponent();
            this.db = db;
            this.main = main;
            for (int i = 0; i < db.couriers.Count; i++)
                comboBox1.Items.Add(db.couriers[i].courier);
            for (int i = 0; i < db.cars.Count; i++)
                comboBox2.Items.Add(db.cars[i].car);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Введите имя курьера!");
                if (textBox2.Text.Length == 0) throw new Exception("Введите номер прав курьера!");
                if (comboBox2.Text.Length == 0) throw new Exception("Выберите автомобиль курьера!");
                courier.addCourier();
                main.set_values_table();
                Close();
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
                if (textBox1.Text.Length == 0) throw new Exception("Введите имя курьера!");
                if (textBox2.Text.Length == 0) throw new Exception("Введите номер прав курьера!");
                if (comboBox2.Text.Length == 0) throw new Exception("Выберите автомобиль курьера!");
                if (comboBox3.Text.Length == 0) throw new Exception("Выберите видимость курьера!");
                if (comboBox1.Text.Length == 0) throw new Exception("Выберите курьера!");
                courier.editCourier();
                main.set_values_table();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Length == 0) throw new Exception("Выберите курьера!");
                courier.deleteCourier();
                main.set_values_table();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                courier = db.couriers.Find(x => x.courier == comboBox1.Text);
                textBox1.Text = courier.courier;
                textBox2.Text = courier.prava;
                comboBox2.Text = courier.id_car.car;
                comboBox3.SelectedIndex = courier.is_active;
                comboBox3.SelectedItem = courier.is_active;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                courier.id_car = db.cars.Find(x => x.car == comboBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                courier.courier = textBox1.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                courier.prava = textBox2.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                courier.is_active = comboBox3.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
