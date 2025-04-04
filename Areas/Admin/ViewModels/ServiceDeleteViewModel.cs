using MongoDB.Bson;

namespace MinhTienHairSalon.Areas.Admin.ViewModels
{
    public class ServiceDeleteViewModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
