using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CoolScrumGames.Pages.Account
{
    public class LoginModel : PageModel
    {
        //credentials model
        [BindProperty]
        public Credential Login { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {

        }
    }

    public class Credential
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       

    }
}
