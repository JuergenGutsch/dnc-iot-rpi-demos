﻿using System.Threading;
using Microsoft.AspNet.Mvc;
using Raspberry.IO.GeneralPurpose;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadSensors.Controllers
{
    public class GpioController : Controller
    {

        public GpioController()
        {

        }


        // GET: /<controller>/
        public IActionResult Sensors()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Gpio11On()
        {
            var led = ConnectorPin.P1Pin11.ToProcessor();
            
            var driver = new FileGpioConnectionDriver();

            driver.Allocate(led, PinDirection.Output);

            driver.Write(led, true);
            Thread.Sleep(500);
            driver.Write(led, false);

            driver.Release(led);

            return Json(new { Gpio11On = true });
        }

        [HttpPost]
        public JsonResult Gpio11Off()
        {
            var led = ConnectorPin.P1Pin11.ToProcessor();
            var driver = GpioConnectionSettings.DefaultDriver;

            driver.Allocate(led, PinDirection.Output);

            driver.Write(led, true);
            Thread.Sleep(500);
            driver.Write(led, false);

            driver.Release(led);

            return Json(new { Gpio11On = false });
        }

        [HttpGet]
        public JsonResult ReadAcceleratorSensor()
        {
            var pin3 = ConnectorPin.P1Pin3.ToProcessor();
            var pin5 = ConnectorPin.P1Pin5.ToProcessor();

            var driver = GpioConnectionSettings.DefaultDriver;

            driver.Allocate(pin3, PinDirection.Input);
            driver.Allocate(pin5, PinDirection.Input);

            driver.Read(pin3);

            var input3 = pin3.Input();
            

            return Json(null);
        }

    }
}
