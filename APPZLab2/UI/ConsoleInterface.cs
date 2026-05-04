using System;
using System.Collections.Generic;
using AnimalSimulation.Models.Base;
using AnimalSimulation.Models.Actors;
using AnimalSimulation.Factories;
using AnimalSimulation.Observers;
using AnimalSimulation.Enums;
using AnimalSimulation.Interfaces;

namespace AnimalSimulation.UI
{
    public class ConsoleInterface
    {
        private readonly List<Animal> _animals;
        private readonly Owner _owner;
        private readonly ConsoleLogObserver _observer;

        public ConsoleInterface(List<Animal> animals, Owner owner, ConsoleLogObserver observer)
        {
            _animals = animals;
            _owner = owner;
            _observer = observer;
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAnimalsStatus(); break;
                    case "2": InteractWithAnimalMenu(); break;
                    case "3": PassTimeGlobal(1); break;
                    case "4": PassTimeGlobal(8); break;
                    case "5": CreateNewAnimalMenu(); break;
                    case "0": isRunning = false; break;
                    default: Console.WriteLine("Невідома команда."); break;
                }
            }
        }

        private void AddAnimalToSystem(string type, string name)
        {
            try
            {
                Animal newAnimal = AnimalFactory.CreateAnimal(type, name);
                _observer.Subscribe(newAnimal);
                _animals.Add(newAnimal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("\n--- СИМУЛЯЦІЯ ТВАРИН ---");
            Console.WriteLine("1. Стан усіх тварин");
            Console.WriteLine("2. Взаємодіяти з твариною");
            Console.WriteLine("3. Пропустити час (1 год)");
            Console.WriteLine("4. Пропустити час (8 год)");
            Console.WriteLine("5. Створити нову тварину");
            Console.WriteLine("0. Вихід");
            Console.Write("Вибір: ");
        }

        private void ShowAnimalsStatus()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_animals[i].Name} ({_animals[i].GetType().Name}) - {_animals[i].GetStatus()}");
            }
        }

        private void CreateNewAnimalMenu()
        {
            Console.Write("Введіть тип (dog, canary, lizard): ");
            string type = Console.ReadLine();
            Console.Write("Введіть ім'я: ");
            string name = Console.ReadLine();
            AddAnimalToSystem(type, name);
            Console.WriteLine($"[Система]: Спроба створення об'єкта завершена.");
        }

        private void PassTimeGlobal(int hours)
        {
            Console.WriteLine($"Пропускаємо {hours} год...");
            foreach (var a in _animals)
            {
                a.PassTime(hours);
            }
        }

        private void InteractWithAnimalMenu()
        {
            ShowAnimalsStatus();
            Console.Write("\nВведіть номер тварини: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > _animals.Count)
            {
                Console.WriteLine("Невірний номер.");
                return;
            }

            Animal selected = _animals[index - 1];
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
                        try
                        {
                            _owner.Adopt(selected);
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "4":
                        try
                        {
                            _owner.Release(selected);
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "5":
                        try
                        {
                            _owner.ReturnToStore(selected);
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
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
    }
}