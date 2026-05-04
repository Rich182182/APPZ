using AnimalSimulation.Interfaces;
using AnimalSimulation.Models.Base;
using System.Buffers;
using System.Net.NetworkInformation;

namespace AnimalSimulation.Models.Animals
{
    public class Canary : Animal, IWalkable, IFlyable, ISingable
    {
        public Canary(string name) : base(name)
        {
            Eyes = 2; Paws = 2; Wings = 2;
        }

        public void Walk()
        {
            if (CanPerformBasicAction()) Notify("Ходить");
        }

        public void Fly()
        {
            if (CanPerformEnergeticAction()) Notify("Летить");
            else Notify("Відмова (минуло понад 8 год після їжі)");
        }

        public void Sing()
        {
            if (CanPerformEnergeticAction()) Notify("Співає");
            else Notify("Відмова (минуло понад 8 год після їжі)");
        }
    }
}