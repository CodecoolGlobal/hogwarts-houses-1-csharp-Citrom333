using Microsoft.AspNetCore.Mvc;

namespace HogwartsHouses.Controllers
{
    // [ApiController, Route("/")]
    public class GreetingController : Controller
    {
        public string Greeting(string name = "Witches and Wizards")
        {
            return $"Welcome to Hogwarts, {name}";
        }
        [HttpGet("{name?}")]
        public IActionResult Index(string name = "Witches and Wizards")
        {
            ViewData["Greeting"] = Greeting(name);
            return View();
        }
    }
}
