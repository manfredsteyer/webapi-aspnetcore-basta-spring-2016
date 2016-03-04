using FlugDemo.Data;
using FlugDemo.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Controllers
{
    public class FlugController: Controller
    {
        [FromServices]
        public IFlugRepository repo { get; set; }

        public FlugController()
        {
            //this.repo = repo;
        }


        // /flug/Index
        // /flug
        public IActionResult Index() {
            var fluege = repo.FindAll();
            return View(fluege);
        }

        // /flug/edit/1
        public IActionResult Edit(int id) {
            var flug = repo.FindById(id);
            return View(flug);
        }

        [HttpPost]
        public IActionResult Edit(Flug flug)
        {
            repo.Save(flug);
            ViewBag.Message = "Flug wurde gespeichert!";
            return View(flug);
            //return View("Success", flug);
        }

    }
}
