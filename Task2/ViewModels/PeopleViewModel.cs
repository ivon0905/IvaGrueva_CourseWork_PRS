using Task1.Commands;
using Task1.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Task2.Models;

namespace Task2.ViewModels
{
    public class PeopleViewModel : BaseViewModel
    {
        private List<Person> listPeople;
        private DelegateCommand loadPeopleCommand;

        public PeopleViewModel() { }

        public List<Person> ListPeople
        {
            get { return listPeople; }
        }

        public DelegateCommand LoadPeopleCommand 
        {
            get
            {
                if (loadPeopleCommand == null)
                    loadPeopleCommand = new DelegateCommand(LoadPeople);
                return loadPeopleCommand; 
            }
        }

        private void LoadPeople(object o)
        {
            string path = Path.Combine(FilesPath, @"people.json");

            using (StreamReader r = new StreamReader(path, Encoding.UTF8))
            {
                string text = r.ReadToEnd();
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }

                Root root = JsonSerializer.Deserialize<Root>(text);
                listPeople = root.Person;
                OnPropertyChanged("ListPeople");
            }
        }
    }
}
