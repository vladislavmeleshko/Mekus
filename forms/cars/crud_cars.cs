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

namespace Mekus.forms.cars
{
    public partial class crud_cars : Form
    {
        Database db = null;
        Main main = null;
        Car car = new Car();

        public crud_cars()
        {
            InitializeComponent();
        }

        public crud_cars(Database db, Main main)
        {
            InitializeComponent();
            this.db = db;
            this.main = main;
            for (int i = 0; i < db.cars.Count; i++)
                comboBox1.Items.Add(db.cars[i].car);
            for (int i = 0; i < db.models.Count; i++)
                comboBox2.Items.Add(db.models[i].model);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Введите гос. номер автомобиля!");
                if (textBox2.Text.Length == 0) throw new Exception("Введите пробег автомобиля!");
                if (textBox3.Text.Length == 0) throw new Exception("Введите остаток топлива автомобиля!");
                if (comboBox2.Text.Length == 0) throw new Exception("Укажите модель автомобиля!");
                if (textBox4.Text.Length == 0) throw new Exception("Введите номер топливной карты автомобиля!");
                car.addCar();
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
                if (textBox1.Text.Length == 0) throw new Exception("Введите гос. номер автомобиля!");
                if (textBox2.Text.Length == 0) throw new Exception("Введите пробег автомобиля!");
                if (textBox3.Text.Length == 0) throw new Exception("Введите остаток топлива автомобиля!");
                if (comboBox2.Text.Length == 0) throw new Exception("Укажите модель автомобиля!");
                if (comboBox1.Text.Length == 0) throw new Exception("Укажите автомобиль!");
                car.editCar();
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
                if (comboBox1.Text.Length == 0) throw new Exception("Укажите автомобиль!");
                car.deleteCar();
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
                car = db.cars.Find(x => x.car == comboBox1.Text);
                textBox1.Text = car.car;
                textBox2.Text = Convert.ToString(car.probeg);
                textBox3.Text = Convert.ToString(car.Gas);
                comboBox2.Text = car.id_model.model;
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
                car.id_model = db.models.Find(x => x.model == comboBox2.Text);
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
                car.car = textBox1.Text;
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
                car.probeg = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                car.Gas = Convert.ToDecimal(textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                car.cardCode = Convert.ToInt32(textBox4.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
