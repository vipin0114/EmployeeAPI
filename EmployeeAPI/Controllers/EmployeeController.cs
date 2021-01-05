using EmployeeAPI.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order_MicroService.DataAccess;
using System;
using System.Linq;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Order_MicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AnotherPolicy")]
    public class EmployeeController : ControllerBase
    {
        EmployeeService objEntity = new EmployeeService();

        [HttpGet]
        [Route("AllEmployeeDetails")]
        public IQueryable<Employee> GetEmaployee()
        {
            try
            {
                return objEntity.GetAllEmployee().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public Employee GetEmaployeeById(string employeeId)
        {
            Employee objEmp = new Employee();
            int ID = Convert.ToInt32(employeeId);
            try
            {
                objEmp = objEntity.GetAllEmployeeById(ID);

            }
            catch (Exception)
            {
                throw;
            }

            return objEmp;
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public void PostEmaployee(Employee data)
        {


            try
            {
                objEntity.CreateEmployee(data);

            }
            catch (Exception)
            {
                throw;
            }


        }

        //[HttpPut]
        [Route("UpdateEmployeeDetails")]
        public void PutEmaployeeMaster(Employee employee)
        {

            try
            {

                objEntity.UpdateEmployee(employee);

            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpDelete]
        [Route("DeleteEmployeeDetails/{employeeId}")]
        public void DeleteEmaployee(int employeeId)
        {

            objEntity.DeleteEmployee(employeeId);

        }
    }
}

