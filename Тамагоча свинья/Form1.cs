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

        private Label lblStatus;
        private Label lblHunger;
        private Label lblHappiness;
        private Label lblCleanliness;
        private Label lblEnergy;
        private ProgressBar pbHunger;
        private ProgressBar pbHappiness;
        private ProgressBar pbCleanliness;
        private ProgressBar pbEnergy;
        private Button btnFeed;
        private Button btnClean;
        private Button btnPlay;
        private Button btnSleep;
        private Button btnRestart;
        private PictureBox picPet;
        private Timer timerGame;

        // Кнопки управления скоростью
        private Button btnSpeedUp;
        private Button btnNormalSpeed;
        private Button btnSpeedDown;

        // Добавляем константы для первоначальных размеров
        private const int OriginalWidth = 500;
        private const int OriginalHeight = 700; 

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
            this.MinimumSize = new Size(500, 650); // Увеличил минимальную высоту

            picPet = new PictureBox();
            picPet.Size = new Size(200, 200);
            picPet.Location = new Point(150, 20);
            picPet.BorderStyle = BorderStyle.FixedSingle;
            picPet.BackColor = Color.White;
            picPet.SizeMode = PictureBoxSizeMode.Zoom;
            // Настраиваем Anchor для PictureBox - будет центрироваться по горизонтали
            picPet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(picPet);

            lblStatus = new Label();
            lblStatus.Location = new Point(50, 240);
            lblStatus.Size = new Size(400, 30);
            lblStatus.Font = new Font("Arial", 14, FontStyle.Bold);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // Настраиваем Anchor для статуса - будет растягиваться по ширине
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(lblStatus);

            int yPos = 280;

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

            btnFeed = CreateButton("Покормить", 50, yPos);
            btnClean = CreateButton("Убрать", 150, yPos);
            btnPlay = CreateButton("Поиграть", 250, yPos);
            btnSleep = CreateButton("Уложить спать", 350, yPos);

            yPos += 40; // Отступ для кнопок скорости

            // Кнопки управления скоростью
            btnSpeedDown = CreateSpeedButton("Замедлить", Color.LightBlue, 50, yPos);
            btnNormalSpeed = CreateSpeedButton("Обычная", Color.LightGreen, 180, yPos);
            btnSpeedUp = CreateSpeedButton("Ускорить", Color.LightCoral, 310, yPos);

            yPos += 40; // Отступ для кнопки рестарта

            // Кнопка рестарта
            btnRestart = new Button();
            btnRestart.Location = new Point(150, yPos);
            btnRestart.Size = new Size(200, 35);
            btnRestart.Text = "Завести нового поросенка";
            btnRestart.Font = new Font("Arial", 10, FontStyle.Bold);
            btnRestart.BackColor = Color.LightCoral;
            btnRestart.ForeColor = Color.DarkRed;
            btnRestart.Visible = false;
            // Настраиваем Anchor для кнопки рестарта - будет центрироваться по горизонтали
            btnRestart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRestart.Click += btnRestart_Click;
            this.Controls.Add(btnRestart);

            timerGame = new Timer();
            timerGame.Interval = NormalSpeed; // Начинаем с обычной скорости
            timerGame.Tick += timerGame_Tick;

            btnFeed.Click += btnFeed_Click;
            btnClean.Click += btnClean_Click;
            btnPlay.Click += btnPlay_Click;
            btnSleep.Click += btnSleep_Click;

            // Обработчики для кнопок скорости
            btnSpeedUp.Click += btnSpeedUp_Click;
            btnNormalSpeed.Click += btnNormalSpeed_Click;
            btnSpeedDown.Click += btnSpeedDown_Click;

            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize; // Добавляем обработчик изменения размера
        }

        private Label CreateLabel(string text, int y)
        {
            var label = new Label();
            label.Location = new Point(50, y);
            label.Size = new Size(400, 20);
            label.Text = text;
            label.Font = new Font("Arial", 10);
            // Настраиваем Anchor для меток - будут растягиваться по ширине
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
            // Настраиваю Anchor для прогресс-баров - будут растягиваться по ширине
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
            // Настраиваем Anchor для кнопок действий - будут сохранять позицию относительно левого и правого краев
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
            // Настраиваем Anchor для кнопок скорости
            button.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(button);
            return button;
        }

        // НОВЫЙ МЕТОД: Обработчик изменения размера формы
        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateControlsLayout();
        }

        // НОВЫЙ МЕТОД: Обновление расположения элементов при изменении размера
        private void UpdateControlsLayout()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Обновляем размер и позицию PictureBox
            int picSize = Math.Min(200, formWidth - 100);
            picPet.Size = new Size(picSize, picSize);
            picPet.Location = new Point((formWidth - picSize) / 2, 20);

            // Обновляем позицию и размер статуса
            lblStatus.Location = new Point(50, picPet.Bottom + 20);
            lblStatus.Size = new Size(formWidth - 100, 30);

            // Обновляем позиции и размеры прогресс-баров и меток
            int yPos = lblStatus.Bottom + 20;
            int controlWidth = formWidth - 100;

            UpdateControlPosition(lblHunger, pbHunger, yPos, controlWidth);
            yPos += 50;

            UpdateControlPosition(lblHappiness, pbHappiness, yPos, controlWidth);
            yPos += 50;

            UpdateControlPosition(lblCleanliness, pbCleanliness, yPos, controlWidth);
            yPos += 50;

            UpdateControlPosition(lblEnergy, pbEnergy, yPos, controlWidth);
            yPos += 50;

            // Обновляем позиции кнопок действий
            UpdateActionButtonsPosition(yPos, formWidth);
            yPos += 40;

            // Обновляем позиции кнопок скорости
            UpdateSpeedButtonsPosition(yPos, formWidth);
            yPos += 40;

            // Обновляем позицию кнопки рестарта
            btnRestart.Location = new Point((formWidth - 200) / 2, yPos);
            btnRestart.Size = new Size(Math.Min(200, formWidth - 100), 35);
        }

        // НОВЫЙ МЕТОД: Обновление позиций меток и прогресс-баров
        private void UpdateControlPosition(Label label, ProgressBar progressBar, int yPos, int width)
        {
            label.Location = new Point(50, yPos);
            label.Size = new Size(width, 20);

            progressBar.Location = new Point(50, yPos + 25);
            progressBar.Size = new Size(width, 20);
        }

        // НОВЫЙ МЕТОД: Обновление позиций кнопок действий
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

        // НОВЫЙ МЕТОД: Обновление позиций кнопок скорости
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
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            pbHunger.Value = hunger;
            pbHappiness.Value = happiness;
            pbCleanliness.Value = cleanliness;
            pbEnergy.Value = energy;

            lblHunger.Text = $"Голод: {hunger}%";
            lblHappiness.Text = $"Счастье: {happiness}%";
            lblCleanliness.Text = $"Чистота: {cleanliness}%";
            lblEnergy.Text = $"Энергия: {energy}%";

            UpdateOverallStatus();
            UpdatePetImage();
        }

      
        private void UpdateOverallStatus()
        {
            if (hunger <= 20 || happiness <= 20 || cleanliness <= 20 || energy <= 20)
            {
                lblStatus.Text = "Поросенок Визенау грустит :(";
                lblStatus.ForeColor = Color.Red;
            }
            else if (hunger >= 80 && happiness >= 80 && cleanliness >= 80 && energy >= 80)
            {
                lblStatus.Text = "Поросенок Визенау счастлив! :)";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "Поросенок Визенау хорошо себя чуствует";
                lblStatus.ForeColor = Color.Blue;
            }
        }

        private void UpdatePetImage()
        {
            try
            {
                string imagePath = "";

                if (energy <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigSleep.png";
                    lblStatus.Text = "Поросенок Визенау хочет спать!";
                }
                else if (hunger <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigHungry.png";
                    lblStatus.Text = "Поросенок Визенау голоден!";
                }
                else if (cleanliness <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigFilthy.png";
                    lblStatus.Text = "Поросенок Визенау грязный!";
                }
                else if (happiness <= 20)
                {
                    imagePath = @"C:\Users\User\Desktop\Tamagochi\Тамагоча свинья\PigsImage\PigSad.png";
                    lblStatus.Text = "Поросенок Визенау грустит!";
                }
                else if (hunger >= 80 && happiness >= 80 && cleanliness >= 80 && energy >= 80)
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
                if (energy <= 20) state = "Сонный";
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
            UpdateStatus();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cleanliness = Math.Min(100, cleanliness + 40);
            happiness = Math.Max(0, happiness - 5);
            UpdateStatus();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            happiness = Math.Min(100, happiness + 25);
            energy = Math.Max(0, energy - 20);
            hunger = Math.Max(0, hunger - 10);
            UpdateStatus();
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            energy = Math.Min(100, energy + 40);
            hunger = Math.Max(0, hunger - 15);
            UpdateStatus();
        }

        // Обработчики для кнопок управления скоростью
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

            btnRestart.Visible = false;
            SetActionButtonsEnabled(true);
            timerGame.Interval = NormalSpeed; // Возвращаем обычную скорость
            timerGame.Start();
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

            // Также включаем/выключаем кнопки скорости
            btnSpeedUp.Enabled = enabled;
            btnNormalSpeed.Enabled = enabled;
            btnSpeedDown.Enabled = enabled;
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            hunger = Math.Max(0, hunger - 5);
            happiness = Math.Max(0, happiness - 3);
            cleanliness = Math.Max(0, cleanliness - 4);
            energy = Math.Max(0, energy - 2);

            UpdateStatus();

            if (hunger == 0 || happiness == 0 || cleanliness == 0 || energy == 0)
            {
                timerGame.Stop();
                SetActionButtonsEnabled(false);
                btnRestart.Visible = true;

                MessageBox.Show("Поросенок Визенау пошел на шаурму! Надо было о нем заботится лучше!",
                              "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdatePetImage();
            // Вызываем обновление layout при загрузке формы
            UpdateControlsLayout();
        }
    }
}