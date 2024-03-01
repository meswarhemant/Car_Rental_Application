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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }


        string connection_string = @"Data Source =(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nusum's\source\repos\CAR_RENTALSYSTEM\CAR RENTALS DB.mdf; Integrated Security=True";
        DataBaseControl sql = new DataBaseControl();
        int indexRow;

        private void Rental_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection_string))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Rental", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView1.DataSource = dtbl;

            }
        }

        private void button1_Click(object sender, EventArgs e)  /*Insert*/
        {
            sql.Param("@RentalID", RentalID.Text);
            sql.Param("@CarID", CarID.Text);
            sql.Param("@CustomerID", CustomerID.Text);
            sql.Param("@RentalStartDate", dateTimePicker1.Text);
            sql.Param("@RentalEndDate", dateTimePicker2.Text);
            sql.Param("@DailyRate", textBox4.Text);
            sql.Param("@TotalCost", textBox5.Text);
            sql.query("insert into Rental (RentalID, CarID, CustomerID, RentalStartDate, RentalEndDate, DailyRate, TotalCost) values( @RentalID, @CarID, @CustomerID, @RentalStartDate, @RentalEndDate, @DailyRate, @TotalCost)");
            if (sql.Check4error(true))
            {
                return;
            }
            MessageBox.Show("New Data Entered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Rental_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg_dial = " ";
            sql.query("SELECT * FROM Rental WHERE RentalID = '" + RentalID.Text + "'");
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                if ((int)sql.dt.Rows.Count > 0)
                {
                    command.CommandText = "update Rental set CarID=@CarID, CustomerID=@CustomerID, RentalStartDate=@RentalStartDate, RentalEndDate=@RentalEndDate, DailyRate=@DailyRate, TotalCost=@TotalCost where RentalID=@RentalID";
                    command.Parameters.AddWithValue("@RentalID", RentalID.Text);
                    command.Parameters.AddWithValue("@CarID", CarID.Text);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID.Text);
                    command.Parameters.AddWithValue("@RentalStartDate", dateTimePicker1.Text);
                    command.Parameters.AddWithValue("@RentalEndDate", dateTimePicker2.Text);
                    command.Parameters.AddWithValue("@DailyRate", textBox4.Text);
                    command.Parameters.AddWithValue("@TotalCost", textBox5.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    msg_dial = "Updated Successfully!";
                }
                else
                {
                    sql.Param("@RentalID", RentalID.Text);
                    sql.Param("@CarID", CarID.Text);
                    sql.Param("@CustomerID", CustomerID.Text);
                    sql.Param("@RentalStartDate", dateTimePicker1.Text);
                    sql.Param("@RentalEndDate", dateTimePicker2.Text);
                    sql.Param("@DailyRate", textBox4.Text);
                    sql.Param("@TotalCost", textBox5.Text);
                    sql.query("insert into Rental (RentalID, CarID, CustomerID, RentalStartDate, RentalEndDate, DailyRate, TotalCost) values(@RentalID, @CarID, @CustomerID, @RentalStartDate, @RentalEndDate, @DailyRate, @TotalCost)");
                    if (sql.Check4error(true))
                    {
                        msg_dial = "Entered Data is Unique! Updated Successfully";
                        return;
                    }
                }


            }

            MessageBox.Show(msg_dial, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Rental_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg_dial_del = " ";
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = "delete Rental where [RentalID] = @rentalID";
                command.Parameters.AddWithValue("@RentalID", RentalID.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                msg_dial_del = "Deleted Successfully!";
            }
            MessageBox.Show(msg_dial_del, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Rental_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            RentalID.Text = row.Cells[0].Value.ToString();
            CarID.Text = row.Cells[1].Value.ToString();
            CustomerID.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[3].Value.ToString();
            dateTimePicker2.Text = row.Cells[4].Value.ToString();
            textBox4.Text = row.Cells[5].Value.ToString();
            textBox5.Text = row.Cells[6].Value.ToString();
        }


    }
}
