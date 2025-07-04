namespace OOProjectBasedLeaning
{

    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();

            // è]ã∆àıÇÃçÏê¨
            new EmployeeCreatorForm().Show();

            // â∆
            new HomeForm().Show();

            // âÔé–
            new CompanyForm().Show();

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now=DateTime.Now;
            label1.Text = now.ToString("yyyy/MM/dd HH:mm:ss");
            timer1.Interval = 1000;
        }
    }

}
