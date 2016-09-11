using MVVMLighTut1.Model;

namespace MVVMLighTut1.Infrastructure.Messaging
{
    public class EmployeeSaved : Message
    {
        public EmployeeSaved(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; private set; }
    }
}