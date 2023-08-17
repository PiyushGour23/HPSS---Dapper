using Hpss.Model;
using Laptop.Models;

namespace Hpss.Interface
{
    public interface IHpssRepository
    {
        Task<List<Employees>> GetEmployees();
        Task<string> Create(Employees employees);

        Task<string> Update(Employees employees, int id);
        Task<string> Delete(int id);
        Task<List<Employees>> GetEmployeesbyId(int CompanyId);
        Task<List<UserModel>> GetAllUsers();
    }
}
