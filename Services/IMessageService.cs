using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public interface IMessageService
    {
        public Task<List<Message>> GetAllMessage();
        public Task CreateMessage(Message message);
        public Task DeleteMessage(Message message);
        public Task<Message> FindMessageById(ObjectId id);

    }
}