using System;
using System.Collections.ObjectModel;

namespace MVVMLighTut1.Model.Services
{
    public class DesignTimeDataAccessService : IDataAccessService
    {
        private readonly ObservableCollection<Employee> _employees =
            new ObservableCollection<Employee>()
            {
                new Employee{Number = 1, Name = "Alice", Salary = 20000, Department = "Sales"},
                new Employee{Number = 2, Name = "Bob", Salary = 15000, Department = "Engineering"},
                new Employee{Number = 3, Name = "Charlie", Salary = 18000, Department = "Accounting"},
            };

        public ObservableCollection<Employee> GetEmployees()
        {
            return _employees;
        }

        public int CreateEmployee(Employee employee)
        {
            employee.Number = new Random().Next(100);
            _employees.Add(employee);
            return employee.Number;
        }
    }
}