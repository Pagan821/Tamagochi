using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // Система ежедневных потребностей
        private int dayCounter = 1;
        private int dailyFeedRequests = 0;
        private int dailySleepRequests = 0;
        private int dailyPlayRequests = 0;
        private int dailyBathRequests = 0;
        private int maxFeedRequests = 3;    // 3 раза в день хочет есть
        private int maxSleepRequests = 2;   // 2 раза в день хочет спать
        private int maxPlayRequests = 2;    // 2 раза в день хочет играть
        private int maxBathRequests = 1;    // 1 раз в 2 дня хочет купаться

        // Для отслеживания времени
        private int timerTickCounter = 0;
        private const int TicksPerDay = 15; // Каждые 15 тиков = 1 день

        private Label lblStatus;
        private Label lblHunger;
        private Label lblHappiness;
        private Label lblCleanliness;
        private Label lblEnergy;
        private Label lblHealth;
        private Label lblDailyNeeds; // Новый лейбл для отображения потребностей
        private ProgressBar pbHunger;
        private ProgressBar pbHappiness;
        private ProgressBar pbCleanliness;
        private ProgressBar pbEnergy;
        private ProgressBar pbHealth;
        private Button btnFeed;
        private Button btnClean;
        private Button btnPlay;
        private Button btnSleep;
        private Button btnRestart;
        private Button btnHeal;
        private PictureBox picPet;
        private Timer timerGame;

        // Кнопки управления скоростью
        private Button btnSpeedUp;
        private Button btnNormalSpeed;
        private Button btnSpeedDown;

        // Добавляем константы для первоначальных размеров
        private const int OriginalWidth = 500;
        private const int OriginalHeight = 750;

        // Константы скоростей игры (интервалы в миллисекундах)
        private const int SlowSpeed = 10000;
        private const int NormalSpeed = 6000;
        private const int FastSpeed = 3000;

        public MainForm()
        {
            InitializeCustomComponents();
            InitializeGame();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Тамагочи - Поросенок Визенау";
            this.Size = new Size(OriginalWidth, OriginalHeight);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;
            this.MinimumSize = new Size(500, 700);

            picPet = new PictureBox();
            picPet.Size = new Size(200, 200);
            picPet.Location = new Point(150, 20);
            picPet.BorderStyle = BorderStyle.FixedSingle;
            picPet.BackColor = Color.White;
            picPet.SizeMode = PictureBoxSizeMode.Zoom;
            picPet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(picPet);

            lblStatus = new Label();
            lblStatus.Location = new Point(50, 240);
            lblStatus.Size = new Size(400, 30);
            lblStatus.Font = new Font("Arial", 14, FontStyle.Bold);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(lblStatus);

            // Лейбл для ежедневных потребностей
            lblDailyNeeds = new Label();
            lblDailyNeeds.Location = new Point(50, 275);
            lblDailyNeeds.Size = new Size(400, 40);
            lblDailyNeeds.Font = new Font("Arial", 9, FontStyle.Italic);
            lblDailyNeeds.TextAlign = ContentAlignment.MiddleCenter;
            lblDailyNeeds.ForeColor = Color.DarkBlue;
            lblDailyNeeds.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(lblDailyNeeds);

            int yPos = 320;

            lblHunger = CreateLabel("Голод: 50%", yPos);
            pbHunger = CreateProgressBar(yPos + 25);
            yPos += 50;

            lblHappiness = CreateLabel("Счастье: 50%", yPos);
            pbHappiness = CreateProgressBar(yPos + 25);
            yPos += 50;

            lblCleanliness = CreateLabel("Чистота: 50%", yPos);
            pbCleanliness = CreateProgressBar(yPos + 25);
            yPos += 50;

            lblEnergy = CreateLabel("Энергия: 50%", yPos);
            pbEnergy = CreateProgressBar(yPos + 25);
            yPos += 50;

            // Добавляем здоровье
            lblHealth = CreateLabel("Здоровье: 100%", yPos);
            pbHealth = CreateProgressBar(yPos + 25);
            pbHealth.ForeColor = Color.Green;
            yPos += 50;

            btnFeed = CreateButton("Покормить", 50, yPos);
            btnClean = CreateButton("Убрать", 150, yPos);
            btnPlay = CreateButton("Поиграть", 250, yPos);
            btnSleep = CreateButton("Уложить спать", 350, yPos);

            yPos += 40;

            // Добавляем кнопку лечения
            btnHeal = CreateHealButton("Лечить", 150, yPos);
            yPos += 40;

            // Кнопки управления скоростью
            btnSpeedDown = CreateSpeedButton("Замедлить", Color.LightBlue, 50, yPos);
            btnNormalSpeed = CreateSpeedButton("Обычная", Color.LightGreen, 180, yPos);
            btnSpeedUp = CreateSpeedButton("Ускорить", Color.LightCoral, 310, yPos);

            yPos += 40;

            // Кнопка рестарта
            btnRestart = new Button();
            btnRestart.Location = new Point(150, yPos);
            btnRestart.Size = new Size(200, 35);
            btnRestart.Text = "Завести нового поросенка";
            btnRestart.Font = new Font("Arial", 10, FontStyle.Bold);
            btnRestart.BackColor = Color.LightCoral;
            btnRestart.ForeColor = Color.DarkRed;
            btnRestart.Visible = false;
            btnRestart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRestart.Click += btnRestart_Click;
            this.Controls.Add(btnRestart);

            timerGame = new Timer();
            timerGame.Interval = NormalSpeed;
            timerGame.Tick += timerGame_Tick;

            btnFeed.Click += btnFeed_Click;
            btnClean.Click += btnClean_Click;
            btnPlay.Click += btnPlay_Click;
            btnSleep.Click += btnSleep_Click;
            btnHeal.Click += btnHeal_Click;

            // Обработчики для кнопок скорости
            btnSpeedUp.Click += btnSpeedUp_Click;
            btnNormalSpeed.Click += btnNormalSpeed_Click;
            btnSpeedDown.Click += btnSpeedDown_Click;

            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;
        }

        private Label CreateLabel(string text, int y)
        {
            var label = new Label();
            label.Location = new Point(50, y);
            label.Size = new Size(400, 20);
            label.Text = text;
            label.Font = new Font("Arial", 10);
            label.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(label);
            return label;
        }

        private ProgressBar CreateProgressBar(int y)
        {
            var pb = new ProgressBar();
            pb.Location = new Point(50, y);
            pb.Size = new Size(400, 20);
            pb.Minimum = 0;
            pb.Maximum = 100;
            pb.Value = 50;
            pb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(pb);
            return pb;
        }

        private Button CreateButton(string text, int x, int y)
        {
            var button = new Button();
            button.Location = new Point(x, y);
            button.Size = new Size(90, 30);
            button.Text = text;
            button.Font = new Font("Arial", 9);
            button.BackColor = Color.LightGreen;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(button);
            return button;
        }

        private Button CreateHealButton(string text, int x, int y)
        {
            var button = new Button();
            button.Location = new Point(x, y);
            button.Size = new Size(200, 30);
            button.Text = text;
            button.Font = new Font("Arial", 9, FontStyle.Bold);
            button.BackColor = Color.LightYellow;
            button.ForeColor = Color.DarkOrange;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(button);
            return button;
        }

        private Button CreateSpeedButton(string text, Color color, int x, int y)
        {
            var button = new Button();
            button.Location = new Point(x, y);
            button.Size = new Size(120, 30);
            button.Text = text;
            button.Font = new Font("Arial", 9, FontStyle.Bold);
            button.BackColor = color;
            button.ForeColor = Color.DarkBlue;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(button);
            return button;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateControlsLayout();
        }

        private void UpdateControlsLayout()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            int picSize = Math.Min(200, formWidth - 100);
            picPet.Size = new Size(picSize, picSize);
            picPet.Location = new Point((formWidth - picSize) / 2, 20);

            lblStatus.Location = new Point(50, picPet.Bottom + 20);
            lblStatus.Size = new Size(formWidth - 100, 30);

            lblDailyNeeds.Location = new Point(50, lblStatus.Bottom + 5);
            lblDailyNeeds.Size = new Size(formWidth - 100, 40);

            int yPos = lblDailyNeeds.Bottom + 10;
            int controlWidth = formWidth - 100;

            UpdateControlPosition(lblHunger, pbHunger, yPos, controlWidth);
            yPos += 50;

            UpdateControlPosition(lblHappiness, pbHappiness, yPos, controlWidth);
            yPos += 50;

            UpdateControlPosition(lblCleanliness, pbCleanliness, yPos, controlWidth);
            yPos += 50;

            UpdateControlPosition(lblEnergy, pbEnergy, yPos, controlWidth);
            yPos += 50;

            UpdateControlPosition(lblHealth, pbHealth, yPos, controlWidth);
            yPos += 50;

            UpdateActionButtonsPosition(yPos, formWidth);
            yPos += 40;

            btnHeal.Location = new Point((formWidth - 200) / 2, yPos);
            btnHeal.Size = new Size(Math.Min(200, formWidth - 100), 30);
            yPos += 40;

            UpdateSpeedButtonsPosition(yPos, formWidth);
            yPos += 40;

            btnRestart.Location = new Point((formWidth - 200) / 2, yPos);
            btnRestart.Size = new Size(Math.Min(200, formWidth - 100), 35);
        }

        private void UpdateControlPosition(Label label, ProgressBar progressBar, int yPos, int width)
        {
            label.Location = new Point(50, yPos);
            label.Size = new Size(width, 20);

            progressBar.Location = new Point(50, yPos + 25);
            progressBar.Size = new Size(width, 20);
        }

        private void UpdateActionButtonsPosition(int yPos, int formWidth)
        {
            int buttonWidth = (formWidth - 120) / 4;
            buttonWidth = Math.Min(100, buttonWidth);

            int totalWidth = buttonWidth * 4 + 30;
            int startX = (formWidth - totalWidth) / 2;

            btnFeed.Location = new Point(startX, yPos);
            btnFeed.Size = new Size(buttonWidth, 30);

            btnClean.Location = new Point(startX + buttonWidth + 10, yPos);
            btnClean.Size = new Size(buttonWidth, 30);

            btnPlay.Location = new Point(startX + (buttonWidth + 10) * 2, yPos);
            btnPlay.Size = new Size(buttonWidth, 30);

            btnSleep.Location = new Point(startX + (buttonWidth + 10) * 3, yPos);
            btnSleep.Size = new Size(buttonWidth, 30);
        }

        private void UpdateSpeedButtonsPosition(int yPos, int formWidth)
        {
            int buttonWidth = (formWidth - 40) / 3;
            buttonWidth = Math.Min(120, buttonWidth);

            int totalWidth = buttonWidth * 3 + 20;
            int startX = (formWidth - totalWidth) / 2;

            btnSpeedDown.Location = new Point(startX, yPos);
            btnSpeedDown.Size = new Size(buttonWidth, 30);

            btnNormalSpeed.Location = new Point(startX + buttonWidth + 10, yPos);
            btnNormalSpeed.Size = new Size(buttonWidth, 30);

            btnSpeedUp.Location = new Point(startX + (buttonWidth + 10) * 2, yPos);
            btnSpeedUp.Size = new Size(buttonWidth, 30);
        }

        private void InitializeGame()
        {
            timerGame.Start();
            UpdateDailyNeeds();
            UpdateStatus();
        }

        // Новый метод для обновления ежедневных потребностей
        private void UpdateDailyNeeds()
        {
            // Сбрасываем счетчики каждый день
            dailyFeedRequests = 0;
            dailySleepRequests = 0;
            dailyPlayRequests = 0;

            // Купание - каждый второй день
            if (dayCounter % 2 == 0)
            {
                dailyBathRequests = 0;
                maxBathRequests = 1;
            }
            else
            {
                maxBathRequests = 0;
            }

            // Генерируем случайные потребности на день
            Random rand = new Random();
            maxFeedRequests = rand.Next(2, 4); // 2-3 раза поесть
            maxSleepRequests = rand.Next(1, 3); // 1-2 раза поспать
            maxPlayRequests = rand.Next(1, 3); // 1-2 раза поиграть

            UpdateDailyNeedsDisplay();
        }

        // Отображение текущих потребностей
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

        // Проверка выполнения всех ежедневных потребностей
        private void CheckDailyNeedsCompletion()
        {
            bool allNeedsMet = (dailyFeedRequests >= maxFeedRequests) &&
                              (dailySleepRequests >= maxSleepRequests) &&
                              (dailyPlayRequests >= maxPlayRequests) &&
                              (dailyBathRequests >= maxBathRequests);

            if (allNeedsMet)
            {
                // Бонус за выполнение всех потребностей
                happiness = Math.Min(100, happiness + 15);
                health = Math.Min(100, health + 10);
                lblStatus.Text = "Отличный день! Все потребности удовлетворены!";
                lblStatus.ForeColor = Color.Green;

                // Если поросенок болен, увеличиваем счетчик дней болезни при смене дня
                if (isSick)
                {
                    sickDays++;
                }

                // Переходим к следующему дню
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

            lblHunger.Text = $"Голод: {hunger}%";
            lblHappiness.Text = $"Счастье: {happiness}%";
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
            // Поросенок заболевает, если чистота ниже 20% и не болеет
            if (cleanliness <= 20 && !isSick && health > 0)
            {
                isSick = true;
                sickDays = 1; // ИЗМЕНЕНИЕ: устанавливаем 1 день болезни сразу
                lblStatus.Text = "Поросенок Визенау заболел от грязи!";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Поросенок Визенау заболел от грязи! Срочно лечите его!", "Болезнь",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Обновляем видимость кнопки лечения
            btnHeal.Visible = isSick;
        }

        private void UpdateOverallStatus()
        {
            if (isSick)
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
                lblStatus.Text = "Поросенок Визенау хорошо себя чувствует";
                lblStatus.ForeColor = Color.Blue;
            }
        }

        private void UpdatePetImage()
        {
            try
            {
                string imagePath = "";

                if (isSick)
                {
                    imagePath = @"D:\Тамагочи на 14.09.2025\Поросенок Визенау Тамагочи на 14.09.2025\Поросенок Визенау Тамагочи на 14.09.2025\Pigs\PigSick.png";
                }
                else if (energy <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigSleep.png";
                    if (!isSick) lblStatus.Text = "Поросенок Визенау хочет спать!";
                }
                else if (hunger <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigHungry.png";
                    if (!isSick) lblStatus.Text = "Поросенок Визенау голоден!";
                }
                else if (cleanliness <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigFilthy.png";
                    if (!isSick) lblStatus.Text = "Поросенок Визенау грязный!";
                }
                else if (happiness <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigSad.png";
                    if (!isSick) lblStatus.Text = "Поросенок Визенау грустит!";
                }
                else if (hunger >= 80 && happiness >= 80 && cleanliness >= 80 && energy >= 80 && health >= 80)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigHappy.png";
                }
                else
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\Pig.png";
                }

                if (System.IO.File.Exists(imagePath))
                {
                    picPet.Image = Image.FromFile(imagePath);
                    picPet.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    ShowDefaultImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                ShowDefaultImage();
            }
        }

        private void ShowDefaultImage()
        {
            picPet.BackColor = Color.Pink;
            using (Graphics g = picPet.CreateGraphics())
            {
                g.Clear(Color.Pink);
                g.DrawString("Поросенок Визенау",
                    new Font("Arial", 12), Brushes.Black, new PointF(10, 80));

                string state = "";
                if (isSick) state = "Больной";
                else if (energy <= 20) state = "Сонный";
                else if (hunger <= 20) state = "Голодный";
                else if (cleanliness <= 20) state = "Грязный";
                else if (happiness <= 20) state = "Грустный";
                else state = "Нормальный";

                g.DrawString($"Состояние: {state}",
                    new Font("Arial", 10), Brushes.Black, new PointF(10, 110));
            }
        }

        private void btnFeed_Click(object sender, EventArgs e)
        {
            hunger = Math.Min(100, hunger + 30);
            cleanliness = Math.Max(0, cleanliness - 10);

            // Учитываем в ежедневных потребностях
            if (dailyFeedRequests < maxFeedRequests)
            {
                dailyFeedRequests++;
                happiness = Math.Min(100, happiness + 5); // Бонус за выполнение потребности
            }

            UpdateDailyNeedsDisplay();
            CheckDailyNeedsCompletion();
            UpdateStatus();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cleanliness = Math.Min(100, cleanliness + 40);
            happiness = Math.Max(0, happiness - 5);

            // Учитываем в ежедневных потребностях (купание)
            if (dailyBathRequests < maxBathRequests)
            {
                dailyBathRequests++;
                happiness = Math.Min(100, happiness + 8); // Бонус за выполнение потребности
            }

            UpdateDailyNeedsDisplay();
            CheckDailyNeedsCompletion();
            UpdateStatus();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            happiness = Math.Min(100, happiness + 25);
            energy = Math.Max(0, energy - 20);
            hunger = Math.Max(0, hunger - 10);

            // Учитываем в ежедневных потребностях
            if (dailyPlayRequests < maxPlayRequests)
            {
                dailyPlayRequests++;
                happiness = Math.Min(100, happiness + 5); // Дополнительный бонус
            }

            UpdateDailyNeedsDisplay();
            CheckDailyNeedsCompletion();
            UpdateStatus();
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            energy = Math.Min(100, energy + 40);
            hunger = Math.Max(0, hunger - 15);

            // Сон помогает восстановить здоровье только если поросенок не болен
            if (!isSick)
            {
                health = Math.Min(100, health + 5);
            }

            // Учитываем в ежедневных потребностях
            if (dailySleepRequests < maxSleepRequests)
            {
                dailySleepRequests++;
                health = Math.Min(100, health + 3); // Бонус за выполнение потребности
            }

            UpdateDailyNeedsDisplay();
            CheckDailyNeedsCompletion();
            UpdateStatus();
        }

        private void btnHeal_Click(object sender, EventArgs e)
        {
            if (isSick)
            {
                health = Math.Min(100, health + 40);
                happiness = Math.Max(0, happiness - 10);
                energy = Math.Max(0, energy - 15);

                // Шанс выздоровления 70%
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
                    // ИЗМЕНЕНИЕ: при неудачном лечении НЕ сбрасываем счетчик дней болезни
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
            timerGame.Interval = FastSpeed;
            lblStatus.Text = "Скорость игры: Ускоренная";
            lblStatus.ForeColor = Color.OrangeRed;
            MessageBox.Show("Скорость игры увеличена!", "Скорость",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNormalSpeed_Click(object sender, EventArgs e)
        {
            timerGame.Interval = NormalSpeed;
            lblStatus.Text = "Скорость игры: Обычная";
            lblStatus.ForeColor = Color.Blue;
            MessageBox.Show("Скорость игры установлена на обычную.", "Скорость",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSpeedDown_Click(object sender, EventArgs e)
        {
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
            hunger = 50;
            happiness = 50;
            cleanliness = 50;
            energy = 50;
            health = 100;
            isSick = false;
            sickDays = 0;

            // Сбрасываем систему дней и потребностей
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
            btnFeed.Enabled = enabled;
            btnClean.Enabled = enabled;
            btnPlay.Enabled = enabled;
            btnSleep.Enabled = enabled;
            btnHeal.Enabled = enabled;

            btnSpeedUp.Enabled = enabled;
            btnNormalSpeed.Enabled = enabled;
            btnSpeedDown.Enabled = enabled;
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
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
                            timerGame.Stop();
                            SetActionButtonsEnabled(false);
                            btnRestart.Visible = true;
                            MessageBox.Show($"Поросенок Визенау умер от болезни после {sickDays} дней болезни! Надо было лечить его вовремя!",
                                          "Смерть", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Если не все потребности выполнены - штраф
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

            // Если поросенок болен, здоровье ухудшается каждый тик
            if (isSick)
            {
                health = Math.Max(0, health - 8);
            }
            else
            {
                // Медленное естественное восстановление здоровья, если не болен
                if (cleanliness > 50 && happiness > 50)
                {
                    health = Math.Min(100, health + 2);
                }
            }

            UpdateStatus();

            // Условия смерти
            if (hunger == 0 || happiness == 0 || energy == 0 || health == 0)
            {
                timerGame.Stop();
                SetActionButtonsEnabled(false);
                btnRestart.Visible = true;

                string deathReason = "";
                if (hunger == 0) deathReason = "голода";
                else if (happiness == 0) deathReason = "тоски";
                else if (energy == 0) deathReason = "истощения";
                else if (health == 0) deathReason = "болезни";

                MessageBox.Show($"Поросенок Визенау умер от {deathReason}! Надо было о нем заботится лучше!",
                              "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Проверка выполнения всех потребностей
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
            UpdateControlsLayout();
        }
    }
}
