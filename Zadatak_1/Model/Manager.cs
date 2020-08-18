using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    class Manager : User
    {

        public int Floor { get; set; }
        public int Experience { get; set; }
        public string EducationLevel { get; set; }

        public Manager()
        {
        }

        public Manager(int floor, int experience, string educationLevel) : base()
        {
            Floor = floor;
            Experience = experience;
            EducationLevel = educationLevel;
        }
    }
}
