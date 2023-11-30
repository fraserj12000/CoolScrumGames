using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoolScrumGames.Pages.Account
{
    public class AccessCodeModel : IndexModel 
    {
        public static int Attempts = 4 - Validated;

        public AccessCodeModel(ILogger<IndexModel> logger, IConfiguration configuration) : base(logger, configuration)
        {
        }

        public static Boolean authenticaded { get; set; }
        public override void OnGet()
        {
            authenticaded = false;
        }

        public override IActionResult OnPost()
        {
            string code = Request.Form["accessCode"];
            var masterCode = _configuration["EmailSettings:MasterPassword"];
            string test = Convert.ToString(masterCode);
            Console.WriteLine(test);


            if (Code.Equals(code) || code.Equals(masterCode))
            {
                authenticaded = true;
                return RedirectToPage("/MainPage");
            }
            else if(Validated > 2)
            {
                return RedirectToPage("/account/login");
            }
            else
            {
                Attempts--;
                Validated++;
                return RedirectToPage("/account/accesscode");
            }
            
        }
    }
}
