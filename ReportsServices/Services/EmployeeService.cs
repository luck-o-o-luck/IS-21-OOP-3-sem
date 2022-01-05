using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsDataAccess.DataBase;
using ReportsDomain.Models;
using ReportsDomain.Tools;
using Task = System.Threading.Tasks.Task;

namespace ReportsServices.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDbContext _context;
        
        public EmployeeService(ReportsDbContext context) => _context = context;

        public bool Exist(Guid id) => _context.Employees.Any(employee => employee.Id == id);
        
        public async Task<Employee> Create(Employee employee)
        {
            if (await _context.Employees.FindAsync(employee.Id) != null)
                throw new ReportsException("Employee already exists");
                
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            
            return employee;
        }

        public async Task<Employee> FindByName(NameEmployee name)
        {
            Employee selectedEmployee = await _context.Employees.FirstOrDefaultAsync(employee => employee.Name == name);
            
            return selectedEmployee;
        }

        public async Task<Employee> FindById(Guid id)
        {
            Employee selectedEmployee = await _context.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
            
            return selectedEmployee;
        }

        public async Task Delete(Guid id)
        {
            Employee selectedEmployee =  await _context.Employees.FirstOrDefaultAsync(employee => employee.Id == id);

            if (selectedEmployee is null)
                throw new ReportsException("Employee doesn't exist");

            _context.Employees.Remove(selectedEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployees() => await _context.Employees.ToListAsync();
        
        public async Task AddTask(Employee employee, ReportsDomain.Models.Task task)
        {
            employee.AddTask(task);
            task.SetEmployee(employee);
            
            _context.Update(employee);
            _context.Update(task);

            await _context.SaveChangesAsync();
        }

        public async Task<Employee> AddChief(Employee employee, Employee chief)
        {
            employee.SetChief(chief);
            chief.AddSubordinates(employee);

            _context.Update(employee);
            _context.Update(chief);

            await _context.SaveChangesAsync();
            return employee;
        }
    }
}