using FlugDemo.Components;
using FlugDemo.Data;
using FlugDemo.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Api
{
    [Route("api/[controller]")]
    // [Authorize]
    public class FlugController: Controller
    {
        private IFlugRepository repo;

        public FlugController(IFlugRepository repo)
        {
            this.repo = repo;
        }

        // GET api/flug
        [HttpGet]
        public List<Flug> GetAll() {
            return repo.FindAll();
        }

        // api/flug/byRoute
        [HttpGet("byRoute")]
        public List<Flug> GetByRoute(string von, string nach) {
            return repo.FindByRoute(von, nach);
        }

        // api/flug/{id}
        [HttpGet("{id}")]
        public Flug GetById(int id) {
            return repo.FindById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Flug flug) {
            repo.Save(flug);
            // Status 201
            // return CreatedAtAction("GetById", new { id = flug.Id }, flug);

            return new AcceptedActionResult();
        }

    }
}
