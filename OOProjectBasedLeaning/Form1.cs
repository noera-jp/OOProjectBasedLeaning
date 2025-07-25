namespace OOProjectBasedLeaning
{

    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();

            // 従業員の作成
            new EmployeeCreatorForm().Show();

            // 家
            new HomeForm().Show();

            // 会社
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
