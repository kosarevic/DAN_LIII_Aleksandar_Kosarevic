using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Zadatak_1.Model;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            try
            {
                List<string> text = new List<string>();
                Owner owner = new Owner();

                StreamReader sr = new StreamReader(@"..\\..\Files\OwnerCredentials.txt");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    text.Add(line);
                }
                sr.Close();

                if (text.Any())
                {
                    foreach (string t in text)
                    {
                        string[] temp = t.Split(' ');
                        owner.Username = temp[1];
                        owner.Password = temp[3];
                    }
                }

                if (txtUsername.Text == owner.Username && txtPassword.Password == owner.Password)
                {
                    OwnerWindow window = new OwnerWindow();
                    window.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
