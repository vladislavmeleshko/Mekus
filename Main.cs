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
                dataGridView3.Rows.Add(db.cars[i].id, db.cars[i].id_model.model + " " + db.cars[i].car, db.cars[i].probeg, db.cars[i].Gas);
            dataGridView4.Rows.Clear();
            for (int i = 0; i < db.couriers.Count; i++)
                dataGridView4.Rows.Add(db.couriers[i].id, db.couriers[i].courier, db.couriers[i].prava, db.couriers[i].id_car.id_model.model + " " + db.couriers[i].id_car.car);
            dataGridView5.Rows.Clear();
            for (int i = 0; i < db.travelings.Count; i++)
            { 
                dataGridView5.Rows.Add(db.travelings[i].number, db.travelings[i].id_courier.courier, db.travelings[i].id_car.id_model.model + " " + db.travelings[i].id_car.car,
                                        db.travelings[i].s_probeg_1, db.travelings[i].e_probeg_1, db.travelings[i].t_probeg_all, db.travelings[i].S_gas_1, db.travelings[i].E_gas_1,
                                        db.travelings[i].T_gas_all, db.travelings[i].R_gas_1, db.travelings[i].Z_gas_1, db.travelings[i].P_gas_1, db.travelings[i].P_traveling_all);
                if (db.travelings[i].status_inRf == 0)
                    comboBox4.Items.Add(db.travelings[i].number);
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text.Length != 0)
            {
                Traveling traveling = db.travelings.Find(x => x.number == Convert.ToInt32(comboBox4.Text));
                close_traveling form = new close_traveling(db, this, traveling);
                form.Show();
            }
        }
    }
}
