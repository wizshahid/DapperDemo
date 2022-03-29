using Dapper;
using DapperDemo.Models.DBModels;
using DapperDemo.Models.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DapperDemo.DB.Repositories
{
    public class AccountRepository : DapperConnection, IAccountRepository
    {
        private readonly IConfiguration configuration;

        public AccountRepository(IConfiguration configuration) : base(configuration)
        {
            this.configuration = configuration;
        }

        public int SignUp(AccountDTO account)
        {
            if (UserNameExists(account.Username))
                return -1;

            var query = @"INSERT INTO Account (Id, Username, Password)
                          VALUES (@Id, @Username, @Password)";

            var parameters = new DynamicParameters();

            parameters.Add("@Id", Guid.NewGuid(), DbType.Guid);
            parameters.Add("@Username", account.Username, DbType.String);
            parameters.Add("@Password", account.Password, DbType.String);

            using (var connection = CreateConnection())
            {
               return connection.Execute(query, parameters);
            }
        }

        public string Login(AccountDTO account)
        {
            var query = "SELECT * FROM Account WHERE Username = @Username AND Password = @Password";

            Account dbAccount;

            var parameters = new DynamicParameters();

            parameters.Add("@Username", account.Username, DbType.String);
            parameters.Add("@Password", account.Password, DbType.String);

            using (var connection = CreateConnection())
            {
               dbAccount = connection.QueryFirstOrDefault<Account>(query, parameters);
            }

            if (dbAccount == null) return null;

            return GenerateToken(dbAccount);
        }

        private string GenerateToken(Account account)
        {
            var securityToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSecret"]));
            var credentials = new SigningCredentials(securityToken, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim("UserId", account.Id.ToString())
            };

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(2), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool UserNameExists(string username)
        {
            var query = "SELECT COUNT(Id) FROM Account WHERE Username = @Username";

            var parameter = new DynamicParameters();
            parameter.Add("@Username", username, DbType.String);

            using (var connection = CreateConnection())
            {
                int count = connection.ExecuteScalar<int>(query, parameter);
                return count > 0;
            }
        }
    }
}
