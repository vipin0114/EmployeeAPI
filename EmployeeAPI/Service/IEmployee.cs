using EmployeeAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Service
{
    public interface IEmployee
    {
        List<Employee> GetAllEmployee();
    }
}
