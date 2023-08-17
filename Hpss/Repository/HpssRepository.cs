
using AutoMapper;
using Dapper;
using Hpss.Data;
using Hpss.Interface;
using Hpss.Model;
using Laptop.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Hpss.Repository
{
    public class HpssRepository : IHpssRepository
    {
        private readonly DapperDbContext _dapperdbContext;
        private readonly IMapper _mapper;

        public HpssRepository(DapperDbContext dapperDbContext, IMapper mapper)
        {
            _dapperdbContext = dapperDbContext ?? throw new ArgumentNullException(nameof(dapperDbContext));
            _mapper = mapper ?? throw new ArgumentNullException();
        }

        public async Task<List<Employees>> GetEmployees()
        {
            string sqlquery = "SELECT * FROM Employees";
            using (var db = _dapperdbContext.CreateConnection())
            {
                var emplist = await db.QueryAsync<Employees>(sqlquery);
                return emplist.ToList();
            }
        }

        public async Task<string> Create(Employees employees)
        {
            string sqlquery = "INSERT INTO Employees (Title,FirstName,LastName,Gender,Email,CompanyId) " +
                "values (@Title,@FirstName,@LastName,@Gender,@Email,@CompanyId)";
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("Title", employees.Title);
            parameters.Add("FirstName", employees.FirstName);
            parameters.Add("LastName", employees.LastName);
            parameters.Add("Gender", employees.Gender);
            parameters.Add("Email", employees.Email);
            parameters.Add("CompanyId", employees.CompanyId);
            using (var db = _dapperdbContext.CreateConnection())
            {
                await db.ExecuteAsync(sqlquery, parameters);
                response = "Complete";
            }
            return response;
        }

        public async Task<string> Update(Employees employees, int id)
        {
            string sqlquery = "UPDATE Employees SET Title=@Title, FirstName=@FirstName, Lastname=@LastName, Gender=@Gender, Email=@Email, CompanyId=@CompanyId where id=@id";
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("Id", employees.Id);
            parameters.Add("Title", employees.Title);
            parameters.Add("FirstName", employees.FirstName);
            parameters.Add("LastName", employees.LastName);
            parameters.Add("Gender", employees.Gender);
            parameters.Add("Email", employees.Email);
            parameters.Add("CompanyId", employees.CompanyId);
            using (var db = _dapperdbContext.CreateConnection())
            {
                await db.ExecuteAsync(sqlquery, parameters);
                response = "Complete";
            }
            return response;
        }

        public async Task<string> Delete(int id)
        {
            string sqlquery = "DELETE FROM Employees WHERE Id=@Id";
            string response = string.Empty;
            using (var db = _dapperdbContext.CreateConnection())
            {
                var emplist = await db.ExecuteAsync(sqlquery, new {id});
                response = "Complete";
            }
            return response;
        }

        public async Task<List<Employees>> GetEmployeesbyId(int CompanyId)
        {
            string sqlquery = "sp_getemployeebycompanyid";
            using (var db = _dapperdbContext.CreateConnection())
            {
                var emplist = await db.QueryAsync<Employees>(sqlquery, new { CompanyId }, commandType:CommandType.StoredProcedure);
                return emplist.ToList();
            }
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            string sqlquery = "SELECT * FROM UserTable";
            using (var db = _dapperdbContext.CreateConnection())
            {
                var emplist = await db.QueryAsync<User>(sqlquery);
                var data = _mapper.Map<List<UserModel>>(emplist);
                return data;             
            }
        }



    }
}
