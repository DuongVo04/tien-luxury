using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public interface IInvoiceService
    {
        public Task<List<Invoice>> GetAll();
        public Task<ObjectId> CreateInvoice(Invoice newInvoice);
        public Task UpdateStatusInvoice(Invoice invoiceToUpdate);
        public Task UpdateTotal(Invoice invoice, decimal Total);
        public Task DeleteInvoice(ObjectId id);
        public Task<IEnumerable<Invoice>> GetAllInvoices();
        public Task<Invoice> GetInvoiceById(ObjectId id);
        public Task<IEnumerable<Invoice>> GetInvoicesByPhoneNumber(string phoneNumber);
    }
}