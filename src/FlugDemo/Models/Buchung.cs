using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Models
{

    public class Buchung
    {
        public int BuchungId { get; set; }
        public DateTime Datum { get; set; }

        public int FlugId { get; set; }
        public Flug Flug { get; set; }

        public int PassagierId { get; set; }

        // public Passagier Passagier { get; set; }
    }
}
