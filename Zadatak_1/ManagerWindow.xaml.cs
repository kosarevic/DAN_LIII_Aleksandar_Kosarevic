using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        ManagerViewModel mvm = new ManagerViewModel();

        public ManagerWindow()
        {
            InitializeComponent();
            DataContext = mvm;
        }

        private void GeneralSalary(object sender, RoutedEventArgs e)
        {
            AllEmployesSalaryWindow window = new AllEmployesSalaryWindow();
            window.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginScreen window = new LoginScreen();
            window.Show();
            Close();
        }

        private void EditSalary(object sender, RoutedEventArgs e)
        {

        }
    }
}
