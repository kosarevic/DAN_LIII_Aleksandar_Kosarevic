using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Validations
{
    static class AddManagerValidation
    {

        public static bool Validate(Manager manager)
        {
            if (manager.FirstName.Length < 1 || !manager.FirstName.All(Char.IsLetter))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect first name, try again.", "Notification");
                return false;
            }
            if (manager.LastName.Length < 1 || !manager.LastName.All(char.IsLetter))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect last name, try again.", "Notification");
                return false;
            }
            if (manager.DateOfBirth.ToShortDateString() == "1/1/0001")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect date of birth, try again.", "Notification");
                return false;
            }
            if (int.Parse(manager.DateOfBirth.ToString("yyyy")) > int.Parse(DateTime.Now.ToString("yyyy")))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Date of birth suggests that you are born in the future, please try again.", "Notification");
                return false;
            }
            if (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(manager.DateOfBirth.ToString("yyyy")) > 100)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests that you lived longer than 100 years, please try again.", "Notification");
                return false;
            }
            if ((int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(manager.DateOfBirth.ToString("yyyyMMdd"))) / 10000 < 18)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests are under aged (less than 18 y/o), please try again.", "Notification");
                return false;
            }
            if (manager.Mail == "" || manager.Mail == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Mail is incorrect, please try again.", "Notification");
                return false;
            }
            if (manager.Username == "" || manager.Username == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Username is incorrect, please try again.", "Notification");
                return false;
            }
            else
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    var cmd = new SqlCommand(@"select Username from tblManger where Username = @Username", conn);
                    cmd.Parameters.AddWithValue("@Username", manager.Username);
                    conn.Open();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        if (reader1[0].ToString() == manager.Username)
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Username already exists in database, try again.", "Notification");
                            return false;
                        }
                    }
                    reader1.Close();
                    conn.Close();
                }

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    var cmd = new SqlCommand(@"select Username from tblEmploye where Username = @Username", conn);
                    cmd.Parameters.AddWithValue("@Username", manager.Username);
                    conn.Open();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        if (reader1[0].ToString() == manager.Username)
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Username already exists in database, try again.", "Notification");
                            return false;
                        }
                    }
                    reader1.Close();
                    conn.Close();
                }
            }
            if (manager.Password == "" || manager.Password == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Password is incorrect, please try again.", "Notification");
                return false;
            }
            if (manager.Floor == 0 || manager.Floor > 50)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Floor is incorrect (must be 1-50), please try again.", "Notification");
                return false;
            }
            if(manager.Experience > 50)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Experience is incorrect (must be 0-50), please try again.", "Notification");
                return false;
            }
            if(manager.EducationLevel == null || manager.EducationLevel == "")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Education is incorrect, please try again.", "Notification");
                return false;
            }
            return true;
        }
    }
}
