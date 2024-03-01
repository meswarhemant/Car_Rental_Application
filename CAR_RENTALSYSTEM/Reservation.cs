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
    public partial class Reservation : Form
    {
        string connection_string = @"Data Source =(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nusum's\source\repos\CAR_RENTALSYSTEM\CAR RENTALS DB.mdf; Integrated Security=True";
        DataBaseControl sql = new DataBaseControl();
        int indexRow;

        public Reservation()
        {
            InitializeComponent();
        }


        private void Reservation_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection_string))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Reservation", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView1.DataSource = dtbl;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql.Param("@ReservationID", ReservationID.Text);
            sql.Param("@CarID", CarID.Text);
            sql.Param("@CustomerID", CustomerID.Text);
            sql.Param("@ReservationStartDate", dateTimePicker1.Text);
            sql.Param("@ReservationEndDate", dateTimePicker2.Text);
            sql.query("insert into Reservation (ReservationID, CarID, CustomerID, ReservationStartDate, ReservationEndDate) values(@ReservationID, @CarID, @CustomerID, @ReservationStartDate, @ReservationEndDate)");
            if (sql.Check4error(true))
            {
                return;
            }
            MessageBox.Show("New Data Entered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Reservation_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg_dial = " ";
            sql.query("SELECT * FROM Reservation WHERE ReservationID = '" + ReservationID.Text + "'");
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                if ((int)sql.dt.Rows.Count > 0)
                {
                    command.CommandText = "update Reservation set CarID=@CarID, CustomerID=@CustomerID, ReservationStartDate=@ReservationStartdate, ReservationEndDate=@ReservationEndDate where ReservationID=@ReservationID";
                    command.Parameters.AddWithValue("@ReservationID", ReservationID.Text);
                    command.Parameters.AddWithValue("@CarID", CarID.Text);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID.Text);
                    command.Parameters.AddWithValue("@ReservationStartDate", dateTimePicker1.Text);
                    command.Parameters.AddWithValue("@ReservationEndDate", dateTimePicker2.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    msg_dial = "Updated Successfully!";
                }
                else
                {
                    sql.Param("@ReservationID", ReservationID.Text);
                    sql.Param("@CarID", CarID.Text);
                    sql.Param("@CustomerID", CustomerID.Text);
                    sql.Param("@ReservationStartDate", dateTimePicker1.Text);
                    sql.Param("@ReservationEndDate", dateTimePicker2.Text);
                    sql.query("insert into Reservation (ReservationID, CarID, CustomerID, ReservationStartDate, ReservationEndDate) values(@ReservationID, @CarID, @CustomerID, @ReservationStartDate, @ReservationEndDate)");
                    if (sql.Check4error(true))
                    {
                        msg_dial = "Entered Data is Unique! Updated Successfully";
                        return;
                    }
                }


            }

            MessageBox.Show(msg_dial, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Reservation_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg_dial_del = " ";
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = "delete Reservation where [ReservationID] = @ReservationID";
                command.Parameters.AddWithValue("@ReservationID", ReservationID.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                msg_dial_del = "Deleted Successfully!";
            }
            MessageBox.Show(msg_dial_del, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Reservation_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            ReservationID.Text = row.Cells[0].Value.ToString();
            CarID.Text = row.Cells[1].Value.ToString();
            CustomerID.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[3].Value.ToString();
            dateTimePicker2.Text = row.Cells[4].Value.ToString();


        }


    }
}
