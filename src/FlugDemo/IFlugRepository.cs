using System.Collections.Generic;
using FlugDemo.Models;

namespace FlugDemo.Data
{
    public interface IFlugRepository
    {
        List<Flug> FindAll();
        Flug FindById(int id);
        List<Flug> FindByRoute(string von, string nach);
        void Save(Flug f);
    }
}