using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmailService _emailService;

    public HomeController(ILogger<HomeController> logger, EmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        return View();
    }
                                    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [Route("api/send-email")]
    public IActionResult SendEmail([FromBody] EmailModel emailModel)
    {
        string body = @"<p>Thank you for trying my demo!</p>
                        <p>Please feel free to contact me on LinkedIn or at the provided phone number.</p>
                        <p><strong>LinkedIn:</strong> <a href='[Your LinkedIn Profile URL]' target='_blank'>[Your LinkedIn Profile]</a></p>
                        <p><strong>Phone:</strong> +447568488047</p>";
        _emailService.SendEmail(emailModel.Recipient, "Thank You Trying My Demo", body);

        return Ok(new { Message = "Email sent successfully" });
    }
}
