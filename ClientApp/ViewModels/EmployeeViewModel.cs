using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task1.Commands;
using Task1.ViewModels;

namespace ClientApp.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        #region Declarations
        private string name;
        private int age;
        private string city;
        private string street;
        private int number;
        private string phoneNumber;
        private string position;
        private string department;
        public event EventHandler<EmployeeAddedEventArgs> EmployeeAdded;
        private DelegateCommand addCommand;
        #endregion

        public EmployeeViewModel() { }

        #region Properties
        public string Name 
        { 
            get { return name; }
            set { name = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        public int Age 
        {
            get { return age; }
            set { age = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        public string City 
        { 
            get { return city; }
            set { city = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        public string Street
        {
            get { return street; }
            set { street = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        public int Number
        { 
            get { return number; }
            set { number = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        public string PhoneNumber 
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        public string Position 
        { 
            get { return position; }
            set { position = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        public string Department 
        {
            get { return department; }
            set { department = value; OnPropertyChanged(); AddCommand.RaiseCanExecuteChanged(); }
        }
        #endregion

        #region Commands
        public DelegateCommand AddCommand
        {
            get
            {
                if (addCommand == null)
                    addCommand = new DelegateCommand(Add, CanAdd);
                return addCommand;
            }
        }
        #endregion

        #region Methods
        private void Add(object o)
        {
            Employee employee = new Employee();
            employee.Name = name;
            employee.Age = age;
            Address address = new Address();
            address.Street = street;
            address.City = city;
            address.Number = number;
            employee.Address = address;
            employee.PhoneNumber = phoneNumber;
            employee.Position = position;
            employee.Department = department;

            EmployeeAddedEventArgs args = new EmployeeAddedEventArgs();
            args.Employee = employee;
            OnEmployeeAdded(args);

            if (o != null)
               (o as Window).Close(); 
        }

        private bool CanAdd(object o)
        {
            return !string.IsNullOrWhiteSpace(name) && age != 0 &&
                   !string.IsNullOrWhiteSpace(city) && number != 0 &&
                   !string.IsNullOrWhiteSpace(street) &&
                   !string.IsNullOrWhiteSpace(position) &&
                   !string.IsNullOrWhiteSpace(department);
        }

        protected virtual void OnEmployeeAdded(EmployeeAddedEventArgs e)
        {
            EventHandler<EmployeeAddedEventArgs> handler = EmployeeAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }
}
