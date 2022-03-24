using Dapper;
using DapperDemo.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace DapperDemo.DB.Repositories
{
    public class EmployeeRepository : DapperConnection, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Employee Add(Employee employee)
        {
            if (employee.Id == Guid.Empty)
                employee.Id = Guid.NewGuid();

            var query = @"INSERT INTO Employee (Id, Name, Address, Gender, Salary, Age) 
                        VALUES (@Id, @Name, @Address, @Gender, @Salary, @Age)";

            var parameters = new DynamicParameters();

            parameters.Add("@Id", employee.Id, DbType.Guid);
            parameters.Add("@Name", employee.Name, DbType.String);
            parameters.Add("@Address", employee.Address, DbType.String);
            parameters.Add("@Gender", employee.Gender, DbType.String);
            parameters.Add("@Salary", employee.Salary, DbType.Int32);
            parameters.Add("@Age", employee.Age, DbType.Int32);

            using (var connection = CreateConnection())
            {
                connection.Execute(query, parameters);
            }
            return employee;
        }

        public void Delete(Guid id)
        {
            var query = "DELETE FROM Employee WHERE Id = @Id";
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Guid);

            using (var connection = CreateConnection())
            {
                connection.Execute(query, parameter);
            }
        }

        public Employee Get(Guid id)
        {
            var query = "SELECT * FROM Employee WHERE Id = @Id";
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Guid);
            using (var connection = CreateConnection())
            {
                return connection.QueryFirst<Employee>(query, parameter);
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            var query = "SELECT * FROM Employee";
            using (var connection = CreateConnection())
            {
                return connection.Query<Employee>(query);
            }
        }

        public Employee Update(Employee employee)
        {
            var query = @"UPDATE Employee SET Name = @Name, Address = @Address, Gender = @Gender,
                          Salary = @Salary, Age = @Age WHERE Id = @Id";
            var parameters = new DynamicParameters();

            parameters.Add("@Id", employee.Id, DbType.Guid);
            parameters.Add("@Name", employee.Name, DbType.String);
            parameters.Add("@Address", employee.Address, DbType.String);
            parameters.Add("@Gender", employee.Gender, DbType.String);
            parameters.Add("@Salary", employee.Salary, DbType.Int32);
            parameters.Add("@Age", employee.Age, DbType.Int32);

            using (var connection = CreateConnection())
            {
                connection.Execute(query, parameters);
            }
            return employee;
        }
    }
}
