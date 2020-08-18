using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zadatak_1.Validations
{
    static class OwnerValidation
    {

        public static bool Validate()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"select * from tblManger", conn);
                conn.Open();
                SqlDataReader reader1 = cmd.ExecuteReader();

                if(!reader1.HasRows)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Manager must first exist in database to add employe.", "Notification");
                    return false;
                }

                reader1.Close();
                conn.Close();
            }

            return true;
        }

    }
}
