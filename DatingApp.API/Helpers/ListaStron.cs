using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Helpers
{
    public class ListaStron<T> : List<T>
    {
        public int DomyslnaStrona { get; set; }
        public int CaloscStron { get; set; }
        public int RozmiarStrony { get; set; }
        public int CaloscKont { get; set; }

        public ListaStron(List<T> items, int kont, int numerStrony, int rozmiarStrony)
        {
            CaloscKont = kont;
            RozmiarStrony = rozmiarStrony;
            DomyslnaStrona = numerStrony;
            CaloscStron = (int)Math.Ceiling(kont / (double)rozmiarStrony);
            this.AddRange(items);
        }

        public static async Task<ListaStron<T>> CreateAsync(IQueryable<T> source,
            int numerStrony, int rozmiarStrony)
        {
            var kont = await source.CountAsync();
            var items = await source.Skip((numerStrony - 1) * rozmiarStrony).Take(rozmiarStrony).ToListAsync();
            return new ListaStron<T>(items, kont, numerStrony, rozmiarStrony);
        }
    }
}