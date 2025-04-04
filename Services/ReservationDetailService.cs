using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public class ReservationDetailService(DBContext dbContext) : IReservationDetailService
    {
        private readonly DBContext _dbContext = dbContext;

        public async Task CreateReservationDetail(ReservationDetail newDetail)
        {
            await _dbContext.ReservationDetails.AddAsync(newDetail);

            _dbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservationDetail>> GetAllDetailsByReservationID(ObjectId reservationId)
            => _dbContext.ReservationDetails.Where(r => r.ReservationID == reservationId);
    }
}