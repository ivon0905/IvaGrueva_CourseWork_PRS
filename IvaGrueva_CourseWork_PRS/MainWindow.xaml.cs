using Task1.ViewModels;
using System.Windows;

namespace IvaGrueva_CourseWork_PRS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CurrenciesViewModel();
        }
    }
}
