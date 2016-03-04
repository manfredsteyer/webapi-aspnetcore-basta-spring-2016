using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Controllers
{

    public class HomeController: Controller
    {
        public IActionResult Index() {
            // /Views/Home/Index
            return View();
        }
    }
}
