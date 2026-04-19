using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace2
{
    internal class Car
    {
        public string Name { get; set; }
        public int Speed { get; set; } = 120;
        public bool Stopped { get; set; }
        public Car(string name, bool stopped)
        {
            Name = name;
            Stopped = stopped;
        }
    }
}
