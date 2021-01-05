using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string Address { get; set; }
        public string EmpName { get; set; }

        public DateTime Dateofbirth { get; set; }
        public string pincode { get; set; }
        public string Gender { get; set; }

        public string EmailId { get; set; }


    }
}
