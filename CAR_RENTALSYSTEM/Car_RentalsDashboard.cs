using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_RENTALSYSTEM
{
    public partial class Car_RentalsDashboard : Form
    {
        public Car_RentalsDashboard()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rental ren = new Rental();
            ren.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cars ca = new Cars();
            ca.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customers cus = new Customers();
            cus.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Payments pa = new Payments();
            pa.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reservation re = new Reservation();
            re.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Execute_Query ex = new Execute_Query();
            ex.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Car_RentalsDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
