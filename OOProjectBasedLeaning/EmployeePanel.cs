using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{

    public class EmployeePanel : DragDropPanel
    {

        private Employee employee = NullEmployee.Instance;
        private EmployeeNameTextBox employeeNameTextBox = NullEmployeeNameTextBox.Instance;

        public EmployeePanel()
        {

            InitializeComponent();

        }

        public EmployeePanel(Employee employee)
        {

            this.employee = employee;

            InitializeComponent();

        }

        private void InitializeComponent()
        {

            AutoSize = true;

            // 従業員の状態ラベルを作成
            EmployeeStatusLabel employeeStatusLabel = new EmployeeStatusLabel(employee)
            {

                Location = new Point(20, 10),

            };
            Controls.Add(employeeStatusLabel);

            // 従業員名のラベルを作成
            EmployeeNameLabel employeeNameLabel = new EmployeeNameLabel(employee)
            {

                Location = new Point(20, 30),
                ForeColor = Color.Black,

            };
            Controls.Add(employeeNameLabel);

            // 従業員名のテキストボックスを作成
            employeeNameTextBox = new EmployeeNameTextBox(employee)
            {

                Location = new Point(140, 26),

            };
            Controls.Add(employeeNameTextBox);

        }

        protected override void OnPanelMouseDown()
        {
            DoDragDropMove();

            if (GetForm() is not EmployeeCreatorForm)
            {

                // 従業員名のテキストボックスを編集不可にして非表示にする
                employeeNameTextBox.ReadOnly = true;
                employeeNameTextBox.Hide();

                try
                {

                    if (GetForm() is CompanyForm)
                    {

                        // 会社フォームにドロップされた場合、「出勤」する
                        employee.Go2Company().ClockIn();

                    }
                    else if (GetForm() is HomeForm)
                    {

                        // ホームフォームにドロップされた場合、「退勤」する
                        employee.ClockOut();

                        // 従業員が帰宅する
                        employee.Go2Home();

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

                // 従業員名のテキストボックスを編集可能にして表示する
                employeeNameTextBox.ReadOnly = false;
                employeeNameTextBox.Show();

            }
        }

        public void AddHome(Home home)
        {

            employee.AddHome(home);

        }
        public void AddCompany(Company company)
        {
            employee.AddCompany(company);
        }
        
        public void RemoveCompany()
        {

            employee.RemoveCompany();

        }

        public void RemoveHome()
        {

            employee.RemoveHome();

        }

    }

}
