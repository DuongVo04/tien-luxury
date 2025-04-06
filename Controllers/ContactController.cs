using Microsoft.AspNetCore.Mvc;
using MinhTienHairSalon.Services;
using MinhTienHairSalon.ViewModels;

public class ContactController(IMessageService messageService) : Controller
{
    private IMessageService _messageService = messageService;
    public IActionResult Index()
        => View();
    
    public IActionResult SendMessage(MessageViewModel model)
    {
        if (ModelState.IsValid)
        {
            _messageService.CreateMessage(model.Message);
            ViewBag.Message = "Successful"; 
            return RedirectToAction("Successful");
        }

        return RedirectToAction("Index");
    }

    public IActionResult Successful()
        => View();
    
}   