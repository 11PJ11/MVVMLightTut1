using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVMLighTut1.Infrastructure.Messaging;
using MVVMLighTut1.Model;
using MVVMLighTut1.Model.Services;

namespace MVVMLighTut1.ViewModel
{
    public class SaveEmployeeViewModel : ViewModelBase
    {
        private readonly IDeliverMessagesToViewModels _messageBus;
        private readonly IDataAccessService _dataAccessService;

        public SaveEmployeeViewModel(
            IDataAccessService dataAccessService, IDeliverMessagesToViewModels messageBus)
        {
            _dataAccessService = dataAccessService;
            _messageBus = messageBus;
            _messageBus.SubscribeTo<EmployeeSelected>(this, m => Employee = m.Employee);
            SaveCommand = new RelayCommand<Employee>(SaveEmployee);
        }

        Employee _employee = new Employee();
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        public RelayCommand<Employee> SaveCommand { get; set; }
        void SaveEmployee(Employee employee)
        {
            var employeeNumber = _dataAccessService.CreateEmployee(Employee);
            if (employeeNumber == 0)
            {
                return;
            }
            _messageBus.SendMessage(new EmployeeSaved(Employee));
            RaisePropertyChanged("Employee");
        }
    }
}
