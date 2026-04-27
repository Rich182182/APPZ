using System;
using System.Collections.Generic;
using AnimalSimulation.Enums;
using AnimalSimulation.Interfaces;
using AnimalSimulation.Models.Actors;
using AnimalSimulation.Models.Animals;
using AnimalSimulation.Models.Base;
using AnimalSimulation.Events;

namespace AnimalSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Owner owner = new Owner();
            List<Animal> animals = new List<Animal>
            {
                new Dog("Бровко"),
                new Canary("Кеша"),
                new Lizard("Гекон")
            };

            foreach (var animal in animals)
            {
                animal.OnNotified += Animal_OnNotified;
            }

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n--- ГОЛОВНЕ МЕНЮ ---");
                Console.WriteLine("1. Переглянути стан усіх тварин");
                Console.WriteLine("2. Взаємодіяти з твариною");
                Console.WriteLine("3. Пропустити час для всіх (1 год)");
                Console.WriteLine("4. Пропустити час для всіх (8 год)");
                Console.WriteLine("0. Вихід");
                Console.Write("Вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAnimalsStatus(animals);
                        break;
                    case "2":
                        InteractWithAnimal(animals, owner);
                        break;
                    case "3":
                        PassTimeGlobal(animals, 1);
                        break;
                    case "4":
                        PassTimeGlobal(animals, 8);
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Невідома команда.");
                        break;
                }
            }
        }

        private static void Animal_OnNotified(object sender, AnimalEventArgs e)
        {
            if (sender is Animal animal)
            {
                Console.WriteLine($"[{animal.Name}]: {e.Message}");
            }
        }

        static void ShowAnimalsStatus(List<Animal> animals)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {animals[i].Name} ({animals[i].GetType().Name}) - {animals[i].GetStatus()}");
            }
        }

        static void InteractWithAnimal(List<Animal> animals, Owner owner)
        {
            ShowAnimalsStatus(animals);
            Console.Write("\nВведіть номер тварини: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > animals.Count)
            {
                Console.WriteLine("Невірний номер.");
                return;
            }

            Animal selected = animals[index - 1];
            if (!selected.IsAlive)
            {
                Console.WriteLine("Тварина мертва, взаємодія неможлива.");
                return;
            }

            bool interacting = true;
            while (interacting)
            {
                Console.WriteLine($"\n-- Взаємодія: {selected.Name} (Локація: {selected.CurrentLocation}) --");
                Console.WriteLine("1. Годувати");
                Console.WriteLine("2. Прибирати");
                Console.WriteLine("3. Взяти до власника (OwnerHouse)");
                Console.WriteLine("4. Відпустити на волю (Wild)");
                Console.WriteLine("5. Віддати в магазин (PetStore)");

                if (selected is IWalkable) Console.WriteLine("W. Йти");
                if (selected is IRunnable) Console.WriteLine("R. Бігти");
                if (selected is IFlyable) Console.WriteLine("F. Летіти");
                if (selected is ISingable) Console.WriteLine("S. Співати");
                if (selected is ICrawlable) Console.WriteLine("C. Повзати");

                Console.WriteLine("0. Назад");
                Console.Write("Дія: ");

                string action = Console.ReadLine()?.ToUpper();
                Console.WriteLine();

                switch (action)
                {
                    case "1":
                        selected.Eat();
                        break;
                    case "2":
                        selected.Clean();
                        break;
                    case "3":
                        if (selected.CurrentLocation == Location.OwnerHouse)
                        {
                            Console.WriteLine("Ця тварина вже у вас.");
                        }
                        else
                        {
                            owner.Adopt(selected);
                        }
                        break;
                    case "4":
                        if (selected.CurrentLocation == Location.Wild)
                        {
                            Console.WriteLine("Тварина вже на волі.");
                        }
                        else if (selected.CurrentLocation == Location.PetStore)
                        {
                            Console.WriteLine("Помилка: Зоомагазин не може випустити тварину на волю. Тільки хазяїн може це зробити.");
                        }
                        else
                        {
                            owner.Release(selected);
                        }
                        break;
                    case "5":
                        if (selected.CurrentLocation == Location.PetStore)
                        {
                            Console.WriteLine("Тварина вже в зоомагазині.");
                        }
                        else
                        {
                            owner.ReturnToStore(selected);
                        }
                        break;
                    case "W": if (selected is IWalkable w) w.Walk(); break;
                    case "R": if (selected is IRunnable r) r.Run(); break;
                    case "F": if (selected is IFlyable f) f.Fly(); break;
                    case "S": if (selected is ISingable s) s.Sing(); break;
                    case "C": if (selected is ICrawlable c) c.Crawl(); break;
                    case "0": interacting = false; break;
                    default: Console.WriteLine("Невідома команда."); break;
                }
            }
        }

        static void PassTimeGlobal(List<Animal> animals, int hours)
        {
            Console.WriteLine($"Пропускаємо {hours} год...");
            foreach (var animal in animals)
            {
                animal.PassTime(hours);
            }
        }
    }
}