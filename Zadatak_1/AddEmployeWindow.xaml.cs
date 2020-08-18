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
using Zadatak_1.Validations;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for AddEmployeWindow.xaml
    /// </summary>
    public partial class AddEmployeWindow : Window
    {
        OwnerViewModel ovm = new OwnerViewModel();

        public AddEmployeWindow()
        {
            InitializeComponent();
            DataContext = ovm;
            ovm.Employe.FirstName = "";
            ovm.Employe.LastName = "";
            ovm.Employe.Mail = "";
            ovm.Employe.Username = "";
            ovm.Employe.Password = "";
            ovm.Employe.Citizenship = "";
            ovm.Employe.Gender = "";
            ovm.Employe.Duty = "";
        }

        private void Btn_Confirm(object sender, RoutedEventArgs e)
        {
            ovm.Employe.DateOfBirth = DateTime.Parse(date.ToString());
            if (AddEmployeValidation.Validate(ovm.Employe))
            {
                ovm.AddEmloye();
                OwnerWindow window = new OwnerWindow();
                window.Show();
                Close(); 
            }
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            OwnerWindow window = new OwnerWindow();
            window.Show();
            Close();
        }
    }
}
