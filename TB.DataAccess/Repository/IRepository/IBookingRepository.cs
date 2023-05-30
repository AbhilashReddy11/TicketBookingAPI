using TB.DataAccess.Models;

namespace TB.DataAccess.Repository.IRepository
{
    public interface IBookingRepository:IRepository<Booking>
    {
        Task<Booking> UpdateAsync(Booking entity);
    }
}
