/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MVVMLighTut1"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MVVMLighTut1.Infrastructure.Messaging;
using MVVMLighTut1.Model.Services;

namespace MVVMLighTut1.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public EmployeesViewModel Employees => ServiceLocator.Current.GetInstance<EmployeesViewModel>();
        public SaveEmployeeViewModel SaveEmployee => ServiceLocator.Current.GetInstance<SaveEmployeeViewModel>();

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            CreateDesignTimeViewServicesAndModels();
            CreateRunTimeViewServicesAndModels();
            CreateCommonViewServiceAndModels();
        }

        private void CreateCommonViewServiceAndModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EmployeesViewModel>();
            SimpleIoc.Default.Register<SaveEmployeeViewModel>();
            SimpleIoc.Default.Register<IDeliverMessagesToViewModels, ViewModelsMessageBus>();
        }

        private void CreateDesignTimeViewServicesAndModels()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataAccessService, DesignTimeDataAccessService>();
            }
        }

        private void CreateRunTimeViewServicesAndModels()
        {
            if (ViewModelBase.IsInDesignModeStatic == false)
            {
                SimpleIoc.Default.Register<IDataAccessService, DesignTimeDataAccessService>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}