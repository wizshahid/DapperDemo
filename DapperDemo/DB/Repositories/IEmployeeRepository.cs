using DapperDemo.Models;
using System;
using System.Collections.Generic;

namespace DapperDemo.DB.Repositories
{
    public interface IEmployeeRepository
    {
        Employee Add(Employee employee);

        Employee Update(Employee employee);

        Employee Get(Guid id);

        IEnumerable<Employee> GetAll();

        void Delete(Guid id);
    }
}
