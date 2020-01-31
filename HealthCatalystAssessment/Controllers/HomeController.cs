using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Description;
//using System.Web.Http;
using System.IO;

namespace HealthCatalyst.Assessment.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /// <summary>
        /// Quick and dirty Download file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DownloadFile(string name)
        {
            var fileName  = name ?? "HC-AddAndSearch.ps1";
            var file = File(fileName, "application/octet-stream");
            file.FileDownloadName = fileName;
            return file;
        }     
    }
}

