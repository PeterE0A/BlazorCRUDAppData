using BlazorCRUDApp.Entities;
using System;

namespace BlazorCRUDApp.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(Guid id);

        bool UpdateEmployee(Employee updateemployee);

        bool AddEmployee(Employee employee);

        public bool DeleteEmployee(Employee employee);
    }
}
