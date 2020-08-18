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
    class ManagerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employe> Employes { get; set; }

        public ManagerViewModel()
        {
            Employe = new Employe();
            FillList();
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

        private double amount;

        public double Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }

        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand("select * from tblEmploye where ManagerID = @ManagerID;", conn);
                query.Parameters.AddWithValue("@ManagerID", LoginScreen.CurrentManager.Id);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Employes == null)
                    Employes = new ObservableCollection<Employe>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Employe e = new Employe
                    {
                        Id = int.Parse(row[0].ToString()),
                        FirstName = row[1].ToString(),
                        LastName = row[2].ToString(),
                        DateOfBirth = DateTime.Parse(row[3].ToString()),
                        Mail = row[4].ToString(),
                        Username = row[5].ToString(),
                        Password = row[6].ToString(),
                        Floor = int.Parse(row[7].ToString()),
                        Gender = row[8].ToString(),
                        Citizenship = row[9].ToString(),
                        Duty = row[10].ToString(),
                        Salary = double.Parse(row[11].ToString()),
                        ManagerID = int.Parse(row[12].ToString())
                    };

                    if (e.Duty == "Overviewing" || e.Duty == "Reporting")
                        e.EditSalary = true;

                    Employes.Add(e);
                }
            }
        }

        public void GeneralSalary(object sender, DoWorkEventArgs e, int Id)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"update tblEmploye set Salary=@Salary where EmployeID=@EmployeID", conn);
                cmd.Parameters.AddWithValue("@Salary", amount);
                cmd.Parameters.AddWithValue("@EmployeID", Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
