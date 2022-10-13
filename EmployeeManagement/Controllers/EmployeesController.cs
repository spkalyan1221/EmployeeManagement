using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Domainmodel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmpDbContextClass empDbContextClass;
        public EmployeesController(EmpDbContextClass empDbContextClass)
        {
            this.empDbContextClass = empDbContextClass;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var employees = await empDbContextClass.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddEmployeeViewModel AddEmployeeRequest)
        {
            var employee = new Employee()
            {
                ID = Guid.NewGuid(),
                Name = AddEmployeeRequest.Name,
                Department = AddEmployeeRequest.Department,
                Email = AddEmployeeRequest.Email,
                Sex = AddEmployeeRequest.Sex,
                MartialStatus= AddEmployeeRequest.MartialStatus,
                Salary = AddEmployeeRequest.Salary,
                DateOfBirth = AddEmployeeRequest.DateOfBirth
            };
            await empDbContextClass.Employees.AddAsync(employee);
            await empDbContextClass.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> View(Guid ID)
        {
          var employee =  await empDbContextClass.Employees.FirstOrDefaultAsync(x =>x.ID == ID);
            if (employee != null)
            {


                var viewModel = new UpdateEmployeeViewModel()
                {
                    ID = employee.ID,
                    Name = employee.Name,
                    Department = employee.Department,
                    Email = employee.Email,
                    Sex = employee.Sex,
                    MartialStatus = employee.MartialStatus,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth

                };
                return await Task.Run(()=>View("View",viewModel));
            }
            return RedirectToAction("Index");
        }


        [HttpPost]

        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee= await empDbContextClass.Employees.FindAsync(model.ID);

            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Department = model.Department;
                employee.Email = model.Email;
                employee.Sex = model.Sex;
                employee.MartialStatus = model.MartialStatus;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
              await  empDbContextClass.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult>Delete(UpdateEmployeeViewModel model)
        {
            var employee= await empDbContextClass.Employees.FindAsync(model.ID);

            if(employee != null)
            {
                empDbContextClass.Employees.Remove(employee);
                await empDbContextClass.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
