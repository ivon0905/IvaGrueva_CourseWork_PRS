using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Task1.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FilesPath { get { return Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory); } }
    }
}
