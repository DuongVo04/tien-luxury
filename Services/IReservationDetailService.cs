using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public interface IReservationDetailService
    {
        public Task CreateReservationDetail(ReservationDetail newDetail);
        public Task<IEnumerable<ReservationDetail>> GetAllDetailsByReservationID(ObjectId reservationId);
    }
}