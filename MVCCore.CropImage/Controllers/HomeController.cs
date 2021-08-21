using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCCore.CropImage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MVCCore.CropImage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment Environment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment _environment)
        {
            _logger = logger;
            Environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save()
        {
            string base64 = Request.Form["imgCropped"];
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);

            string filePath = Path.Combine(this.Environment.WebRootPath, "Images", "Cropped.png");
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
