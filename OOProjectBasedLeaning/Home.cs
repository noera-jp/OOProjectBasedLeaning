using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{
   public interface Home :Model, Place
    {

        Home AddEmployee(Employee employee);

        Home RemoveEmployee(Employee employee);

    }

    public class HomeModel : ModelEntity, Home
    {

        private Employee employee = NullEmployee.Instance;

        public HomeModel() : this(string.Empty)
        {

        }

        public HomeModel(string name)
        {

            this.Name = name;

        }

        public Home AddEmployee(Employee employee)
        {

            // TODO: Implement logic to add an employee to the home.
            this.employee = employee;

            return this;

        }

        public Home RemoveEmployee(Employee employee)
        {

            // TODO: Implement logic to remove an employee from the home.
            employee = NullEmployee.Instance;

            return this;

        }

    }

    public class NullHome : ModelEntity, Home, NullObject
    {

        private static readonly Home instance = new NullHome();

        private NullHome()
        {

        }

        public override string Name
        {

            get { return string.Empty; }
            set { /* Do nothing */ }

        }

        public static Home Instance
        {
            get { return instance; }
        }

        public Home AddEmployee(Employee employee)
        {

            return this;

        }

        public Home RemoveEmployee(Employee employee)
        {

            return this;

        }

    }
}
