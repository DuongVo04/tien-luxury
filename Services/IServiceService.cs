using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public interface IServiceService
    {
        public Task AddService(Service newService);

        public Task<IEnumerable<Service>> GetAllServices();

        public Task<IEnumerable<Service>> GetAllServicesActivated();

        public Service? GetServiceByID(ObjectId? Id);

        public Task<string?> GetServiceNameById(ObjectId? Id);

        public Task RemoveService(Service serviceToRemove);
        public Task UpdateService(Service serviceToUpdate);
    }
}
