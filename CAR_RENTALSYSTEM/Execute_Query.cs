using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_RENTALSYSTEM
{
    public partial class Execute_Query : Form
    {
        string connection_string = @"Data Source =(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nusum's\source\repos\CAR_RENTALSYSTEM\CAR RENTALS DB.mdf; Integrated Security=True";

        public Execute_Query()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sqlQuery = "SELECT DISTINCT Model FROM Cars";
            //MessageBox.Show(sqlQuery, "Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand cmd = connection.CreateCommand())
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                connection.Open();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sqlQuery = "SELECT Customer.* FROM Customer JOIN Reservation ON Customer.CustomerID = Reservation.CustomerID JOIN Cars ON Reservation.CarID = Cars.CarID WHERE Cars.Company = 'Tayota'";
            richTextBox1.Text = sqlQuery;
            //MessageBox.Show(sqlQuery, "Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand cmd = connection.CreateCommand())
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                connection.Open();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sqlQuery = "Select * From Payment";
            richTextBox1.Text = sqlQuery;
            //MessageBox.Show(sqlQuery, "Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand cmd = connection.CreateCommand())
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                connection.Open();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)   /* Execute Query Button*/
        {
            DataTable dt = new DataTable();

            string sqlQuery = richTextBox1.Text;
            MessageBox.Show(sqlQuery, "Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand cmd = connection.CreateCommand())
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                connection.Open();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Execute_Query_Load(object sender, EventArgs e)
        {

        }
    }
}
