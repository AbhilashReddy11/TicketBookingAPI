using TB.DataAccess.Data;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;

namespace TB.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Employee> UpdateAsync(Employee entity)
        {

            _db.Employees.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
