using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Areas.Admin.Services
{
    public interface IAdminAccountService
    {
        public bool ChangePassword(ObjectId idAccountToChange, string newPassword, string oldPassword);

        public bool CheckAccount(AdminAccount adminAccount);

        public void CreateAccount(AdminAccount newAdminAccount);

        public AdminAccount FindAccountByID(ObjectId adminID);

        public ObjectId FindIdByUserName(string userName);
    }

}
