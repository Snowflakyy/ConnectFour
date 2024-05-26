namespace CourseWork_OOP_Phase1_
{
    partial class StartMenu
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
            Player2_color = new Button();
            Player1_color = new Button();
            actionMenu = new ContextMenuStrip(components);
            SuspendLayout();
            // 
            // Start_game
            // 
            Start_game.Location = new Point(374, 182);
            Start_game.Name = "Start_game";
            Start_game.Size = new Size(94, 29);
            Start_game.TabIndex = 0;
            Start_game.Text = "StartGame";
            Start_game.UseVisualStyleBackColor = true;
            // 
            // Player2_color
            // 
            Player2_color.Location = new Point(200, 356);
            Player2_color.Name = "Player2_color";
            Player2_color.Size = new Size(94, 29);
            Player2_color.TabIndex = 1;
            Player2_color.Text = "Player2";
            Player2_color.UseVisualStyleBackColor = true;
            Player2_color.Click += Player2_color_Click;
            // 
            // Player1_color
            // 
            Player1_color.Location = new Point(52, 356);
            Player1_color.Name = "Player1_color";
            Player1_color.Size = new Size(94, 29);
            Player1_color.TabIndex = 2;
            Player1_color.Text = "Player 1 ";
            Player1_color.UseVisualStyleBackColor = true;
            Player1_color.Click += Player1_color_Click;
            // 
            // actionMenu
            // 
            actionMenu.ImageScalingSize = new Size(20, 20);
            actionMenu.Name = "actionMenu";
            actionMenu.Size = new Size(61, 4);
            // 
            // StartMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Player1_color);
            Controls.Add(Player2_color);
            Controls.Add(Start_game);
            Name = "StartMenu";
            Text = "StartMenu";
            ResumeLayout(false);
        }

        #endregion

        private Button Start_game;
        private Button Player2_color;
        private Button Player1_color;
        private ContextMenuStrip actionMenu;
    }
}