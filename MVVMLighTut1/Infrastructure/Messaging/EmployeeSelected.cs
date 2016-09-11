using MVVMLighTut1.Model;

namespace MVVMLighTut1.Infrastructure.Messaging
{
    public class EmployeeSelected : Message
    {
        public EmployeeSelected(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; private set; }
    }
}
