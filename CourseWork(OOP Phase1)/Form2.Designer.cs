namespace CourseWork_OOP_Phase1_
{
    partial class Form2
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
            Start_game = new Button();
            Open_Dialog = new Button();
            BtnPickColorForPlayer2 = new Button();
            actionMenu = new ContextMenuStrip(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            SuspendLayout();
            // 
            // Start_game
            // 
            Start_game.Location = new Point(338, 292);
            Start_game.Margin = new Padding(3, 4, 3, 4);
            Start_game.Name = "Start_game";
            Start_game.Size = new Size(86, 31);
            Start_game.TabIndex = 2;
            Start_game.Text = "StartGame";
            Start_game.UseVisualStyleBackColor = true;
            Start_game.Click += Start_game_Click;
            // 
            // Open_Dialog
            // 
            Open_Dialog.Location = new Point(33, 532);
            Open_Dialog.Margin = new Padding(3, 4, 3, 4);
            Open_Dialog.Name = "Open_Dialog";
            Open_Dialog.Size = new Size(174, 31);
            Open_Dialog.TabIndex = 3;
            Open_Dialog.Text = "Color PLayer 1";
            Open_Dialog.UseVisualStyleBackColor = true;
            Open_Dialog.Click += Open_Dialog_Click;
            // 
            // BtnPickColorForPlayer2
            // 
            BtnPickColorForPlayer2.Location = new Point(241, 532);
            BtnPickColorForPlayer2.Margin = new Padding(3, 4, 3, 4);
            BtnPickColorForPlayer2.Name = "BtnPickColorForPlayer2";
            BtnPickColorForPlayer2.Size = new Size(174, 31);
            BtnPickColorForPlayer2.TabIndex = 4;
            BtnPickColorForPlayer2.Text = "Color PLayer 2";
            BtnPickColorForPlayer2.UseVisualStyleBackColor = true;
            BtnPickColorForPlayer2.Click += BtnPickColorForPlayer2_Click;
            // 
            // actionMenu
            // 
            actionMenu.ImageScalingSize = new Size(20, 20);
            actionMenu.Name = "contextMenuStrip1";
            actionMenu.Size = new Size(61, 4);
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(211, 32);
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Untitled;
            ClientSize = new Size(1458, 679);
            Controls.Add(BtnPickColorForPlayer2);
            Controls.Add(Open_Dialog);
            Controls.Add(Start_game);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion
        private Button Start_game;
        private Button Open_Dialog;
        private Button BtnPickColorForPlayer2;
        private ContextMenuStrip actionMenu;
        private ContextMenuStrip contextMenuStrip1;
    }
}