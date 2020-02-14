using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab2Graph
{
    public class Pairs<T>
    {
        public T First { get; set; }
        public T Second { get; set; }

        public Pairs(T first, T second)
        {
            First = first;
            Second = second;
        }
    }
}
