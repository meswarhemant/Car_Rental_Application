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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CAR_RENTALSYSTEM
{
    public partial class Payments : Form
    {
        public Payments()
        {
            InitializeComponent();
        }

        string connection_string = @"Data Source =(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nusum's\source\repos\CAR_RENTALSYSTEM\CAR RENTALS DB.mdf; Integrated Security=True";
        DataBaseControl sql = new DataBaseControl();
        int indexRow;

        private void Payments_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection_string))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Payment", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView1.DataSource = dtbl;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql.Param("@PaymentID", PaymentID.Text);
            sql.Param("@RentalID", RentalID.Text);
            sql.Param("@Amount", textBox3.Text);
            sql.Param("@PaymentDate", dateTimePicker1.Text);
            sql.Param("@PaymentMethod", comboBox1.Text);
            sql.query("insert into Payment (PaymentID, RentalID, Amount, PaymentDate, PaymentMethod) values(@PaymentID, @RentalID, @Amount, @PaymentDate, @PaymentMethod)");
            if (sql.Check4error(true))
            {
                return;
            }
            MessageBox.Show("New Data Entered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Payments_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg_dial = " ";
            sql.query("SELECT * FROM Payment WHERE PaymentID = '" + PaymentID.Text + "'");
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                if ((int)sql.dt.Rows.Count > 0)
                {
                    command.CommandText = "update Payment set RentalId=@RentalID, Amount=@Amount, PaymentDate=@PaymentDate, PaymentMethod=@PaymentMethod where PaymentID = @PaymentID";
                    command.Parameters.AddWithValue("@PaymentID", PaymentID.Text);
                    command.Parameters.AddWithValue("@RentalID", RentalID.Text);
                    command.Parameters.AddWithValue("@Amount", textBox3.Text);
                    command.Parameters.AddWithValue("@PaymentDate", dateTimePicker1.Text);
                    command.Parameters.AddWithValue("@PaymentMethod", comboBox1.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    msg_dial = "Updated Successfully!";
                }
                else
                {
                    sql.Param("@PaymentID", PaymentID.Text);
                    sql.Param("@RentalID", RentalID.Text);
                    sql.Param("@Amount", textBox3.Text);
                    sql.Param("@PaymentDate", dateTimePicker1.Text);
                    sql.Param("@PaymentMethod", comboBox1.Text);
                    sql.query("insert into Payment (PaymentID, RentalID, Amount, PaymentDate, PaymentMethod) values(@PaymentID, @RentalID, @Amount, @PaymentDate, @PaymentMethod)");
                    if (sql.Check4error(true))
                    {
                        msg_dial = "Entered Data is Unique! Updated Successfully";
                        return;
                    }
                }


            }

            MessageBox.Show(msg_dial, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Payments_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg_dial_del = " ";
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = "delete Payment where [PaymentID] = @PaymentID";
                command.Parameters.AddWithValue("@PaymentID", PaymentID.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                msg_dial_del = "Deleted Successfully!";
            }
            MessageBox.Show(msg_dial_del, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Payments_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            PaymentID.Text = row.Cells[0].Value.ToString();
            RentalID.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[3].Value.ToString();
            comboBox1.Text = row.Cells[4].Value.ToString();
        }
    }
}
