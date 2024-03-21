namespace RemoveWaterMar
{
    partial class Player
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            play = new Vlc.DotNet.Forms.VlcControl();
            btn1x = new Button();
            btn125x = new Button();
            btn2x = new Button();
            btn15x = new Button();
            pBarPlayer = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)play).BeginInit();
            SuspendLayout();
            // 
            // play
            // 
            play.BackColor = Color.Black;
            play.Location = new Point(0, -1);
            play.Margin = new Padding(2);
            play.Name = "play";
            play.Size = new Size(716, 424);
            play.Spu = -1;
            play.TabIndex = 0;
            play.Text = "vlcControl1";
            play.VlcLibDirectory = null;
            play.VlcMediaplayerOptions = null;
            play.VlcLibDirectoryNeeded += vlcControl1_VlcLibDirectoryNeeded;
            play.Playing += play_Playing;
            play.PositionChanged += play_PositionChanged;
            play.DoubleClick += play_DoubleClick;
            play.KeyUp += Player_KeyUp;
            // 
            // btn1x
            // 
            btn1x.ForeColor = Color.Red;
            btn1x.Location = new Point(0, 10);
            btn1x.Name = "btn1x";
            btn1x.Size = new Size(75, 23);
            btn1x.TabIndex = 1;
            btn1x.Text = "1x";
            btn1x.UseVisualStyleBackColor = true;
            btn1x.Click += btn1x_Click;
            // 
            // btn125x
            // 
            btn125x.Location = new Point(150, 10);
            btn125x.Name = "btn125x";
            btn125x.Size = new Size(75, 23);
            btn125x.TabIndex = 2;
            btn125x.Text = "1.25x";
            btn125x.UseVisualStyleBackColor = true;
            btn125x.Click += btn125x_Click;
            // 
            // btn2x
            // 
            btn2x.Location = new Point(447, 10);
            btn2x.Name = "btn2x";
            btn2x.Size = new Size(75, 23);
            btn2x.TabIndex = 3;
            btn2x.Text = "2x";
            btn2x.UseVisualStyleBackColor = true;
            btn2x.Click += btn2x_Click;
            // 
            // btn15x
            // 
            btn15x.Location = new Point(299, 10);
            btn15x.Name = "btn15x";
            btn15x.Size = new Size(75, 23);
            btn15x.TabIndex = 4;
            btn15x.Text = "1.5x";
            btn15x.UseVisualStyleBackColor = true;
            btn15x.Click += btn15x_Click;
            // 
            // pBarPlayer
            // 
            pBarPlayer.Location = new Point(-5, -2);
            pBarPlayer.Maximum = 10000;
            pBarPlayer.Name = "pBarPlayer";
            pBarPlayer.Size = new Size(721, 7);
            pBarPlayer.TabIndex = 5;
            pBarPlayer.MouseHover += pBarPlayer_MouseHover;
            // 
            // timer1
            // 
            timer1.Tick += time1_Tick;
            // 
            // Player
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 430);
            Controls.Add(pBarPlayer);
            Controls.Add(btn15x);
            Controls.Add(btn2x);
            Controls.Add(btn125x);
            Controls.Add(btn1x);
            Controls.Add(play);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Player";
            ShowInTaskbar = false;
            Text = "Player";
            HelpButtonClicked += Player_HelpButtonClicked;
            FormClosed += Player_FormClosed;
            Load += Player_Load;
            KeyUp += Player_KeyUp;
            ((System.ComponentModel.ISupportInitialize)play).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Vlc.DotNet.Forms.VlcControl play;
        private Button btn1x;
        private Button btn125x;
        private Button btn2x;
        private Button btn15x;
        private ProgressBar pBarPlayer;
        private System.Windows.Forms.Timer timer1;
    }
}