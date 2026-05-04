using System;
using AnimalSimulation.Enums;
using AnimalSimulation.Models.Base;

namespace AnimalSimulation.Models.Actors
{
    public class Owner
    {
        public void Adopt(Animal animal)
        {
            if (animal.CurrentLocation == Location.OwnerHouse)
            {
                throw new InvalidOperationException("Ця тварина вже у вас.");
            }
            animal.ChangeLocation(Location.OwnerHouse);
        }

        public void Release(Animal animal)
        {
            if (animal.CurrentLocation == Location.Wild)
            {
                throw new InvalidOperationException("Тварина вже на волі.");
            }
            if (animal.CurrentLocation == Location.PetStore)
            {
                throw new InvalidOperationException("Помилка: Зоомагазин не може випустити тварину на волю. Тільки хазяїн може це зробити.");
            }
            animal.ChangeLocation(Location.Wild);
        }

        public void ReturnToStore(Animal animal)
        {
            if (animal.CurrentLocation == Location.PetStore)
            {
                throw new InvalidOperationException("Тварина вже в зоомагазині.");
            }
            animal.ChangeLocation(Location.PetStore);
        }
    }
}