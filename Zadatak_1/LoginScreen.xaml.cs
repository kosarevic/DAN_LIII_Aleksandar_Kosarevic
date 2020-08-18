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

        public static Manager CurrentManager = new Manager();

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            List<string> text = new List<string>();
            Owner owner = new Owner();

            StreamReader sr = new StreamReader(@"..\\..\Files\OwnerAccess.txt");
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
                Close();
                return;
            }

            CurrentManager = null;

            //Inserted value in password field is being converted into enrypted verson for latter matching with database version.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            //User is extracted from the database matching inserted paramaters Username and Password.
            SqlCommand query = new SqlCommand("SELECT * FROM tblManger WHERE Username=@Username AND Password=@Password", sqlCon);
            query.CommandType = CommandType.Text;
            query.Parameters.AddWithValue("@Username", txtUsername.Text);
            query.Parameters.AddWithValue("@Password", hash);
            sqlCon.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                CurrentManager = new Manager
                {
                    Id = int.Parse(row[0].ToString()),
                    FirstName = row[1].ToString(),
                    LastName = row[2].ToString(),
                    DateOfBirth = DateTime.Parse(row[3].ToString()),
                    Mail = row[4].ToString(),
                    Username = row[5].ToString(),
                    Password = row[6].ToString(),
                    Floor = int.Parse(row[7].ToString()),
                    Experience = int.Parse(row[8].ToString()),
                    EducationLevel = row[9].ToString()
                };
            }
            sqlCon.Close();

            if (CurrentManager != null)
            {
                ManagerWindow window = new ManagerWindow();
                window.Show();
                Close();
                return;
            }

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Incorrect login credentials, please try again.", "Notification");
        }
    }
}
