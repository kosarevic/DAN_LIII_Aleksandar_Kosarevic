using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    public class Employe : User
    {

        public int Floor { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Duty { get; set; }
        public double Salary { get; set; }
        public bool EditSalary { get; set; }
        public int ManagerID { get; set; }

        public Employe()
        {
        }
    }
}
