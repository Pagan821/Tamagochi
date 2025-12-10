using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Тамагоча_свинья
{
    public partial class MainForm : Form
    {
        private int hunger = 50;
        private int happiness = 50;
        private int cleanliness = 50;
        private int energy = 50;
        private int health = 100;
        private bool isSick = false;
        private int sickDays = 0;
        private bool isDead = false;

        // Система ежедневных потребностей
        private int dayCounter = 1;
        private int dailyFeedRequests = 0;
        private int dailySleepRequests = 0;
        private int dailyPlayRequests = 0;
        private int dailyBathRequests = 0;
        private int maxFeedRequests = 3;
        private int maxSleepRequests = 2;
        private int maxPlayRequests = 2;
        private int maxBathRequests = 1;

        // Для отслеживания времени
        private int timerTickCounter = 0;
        private const int TicksPerDay = 15;

        // Для системы сна
        private bool isSleeping = false;
        //private Timer sleepTimer;
        private int sleepDuration = 4000;

        // Константы скоростей игры
        private const int SlowSpeed = 10000;
        private const int NormalSpeed = 6000;
        private const int FastSpeed = 3000;

        public MainForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            timerGame.Start();
            UpdateDailyNeeds();
            UpdateStatus();
        }

        private void UpdateDailyNeeds()
        {
            dailyFeedRequests = 0;
            dailySleepRequests = 0;
            dailyPlayRequests = 0;
            dailyBathRequests = 0;

            if (dayCounter % 2 == 0)
            {
                maxBathRequests = 1;
            }
            else
            {
                maxBathRequests = 0;
            }

            Random rand = new Random();
            maxFeedRequests = rand.Next(2, 4);
            maxSleepRequests = rand.Next(1, 3);
            maxPlayRequests = rand.Next(1, 3);

            UpdateDailyNeedsDisplay();
        }

        private void UpdateDailyNeedsDisplay()
        {
            string needsText = $"День {dayCounter}: ";
            List<string> needs = new List<string>();

            if (dailyFeedRequests < maxFeedRequests)
                needs.Add($"еда ({dailyFeedRequests}/{maxFeedRequests})");
            if (dailySleepRequests < maxSleepRequests)
                needs.Add($"сон ({dailySleepRequests}/{maxSleepRequests})");
            if (dailyPlayRequests < maxPlayRequests)
                needs.Add($"игра ({dailyPlayRequests}/{maxPlayRequests})");
            if (dailyBathRequests < maxBathRequests)
                needs.Add($"купание ({dailyBathRequests}/{maxBathRequests})");

            if (needs.Count > 0)
            {
                needsText += "Нужно: " + string.Join(", ", needs);
            }
            else
            {
                needsText += "Все потребности удовлетворены!";
            }

            lblDailyNeeds.Text = needsText;
        }

        private void CheckDailyNeedsCompletion()
        {
            bool allNeedsMet = (dailyFeedRequests >= maxFeedRequests) &&
                              (dailySleepRequests >= maxSleepRequests) &&
                              (dailyPlayRequests >= maxPlayRequests) &&
                              (dailyBathRequests >= maxBathRequests);

            if (allNeedsMet && maxFeedRequests > 0)
            {
                happiness = Math.Min(100, happiness + 15);
                health = Math.Min(100, health + 10);
                lblStatus.Text = "Отличный день! Все потребности удовлетворены!";
                lblStatus.ForeColor = Color.Green;

                if (isSick)
                {
                    sickDays++;
                    if (new Random().Next(100) < 30)
                    {
                        health = Math.Min(100, health + 15);
                    }
                }

                dayCounter++;
                UpdateDailyNeeds();

                MessageBox.Show($"День {dayCounter - 1} завершен! Поросенок счастлив!\nНачинается день {dayCounter}.", "Новый день",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                UpdateOverallStatus();
            }
        }

        private void UpdateStatus()
        {
            pbHunger.Value = hunger;
            pbHappiness.Value = happiness;
            pbCleanliness.Value = cleanliness;
            pbEnergy.Value = energy;
            pbHealth.Value = health;

            lblHunger.Text = $"Сытость: {hunger}%";
            lblHappiness.Text = $"Настроение: {happiness}%";
            lblCleanliness.Text = $"Чистота: {cleanliness}%";
            lblEnergy.Text = $"Энергия: {energy}%";
            lblHealth.Text = $"Здоровье: {health}%";

            if (health > 70)
                pbHealth.ForeColor = Color.Green;
            else if (health > 30)
                pbHealth.ForeColor = Color.Orange;
            else
                pbHealth.ForeColor = Color.Red;

            UpdateOverallStatus();
            UpdatePetImage();
            CheckSickness();
        }

        private void CheckSickness()
        {
            if (cleanliness <= 10 && !isSick && health > 0 && !isDead)
            {
                isSick = true;
                sickDays = 1;
                lblStatus.Text = "Поросенок Визенау заболел от грязи!";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Поросенок Визенау заболел от грязи! Срочно лечите его!", "Болезнь",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btnHeal.Visible = isSick && !isDead;
        }

        private void UpdateOverallStatus()
        {
            if (isDead)
            {
                lblStatus.Text = "Поросенок Визенау умер... RIP";
                lblStatus.ForeColor = Color.DarkRed;
            }
            else if (isSleeping)
            {
                lblStatus.Text = "Поросенок Визенау крепко спит... zZz";
                lblStatus.ForeColor = Color.DarkBlue;
            }
            else if (isSick)
            {
                lblStatus.Text = $"Поросенок Визенау болен! ({sickDays} день)";
                lblStatus.ForeColor = Color.Red;
            }
            else if (hunger <= 20 || happiness <= 20 || energy <= 20 || health <= 30)
            {
                lblStatus.Text = "Поросенок Визенау грустит :(";
                lblStatus.ForeColor = Color.OrangeRed;
            }
            else if (hunger >= 80 && happiness >= 80 && cleanliness >= 80 && energy >= 80 && health >= 80)
            {
                lblStatus.Text = "Поросенок Визенау счастлив! :)";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "Поросенок <<Визенау>> отдыхает";
                lblStatus.ForeColor = Color.Blue;
            }
        }

        private void UpdatePetImage()
        {
            try
            {
                if (isDead)
                {
                    this.BackgroundImage = Properties.Resources.PigDead;
                }
                else if (isSleeping)
                {
                    this.BackgroundImage = Properties.Resources.PigSleep1;
                }
                else if (isSick)
                {
                    this.BackgroundImage = Properties.Resources.PigSick1;
                }
                else if (energy <= 20)
                {
                    this.BackgroundImage = Properties.Resources.PigSleep11;
                    if (!isSick) lblStatus.Text = "Поросенок Визенау хочет спать!";
                }
                else if (hunger <= 20)
                {
                    this.BackgroundImage = Properties.Resources.PigHungry2;
                    if (!isSick) lblStatus.Text = "Поросенок Визенау голоден!";
                }
                else if (cleanliness <= 30)
                {
                    this.BackgroundImage = Properties.Resources.PigFlithy;
                    if (!isSick) lblStatus.Text = "Поросенок Визенау грязный!";
                }
                else if (happiness <= 20)
                {
                    this.BackgroundImage = Properties.Resources.PigSad1;
                    if (!isSick) lblStatus.Text = "Поросенок Визенау грустит!";
                }
                else if (hunger >= 80 && happiness >= 80 && cleanliness >= 80 && energy >= 80 && health >= 80)
                {
                    this.BackgroundImage = Properties.Resources.PigHappy1;
                }
                else
                {
                    this.BackgroundImage = Properties.Resources.Pig1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
            }
        }

        private void HandleDeath(string deathReason)
        {
            isDead = true;
            timerGame.Stop();
            sleepTimer.Stop();
            SetActionButtonsEnabled(false);
            btnRestart.Visible = true;

            UpdateStatus();

            string message = $"Поросенок Визенау умер от {deathReason}!\n";
            message += "Надо было о нем заботится лучше!\n\n";
            message += "Нажмите кнопку 'Завести нового поросенка', чтобы начать заново.";

            MessageBox.Show(message, "Смерть поросенка",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnFeed_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            hunger = Math.Min(100, hunger + 30);
            cleanliness = Math.Max(0, cleanliness - 10);

            if (dailyFeedRequests < maxFeedRequests)
            {
                dailyFeedRequests++;
                happiness = Math.Min(100, happiness + 5);
            }

            UpdateDailyNeedsDisplay();
            CheckDailyNeedsCompletion();
            UpdateStatus();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            cleanliness = Math.Min(100, cleanliness + 40);
            happiness = Math.Max(0, happiness - 5);

            if (dailyBathRequests < maxBathRequests)
            {
                dailyBathRequests++;
                happiness = Math.Min(100, happiness + 8);
            }

            UpdateDailyNeedsDisplay();
            CheckDailyNeedsCompletion();
            UpdateStatus();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            if (energy >= 20)
            {
                happiness = Math.Min(100, happiness + 25);
                energy = Math.Max(0, energy - 20);
                hunger = Math.Max(0, hunger - 10);

                if (dailyPlayRequests < maxPlayRequests)
                {
                    dailyPlayRequests++;
                    happiness = Math.Min(100, happiness + 5);
                }

                UpdateDailyNeedsDisplay();
                CheckDailyNeedsCompletion();
                UpdateStatus();
            }
            else
            {
                MessageBox.Show("Поросенок слишком устал для игр!", "Усталость",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;
            StartSleeping();
        }

        private void StartSleeping()
        {
            isSleeping = true;
            timerGame.Stop();
            SetActionButtonsEnabled(false);
            UpdateStatus();

            if (dailySleepRequests < maxSleepRequests)
            {
                dailySleepRequests++;
                health = Math.Min(100, health + 3);
            }

            UpdateDailyNeedsDisplay();
            sleepTimer.Start();

            MessageBox.Show("Поросенок Визенау засыпает... zZz\nИгра временно приостановлена.", "Сон",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sleepTimer_Tick(object sender, EventArgs e)
        {
            sleepTimer.Stop();
            isSleeping = false;

            energy = Math.Min(100, energy + 40);
            hunger = Math.Max(0, hunger - 15);

            if (!isSick)
            {
                health = Math.Min(100, health + 5);
            }

            SetActionButtonsEnabled(true);
            timerGame.Start();
            UpdateStatus();
            CheckDailyNeedsCompletion();

            if (isSick)
            {
                MessageBox.Show("Поросенок Визенау проснулся, но чувствует себя плохо...", "Пробуждение",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Поросенок Визенау проснулся отдохнувшим и полным сил!", "Пробуждение",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHeal_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            if (isSick)
            {
                health = Math.Min(100, health + 40);
                happiness = Math.Max(0, happiness - 10);
                energy = Math.Max(0, energy - 15);

                if (new Random().Next(100) < 70)
                {
                    isSick = false;
                    sickDays = 0;
                    lblStatus.Text = "Поросенок Визенау выздоровел!";
                    lblStatus.ForeColor = Color.Green;
                    MessageBox.Show("Поросенок Визенау успешно вылечен!", "Лечение",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Лечение не помогло, поросенок все еще болен";
                    lblStatus.ForeColor = Color.Orange;
                    MessageBox.Show("Лечение не помогло, попробуйте еще раз!", "Лечение",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                UpdateStatus();
            }
        }

        private void btnSpeedUp_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            timerGame.Interval = FastSpeed;
            lblStatus.Text = "Скорость игры: Ускоренная";
            lblStatus.ForeColor = Color.OrangeRed;
            MessageBox.Show("Скорость игры увеличена!", "Скорость",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNormalSpeed_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            timerGame.Interval = NormalSpeed;
            lblStatus.Text = "Скорость игры: Обычная";
            lblStatus.ForeColor = Color.Blue;
            MessageBox.Show("Скорость игры установлена на обычную.", "Скорость",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSpeedDown_Click(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            timerGame.Interval = SlowSpeed;
            lblStatus.Text = "Скорость игры: Замедленная";
            lblStatus.ForeColor = Color.DarkGreen;
            MessageBox.Show("Скорость игры замедлена.", "Скорость",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            timerGame.Stop();
            sleepTimer.Stop();

            hunger = 50;
            happiness = 50;
            cleanliness = 50;
            energy = 50;
            health = 100;
            isSick = false;
            sickDays = 0;
            isSleeping = false;
            isDead = false;

            dayCounter = 1;
            timerTickCounter = 0;
            dailyFeedRequests = 0;
            dailySleepRequests = 0;
            dailyPlayRequests = 0;
            dailyBathRequests = 0;

            btnRestart.Visible = false;
            btnHeal.Visible = false;
            SetActionButtonsEnabled(true);
            timerGame.Interval = NormalSpeed;
            timerGame.Start();

            UpdateDailyNeeds();
            UpdateStatus();

            MessageBox.Show("У вас появился новый поросенок Визенау! Заботьтесь о нем лучше!", "Новая жизнь",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetActionButtonsEnabled(bool enabled)
        {
            btnFeed.Enabled = enabled && !isDead;
            btnClean.Enabled = enabled && !isDead;
            btnPlay.Enabled = enabled && !isDead;
            btnSleep.Enabled = enabled && !isDead;
            btnHeal.Enabled = enabled && !isDead;

            btnSpeedUp.Enabled = enabled && !isDead;
            btnNormalSpeed.Enabled = enabled && !isDead;
            btnSpeedDown.Enabled = enabled && !isDead;
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            if (isSleeping || isDead) return;

            timerTickCounter++;

            if (timerTickCounter >= TicksPerDay)
            {
                timerTickCounter = 0;

                if (isSick)
                {
                    sickDays++;
                    UpdateOverallStatus();

                    if (sickDays >= 5)
                    {
                        int deathChance = (sickDays - 4) * 15;
                        if (new Random().Next(100) < deathChance && health <= 20)
                        {
                            HandleDeath($"болезни после {sickDays} дней болезни");
                            return;
                        }
                    }
                }

                if (!AreAllNeedsMet())
                {
                    happiness = Math.Max(0, happiness - 20);
                    health = Math.Max(0, health - 10);
                    lblStatus.Text = "Поросенок расстроен - не все потребности выполнены!";
                    lblStatus.ForeColor = Color.OrangeRed;
                    MessageBox.Show($"День {dayCounter} завершен. Не все потребности были удовлетворены!\nПоросенок расстроен.", "Неполный день",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                dayCounter++;
                UpdateDailyNeeds();
            }

            hunger = Math.Max(0, hunger - 5);
            happiness = Math.Max(0, happiness - 3);
            cleanliness = Math.Max(0, cleanliness - 4);
            energy = Math.Max(0, energy - 2);

            if (isSick)
            {
                health = Math.Max(0, health - 8);
            }
            else
            {
                if (cleanliness > 50 && happiness > 50)
                {
                    health = Math.Min(100, health + 2);
                }
            }

            UpdateStatus();

            if (hunger == 0)
            {
                HandleDeath("голода");
                return;
            }
            else if (happiness == 0)
            {
                HandleDeath("тоски");
                return;
            }
            else if (energy == 0)
            {
                HandleDeath("истощения");
                return;
            }
            else if (health == 0)
            {
                HandleDeath("болезни");
                return;
            }
        }

        private bool AreAllNeedsMet()
        {
            return (dailyFeedRequests >= maxFeedRequests) &&
                   (dailySleepRequests >= maxSleepRequests) &&
                   (dailyPlayRequests >= maxPlayRequests) &&
                   (dailyBathRequests >= maxBathRequests);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdatePetImage();
        }

        private void lblDailyNeeds_Click(object sender, EventArgs e)
        {

        }
    }
}