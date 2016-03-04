using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Components
{
    public class AcceptedActionResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 202; // Accept
            context.HttpContext.Response.Headers.Add("X-RefId", "4711");
            
            return Task.FromResult<object>(null);
        }
    }
}
