using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoolScrumGames.Pages.Account
{
    public class AccessCodeModel : LoginModel 
    {
        public static int Attempts = 4 - Validated;
        public static Boolean authenticaded { get; set; }
        public override void OnGet()
        {
            authenticaded = false;
        }

       
       

        public override IActionResult OnPost()
        {
            string code = Request.Form["accessCode"];
            

            if (Code.Equals(code))
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
