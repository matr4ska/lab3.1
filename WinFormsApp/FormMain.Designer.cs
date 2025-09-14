namespace WinFormsApp
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ButtonCreateShip = new Button();
            ButtonDeleteShip = new Button();
            ButtonChangeShipStats = new Button();
            ListViewMain = new ListView();
            ButtonStartGame = new Button();
            TextBoxName = new TextBox();
            ComboBoxColor = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            buttonHelp = new Button();
            SuspendLayout();
            // 
            // ButtonCreateShip
            // 
            ButtonCreateShip.Location = new Point(542, 171);
            ButtonCreateShip.Name = "ButtonCreateShip";
            ButtonCreateShip.Size = new Size(171, 45);
            ButtonCreateShip.TabIndex = 0;
            ButtonCreateShip.Text = "Построить корабль";
            ButtonCreateShip.UseVisualStyleBackColor = true;
            ButtonCreateShip.Click += ButtonCreateShip_Click;
            // 
            // ButtonDeleteShip
            // 
            ButtonDeleteShip.Location = new Point(542, 222);
            ButtonDeleteShip.Name = "ButtonDeleteShip";
            ButtonDeleteShip.Size = new Size(171, 45);
            ButtonDeleteShip.TabIndex = 1;
            ButtonDeleteShip.Text = "Потопить корабль";
            ButtonDeleteShip.UseVisualStyleBackColor = true;
            ButtonDeleteShip.Click += ButtonDeleteShip_Click;
            // 
            // ButtonChangeShipStats
            // 
            ButtonChangeShipStats.Location = new Point(542, 273);
            ButtonChangeShipStats.Name = "ButtonChangeShipStats";
            ButtonChangeShipStats.Size = new Size(171, 45);
            ButtonChangeShipStats.TabIndex = 2;
            ButtonChangeShipStats.Text = "Изменить корабль";
            ButtonChangeShipStats.UseVisualStyleBackColor = true;
            ButtonChangeShipStats.Click += ButtonChangeShipStats_Click;
            // 
            // ListViewMain
            // 
            ListViewMain.FullRowSelect = true;
            ListViewMain.Location = new Point(12, 12);
            ListViewMain.MultiSelect = false;
            ListViewMain.Name = "ListViewMain";
            ListViewMain.Size = new Size(510, 426);
            ListViewMain.TabIndex = 3;
            ListViewMain.UseCompatibleStateImageBehavior = false;
            // 
            // ButtonStartGame
            // 
            ButtonStartGame.Location = new Point(542, 376);
            ButtonStartGame.Name = "ButtonStartGame";
            ButtonStartGame.Size = new Size(249, 62);
            ButtonStartGame.TabIndex = 4;
            ButtonStartGame.Text = "Новая игра";
            ButtonStartGame.UseVisualStyleBackColor = true;
            ButtonStartGame.Click += ButtonStartGame_Click;
            // 
            // TextBoxName
            // 
            TextBoxName.Location = new Point(653, 67);
            TextBoxName.Name = "TextBoxName";
            TextBoxName.Size = new Size(243, 27);
            TextBoxName.TabIndex = 9;
            // 
            // ComboBoxColor
            // 
            ComboBoxColor.FormattingEnabled = true;
            ComboBoxColor.Location = new Point(653, 114);
            ComboBoxColor.Name = "ComboBoxColor";
            ComboBoxColor.Size = new Size(124, 28);
            ComboBoxColor.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(542, 117);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 7;
            label3.Text = "Цвет флага";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(542, 71);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 6;
            label2.Text = "Название";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(542, 12);
            label1.Name = "label1";
            label1.Size = new Size(246, 28);
            label1.TabIndex = 5;
            label1.Text = "Делай корабли, йохохо";
            // 
            // buttonHelp
            // 
            buttonHelp.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonHelp.Location = new Point(835, 376);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.RightToLeft = RightToLeft.No;
            buttonHelp.Size = new Size(61, 62);
            buttonHelp.TabIndex = 10;
            buttonHelp.Text = " ?";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 456);
            Controls.Add(buttonHelp);
            Controls.Add(TextBoxName);
            Controls.Add(ComboBoxColor);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ButtonStartGame);
            Controls.Add(ListViewMain);
            Controls.Add(ButtonChangeShipStats);
            Controls.Add(ButtonDeleteShip);
            Controls.Add(ButtonCreateShip);
            Name = "FormMain";
            Text = "Меню надзора за флотом";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonCreateShip;
        private Button ButtonDeleteShip;
        private Button ButtonChangeShipStats;
        private ListView ListViewMain;
        private Button ButtonStartGame;
        private TextBox TextBoxName;
        private ComboBox ComboBoxColor;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button buttonHelp;
    }
}
