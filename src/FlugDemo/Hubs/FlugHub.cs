using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Hubs
{
    public class FlugHub: Hub
    {
        public void SelectFlug(int id) {
            this.Clients.All.FlugSelected(id);
        }

    }
}

