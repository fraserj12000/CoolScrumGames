using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;


namespace CoolScrumGames.Pages.Account
{
    public class LoginModel : PageModel
    {
        public Credential Login { get; set; }
        public string Code = "12B45";
        public static int Validated = 0;
        public virtual void OnGet()
        {
        }

        public virtual IActionResult OnPost()
        {
            //email account source
            string toEmail = Request.Form["toEmail"];
            var from = "coolscrumgames@gmail.com";
            var password = "erga tqwz qzru ucfy";

            //sets up email client
            var Client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true,
            };
            
            string codeMessage = "Your code is: " + Code;
            //sets up email contents
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

    public class Credential
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
