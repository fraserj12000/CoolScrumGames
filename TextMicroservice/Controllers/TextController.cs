//This TextController handles HTTP GET requests to /api/text by reading text from a file
//and returning it in a JSON response


using Microsoft.AspNetCore.Mvc; //allows you to define and use controllers, actions, and HTTP-related attributes
using System; //provides fundamental types and low-level file and I/O operations
using System.IO; //used for file input and output operations
using static System.Net.WebRequestMethods;

namespace TextService.Controllers
{
    [Route("api/text")] //specifies route
    [ApiController]
    public class TextController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetText()
        {
            //attempts to read text from a file, and if an error occurs, it will be caught and handled
            try
            {
                //sets the path to the text file that you want to read
                string filePath = "AboutUs.txt";
                //reads the content of the text file
                string text = System.IO.File.ReadAllText(filePath);

                return Ok(new { Text = text });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve text from the file." });
            }
        }
    }
}





