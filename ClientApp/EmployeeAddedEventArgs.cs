using ClientApp.Models;
using System;

namespace ClientApp
{
    public class EmployeeAddedEventArgs : EventArgs
    {
        public Employee Employee { get; set; }
    }
}
