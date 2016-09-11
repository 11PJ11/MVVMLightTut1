using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLighTut1.Model.Services
{
    public interface IDataAccessService
    {
        ObservableCollection<Employee> GetEmployees();
        int CreateEmployee(Employee employee);
    }
}
