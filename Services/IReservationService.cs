using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public interface IReservationService
    {
        public Task<ObjectId> AddReservation(Reservation newReservation);

        public Task UpdateReservationStatus(Reservation reservationToUpdate);

        public Task<IEnumerable<Reservation>> GetAllReservation();

        public Task<Reservation?> GetBookingByID(ObjectId id);
    }
}
