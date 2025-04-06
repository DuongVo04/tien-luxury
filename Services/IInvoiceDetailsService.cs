using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public interface IInvoiceDetailsService
    {
        public Task CreateInvoiceDetail(ObjectId invoiceId, ObjectId productId, int quantity);
        public Task CreateInvoiceDetail(InvoiceDetail detail);
        public Task DeleteInvoiceDetail(ObjectId invoiceId);
        public Task<List<InvoiceDetail>> GetDetailsByInvoiceId(ObjectId invoiceId);
    }
}