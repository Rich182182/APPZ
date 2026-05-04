using System.Collections.Generic;
using AnimalSimulation.Models.Base;
using AnimalSimulation.Models.Actors;
using AnimalSimulation.Observers;
using AnimalSimulation.UI;

namespace AnimalSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Animal> animals = new List<Animal>();
            Owner owner = new Owner();
            ConsoleLogObserver observer = new ConsoleLogObserver();

            ConsoleInterface ui = new ConsoleInterface(animals, owner, observer);
            ui.Run();
        }
    }
}