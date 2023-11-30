using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoolScrumGames.Pages
{
	public class MainPageModel : PageModel
    {
        public void OnGet()
        {
            Verify();
            //if (Account.AccessCodeModel.authenticaded) { RedirectToPage("/MainPage"); }
            //else { RedirectToPage("/Index"); }
        }

        public IActionResult Verify()
        {
            return RedirectToPage("/Index");
            if (Account.AccessCodeModel.authenticaded) { return RedirectToPage("/MainPage"); }
            else { return RedirectToPage("/Index"); }
        }

    }
}
