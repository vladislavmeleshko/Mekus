using Mekus.classes;
using Mekus.forms.cars;
using Mekus.forms.couriers;
using Mekus.forms.gases;
using Mekus.forms.models;
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
                dataGridView3.Rows.Add(db.cars[i].id, db.cars[i].car, db.cars[i].id_model.model, db.cars[i].probeg, db.cars[i].Gas);
            dataGridView4.Rows.Clear();
            for (int i = 0; i < db.couriers.Count; i++)
                dataGridView4.Rows.Add(db.couriers[i].id, db.couriers[i].courier, db.couriers[i].prava, db.couriers[i].id_car.car, db.couriers[i].id_car.id_model.model);
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
    }
}
