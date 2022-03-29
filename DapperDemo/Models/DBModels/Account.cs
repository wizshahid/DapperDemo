using System;

namespace DapperDemo.Models.DBModels
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
