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

namespace Mekus.forms.models
{
    public partial class crud_models : Form
    {
        Database db = null;
        Main main = null;
        Model model = new Model();

        public crud_models()
        {
            InitializeComponent();
        }

        public crud_models(Database db, Main main)
        {
            InitializeComponent();
            this.db = db;
            this.main = main;
            for (int i = 0; i < db.models.Count; i++)
                comboBox1.Items.Add(db.models[i].model);
            for (int i = 0; i < db.gases.Count; i++)
                comboBox2.Items.Add(db.gases[i].gas);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Укажите название модели!");
                if (textBox2.Text.Length == 0) throw new Exception("Укажите расход модели!");
                if(comboBox2.Text.Length == 0) throw new Exception("Укажите вид топлива для модели!");
                model.addModel();
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
                if (textBox1.Text.Length == 0) throw new Exception("Укажите название модели!");
                if (textBox2.Text.Length == 0) throw new Exception("Укажите расход модели!");
                if (comboBox2.Text.Length == 0) throw new Exception("Укажите вид топлива для модели!");
                if (comboBox1.Text.Length == 0) throw new Exception("Укажите модели для обновления!");
                model.editModel();
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
                if (comboBox1.Text.Length == 0) throw new Exception("Укажите модели для удаления!");
                model.deleteModel();
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
                model = db.models.Find(x => x.model == comboBox1.Text);
                textBox1.Text = model.model;
                textBox2.Text = Convert.ToString(model.Rasxod);
                comboBox2.Text = model.id_gas.gas;
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
                model.id_gas = db.gases.Find(x => x.gas == comboBox2.Text);
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
                model.model = textBox1.Text;
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
                model.Rasxod = Convert.ToDecimal(textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
