using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    class OwnerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<int> Floors { get; set; }

        public OwnerViewModel()
        {
            Manager = new Manager();
            Employe = new Employe();
            FillList();
        }

        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand("select Floor from tblManger;", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Floors == null)
                    Floors = new ObservableCollection<int>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Floors.Add(int.Parse(row[0].ToString()));
                }
            }
        }

        private Manager manager;

        public Manager Manager
        {
            get { return manager; }
            set
            {
                if (manager != value)
                {
                    manager = value;
                    OnPropertyChanged("Manager");
                }
            }
        }

        private Employe employe;

        public Employe Employe
        {
            get { return employe; }
            set
            {
                if (employe != value)
                {
                    employe = value;
                    OnPropertyChanged("Employe");
                }
            }
        }

        private List<string> educations;

        public List<string> Educations
        {
            get { return new List<string> { "I", "II", "III", "IV", "V", "VI", "VII" }; }
            set { educations = value; }
        }

        private List<string> duties;

        public List<string> Duties
        {
            get { return new List<string> { "Cleaning", "Cooking", "Overviewing", "Reporting" }; }
            set { duties = value; }
        }

        private List<string> genders;

        public List<string> Genders
        {
            get { return new List<string> { "M", "F", "X" }; }
            set { genders = value; }
        }

        public void AddManager()
        {
            //User password is encrypted before being deposited in database.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(manager.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"insert into tblManger values (@FirstName, @LastName, @DateOfBirth, @Mail, @Username, @Password, @Floor, @Experience, @EducationLevel);", conn);
                cmd.Parameters.AddWithValue("@FirstName", manager.FirstName);
                cmd.Parameters.AddWithValue("@LastName", manager.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", manager.DateOfBirth.ToShortDateString());
                cmd.Parameters.AddWithValue("@Mail", manager.Mail);
                cmd.Parameters.AddWithValue("@Username", manager.Username);
                cmd.Parameters.AddWithValue("@Password", hash);
                cmd.Parameters.AddWithValue("@Floor", manager.Floor);
                cmd.Parameters.AddWithValue("@Experience", manager.Experience);
                cmd.Parameters.AddWithValue("@EducationLevel", manager.EducationLevel);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Clinic manager successfully created.", "Notification");
            }
        }

        public void AddEmloye()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand("select ManagerID from tblManger where Floor = @Floor;", conn);
                query.Parameters.AddWithValue("@Floor", employe.Floor);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    employe.ManagerID = int.Parse(row[0].ToString());
                }
                conn.Close();
            }

            //User password is encrypted before being deposited in database.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(employe.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"insert into tblEmploye values (@FirstName, @LastName, @DateOfBirth, @Mail, @Username, @Password, @Floor, @Gender, @Citizenship, @Duty, @Salary, @ManagerID);", conn);
                cmd.Parameters.AddWithValue("@FirstName", employe.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employe.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employe.DateOfBirth.ToShortDateString());
                cmd.Parameters.AddWithValue("@Mail", employe.Mail);
                cmd.Parameters.AddWithValue("@Username", employe.Username);
                cmd.Parameters.AddWithValue("@Password", hash);
                cmd.Parameters.AddWithValue("@Floor", employe.Floor);
                cmd.Parameters.AddWithValue("@Gender", employe.Gender);
                cmd.Parameters.AddWithValue("@Citizenship", employe.Citizenship);
                cmd.Parameters.AddWithValue("@Duty", employe.Duty);
                cmd.Parameters.AddWithValue("@Salary", employe.Salary);
                cmd.Parameters.AddWithValue("@ManagerID", employe.ManagerID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Employe successfully created.", "Notification");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
