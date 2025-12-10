namespace Тамагоча_свинья
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDailyNeeds = new System.Windows.Forms.Label();
            this.lblHunger = new System.Windows.Forms.Label();
            this.pbHunger = new System.Windows.Forms.ProgressBar();
            this.lblHappiness = new System.Windows.Forms.Label();
            this.pbHappiness = new System.Windows.Forms.ProgressBar();
            this.lblCleanliness = new System.Windows.Forms.Label();
            this.pbCleanliness = new System.Windows.Forms.ProgressBar();
            this.lblEnergy = new System.Windows.Forms.Label();
            this.pbEnergy = new System.Windows.Forms.ProgressBar();
            this.lblHealth = new System.Windows.Forms.Label();
            this.pbHealth = new System.Windows.Forms.ProgressBar();
            this.btnFeed = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnSleep = new System.Windows.Forms.Button();
            this.btnHeal = new System.Windows.Forms.Button();
            this.btnSpeedDown = new System.Windows.Forms.Button();
            this.btnNormalSpeed = new System.Windows.Forms.Button();
            this.btnSpeedUp = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            this.sleepTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(445, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(339, 22);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Поросенок <<Визенау>> отдыхает";
            // 
            // lblDailyNeeds
            // 
            this.lblDailyNeeds.AutoSize = true;
            this.lblDailyNeeds.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDailyNeeds.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblDailyNeeds.Location = new System.Drawing.Point(446, 57);
            this.lblDailyNeeds.Name = "lblDailyNeeds";
            this.lblDailyNeeds.Size = new System.Drawing.Size(197, 15);
            this.lblDailyNeeds.TabIndex = 1;
            this.lblDailyNeeds.Text = "День 1: Нужно: еда (0/2), сон (0/1)";
            this.lblDailyNeeds.Click += new System.EventHandler(this.lblDailyNeeds_Click);
            // 
            // lblHunger
            // 
            this.lblHunger.AutoSize = true;
            this.lblHunger.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHunger.Location = new System.Drawing.Point(12, 227);
            this.lblHunger.Name = "lblHunger";
            this.lblHunger.Size = new System.Drawing.Size(100, 16);
            this.lblHunger.TabIndex = 2;
            this.lblHunger.Text = "Сытость: 50%";
            // 
            // pbHunger
            // 
            this.pbHunger.Location = new System.Drawing.Point(12, 252);
            this.pbHunger.Name = "pbHunger";
            this.pbHunger.Size = new System.Drawing.Size(275, 20);
            this.pbHunger.TabIndex = 3;
            this.pbHunger.Value = 50;
            // 
            // lblHappiness
            // 
            this.lblHappiness.AutoSize = true;
            this.lblHappiness.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHappiness.Location = new System.Drawing.Point(12, 277);
            this.lblHappiness.Name = "lblHappiness";
            this.lblHappiness.Size = new System.Drawing.Size(122, 16);
            this.lblHappiness.TabIndex = 4;
            this.lblHappiness.Text = "Настроение: 50%";
            // 
            // pbHappiness
            // 
            this.pbHappiness.Location = new System.Drawing.Point(12, 302);
            this.pbHappiness.Name = "pbHappiness";
            this.pbHappiness.Size = new System.Drawing.Size(275, 20);
            this.pbHappiness.TabIndex = 5;
            this.pbHappiness.Value = 50;
            // 
            // lblCleanliness
            // 
            this.lblCleanliness.AutoSize = true;
            this.lblCleanliness.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCleanliness.Location = new System.Drawing.Point(12, 327);
            this.lblCleanliness.Name = "lblCleanliness";
            this.lblCleanliness.Size = new System.Drawing.Size(96, 16);
            this.lblCleanliness.TabIndex = 6;
            this.lblCleanliness.Text = "Чистота: 50%";
            // 
            // pbCleanliness
            // 
            this.pbCleanliness.Location = new System.Drawing.Point(12, 352);
            this.pbCleanliness.Name = "pbCleanliness";
            this.pbCleanliness.Size = new System.Drawing.Size(275, 20);
            this.pbCleanliness.TabIndex = 7;
            this.pbCleanliness.Value = 50;
            // 
            // lblEnergy
            // 
            this.lblEnergy.AutoSize = true;
            this.lblEnergy.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblEnergy.Location = new System.Drawing.Point(12, 377);
            this.lblEnergy.Name = "lblEnergy";
            this.lblEnergy.Size = new System.Drawing.Size(98, 16);
            this.lblEnergy.TabIndex = 8;
            this.lblEnergy.Text = "Энергия: 50%";
            // 
            // pbEnergy
            // 
            this.pbEnergy.Location = new System.Drawing.Point(12, 402);
            this.pbEnergy.Name = "pbEnergy";
            this.pbEnergy.Size = new System.Drawing.Size(275, 20);
            this.pbEnergy.TabIndex = 9;
            this.pbEnergy.Value = 50;
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHealth.Location = new System.Drawing.Point(12, 427);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(114, 16);
            this.lblHealth.TabIndex = 10;
            this.lblHealth.Text = "Здоровье: 100%";
            // 
            // pbHealth
            // 
            this.pbHealth.ForeColor = System.Drawing.Color.Green;
            this.pbHealth.Location = new System.Drawing.Point(12, 452);
            this.pbHealth.Name = "pbHealth";
            this.pbHealth.Size = new System.Drawing.Size(275, 20);
            this.pbHealth.TabIndex = 11;
            this.pbHealth.Value = 100;
            // 
            // btnFeed
            // 
            this.btnFeed.BackColor = System.Drawing.Color.LightGreen;
            this.btnFeed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFeed.Location = new System.Drawing.Point(12, 493);
            this.btnFeed.Name = "btnFeed";
            this.btnFeed.Size = new System.Drawing.Size(133, 30);
            this.btnFeed.TabIndex = 12;
            this.btnFeed.Text = "Покормить";
            this.btnFeed.UseVisualStyleBackColor = false;
            this.btnFeed.Click += new System.EventHandler(this.btnFeed_Click);
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.Color.LightGreen;
            this.btnClean.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClean.Location = new System.Drawing.Point(151, 493);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(136, 30);
            this.btnClean.TabIndex = 13;
            this.btnClean.Text = "Почистить";
            this.btnClean.UseVisualStyleBackColor = false;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.LightGreen;
            this.btnPlay.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlay.Location = new System.Drawing.Point(12, 539);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(133, 30);
            this.btnPlay.TabIndex = 14;
            this.btnPlay.Text = "Поиграть";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnSleep
            // 
            this.btnSleep.BackColor = System.Drawing.Color.LightGreen;
            this.btnSleep.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSleep.Location = new System.Drawing.Point(151, 539);
            this.btnSleep.Name = "btnSleep";
            this.btnSleep.Size = new System.Drawing.Size(136, 30);
            this.btnSleep.TabIndex = 15;
            this.btnSleep.Text = "Уложить спать";
            this.btnSleep.UseVisualStyleBackColor = false;
            this.btnSleep.Click += new System.EventHandler(this.btnSleep_Click);
            // 
            // btnHeal
            // 
            this.btnHeal.BackColor = System.Drawing.Color.LightYellow;
            this.btnHeal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnHeal.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnHeal.Location = new System.Drawing.Point(12, 575);
            this.btnHeal.Name = "btnHeal";
            this.btnHeal.Size = new System.Drawing.Size(275, 30);
            this.btnHeal.TabIndex = 16;
            this.btnHeal.Text = "Лечить";
            this.btnHeal.UseVisualStyleBackColor = false;
            this.btnHeal.Visible = false;
            this.btnHeal.Click += new System.EventHandler(this.btnHeal_Click);
            // 
            // btnSpeedDown
            // 
            this.btnSpeedDown.BackColor = System.Drawing.Color.LightBlue;
            this.btnSpeedDown.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSpeedDown.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSpeedDown.Location = new System.Drawing.Point(12, 623);
            this.btnSpeedDown.Name = "btnSpeedDown";
            this.btnSpeedDown.Size = new System.Drawing.Size(118, 30);
            this.btnSpeedDown.TabIndex = 17;
            this.btnSpeedDown.Text = "Замедлить";
            this.btnSpeedDown.UseVisualStyleBackColor = false;
            this.btnSpeedDown.Click += new System.EventHandler(this.btnSpeedDown_Click);
            // 
            // btnNormalSpeed
            // 
            this.btnNormalSpeed.BackColor = System.Drawing.Color.LightGreen;
            this.btnNormalSpeed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNormalSpeed.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnNormalSpeed.Location = new System.Drawing.Point(142, 623);
            this.btnNormalSpeed.Name = "btnNormalSpeed";
            this.btnNormalSpeed.Size = new System.Drawing.Size(118, 30);
            this.btnNormalSpeed.TabIndex = 18;
            this.btnNormalSpeed.Text = "Обычная";
            this.btnNormalSpeed.UseVisualStyleBackColor = false;
            this.btnNormalSpeed.Click += new System.EventHandler(this.btnNormalSpeed_Click);
            // 
            // btnSpeedUp
            // 
            this.btnSpeedUp.BackColor = System.Drawing.Color.LightCoral;
            this.btnSpeedUp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSpeedUp.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSpeedUp.Location = new System.Drawing.Point(272, 623);
            this.btnSpeedUp.Name = "btnSpeedUp";
            this.btnSpeedUp.Size = new System.Drawing.Size(118, 30);
            this.btnSpeedUp.TabIndex = 19;
            this.btnSpeedUp.Text = "Ускорить";
            this.btnSpeedUp.UseVisualStyleBackColor = false;
            this.btnSpeedUp.Click += new System.EventHandler(this.btnSpeedUp_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.LightCoral;
            this.btnRestart.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestart.ForeColor = System.Drawing.Color.DarkRed;
            this.btnRestart.Location = new System.Drawing.Point(536, 628);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(198, 35);
            this.btnRestart.TabIndex = 20;
            this.btnRestart.Text = "Завести нового поросенка";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Visible = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // timerGame
            // 
            this.timerGame.Interval = 6000;
            this.timerGame.Tick += new System.EventHandler(this.timerGame_Tick);
            // 
            // sleepTimer
            // 
            this.sleepTimer.Interval = 4000;
            this.sleepTimer.Tick += new System.EventHandler(this.sleepTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1237, 675);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnSpeedUp);
            this.Controls.Add(this.btnNormalSpeed);
            this.Controls.Add(this.btnSpeedDown);
            this.Controls.Add(this.btnHeal);
            this.Controls.Add(this.btnSleep);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnFeed);
            this.Controls.Add(this.pbHealth);
            this.Controls.Add(this.lblHealth);
            this.Controls.Add(this.pbEnergy);
            this.Controls.Add(this.lblEnergy);
            this.Controls.Add(this.pbCleanliness);
            this.Controls.Add(this.lblCleanliness);
            this.Controls.Add(this.pbHappiness);
            this.Controls.Add(this.lblHappiness);
            this.Controls.Add(this.pbHunger);
            this.Controls.Add(this.lblHunger);
            this.Controls.Add(this.lblDailyNeeds);
            this.Controls.Add(this.lblStatus);
            this.MinimumSize = new System.Drawing.Size(500, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тамагочи - Поросенок Визенау";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDailyNeeds;
        private System.Windows.Forms.Label lblHunger;
        private System.Windows.Forms.ProgressBar pbHunger;
        private System.Windows.Forms.Label lblHappiness;
        private System.Windows.Forms.ProgressBar pbHappiness;
        private System.Windows.Forms.Label lblCleanliness;
        private System.Windows.Forms.ProgressBar pbCleanliness;
        private System.Windows.Forms.Label lblEnergy;
        private System.Windows.Forms.ProgressBar pbEnergy;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.ProgressBar pbHealth;
        private System.Windows.Forms.Button btnFeed;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnSleep;
        private System.Windows.Forms.Button btnHeal;
        private System.Windows.Forms.Button btnSpeedDown;
        private System.Windows.Forms.Button btnNormalSpeed;
        private System.Windows.Forms.Button btnSpeedUp;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Timer timerGame;
        private System.Windows.Forms.Timer sleepTimer;
    }
}
