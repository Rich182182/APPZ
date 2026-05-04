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
                "собака" => new Dog(name),
                "канарка" => new Canary(name),
                "ящірка" => new Lizard(name),
                _ => throw new ArgumentException("Невідомий тип тварини.")
            };
        }
    }
}