using AnimalSimulation.Interfaces;
using AnimalSimulation.Models.Base;
using System.Buffers;
using System.Net.NetworkInformation;

namespace AnimalSimulation.Models.Animals
{
    public class Dog : Animal, IWalkable, IRunnable
    {
        public Dog(string name) : base(name)
        {
            Eyes = 2; Paws = 4; Wings = 0;
        }

        public void Walk()
        {
            if (CanPerformBasicAction()) Notify("Ходить");
        }

        public void Run()
        {
            if (CanPerformEnergeticAction()) Notify("Біжить");
            else Notify("Відмова (минуло понад 8 год після їжі)");
        }
    }
}