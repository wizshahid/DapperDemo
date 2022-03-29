using System;

namespace DapperDemo.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public int Salary { get; set; }

        public int Age { get; set; }
    }
}
