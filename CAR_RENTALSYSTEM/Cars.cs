using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_RENTALSYSTEM
{
    public partial class Cars : Form
    {

        string connection_string = @"Data Source =(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nusum's\source\repos\CAR_RENTALSYSTEM\CAR RENTALS DB.mdf; Integrated Security=True";
        DataBaseControl sql = new DataBaseControl();
        int indexRow;

        public Cars()
        {
            InitializeComponent();
        }
        private void Cars_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection_string))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Cars", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView1.DataSource = dtbl;

            }

        }
        private void button1_Click(object sender, EventArgs e)  /*Insert*/
        {
            sql.Param("@CarID", CarID.Text);
            sql.Param("@Company", comboBox1.Text);
            sql.Param("@Model", textBox3.Text);
            sql.Param("@Year", textBox4.Text);
            sql.Param("@LicensePlate", textBox5.Text);
            sql.Param("@Color", textBox6.Text);
            sql.Param("@CurrentMileage", textBox7.Text);
            sql.query("insert into Cars (CarID, Company, Model, Year, LicensePlate, Color, CurrentMileage) values(@CarID, @Company, @Model, @Year, @LicensePlate, @Color, @CurrentMileage)");
            if (sql.Check4error(true))
            {
                return;
            }
            MessageBox.Show("New Data Entered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cars_Load(sender, e);

        }

        private void button2_Click(object sender, EventArgs e) /*Update*/
        {
            string msg_dial = " ";
            sql.query("SELECT * FROM Cars WHERE CarID = '" + CarID.Text + "'");
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                if ((int)sql.dt.Rows.Count > 0)
                {
                    command.CommandText = "update Cars set Company=@Company,Model=@Model,[Year]=@Year, LicensePlate=@LicensePlate, Color=@Color, CurrentMileage=@CurrentMileage where CarID = @CarID";
                    command.Parameters.AddWithValue("@CarID", CarID.Text);
                    command.Parameters.AddWithValue("@Company", comboBox1.Text);
                    command.Parameters.AddWithValue("@Model", textBox3.Text);
                    command.Parameters.AddWithValue("@Year", textBox4.Text);
                    command.Parameters.AddWithValue("@LicensePlate", textBox5.Text);
                    command.Parameters.AddWithValue("@Color", textBox6.Text);
                    command.Parameters.AddWithValue("@CurrentMileage", textBox7.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    msg_dial = "Updated Successfully!";
                }
                else
                {
                    sql.Param("@CarID", CarID.Text);
                    sql.Param("@Company", comboBox1.Text);
                    sql.Param("@Model", textBox3.Text);
                    sql.Param("@Year", textBox4.Text);
                    sql.Param("@LicensePlate", textBox5.Text);
                    sql.Param("@Color", textBox6.Text);
                    sql.Param("@CurrentMileage", textBox7.Text);
                    sql.query("insert into Cars (CarID, Company, Model, Year, LicensePlate, Color, CurrentMileage) values(@CarID, @Company, @Model, @Year, @LicensePlate, @Color, @CurrentMileage)");
                    if (sql.Check4error(true))
                    {
                        msg_dial = "Entered Data is Unique! Updated Successfully";
                        return;
                    }
                }


            }

            MessageBox.Show(msg_dial, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cars_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)  /*Delete*/
        {
            string msg_dial_del = " ";
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = "delete Cars where [CarID] = @CarID";
                command.Parameters.AddWithValue("@CarID", CarID.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                msg_dial_del = "Deleted Successfully!";
            }
            MessageBox.Show(msg_dial_del, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cars_Load(sender, e);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            CarID.Text = row.Cells[0].Value.ToString();
            comboBox1.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();
            textBox5.Text = row.Cells[4].Value.ToString();
            textBox6.Text = row.Cells[5].Value.ToString();
            textBox7.Text = row.Cells[6].Value.ToString();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
