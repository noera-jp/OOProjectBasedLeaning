using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{
    public class EmployeeNameTextBox : TextBox
    {
        private Employee employee = NullEmployee.Instance;

        public EmployeeNameTextBox(Employee employee)
        {
            this.employee = employee;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AutoSize = true;
            Text = employee.Name;
        }

        public void Save()
        {
            employee.Name = Text;
        }
    }

    public class NullEmployeeNameTextBox : EmployeeNameTextBox
    {
        private static readonly EmployeeNameTextBox instance = new NullEmployeeNameTextBox();

        private NullEmployeeNameTextBox() : base(NullEmployee.Instance)
        {
            //this constructor is used for the null object pattem
        }
        public static EmployeeNameTextBox Instance { get { return instance; } }

        public override string Text
        {
            get { return string.Empty; }
            set { /*Do nothing */}//Prevent setting a name for the null object

        }
    }
}
