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
        private PictureBox picPet;
        private Timer timerGame;

        public MainForm()
        {
            InitializeCustomComponents();
            InitializeGame();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Тамагочи - Поросенок Визенау";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;

            picPet = new PictureBox();
            picPet.Size = new Size(200, 200);
            picPet.Location = new Point(150, 20);
            picPet.BorderStyle = BorderStyle.FixedSingle;
            picPet.BackColor = Color.White;
            this.Controls.Add(picPet);

            lblStatus = new Label();
            lblStatus.Location = new Point(50, 240);
            lblStatus.Size = new Size(400, 30);
            lblStatus.Font = new Font("Arial", 14, FontStyle.Bold);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
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

            timerGame = new Timer();
            timerGame.Interval = 5000;
            timerGame.Tick += timerGame_Tick;

            btnFeed.Click += btnFeed_Click;
            btnClean.Click += btnClean_Click;
            btnPlay.Click += btnPlay_Click;
            btnSleep.Click += btnSleep_Click;

            this.Load += MainForm_Load; 
        }

        private Label CreateLabel(string text, int y)
        {
            var label = new Label();
            label.Location = new Point(50, y);
            label.Size = new Size(400, 20);
            label.Text = text;
            label.Font = new Font("Arial", 10);
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
            this.Controls.Add(button);
            return button;
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
                MessageBox.Show("Поросенок Визенау пошел на шаурму! Надо было о нем заботится лучше!", "Внимание",MessageBoxButtons.OK, MessageBoxIcon.Warning);

                hunger = 50;
                happiness = 50;
                cleanliness = 50;
                energy = 50;
                timerGame.Start();
                UpdateStatus();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdatePetImage();
        }
    }
}
