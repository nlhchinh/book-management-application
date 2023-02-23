using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS_4
{
    class Employee
    {
        public string empID { get; set; }
        public string empPassword { get; set; }
        public bool empRole { get; set; }

        public Employee(string empID, string empPassword, bool empRole)
        {
            this.empID = empID;
            this.empPassword = empPassword;
            this.empRole = empRole;
        }
    }
}
