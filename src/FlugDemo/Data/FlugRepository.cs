using FlugDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Data
{
    public class FlugDemoRepository : IFlugRepository
    {
        static List<Flug> fluege;

        static FlugDemoRepository() {
            fluege = new List<Flug>();

            fluege.Add(new Flug { Id = 1, FlugNr = "LH4711", Datum = DateTime.Now, AblugOrt = "Graz", ZielOrt = "Frankfurt" });
            fluege.Add(new Flug { Id = 2, FlugNr = "AUA0815", Datum = DateTime.Now.AddHours(2), AblugOrt = "Graz", ZielOrt = "Kognito" });
            fluege.Add(new Flug { Id = 3, FlugNr = "LH4712", Datum = DateTime.Now, AblugOrt = "Graz", ZielOrt = "Flagranti" });
            fluege.Add(new Flug { Id = 4, FlugNr = "LH4713", Datum = DateTime.Now.AddHours(3), AblugOrt = "Graz", ZielOrt = "Frankfurt" });

        }

        public List<Flug> FindAll() {
            return fluege.OrderBy(f => f.Id).ToList(); ;
        }

        public Flug FindById(int id) {
            return fluege.FirstOrDefault(f => f.Id == id);
        }

        public List<Flug> FindByRoute(string von, string nach) {
            return fluege.Where(f => f.AblugOrt == von && f.ZielOrt == nach)
                         .OrderBy(f => f.FlugNr)
                         .ToList();
        }

        public void Save(Flug f) {

            // INSERT ?
            if (f.Id == 0) {
                f.Id = fluege.Max(flg => flg.Id) + 1;
                fluege.Add(f);
                return;
            }

            // Update
            var flugInDb = FindById(f.Id);
            flugInDb.AblugOrt = f.AblugOrt;
            flugInDb.ZielOrt = f.ZielOrt;
            flugInDb.FlugNr = f.FlugNr;
            flugInDb.Datum = f.Datum;

        }

    }
}
