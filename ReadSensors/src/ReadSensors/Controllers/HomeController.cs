using Microsoft.AspNet.Mvc;
using MvcSample.Web.Models;
using Raspberry;

namespace ReadSensors.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(User());
        }

        public User User()
        {
            User user = new User()
            {
                Name = "Jürgen Gutsch",
                Address = "78267 Aach",
                Board = Board.Current
            };

            return user;
        }
    }
}
