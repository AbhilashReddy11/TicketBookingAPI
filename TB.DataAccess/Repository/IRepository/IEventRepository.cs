using TB.DataAccess.Models;

namespace TB.DataAccess.Repository.IRepository
{
    public interface IEventRepository:IRepository<Event>
    {
        Task<Event> UpdateAsync(Event entity);
    }
}
