using System;
using AnimalSimulation.Events;
using AnimalSimulation.Models.Base;

namespace AnimalSimulation.Observers
{
    public class ConsoleLogObserver
    {
        public void Subscribe(Animal animal)
        {
            if (animal != null)
            {
                animal.OnNotified += HandleAnimalNotification;
            }
        }

        public void Unsubscribe(Animal animal)
        {
            if (animal != null)
            {
                animal.OnNotified -= HandleAnimalNotification;
            }
        }

        private void HandleAnimalNotification(object sender, AnimalEventArgs e)
        {
            if (sender is Animal animal)
            {
                Console.WriteLine($"[{animal.Name}]: {e.Message}");
            }
        }
    }
}