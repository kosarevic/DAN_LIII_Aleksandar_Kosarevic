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

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        public OwnerWindow()
        {
            InitializeComponent();
        }

        private void Add_Employe(object sender, RoutedEventArgs e)
        {
            AddEmployeWindow window = new AddEmployeWindow();
            window.Show();
            Close();
        }

        private void Add_Manager(object sender, RoutedEventArgs e)
        {
            AddManagerWindow window = new AddManagerWindow();
            window.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginScreen window = new LoginScreen();
            window.Show();
            Close();
        }
    }
}
