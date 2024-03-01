using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CAR_RENTALSYSTEM
{
    class DataBaseControl
    {
        SqlConnection lms_con = new SqlConnection(@"Data Source =(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nusum's\source\repos\CAR_RENTALSYSTEM\CAR RENTALS DB.mdf; Integrated Security=True");
        SqlCommand cm;
        public SqlDataAdapter da;
        List<SqlParameter> Params = new List<SqlParameter>();
        public DataTable dt;
        public int recordcount;
        public string exception;

        public void query(string name)
        {
            recordcount = 0;
            exception = null;
            try
            {
                lms_con.Open();
                cm = new SqlCommand(name, lms_con);
                Params.ForEach(p => cm.Parameters.Add(p));
                Params.Clear();
                dt = new DataTable();
                da = new SqlDataAdapter(cm);
                recordcount = da.Fill(dt);

            }
            catch (Exception e)
            {
                exception = "Problem: " + e.Message;

            }
            finally
            {
                if (lms_con.State == ConnectionState.Open)
                {
                    lms_con.Close();
                }
            }
        }

        public void Param(string name, object value)
        {
            SqlParameter newparam = new SqlParameter(name, value);
            Params.Add(newparam);

        }
        public bool Check4error(bool err = false)
        {
            if (string.IsNullOrEmpty(exception))
            {
                return false;
            }
            if (err == true)
            {
                MessageBox.Show(exception, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            return true;
        }
    }
}
