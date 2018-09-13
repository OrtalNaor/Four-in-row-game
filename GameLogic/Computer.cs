using System;
using System.Collections.Generic;
using System.Linq;

namespace C18_Ex02
{
    public class Computer
    {
        public Computer()
        {
        }

        public short GetNextMove(List<short> i_ColsOptions)
        {
            Random random = new Random();
            int index = random.Next(0, i_ColsOptions.Count);

            return i_ColsOptions.ElementAt(index);
        }
    }
}
