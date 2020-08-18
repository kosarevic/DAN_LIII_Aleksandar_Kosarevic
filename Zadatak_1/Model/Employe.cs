using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    class Employe : User
    {

        public int Floor { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Duty { get; set; }
        public double Salary { get; set; }

        public Employe()
        {
        }

        public Employe(int floor, string gender, string citizenship, string duty, double salary) : base()
        {
            Floor = floor;
            Gender = gender;
            Citizenship = citizenship;
            Duty = duty;
            Salary = salary;
        }
    }
}
