 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOProjectBasedLeaning
{

    public partial class CompanyForm : DragDropForm
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

        protected override void OnFormDragEnterSerializable(DragEventArgs dragEventArgs)
        {

            dragEventArgs.Effect = DragDropEffects.Move;

        }

        protected override void OnFormDragDropSerializable(object? serializableObject, DragEventArgs dragEventArgs)
        {

            if (serializableObject is EmployeePanel)
            {

                EmployeePanel employeePanel = serializableObject as EmployeePanel;
                employeePanel.AddDragDropForm(this, PointToClient(new Point(dragEventArgs.X, dragEventArgs.Y)));
                employeePanel.AddCompany(company);

                UpdateDisplay();

            }

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
