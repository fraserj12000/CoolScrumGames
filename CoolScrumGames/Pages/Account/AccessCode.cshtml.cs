using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoolScrumGames.Pages.Account
{
    public class AccessCodeModel : LoginModel 
    {
        public static int Attempts = 4 - Validated;
        public override void OnGet()
        {
            
        }

        public override IActionResult OnPost()
        {
            string code = Request.Form["accessCode"];
            

            if (Code.Equals(code))
            {
                return RedirectToPage("/Index");
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
