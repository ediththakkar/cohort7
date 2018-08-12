using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //GET: /HelloWorld
        //public string Index()
        //{
        //    return "This is my default action";
        //}

        public IActionResult index()
        {
            return View();
        }    

        //Get: /Welcome/
        public IActionResult Welcome(string name, int ID = 1)
        {
            //return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");
            ViewData["Messages"] = $"Hello {name}";
            ViewData["NumTimes"] = ID;

            return View();
        }
    }
}