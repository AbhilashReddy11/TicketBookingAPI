using TB.DataAccess.Data;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;

namespace TB.DataAccess.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {

            _db.Bookings.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
