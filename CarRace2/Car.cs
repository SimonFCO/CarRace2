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
        public int Seconds { get; set; } = 0;
        public double DistanceDriven { get; set; } = 0;
        public bool HasWon { get; set; } = false;
        public string Problem { get; set; } = "";
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
                PauseTimer -= 1;
            }
        }

        public void Drive()
        {
            Update();
            if (Stopped != true)
            {
                DistanceDriven += Speed * 1000 / 3600;
            }
        }
    }
}
