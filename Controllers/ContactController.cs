using Microsoft.AspNetCore.Mvc;
using MinhTienHairSalon.Services;
using MinhTienHairSalon.ViewModels;

public class ContactController(IMessageService messageService) : Controller
{
    private IMessageService _messageService = messageService;
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult SendMessage(MessageViewModel model)
    {
        if (ModelState.IsValid)
        {
            _messageService.CreateMessage(model.Message);
            ViewBag.Message = "Successful";
            return View("Successful");
        }

        return RedirectToAction("Index");
    }
}   