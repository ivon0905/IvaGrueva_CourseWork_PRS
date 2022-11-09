using ClientApp.Models;
using ClientApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using Task1.Commands;
using Task1.ViewModels;

namespace ClientApp.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        #region Declarations
        private ObservableCollection<Employee> listEmployees;
        private bool canGetEmployeesInfo;
        private bool canSaveEmployeesInfo;
        private bool canAddEmployee; 

        private DelegateCommand getEmployeesInfoCommand;
        private DelegateCommand saveEmployeesInfoCommand;
        private DelegateCommand addNewEmployeeCommand;
        #endregion

        public ClientViewModel()
        {
            canGetEmployeesInfo = true;
            canSaveEmployeesInfo = false;
            canAddEmployee = false;
        }

        #region Properties
        public ObservableCollection<Employee> ListEmployees
        {
            get { return listEmployees; }
        }
        #endregion

        #region Commands
        public DelegateCommand GetEmployeesInfoCommand
        {
            get
            {
                if (getEmployeesInfoCommand == null)
                    getEmployeesInfoCommand = new DelegateCommand(GetEmployeesInfo, CanGetEmployeesInfo);
                return getEmployeesInfoCommand;
            }
        }

        public DelegateCommand SaveEmployeesInfoCommand
        {
            get
            {
                if (saveEmployeesInfoCommand == null)
                    saveEmployeesInfoCommand = new DelegateCommand(SaveEmployeesInfo, CanSaveEmployeesInfo);
                return saveEmployeesInfoCommand;
            }
        }

        public DelegateCommand AddNewEmployeeCommand
        {
            get
            {
                if (addNewEmployeeCommand == null)
                    addNewEmployeeCommand = new DelegateCommand(AddNewEmployee, CanAddEmployee);
                return addNewEmployeeCommand;
            }
        }
#endregion

        #region Methods
        private void GetEmployeesInfo(object o)
        {
            ServiceReference1.WebServiceSoapClient service = 
                new ServiceReference1.WebServiceSoapClient(ServiceReference1.WebServiceSoapClient.EndpointConfiguration.WebServiceSoap);
            Task<ServiceReference1.GetFileResponse> response = service.GetFileAsync();
            ServiceReference1.GetFileResponseBody body = response.Result.Body;
            string result = body.GetFileResult;

            Root root = JsonSerializer.Deserialize<Root>(result);
            listEmployees = new ObservableCollection<Employee>(root.Employees);
            OnPropertyChanged("ListEmployees");
            canGetEmployeesInfo = false;
            GetEmployeesInfoCommand.RaiseCanExecuteChanged();
            canAddEmployee = true;
            AddNewEmployeeCommand.RaiseCanExecuteChanged();
        }

        private bool CanGetEmployeesInfo(object o)
        {
            return canGetEmployeesInfo;
        }

        private void SaveEmployeesInfo(object o)
        {
            Root root = new Root();
            root.Employees = new List<Employee>(listEmployees);
            string employees = JsonSerializer.Serialize(root);

            ServiceReference1.WebServiceSoapClient service 
                = new ServiceReference1.WebServiceSoapClient(ServiceReference1.WebServiceSoapClient.EndpointConfiguration.WebServiceSoap);
            service.SendFileAsync(employees);

            canSaveEmployeesInfo = false;
            SaveEmployeesInfoCommand.RaiseCanExecuteChanged();
        }

        private bool CanSaveEmployeesInfo(object o)
        {
            return canSaveEmployeesInfo;
        }

        private void AddNewEmployee(object o)
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            EmployeeView view = new EmployeeView();
            viewModel.EmployeeAdded += EmployeeAdded;
            view.DataContext = viewModel;
            view.Show();
        }

        private bool CanAddEmployee(object o)
        {
            return canAddEmployee;
        }

        void EmployeeAdded(object sender, EmployeeAddedEventArgs args)
        {
            ListEmployees.Add(args.Employee);
            OnPropertyChanged("ListEmployees");

            canSaveEmployeesInfo = true;
            SaveEmployeesInfoCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
