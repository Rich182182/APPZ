using System.Collections.Generic;
using AnimalSimulation.Enums;
using AnimalSimulation.Models.Base;

namespace AnimalSimulation.Models.Actors
{
    public class Owner
    {
        private List<Animal> _pets = new List<Animal>();

        public void Adopt(Animal animal)
        {
            if (!animal.IsAlive) return;
            if (!_pets.Contains(animal))
            {
                _pets.Add(animal);
                animal.ChangeLocation(Location.OwnerHouse);
            }
        }

        public bool Release(Animal animal)
        {
            if (_pets.Contains(animal))
            {
                _pets.Remove(animal);
                animal.ChangeLocation(Location.Wild);
                return true;
            }
            return false;
        }

        public void ReturnToStore(Animal animal)
        {
            if (_pets.Contains(animal))
            {
                _pets.Remove(animal);
            }
            animal.ChangeLocation(Location.PetStore);
        }
    }
}