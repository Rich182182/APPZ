using System;
using AnimalSimulation.Models.Base;
using AnimalSimulation.Models.Animals;

namespace AnimalSimulation.Factories
{
    public static class AnimalFactory
    {
        public static Animal CreateAnimal(string type, string name)
        {
            return type.ToLower() switch
            {
                "dog" => new Dog(name),
                "canary" => new Canary(name),
                "lizard" => new Lizard(name),
                _ => throw new ArgumentException("Невідомий тип тварини.")
            };
        }
    }
}