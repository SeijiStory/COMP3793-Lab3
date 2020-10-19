using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using COMP3793_Lab2.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace COMP3793_Lab2.Controllers
{
    public class FilesController : Controller
    {
        private readonly ILogger<FilesController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesController(ILogger<FilesController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_webHostEnvironment);
        }
        public IActionResult Content(string id)
        {
            string tmpContents = null;
            char slash = System.IO.Path.DirectorySeparatorChar;
            string filename = _webHostEnvironment.ContentRootPath + slash + "TextFiles" + slash + id;
            if (System.IO.File.Exists(filename))
                {
                using (StreamReader sr = System.IO.File.OpenText(filename)) {
                    tmpContents = "";
                    string tmp = "";
                    while ((tmp = sr.ReadLine()) != null) {
                        tmpContents += tmp;
                        tmpContents += '\n';
                    }
                }
            }
            return View(new FilesModel() {
                filename = System.IO.Path.GetFileName(id),
                fileContents = tmpContents
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
