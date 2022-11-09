using Task1.Commands;
using Task1.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System;

namespace Task1.ViewModels
{
    public class CurrenciesViewModel : BaseViewModel
    {
        private List<Currencies> currencies;
        private DelegateCommand loadCurrenciesCommand;
        private DelegateCommand saveCurrenciesCommand;

        public CurrenciesViewModel()
        { }

        public List<Currencies> Currencies
        {
            get { return currencies; }
        }

        public DelegateCommand LoadCurrenciesCommand
        {
            get
            {
                if (loadCurrenciesCommand == null)
                {
                    loadCurrenciesCommand = new DelegateCommand(LoadCurrencies);
                }
                return loadCurrenciesCommand;
            }
        }

        public DelegateCommand SaveCurrenciesCommand
        {
            get 
            {
                if (saveCurrenciesCommand == null)
                {
                    saveCurrenciesCommand = new DelegateCommand(SaveCurrencies);
                }
                return saveCurrenciesCommand; 
            }
        }

        private void LoadCurrencies(object o)
        {
            string path = Path.Combine(FilesPath, @"Currencies.json");            

            using (StreamReader r = new StreamReader(path, Encoding.UTF8))
            {
                string curr = r.ReadToEnd();
                if (string.IsNullOrEmpty(curr))
                {
                    return;
                }
                currencies = JsonSerializer.Deserialize<List<Currencies>>(curr);
                OnPropertyChanged("Currencies");
            }
        }

        private void SaveCurrencies(object o)
        {
            string path = Path.Combine(FilesPath, @"newCurrencies.json");
            string json = JsonSerializer.Serialize(currencies);
            File.WriteAllText(path, json);
        }
    }
}
