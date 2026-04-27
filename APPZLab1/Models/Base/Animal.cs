using System;
using AnimalSimulation.Enums;
using AnimalSimulation.Events;

namespace AnimalSimulation.Models.Base
{
    public abstract class Animal
    {
        public string Name { get; }
        public int Eyes { get; protected set; }
        public int Paws { get; protected set; }
        public int Wings { get; protected set; }

        public Location CurrentLocation { get; private set; }
        public bool IsAlive { get; private set; } = true;
        public bool IsHappy { get; private set; }

        private int _hoursSinceLastMeal = 4;
        private int _hoursSinceLastClean = 0;
        private int _mealsToday = 0;
        private int _hoursPassedToday = 0;

        public event EventHandler<AnimalEventArgs> OnNotified;

        protected Animal(string name)
        {
            Name = name;
            CurrentLocation = Location.PetStore;
            UpdateHappiness();
        }

        protected void Notify(string message)
        {
            OnNotified?.Invoke(this, new AnimalEventArgs(message));
        }

        public void ChangeLocation(Location newLocation)
        {
            if (!IsAlive) return;
            CurrentLocation = newLocation;
            Notify($"Локація змінена на {newLocation}");
            UpdateHappiness();
        }

        public void Eat()
        {
            if (!IsAlive) return;

            if (_hoursSinceLastMeal < 3)
            {
                Notify("Відмова від їжі (минуло менше 3 год з минулого прийому - дотримуйтесь проміжків)");
                return;
            }

            _mealsToday++;
            _hoursSinceLastMeal = 0;

            if (_mealsToday > 5)
            {
                Die("Смерть від переїдання (> 5 разів на день)");
                return;
            }

            Notify("Прийом їжі");
        }

        public void Clean()
        {
            if (!IsAlive) return;

            if (CurrentLocation == Location.Wild)
            {
                Notify("Неможливо прибрати: тварина на волі (за нею не доглядають)");
                return;
            }

            _hoursSinceLastClean = 0;
            UpdateHappiness();
            Notify("Прибирання виконано");
        }

        public void PassTime(int hours)
        {
            if (!IsAlive) return;

            for (int i = 0; i < hours; i++)
            {
                if (!IsAlive) break;

                _hoursPassedToday++;
                _hoursSinceLastMeal++;
                _hoursSinceLastClean++;

                if (_hoursPassedToday >= 24)
                {
                    if (_mealsToday < 1)
                    {
                        Die("Смерть від голоду (жодного прийому їжі за день)");
                        return;
                    }
                    _hoursPassedToday = 0;
                    _mealsToday = 0;
                }

                if (_hoursSinceLastMeal >= 24)
                {
                    Die("Смерть від голоду (понад 24 год без їжі)");
                    return;
                }

                UpdateHappiness();
            }
        }

        private void Die(string reason)
        {
            IsAlive = false;
            Notify($"[СМЕРТЬ] {reason}");
        }

        private void UpdateHappiness()
        {
            if (!IsAlive) return;

            bool previousHappiness = IsHappy;

            if (CurrentLocation == Location.Wild)
            {
                IsHappy = true;
            }
            else
            {
                IsHappy = _hoursSinceLastClean <= 24;
            }

            if (previousHappiness != IsHappy)
            {
                Notify(IsHappy ? "Статус щастя змінено: Щаслива :)" : "Статус щастя змінено: Нещасна (потребує прибирання) :(");
            }
        }

        protected bool CanPerformEnergeticAction()
        {
            return IsAlive && _hoursSinceLastMeal <= 8;
        }

        protected bool CanPerformBasicAction()
        {
            return IsAlive;
        }

        public string GetStatus()
        {
            return $"Живий: {IsAlive} | Щасливий: {IsHappy} | Локація: {CurrentLocation} | Їла годин тому: {_hoursSinceLastMeal}";
        }
    }
}