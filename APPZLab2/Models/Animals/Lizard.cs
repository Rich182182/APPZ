using AnimalSimulation.Interfaces;
using AnimalSimulation.Models.Base;
using System.Net.NetworkInformation;

namespace AnimalSimulation.Models.Animals
{
    public class Lizard : Animal, IWalkable, ICrawlable
    {
        public Lizard(string name) : base(name)
        {
            Eyes = 2; Paws = 4; Wings = 0;
        }

        public void Walk()
        {
            if (CanPerformBasicAction()) Notify("Ходить");
        }

        public void Crawl()
        {
            if (CanPerformBasicAction()) Notify("Повзає");
        }
    }
}