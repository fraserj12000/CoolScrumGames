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
        public void OnGet()
        {
        }

        public IActionResult OnPost()
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

     

            var mailMessage = new MailMessage(from, toEmail)
            {
                Subject = "Here is your temporary access code.",
                Body = "Your code is: ",
                IsBodyHtml = false,
            };

            Client.Send(mailMessage);



            return RedirectToPage("/account/login");

        }
    }

    public class Credential
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       

    }
}
