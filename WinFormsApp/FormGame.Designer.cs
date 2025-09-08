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
            label2 = new Label();
            SuspendLayout();
            // 
            // ListViewGame
            // 
            ListViewGame.Location = new Point(102, 94);
            ListViewGame.Name = "ListViewGame";
            ListViewGame.Size = new Size(571, 254);
            ListViewGame.TabIndex = 0;
            ListViewGame.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(365, 18);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // ButtonAttack
            // 
            ButtonAttack.Location = new Point(198, 365);
            ButtonAttack.Name = "ButtonAttack";
            ButtonAttack.Size = new Size(94, 29);
            ButtonAttack.TabIndex = 2;
            ButtonAttack.Text = "Attack!";
            ButtonAttack.UseVisualStyleBackColor = true;
            ButtonAttack.Click += ButtonAttack_Click;
            // 
            // ButtonHeal
            // 
            ButtonHeal.Location = new Point(506, 365);
            ButtonHeal.Name = "ButtonHeal";
            ButtonHeal.Size = new Size(94, 29);
            ButtonHeal.TabIndex = 3;
            ButtonHeal.Text = "Heal!";
            ButtonHeal.UseVisualStyleBackColor = true;
            ButtonHeal.Click += ButtonHeal_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(365, 55);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 4;
            label2.Text = "label2";
            // 
            // FormGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(783, 450);
            Controls.Add(label2);
            Controls.Add(ButtonHeal);
            Controls.Add(ButtonAttack);
            Controls.Add(label1);
            Controls.Add(ListViewGame);
            Name = "FormGame";
            Text = "FormGame";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView ListViewGame;
        private Label label1;
        private Button ButtonAttack;
        private Button ButtonHeal;
        private Label label2;
    }
}