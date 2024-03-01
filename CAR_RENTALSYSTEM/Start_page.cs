namespace CAR_RENTALSYSTEM
{
    public partial class Start_page : Form
    {
        public Start_page()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Car_RentalsDashboard bc = new Car_RentalsDashboard();
            bc.Show();
        }

        private void Start_page_Load(object sender, EventArgs e)
        {

        }
    }
}