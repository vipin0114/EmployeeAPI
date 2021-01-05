using EmployeeAPI.Context;
using EmployeeAPI.Model;
using EmployeeAPI.Service;
using System.Collections.Generic;
using System.Linq;

namespace Order_MicroService.DataAccess
{
    public class EmployeeService : IEmployee
    {
        private EmployeeDBContext context= new EmployeeDBContext();

       public  EmployeeService()
        { }
        
        public List<Employee> GetAllEmployee()
        {
           
                return context.Employees.ToList();
            
        }

        public void CreateEmployee(Employee emp)
        {
          
                context.Add<Employee>(emp);
                context.SaveChanges();
            

        }

        public void DeleteEmployee(int emp)
        {
           
                Employee emaployee = context.Employees.Find(emp);


                context.Employees.Remove(emaployee);
                context.SaveChanges();


            
        }

        public Employee GetAllEmployeeById(int Id)
        {
            
                return context.Employees.FirstOrDefault(emp => emp.EmpId == Id);

            
        }

        public void UpdateEmployee(Employee employee)
        {
              Employee objEmp = new Employee();
                objEmp = context.Employees.FirstOrDefault(emp => emp.EmpId == employee.EmpId);
                if (objEmp != null)
                {
                    objEmp.EmpName = employee.EmpName;
                    objEmp.Address = employee.Address;
                    objEmp.EmailId = employee.EmailId;
                    objEmp.Dateofbirth = employee.Dateofbirth;
                    objEmp.Gender = employee.Gender;
                    objEmp.pincode = employee.pincode;

                    context.SaveChanges();
                

            }




        }
    }
}
