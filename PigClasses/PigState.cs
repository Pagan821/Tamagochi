using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigClasses
{
    public class PigState
    {
        // Основные характеристики
        public int Hunger { get; set; }
        public int Happiness { get; set; }
        public int Cleanliness { get; set; }
        public int Energy { get; set; }
        public int Health { get; set; }

        // Состояния
        public bool IsSick { get; set; }
        public bool IsSleeping { get; set; }
        public bool IsDead { get; set; }
        public int SickDays { get; set; }

        // Дневные потребности
        public int DayCounter { get; set; }
        public int DailyFeedRequests { get; set; }
        public int DailySleepRequests { get; set; }
        public int DailyPlayRequests { get; set; }
        public int DailyBathRequests { get; set; }
        public int MaxFeedRequests { get; set; }
        public int MaxSleepRequests { get; set; }
        public int MaxPlayRequests { get; set; }
        public int MaxBathRequests { get; set; }

        public PigState()
        {
            ResetToInitial();
        }

        public void ResetToInitial()
        {
            Hunger = 50;
            Happiness = 50;
            Cleanliness = 50;
            Energy = 50;
            Health = 100;
            IsSick = false;
            IsSleeping = false;
            IsDead = false;
            SickDays = 0;
            DayCounter = 1;
            UpdateDailyNeeds();
        }

        public void UpdateDailyNeeds()
        {
            DailyFeedRequests = 0;
            DailySleepRequests = 0;
            DailyPlayRequests = 0;
            DailyBathRequests = 0;

            // Купание - каждый второй день
            MaxBathRequests = (DayCounter % 2 == 0) ? 1 : 0;

            // Генерируем случайные потребности на день
            Random rand = new Random();
            MaxFeedRequests = rand.Next(2, 4);
            MaxSleepRequests = rand.Next(1, 3);
            MaxPlayRequests = rand.Next(1, 3);
        }

        public bool AreAllNeedsMet()
        {
            return (DailyFeedRequests >= MaxFeedRequests) &&
                   (DailySleepRequests >= MaxSleepRequests) &&
                   (DailyPlayRequests >= MaxPlayRequests) &&
                   (DailyBathRequests >= MaxBathRequests);
        }
    }

    public class GameMechanics
    {
        private readonly PigState _state;

        public GameMechanics(PigState state)
        {
            _state = state;
        }

        // Механика кормления
        public void Feed()
        {
            if (_state.IsSleeping || _state.IsDead) return;

            _state.Hunger = Math.Min(100, _state.Hunger + 30);
            _state.Cleanliness = Math.Max(0, _state.Cleanliness - 10);

            if (_state.DailyFeedRequests < _state.MaxFeedRequests)
            {
                _state.DailyFeedRequests++;
                _state.Happiness = Math.Min(100, _state.Happiness + 5);
            }
        }

        // Механика купания/чистки
        public void Clean()
        {
            if (_state.IsSleeping || _state.IsDead) return;

            _state.Cleanliness = Math.Min(100, _state.Cleanliness + 40);
            _state.Happiness = Math.Max(0, _state.Happiness - 5);

            if (_state.DailyBathRequests < _state.MaxBathRequests)
            {
                _state.DailyBathRequests++;
                _state.Happiness = Math.Min(100, _state.Happiness + 8);
            }

            CheckSickness();
        }

        // Механика сна
        public void StartSleep()
        {
            if (_state.IsSleeping || _state.IsDead) return;

            _state.IsSleeping = true;

            if (_state.DailySleepRequests < _state.MaxSleepRequests)
            {
                _state.DailySleepRequests++;
                _state.Health = Math.Min(100, _state.Health + 3);
            }
        }

        public void EndSleep()
        {
            if (!_state.IsSleeping) return;

            _state.IsSleeping = false;
            _state.Energy = Math.Min(100, _state.Energy + 40);
            _state.Hunger = Math.Max(0, _state.Hunger - 15);

            if (!_state.IsSick)
            {
                _state.Health = Math.Min(100, _state.Health + 5);
            }
        }

        // Механика лечения
        public void Heal()
        {
            if (_state.IsSleeping || _state.IsDead || !_state.IsSick) return;

            _state.Health = Math.Min(100, _state.Health + 40);
            _state.Happiness = Math.Max(0, _state.Happiness - 10);
            _state.Energy = Math.Max(0, _state.Energy - 15);

            Random rand = new Random();
            if (rand.Next(100) < 70) // 70% шанс выздоровления
            {
                _state.IsSick = false;
                _state.SickDays = 0;
            }
        }

        // Механика игры
        public void Play()
        {
            if (_state.IsSleeping || _state.IsDead) return;

            if (_state.Energy >= 20)
            {
                _state.Happiness = Math.Min(100, _state.Happiness + 25);
                _state.Energy = Math.Max(0, _state.Energy - 20);
                _state.Hunger = Math.Max(0, _state.Hunger - 10);

                if (_state.DailyPlayRequests < _state.MaxPlayRequests)
                {
                    _state.DailyPlayRequests++;
                    _state.Happiness = Math.Min(100, _state.Happiness + 5);
                }
            }
        }

        // Проверка болезни
        public void CheckSickness()
        {
            if (_state.Cleanliness <= 10 && !_state.IsSick && _state.Health > 0 && !_state.IsDead)
            {
                _state.IsSick = true;
                _state.SickDays = 1;
            }
        }

        // Проверка смерти
        public bool CheckForDeath()
        {
            if (_state.Hunger == 0)
            {
                _state.IsDead = true;
                return true;
            }
            else if (_state.Happiness == 0)
            {
                _state.IsDead = true;
                return true;
            }
            else if (_state.Energy == 0)
            {
                _state.IsDead = true;
                return true;
            }
            else if (_state.Health == 0)
            {
                _state.IsDead = true;
                return true;
            }

            return false;
        }

        // Обновление состояния с течением времени
        public void UpdateOverTime()
        {
            if (_state.IsSleeping || _state.IsDead) return;

            _state.Hunger = Math.Max(0, _state.Hunger - 5);
            _state.Happiness = Math.Max(0, _state.Happiness - 3);
            _state.Cleanliness = Math.Max(0, _state.Cleanliness - 4);
            _state.Energy = Math.Max(0, _state.Energy - 2);

            if (_state.IsSick)
            {
                _state.Health = Math.Max(0, _state.Health - 8);
                _state.SickDays++;
            }
            else
            {
                if (_state.Cleanliness > 50 && _state.Happiness > 50)
                {
                    _state.Health = Math.Min(100, _state.Health + 2);
                }
            }

            CheckSickness();
        }

        // Проверка завершения дня
        public void CheckDayCompletion()
        {
            if (_state.AreAllNeedsMet() && _state.MaxFeedRequests > 0)
            {
                _state.Happiness = Math.Min(100, _state.Happiness + 15);
                _state.Health = Math.Min(100, _state.Health + 10);

                if (_state.IsSick)
                {
                    _state.SickDays++;
                    Random rand = new Random();
                    if (rand.Next(100) < 30)
                    {
                        _state.Health = Math.Min(100, _state.Health + 15);
                    }
                }

                _state.DayCounter++;
                _state.UpdateDailyNeeds();
            }
            else if (!_state.AreAllNeedsMet())
            {
                _state.Happiness = Math.Max(0, _state.Happiness - 20);
                _state.Health = Math.Max(0, _state.Health - 10);
                _state.DayCounter++;
                _state.UpdateDailyNeeds();
            }
        }
    }
}
