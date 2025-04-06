using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MinhTienHairSalon.Areas.Admin.ViewModels;
using MinhTienHairSalon.Areas.Filter;
using MinhTienHairSalon.Services;
using MongoDB.Bson;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    [AdminAuth]
    [Area("Admin")]
    [DesktopOnly]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class MessagesManagementController(IMessageService messagesService) : Controller
    {
        private readonly IMessageService _messagesService = messagesService;

        public async Task<IActionResult> Index()
        {
            MessageListViewModel model = new MessageListViewModel()
            {
                Messages = await _messagesService.GetAllMessage()
            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(ObjectId id)
        {
            var message = await _messagesService.GetMessageById(id);
            if (message == null)
            {
                return Json(new { success = false });
            }

            await _messagesService.DeleteMessage(message);

            var redirectUrl = Url.Action("Index", "MessagesManagement");
            return Json(new { success = true, redirectUrl });
        }

    }
}