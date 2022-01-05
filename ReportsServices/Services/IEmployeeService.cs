using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsDomain.Models;

using Task = System.Threading.Tasks.Task;

namespace ReportsServices.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(Employee employee);

        Task<Employee> FindByName(NameEmployee name);

        Task<Employee> FindById(Guid id);

        Task Delete(Guid id);

        Task<List<Employee>> GetAllEmployees();

        Task AddTask(Employee employee, WorkTask workTask);
        Task<Employee> AddChief(Employee employee, Employee chief);

        bool Exist(Guid id);
    }
}