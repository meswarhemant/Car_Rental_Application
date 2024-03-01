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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }

        string connection_string = @"Data Source =(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nusum's\source\repos\CAR_RENTALSYSTEM\CAR RENTALS DB.mdf; Integrated Security=True";
        DataBaseControl sql = new DataBaseControl();
        int indexRow;

        private void Customers_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection_string))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Customer", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView1.DataSource = dtbl;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql.Param("@CustomerID", CustomerID.Text);
            sql.Param("@Name", textBox2.Text);
            sql.Param("@Address", textBox3.Text);
            sql.Param("@PhoneNumber", textBox4.Text);
            sql.Param("@Email", textBox5.Text);
            sql.query("insert into Customer (CustomerID, Name, Address, PhoneNumber, Email) values(@CustomerID, @Name, @Address, @PhoneNumber, @Email)");
            if (sql.Check4error(true))
            {
                return;
            }
            MessageBox.Show("New Data Entered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Customers_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg_dial = " ";
            sql.query("SELECT * FROM Customer WHERE CustomerID = '" + CustomerID.Text + "'");
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                if ((int)sql.dt.Rows.Count > 0)
                {
                    command.CommandText = "update Customer set Name=@Name, Address=@Address, PhoneNumber=@PhoneNumber, Email=@Email where CustomerID = @CustomerID";
                    command.Parameters.AddWithValue("@CustomerID", CustomerID.Text);
                    command.Parameters.AddWithValue("@Name", textBox2.Text);
                    command.Parameters.AddWithValue("@Address", textBox3.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", textBox4.Text);
                    command.Parameters.AddWithValue("@Email", textBox5.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    msg_dial = "Updated Successfully!";
                }
                else
                {
                    sql.Param("@CustomerID", CustomerID.Text);
                    sql.Param("@Name", textBox2.Text);
                    sql.Param("@Address", textBox3.Text);
                    sql.Param("@PhoneNumber", textBox4.Text);
                    sql.Param("@Email", textBox5.Text);
                    sql.query("insert into Customer (CustomerID, Name, Address, PhoneNumber, Email) values(@CustomerID, @Name, @Address, @PhoneNumber, @Email)");
                    if (sql.Check4error(true))
                    {
                        msg_dial = "Entered Data is Unique! Updated Successfully";
                        return;
                    }
                }


            }

            MessageBox.Show(msg_dial, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Customers_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg_dial_del = " ";
            using (SqlConnection connection = new SqlConnection(connection_string))
            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = "delete Customer where [CustomerID] = @CustomerID";
                command.Parameters.AddWithValue("@CustomerID", CustomerID.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                msg_dial_del = "Deleted Successfully!";
            }
            MessageBox.Show(msg_dial_del, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Customers_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            CustomerID.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();
            textBox5.Text = row.Cells[4].Value.ToString();
        }
    }
}
