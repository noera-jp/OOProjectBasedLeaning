 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{

    public partial class CompanyForm : Form
    {

        private Company company = NullCompany.Instance; 
        private TimeTrackerPanel timeTrackerPanel;
        private Label employeeNamesLabel;


        public CompanyForm()
        {

            InitializeComponent();

            company = new CompanyModel("MyCompany");

            // TODO: タイムレコーダーのパネルを設置する
            new TimeTrackerModel(company);

            Text = company.Name;

            // 
            // timeTrackerPanel
            // 
            timeTrackerPanel = new TimeTrackerPanel(new TimeTrackerModel(company));
            timeTrackerPanel.Location = new Point(480, 20);
            timeTrackerPanel.Name = "timeTrackerPanel";
            timeTrackerPanel.Size = new Size(280, 280);
            timeTrackerPanel.TabIndex = 0;
            Controls.Add(timeTrackerPanel);

            employeeNamesLabel = new Label
            {

                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Regular),

            };
            Controls.Add(employeeNamesLabel);

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {

            StringBuilder employeeNames = new StringBuilder();
            company.Employees().ForEach(employee =>
            {

                employeeNames.Append(employee.Name);
                employeeNames.Append("\n");

            });

            employeeNamesLabel.Text = employeeNames.ToString();

        }
    }

}
