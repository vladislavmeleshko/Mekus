using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Mekus.classes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mekus.forms.travelings
{
    public partial class create_traveling : Form
    {
        Database db = null;
        Main main = null;
        Traveling traveling = new Traveling();

        public create_traveling()
        {
            InitializeComponent();
        }

        public create_traveling(Database db, Main main)
        {
            InitializeComponent();
            this.db = db;
            this.main = main;
            for (int i = 0; i < db.couriers.Count; i++)
                comboBox1.Items.Add(db.couriers[i].courier);
            for (int i = 0; i < db.cars.Count; i++)
                comboBox2.Items.Add(db.cars[i].car);
            if (db.travelings.Count == 0)
                textBox1.Text = "1";
            else textBox1.Text = Convert.ToString(db.travelings[0].number + 1);
            traveling.date_traveling = dateTimePicker1.Value.Date;
            traveling.id_gasstation = new Gasstation();
            comboBox3.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.number = Convert.ToInt32(textBox1.Text);
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
                traveling.date_traveling = dateTimePicker1.Value.Date;
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
                traveling.id_courier = db.couriers.Find(x => x.courier == comboBox1.Text);
                comboBox2.Text = traveling.id_courier.id_car.car;
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
                traveling.id_car = db.cars.Find(x => x.car == comboBox2.Text);
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
                traveling.id_gasstation = traveling.id_gasstation.get_gasstation(db, traveling);
                traveling.createTraveling(traveling.status_inRf);
                main.set_values_table();
                // Close();
                textBox1.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
                traveling.status_inRf = 0;
            else if (comboBox3.SelectedIndex == 1)
                traveling.status_inRf = 1;
            else
                traveling.status_inRf = 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Value = dateTimePicker1.Value.Date.AddDays(1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
