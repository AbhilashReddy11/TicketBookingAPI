using TB.DataAccess.Models;

namespace TB.DataAccess.Repository.IRepository
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
        Task<Employee> UpdateAsync(Employee entity);
    }
}
