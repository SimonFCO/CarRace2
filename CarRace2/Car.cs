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
        public bool Stopped { get; set; } = false;
        public int PauseTimer { get; set; } = 0;
        public int DistanceDriven { get; set; } = 0;
        public Car(string name)
        {
            Name = name;
        }

        public void Update()
        {
            if(PauseTimer <= 0)
            {
                Stopped = false;
            }
            else
            {
                Stopped = true;
            }
        }

        public void Drive()
        {
            DistanceDriven += Speed;
        }
    }
}
