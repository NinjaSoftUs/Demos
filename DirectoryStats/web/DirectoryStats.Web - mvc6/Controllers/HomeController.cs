﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace DirectoryStats.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
          

            return View();
        }

        public IActionResult Scan()
        {
           

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}