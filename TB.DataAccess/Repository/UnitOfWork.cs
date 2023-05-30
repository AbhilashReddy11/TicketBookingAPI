
using Microsoft.Extensions.Configuration;
using TB.DataAccess.Data;
using TB.DataAccess.Repository.IRepository;

namespace TB.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        private string secretKey;


        public IUserRepository User { get; private set; }
        public IEventRepository Event { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");

            User = new UserRepository(_db,configuration);
            Event = new EventRepository(_db);
           Booking = new BookingRepository(_db);
        }

        }
}
