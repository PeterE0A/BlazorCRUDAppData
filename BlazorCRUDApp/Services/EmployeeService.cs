using BlazorCRUDApp.Entities;
using BlazorCRUDApp.Repositories;

namespace BlazorCRUDApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _repository = employeeRepository;
        }

        private List<Employee> employees = new List<Employee>();


        public void AddEmployee(Employee employee)
        {
            var id = Guid.NewGuid();
            employee.Id = id;
            _repository.AddEmployee(employee);
            employees.Add(employee);
        }

        public void DeleteEmployee(Guid id)
        {
            var employee = _repository.GetEmployee(id);
            _repository.DeleteEmployee(employee);
        }


        public Employee? GetEmployee(Guid id)
        {
            Employee FindDBEmployee = _repository.GetEmployee(id);
            return FindDBEmployee;
        }

        public List<Employee> GetEmployees()
        {
            var emps = _repository.GetEmployees();
            return emps;
        }

        public void UpdateEmployee(Employee employee)
        {
            var dbEmployee = _repository.GetEmployee(employee.Id);
            dbEmployee.Name = employee.Name;
            _repository.UpdateEmployee(dbEmployee);
            employees.Clear();
            employees = _repository.GetEmployees();
        }
    }
}
