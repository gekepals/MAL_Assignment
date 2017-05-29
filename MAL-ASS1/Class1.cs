using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MAL_ASS1
{
    partial class Skaters
    {
        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.Start = new Button();
            this.Pause = new Button();
			this.Greed = new Button();
            this.button3 = new Button();
            this.SkatersAmount = new TextBox();
            this.SelectPlayers = new Label();
            this.Go = new Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = SystemColors.Highlight;
            this.panel1.Location = new Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(300, 300);
            this.panel1.TabIndex = 0;
            // 
            // Start
            // 
            this.Start.Location = new Point(324, 12);
            this.Start.Name = "Start";
            this.Start.Size = new Size(75, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // Pause
            // 
            this.Pause.Location = new Point(324, 41);
            this.Pause.Name = "Pause";
            this.Pause.Size = new Size(75, 23);
            this.Pause.TabIndex = 1;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            // 
            // Stop
            // 
            this.button3.Location = new Point(324, 70);
            this.button3.Name = "Stop";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // SkatersAmount
            // 
            this.SkatersAmount.Location = new Point(324, 179);
            this.SkatersAmount.Name = "SkatersAmount";
            this.SkatersAmount.Size = new Size(74, 20);
            this.SkatersAmount.TabIndex = 3;
            // 
            // SelectPlayers
            // 
            this.SelectPlayers.AutoSize = true;
            this.SelectPlayers.Location = new Point(304, 163);
            this.SelectPlayers.Name = "SelectPlayers";
            this.SelectPlayers.Size = new Size(95, 13);
            this.SelectPlayers.TabIndex = 4;
            this.SelectPlayers.Text = "Amount of skaters:";
            // 
            // Go
            // 
            this.Go.Location = new Point(324, 205);
            this.Go.Name = "Go";
            this.Go.Size = new Size(75, 23);
            this.Go.TabIndex = 5;
            this.Go.Text = "Go!";
            this.Go.UseVisualStyleBackColor = true;
            // 
            // Skaters
            // 
            this.ClientSize = new Size(411, 359);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.SelectPlayers);
            this.Controls.Add(this.SkatersAmount);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.Greed);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.panel1);
            this.Name = "Skaters";
            this.ResumeLayout(false);
            this.PerformLayout();
			//
			// Greedy
			//
			this.Greed.Location = new Point(324, 100);
			this.Greed.Name = "greed";
            this.Greed.Size = new Size(75, 23);
            this.Greed.TabIndex = 6;
            this.Greed.Text = "Greedy";
            this.Greed.UseVisualStyleBackColor = true;

        }
    }
}
