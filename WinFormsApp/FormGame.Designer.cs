namespace WinFormsApp
{
    partial class FormGame
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
            ListViewGame = new ListView();
            label1 = new Label();
            ButtonAttack = new Button();
            ButtonHeal = new Button();
            labelPlayer = new Label();
            SuspendLayout();
            // 
            // ListViewGame
            // 
            ListViewGame.FullRowSelect = true;
            ListViewGame.Location = new Point(102, 94);
            ListViewGame.MultiSelect = false;
            ListViewGame.Name = "ListViewGame";
            ListViewGame.Size = new Size(571, 254);
            ListViewGame.TabIndex = 0;
            ListViewGame.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(260, 16);
            label1.Name = "label1";
            label1.Size = new Size(255, 31);
            label1.TabIndex = 1;
            label1.Text = "Битва пошла поехала";
            // 
            // ButtonAttack
            // 
            ButtonAttack.Location = new Point(142, 365);
            ButtonAttack.Name = "ButtonAttack";
            ButtonAttack.Size = new Size(182, 29);
            ButtonAttack.TabIndex = 2;
            ButtonAttack.Text = "Атаковать!";
            ButtonAttack.UseVisualStyleBackColor = true;
            ButtonAttack.Click += ButtonAttack_Click;
            // 
            // ButtonHeal
            // 
            ButtonHeal.Location = new Point(452, 365);
            ButtonHeal.Name = "ButtonHeal";
            ButtonHeal.Size = new Size(182, 29);
            ButtonHeal.TabIndex = 3;
            ButtonHeal.Text = "Отремонтировать!";
            ButtonHeal.UseVisualStyleBackColor = true;
            ButtonHeal.Click += ButtonHeal_Click;
            // 
            // labelPlayer
            // 
            labelPlayer.Location = new Point(260, 52);
            labelPlayer.Name = "labelPlayer";
            labelPlayer.Size = new Size(255, 24);
            labelPlayer.TabIndex = 4;
            labelPlayer.Text = "а нет, никто ж не пришел";
            labelPlayer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(783, 430);
            Controls.Add(labelPlayer);
            Controls.Add(ButtonHeal);
            Controls.Add(ButtonAttack);
            Controls.Add(label1);
            Controls.Add(ListViewGame);
            Name = "FormGame";
            Text = "Меню разноса флота";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView ListViewGame;
        private Label label1;
        private Button ButtonAttack;
        private Button ButtonHeal;
        private Label labelPlayer;
    }
}