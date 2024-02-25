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
        public static List<TypeTraveling> typeTravelings = new List<TypeTraveling>() 
        { 
            new TypeTraveling(0, "Минск", ""),
            new TypeTraveling(1, "РФ", ""),
            new TypeTraveling(2, "Барановичи", "БАР"),
            new TypeTraveling(3, "Бобруйск", "БОБ"),
            new TypeTraveling(4, "Борисов", "БОР"),
            new TypeTraveling(5, "Брест", "БРЕ"),
            new TypeTraveling(6, "Витебск", "ВИТ"),
            new TypeTraveling(7, "Волковыск", "ВОЛ"),
            new TypeTraveling(8, "Гомель", "ГО"),
            new TypeTraveling(9, "Гродно", "ГРО"),
            new TypeTraveling(10, "Жлобин", "ЖЛО"),
            new TypeTraveling(11, "Лида", "ЛИД"),
            new TypeTraveling(12, "Могилев", "МОГ"),
            new TypeTraveling(13, "Мозырь", "МОЗ"),
            new TypeTraveling(14, "Молодечно", "МОЛ"),
            new TypeTraveling(15, "Новополоцк", "НОВ"),
            new TypeTraveling(16, "Орша", "ОРШ"),
            new TypeTraveling(17, "Пинск", "ПИН"),
            new TypeTraveling(18, "Речица", "РЕЧ"),
            new TypeTraveling(19, "Светлогорск", "СВЕ"),
            new TypeTraveling(20, "Солигорск", "СОЛ"),
        };

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
                if (db.couriers[i].is_active == 1)
                    comboBox2.Items.Add(db.couriers[i].courier);
            for (int i = 0; i < db.cars.Count; i++)
                comboBox3.Items.Add(db.cars[i].car);
            traveling.date_traveling = dateTimePicker1.Value.Date;
            traveling.id_gasstation = new Gasstation();
            for (int i = 0; i < typeTravelings.Count; i++)
                comboBox1.Items.Add(typeTravelings[i].name);
            comboBox1.SelectedIndex = 0;
            if (db.travelings.Count == 0)
                textBox1.Text = "1";
            else textBox1.Text = get_number_traveling(comboBox1.SelectedIndex);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                traveling.number = textBox1.Text;
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
                traveling.id_courier = db.couriers.Find(x => x.courier == comboBox2.Text);
                comboBox3.Text = traveling.id_courier.id_car.car;
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
                traveling.id_car = db.cars.Find(x => x.car == comboBox3.Text);
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
                textBox1.Text = get_number_traveling(comboBox1.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            { 
                traveling.status_inRf = 0;
                textBox1.Text = get_number_traveling();
            }
            else if (comboBox1.SelectedIndex == 1)
            { 
                traveling.status_inRf = 1;
                textBox1.Text = get_number_traveling();
            }
            else
            { 
                traveling.status_inRf = 2;
                textBox1.Text = get_number_traveling_in_gomel();
            }
        }*/

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            traveling.status_inRf = comboBox1.SelectedIndex;
            textBox1.Text = get_number_traveling(comboBox1.SelectedIndex);
        }

        public string get_number_traveling(int status_inRf)
        {
            try
            {
                if (status_inRf != -1)
                {
                    if (db.travelings.Find(x => x.status_inRf == status_inRf).number == null) throw new NullReferenceException();
                    if ((status_inRf == 0) || (status_inRf == 1))
                    {
                        string number = db.travelings.Find(x => x.status_inRf <= 1).number;
                        return Convert.ToString(Convert.ToInt32(number) + 1);
                    }
                    else if(status_inRf == 8)
                    {
                        Traveling traveling = null;
                        List<Traveling> list = db.travelings.OrderByDescending(x => x.id).ToList();
                        if ((traveling = list.Find(x => x.status_inRf == 8)) != null)
                            return typeTravelings[status_inRf].prefix + Convert.ToString(Convert.ToInt32(traveling.number.Substring(2)) + 1);
                    }
                    else
                    {
                        Traveling traveling = null;
                        List<Traveling> list = db.travelings.OrderByDescending(x => x.id).ToList();
                        if ((traveling = list.Find(x => x.status_inRf == status_inRf)) != null)
                            return typeTravelings[status_inRf].prefix + Convert.ToString(Convert.ToInt32(traveling.number.Substring(3)) + 1);
                    }       
                }
                return null;
            }
            catch(NullReferenceException)
            {
                return typeTravelings[status_inRf].prefix + "1";
            }
            catch (Exception)
            {
                return typeTravelings[status_inRf].prefix + "1";
            }
        }

        /*public string get_number_traveling()
        {
            string number = db.travelings.Find(x => x.status_inRf != 2).number;
            if (number != null)
                return Convert.ToString(Convert.ToInt32(number) + 1);
            else
                return "1";
        }

        public string get_number_traveling_in_gomel()
        {
            try
            {
                Traveling traveling = null;
                if ((traveling = db.travelings.Find(x => x.status_inRf == 2)) != null)
                    return "ГО" + Convert.ToString(Convert.ToInt32(traveling.number.Substring(2)) + 1);
                else
                    return "ГО230";
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }*/

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
