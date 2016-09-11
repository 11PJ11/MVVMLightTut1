using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVMLighTut1.Infrastructure.Messaging;
using MVVMLighTut1.Model;
using MVVMLighTut1.Model.Services;

namespace MVVMLighTut1.ViewModel
{
    public class EmployeesViewModel : ViewModelBase
    {
        private readonly IDeliverMessagesToViewModels _messageBus;
        private readonly IDataAccessService _dataAccessService;
        private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();

        public EmployeesViewModel(
            IDataAccessService dataAccessService, IDeliverMessagesToViewModels messageBus)
        {
            _dataAccessService = dataAccessService;
            _messageBus = messageBus;
            Employee = new Employee();

            GetAllEmployeesCommand = new RelayCommand(GetEmployees);
            SearchEmployeeCommand = new RelayCommand(SearchEmployee);
            SelectEmployeeCommand = new RelayCommand<Employee>(SelectEmployee);

            _messageBus.SubscribeTo<EmployeeSaved>(this, m => GetEmployees());
        }

        Employee _employee;
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                RaisePropertyChanged("Employees");
            }
        }

        public RelayCommand GetAllEmployeesCommand { get; set; }
        private void GetEmployees()
        {
            Employees.Clear();
            foreach (var employee in _dataAccessService.GetEmployees())
            {
                Employees.Add(employee);
            }
        }

        private string _employeeNameToSearch = string.Empty;
        public string EmployeeNameToSearch
        {
            get { return _employeeNameToSearch; }
            set
            {
                _employeeNameToSearch = value;
                RaisePropertyChanged("EmployeeNameToSearch");
            }
        }

        public RelayCommand SearchEmployeeCommand { get; private set; }
        void SearchEmployee()
        {
            Employees.Clear();
            var employees =
                from employee in _dataAccessService.GetEmployees()
                where employee.Name.StartsWith(EmployeeNameToSearch)
                select employee;

            Employees = new ObservableCollection<Employee>(employees);
        }

        public RelayCommand<Employee> SelectEmployeeCommand { get; private set; }
        void SelectEmployee(Employee employee)
        {
            if (employee != null)
            {
                _messageBus.SendMessage(new EmployeeSelected(employee));
            }
        }
    }
}
