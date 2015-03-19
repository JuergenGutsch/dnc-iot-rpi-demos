using Microsoft.AspNet.Mvc;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.GeneralPurpose.Behaviors;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadSensors.Controllers
{
    public class GpioController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Gpio11On()
        {
            var led1 = ConnectorPin.P1Pin11.Output();
            using (var connection = new GpioConnection(led1))
            {
                connection.Toggle(led1);
            }

            return Json(new { Gpio11On = true});
        }

        [HttpPost]
        public JsonResult Gpio11Off()
        {
            var led1 = ConnectorPin.P1Pin11.Output();
            using (var connection = new GpioConnection(led1))
            {
                connection.Toggle(led1);
            }

            return Json(new { Gpio11On = false });
        }
    }
}
