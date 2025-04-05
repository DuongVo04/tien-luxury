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

        public async Task<IActionResult> DeleteMessage(ObjectId id)
        {
            var message = await _messagesService.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }
            await _messagesService.DeleteMessage(message);
            return RedirectToAction("Index", "MessagesManagement");
        }
    }
}