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
    /// Interaction logic for AddManagerWindow.xaml
    /// </summary>
    public partial class AddManagerWindow : Window
    {
        OwnerViewModel ovm = new OwnerViewModel();

        public AddManagerWindow()
        {
            InitializeComponent();
            DataContext = ovm;
            ovm.Manager.FirstName = "";
            ovm.Manager.LastName = "";
            ovm.Manager.Mail = "";
            ovm.Manager.Username = "";
            ovm.Manager.Password = "";
            ovm.Manager.EducationLevel = "";
        }

        private void Btn_Confirm(object sender, RoutedEventArgs e)
        {
            ovm.Manager.DateOfBirth = DateTime.Parse(date.ToString());
            ovm.AddManager();
            OwnerWindow window = new OwnerWindow();
            window.Show();
            Close();
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            OwnerWindow window = new OwnerWindow();
            window.Show();
            Close();
        }
    }
}
