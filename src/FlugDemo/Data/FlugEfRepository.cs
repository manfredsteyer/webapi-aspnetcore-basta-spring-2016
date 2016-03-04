using FlugDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace FlugDemo.Data
{
    public class FlugEfRepository : IFlugRepository
    {
        static FlugEfRepository() {

            using (var ctx = new FlugDbContext()) {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                var f1 = new Flug { FlugNr = "LH4711", Datum = DateTime.Now, AblugOrt = "Graz", ZielOrt = "Frankfurt" };

                var buchung = new Buchung {
                    Datum = DateTime.Now,
                    Flug = f1
                };

                f1.Buchungen.Add(buchung);

                ctx.Fluege.Add(f1);
                ctx.Fluege.Add(new Flug { FlugNr = "AUA0815", Datum = DateTime.Now.AddHours(2), AblugOrt = "Graz", ZielOrt = "Kognito" });
                ctx.Fluege.Add(new Flug { FlugNr = "LH4712", Datum = DateTime.Now, AblugOrt = "Graz", ZielOrt = "Flagranti" });
                ctx.Fluege.Add(new Flug { FlugNr = "LH4713", Datum = DateTime.Now.AddHours(3), AblugOrt = "Graz", ZielOrt = "Frankfurt" });

                ctx.SaveChanges();
            }
        }

        public List<Flug> FindAll() {
            using (var ctx = new FlugDbContext()) {
                return ctx.Fluege.OrderBy(f => f.Id).ToList();
            }
        }

        public Flug FindById(int id) {
            using (var ctx = new FlugDbContext())
            {
                // Include kommt aus: Microsoft.Data.Entity
                return ctx.Fluege
                          .Include(f => f.Buchungen)
                          .FirstOrDefault(f => f.Id == id);
            }
        }

        public List<Flug> FindByRoute(string von, string nach) {
            using (var ctx = new FlugDbContext())
            {
                return ctx.Fluege.Where(f => f.AblugOrt == von && f.ZielOrt == nach)
                             .OrderBy(f => f.FlugNr)
                             .ToList();
            }
        }

        public void Save(Flug f) {

            using (var ctx = new FlugDbContext())
            {
                ctx.Fluege.Update(f);
                ctx.SaveChanges();
            }
        }
    }
}
