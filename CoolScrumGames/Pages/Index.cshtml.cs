using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoolScrumGames.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
           
        }

        public string Code = "12B45";
        public static int Validated = 0;

        public virtual IActionResult OnPost()
        {
            string toEmail = Request.Form["toEmail"];
            var from = "coolscrumgames@gmail.com";
            var password = "erga tqwz qzru ucfy";

            var Client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true,
            };

            string codeMessage = "Your code is: " + Code;

            var mailMessage = new MailMessage(from, toEmail)
            {
                Subject = "Here is your temporary access code.",
                Body = codeMessage,
                IsBodyHtml = false,
            };

            Client.Send(mailMessage);



            return RedirectToPage("/account/accesscode");

        }
    }
}
