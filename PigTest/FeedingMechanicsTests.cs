using Microsoft.VisualStudio.TestTools.UnitTesting;
using PigClasses;
using System;

namespace PigTest
{
    [TestClass]
    public class FeedingMechanicsTests
    {
        [TestMethod]
        public void Feed_IncreasesHunger()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Hunger = 50;

            // Act
            mechanics.Feed();

            // Assert
            Assert.AreEqual(80, state.Hunger); 
        }

        [TestMethod]
        public void Feed_DecreasesCleanliness()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Cleanliness = 50;

            // Act
            mechanics.Feed();

            // Assert
            Assert.AreEqual(40, state.Cleanliness); 
        }

        [TestMethod]
        public void Feed_DoesNotExceedMaxHunger()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Hunger = 90;

            // Act
            mechanics.Feed();

            // Assert
            Assert.AreEqual(100, state.Hunger); 
        }

        [TestMethod]
        public void Feed_DoesNotWorkWhenSleeping()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSleeping = true;
            state.Hunger = 50;

            // Act
            mechanics.Feed();

            // Assert
            Assert.AreEqual(50, state.Hunger); 
        }

        [TestMethod]
        public void Feed_DoesNotWorkWhenDead()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsDead = true;
            state.Hunger = 50;

            // Act
            mechanics.Feed();

            // Assert
            Assert.AreEqual(50, state.Hunger); 
        }

        [TestMethod]
        public void Feed_IncreasesDailyFeedRequests()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.DailyFeedRequests = 0;
            state.MaxFeedRequests = 3;

            // Act
            mechanics.Feed();

            // Assert
            Assert.AreEqual(1, state.DailyFeedRequests);
        }

        [TestMethod]
        public void Feed_IncreasesHappinessWhenMeetingDailyNeed()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Happiness = 50;
            state.DailyFeedRequests = 0;
            state.MaxFeedRequests = 3;

            // Act
            mechanics.Feed();

            // Assert
            Assert.AreEqual(55, state.Happiness); 
        }
    }

    [TestClass]
    public class SleepingMechanicsTests
    {
        [TestMethod]
        public void StartSleep_SetsIsSleepingToTrue()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);

            // Act
            mechanics.StartSleep();

            // Assert
            Assert.IsTrue(state.IsSleeping);
        }

        [TestMethod]
        public void StartSleep_DoesNotWorkWhenDead()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsDead = true;

            // Act
            mechanics.StartSleep();

            // Assert
            Assert.IsFalse(state.IsSleeping);
        }

        [TestMethod]
        public void StartSleep_IncreasesDailySleepRequests()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.DailySleepRequests = 0;
            state.MaxSleepRequests = 2;

            // Act
            mechanics.StartSleep();

            // Assert
            Assert.AreEqual(1, state.DailySleepRequests);
        }

        [TestMethod]
        public void StartSleep_IncreasesHealthWhenMeetingDailyNeed()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Health = 80;
            state.DailySleepRequests = 0;
            state.MaxSleepRequests = 2;

            // Act
            mechanics.StartSleep();

            // Assert
            Assert.AreEqual(83, state.Health); 
        }

        [TestMethod]
        public void EndSleep_IncreasesEnergy()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSleeping = true;
            state.Energy = 50;

            // Act
            mechanics.EndSleep();

            // Assert
            Assert.AreEqual(90, state.Energy);
        }

        [TestMethod]
        public void EndSleep_DecreasesHunger()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSleeping = true;
            state.Hunger = 50;

            // Act
            mechanics.EndSleep();

            // Assert
            Assert.AreEqual(35, state.Hunger); 
        }

        [TestMethod]
        public void EndSleep_IncreasesHealthWhenNotSick()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSleeping = true;
            state.IsSick = false;
            state.Health = 80;

            // Act
            mechanics.EndSleep();

            // Assert
            Assert.AreEqual(85, state.Health);
        }

        [TestMethod]
        public void EndSleep_DoesNotIncreaseHealthWhenSick()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSleeping = true;
            state.IsSick = true;
            state.Health = 80;

            // Act
            mechanics.EndSleep();

            // Assert
            Assert.AreEqual(80, state.Health);
        }
    }

    [TestClass]
    public class HealingMechanicsTests
    {
        [TestMethod]
        public void Heal_IncreasesHealth()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.Health = 50;

            // Act
            mechanics.Heal();

            // Assert
            Assert.AreEqual(90, state.Health); 
        }

        [TestMethod]
        public void Heal_DecreasesHappiness()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.Happiness = 50;

            // Act
            mechanics.Heal();

            // Assert
            Assert.AreEqual(40, state.Happiness); 
        }

        [TestMethod]
        public void Heal_DecreasesEnergy()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.Energy = 50;

            // Act
            mechanics.Heal();

            // Assert
            Assert.AreEqual(35, state.Energy); 
        }

        [TestMethod]
        public void Heal_DoesNotWorkWhenNotSick()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = false;
            state.Health = 50;

            // Act
            mechanics.Heal();

            // Assert
            Assert.AreEqual(50, state.Health); 
        }

        [TestMethod]
        public void Heal_DoesNotWorkWhenSleeping()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.IsSleeping = true;
            state.Health = 50;

            // Act
            mechanics.Heal();

            // Assert
            Assert.AreEqual(50, state.Health);
        }

        [TestMethod]
        public void Heal_DoesNotWorkWhenDead()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.IsDead = true;
            state.Health = 50;

            // Act
            mechanics.Heal();

            // Assert
            Assert.AreEqual(50, state.Health); 
        }

        [TestMethod]
        public void Heal_DoesNotExceedMaxHealth()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.Health = 90;

            // Act
            mechanics.Heal();

            // Assert
            Assert.AreEqual(100, state.Health);
        }
    }

    [TestClass]
    public class DeathMechanicsTests
    {
        [TestMethod]
        public void CheckForDeath_ReturnsTrueWhenHungerZero()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Hunger = 0;

            // Act
            bool isDead = mechanics.CheckForDeath();

            // Assert
            Assert.IsTrue(isDead);
            Assert.IsTrue(state.IsDead);
        }

        [TestMethod]
        public void CheckForDeath_ReturnsTrueWhenHappinessZero()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Happiness = 0;

            // Act
            bool isDead = mechanics.CheckForDeath();

            // Assert
            Assert.IsTrue(isDead);
            Assert.IsTrue(state.IsDead);
        }

        [TestMethod]
        public void CheckForDeath_ReturnsTrueWhenEnergyZero()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Energy = 0;

            // Act
            bool isDead = mechanics.CheckForDeath();

            // Assert
            Assert.IsTrue(isDead);
            Assert.IsTrue(state.IsDead);
        }

        [TestMethod]
        public void CheckForDeath_ReturnsTrueWhenHealthZero()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Health = 0;

            // Act
            bool isDead = mechanics.CheckForDeath();

            // Assert
            Assert.IsTrue(isDead);
            Assert.IsTrue(state.IsDead);
        }

        [TestMethod]
        public void CheckForDeath_ReturnsFalseWhenAllStatsPositive()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Hunger = 10;
            state.Happiness = 10;
            state.Energy = 10;
            state.Health = 10;

            // Act
            bool isDead = mechanics.CheckForDeath();

            // Assert
            Assert.IsFalse(isDead);
            Assert.IsFalse(state.IsDead);
        }
    }

    [TestClass]
    public class CleaningMechanicsTests
    {
        [TestMethod]
        public void Clean_IncreasesCleanliness()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Cleanliness = 50;

            // Act
            mechanics.Clean();

            // Assert
            Assert.AreEqual(90, state.Cleanliness); 
        }

        [TestMethod]
        public void Clean_DecreasesHappiness()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Happiness = 50;

            // Act
            mechanics.Clean();

            // Assert
            Assert.AreEqual(45, state.Happiness); 
        }

        [TestMethod]
        public void Clean_DoesNotExceedMaxCleanliness()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Cleanliness = 90;

            // Act
            mechanics.Clean();

            // Assert
            Assert.AreEqual(100, state.Cleanliness);
        }

        [TestMethod]
        public void Clean_DoesNotWorkWhenSleeping()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSleeping = true;
            state.Cleanliness = 50;

            // Act
            mechanics.Clean();

            // Assert
            Assert.AreEqual(50, state.Cleanliness);
        }

        [TestMethod]
        public void Clean_DoesNotWorkWhenDead()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsDead = true;
            state.Cleanliness = 50;

            // Act
            mechanics.Clean();

            // Assert
            Assert.AreEqual(50, state.Cleanliness); 
        }

        [TestMethod]
        public void Clean_IncreasesDailyBathRequests()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.DailyBathRequests = 0;
            state.MaxBathRequests = 1;

            // Act
            mechanics.Clean();

            // Assert
            Assert.AreEqual(1, state.DailyBathRequests);
        }

        [TestMethod]
        public void Clean_IncreasesHappinessWhenMeetingDailyNeed()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Happiness = 50;
            state.DailyBathRequests = 0;
            state.MaxBathRequests = 1;

            // Act
            mechanics.Clean();

            // Assert
            Assert.AreEqual(53, state.Happiness); 
        }

        [TestMethod]
        public void Clean_PreventsSicknessWhenClean()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Cleanliness = 10;
            state.IsSick = false;

            // Act
            mechanics.Clean();

            // Assert
            Assert.IsFalse(state.IsSick); 
        }
    }

    [TestClass]
    public class SicknessMechanicsTests
    {
        [TestMethod]
        public void CheckSickness_SetsSickWhenCleanlinessLow()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Cleanliness = 5;
            state.IsSick = false;

            // Act
            mechanics.CheckSickness();

            // Assert
            Assert.IsTrue(state.IsSick);
            Assert.AreEqual(1, state.SickDays);
        }

        [TestMethod]
        public void CheckSickness_DoesNotSetSickWhenAlreadySick()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Cleanliness = 5;
            state.IsSick = true;
            state.SickDays = 3;

            // Act
            mechanics.CheckSickness();

            // Assert
            Assert.AreEqual(3, state.SickDays); 
        }

        [TestMethod]
        public void CheckSickness_DoesNotSetSickWhenCleanlinessHigh()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Cleanliness = 20;
            state.IsSick = false;

            // Act
            mechanics.CheckSickness();

            // Assert
            Assert.IsFalse(state.IsSick);
        }

        [TestMethod]
        public void UpdateOverTime_DecreasesHealthWhenSick()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.Health = 50;

            // Act
            mechanics.UpdateOverTime();

            // Assert
            Assert.AreEqual(42, state.Health); 
        }

        [TestMethod]
        public void UpdateOverTime_IncreasesSickDaysWhenSick()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.IsSick = true;
            state.SickDays = 1;

            // Act
            mechanics.UpdateOverTime();

            // Assert
            Assert.AreEqual(2, state.SickDays);
        }
    }

    [TestClass]
    public class DayMechanicsTests
    {
        [TestMethod]
        public void CheckDayCompletion_IncreasesDayCounterWhenAllNeedsMet()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.DayCounter = 1;
            state.DailyFeedRequests = 3;
            state.DailySleepRequests = 2;
            state.DailyPlayRequests = 2;
            state.DailyBathRequests = 1;
            state.MaxFeedRequests = 3;
            state.MaxSleepRequests = 2;
            state.MaxPlayRequests = 2;
            state.MaxBathRequests = 1;

            // Act
            mechanics.CheckDayCompletion();

            // Assert
            Assert.AreEqual(2, state.DayCounter);
        }

        [TestMethod]
        public void CheckDayCompletion_IncreasesHappinessWhenAllNeedsMet()
        {
            // Arrange
            var state = new PigState();

            state.MaxFeedRequests = 3;
            state.MaxSleepRequests = 2;
            state.MaxPlayRequests = 2;
            state.MaxBathRequests = 1;

            state.DailyFeedRequests = 3;
            state.DailySleepRequests = 2;
            state.DailyPlayRequests = 2;
            state.DailyBathRequests = 1;

            state.Happiness = 50;

            var mechanics = new GameMechanics(state);

            // Act
            mechanics.CheckDayCompletion();

            // Assert
            Assert.AreEqual(65, state.Happiness); 
        }

        [TestMethod]
        public void CheckDayCompletion_DecreasesHappinessWhenNeedsNotMet()
        {
            // Arrange
            var state = new PigState();
            var mechanics = new GameMechanics(state);
            state.Happiness = 50;
            state.DailyFeedRequests = 0;
            state.MaxFeedRequests = 3;

            // Act
            mechanics.CheckDayCompletion();

            // Assert
            Assert.AreEqual(30, state.Happiness); 
        }

        [TestMethod]
        public void AreAllNeedsMet_ReturnsTrueWhenAllMet()
        {
            // Arrange
            var state = new PigState();
            state.DailyFeedRequests = 3;
            state.DailySleepRequests = 2;
            state.DailyPlayRequests = 2;
            state.DailyBathRequests = 1;
            state.MaxFeedRequests = 3;
            state.MaxSleepRequests = 2;
            state.MaxPlayRequests = 2;
            state.MaxBathRequests = 1;

            // Act
            bool result = state.AreAllNeedsMet();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreAllNeedsMet_ReturnsFalseWhenSomeNotMet()
        {
            // Arrange
            var state = new PigState();
            state.DailyFeedRequests = 2;
            state.DailySleepRequests = 2;
            state.DailyPlayRequests = 2;
            state.DailyBathRequests = 1;
            state.MaxFeedRequests = 3;
            state.MaxSleepRequests = 2;
            state.MaxPlayRequests = 2;
            state.MaxBathRequests = 1;

            // Act
            bool result = state.AreAllNeedsMet();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
