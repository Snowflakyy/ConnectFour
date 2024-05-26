using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

//var time = System.Windows.Forms;
namespace CourseWork_OOP_Phase1_
{
    
    public partial class PauseForm : Form
    {

        public Button unPauseButton { get;private set; }
        public PauseForm(ConnectFourMockUp connectfour)
        {
            InitializeComponent();




            this.StartPosition = connectfour.StartPosition;
            this.ShowInTaskbar = connectfour.ShowInTaskbar;
            WindowState = connectfour.WindowState;
            this.Size = connectfour.Size;
            this.FormBorderStyle = connectfour.FormBorderStyle;
            this.Location = connectfour.Location;
            this.TopMost = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Font = new Font("Segoe UI", 12, FontStyle.Bold);

            unPauseButton = new Button();
            unPauseButton.Location = new Point(0, 0);
            unPauseButton.Size = new Size(150, 100);
            unPauseButton.FlatStyle = FlatStyle.Flat;
            unPauseButton.BackColor = Color.Transparent;
            unPauseButton.FlatAppearance.BorderSize = 0;
            unPauseButton.FlatAppearance.MouseOverBackColor=Color.Transparent;
            unPauseButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            unPauseButton.Visible = true;
            this.Controls.Add(unPauseButton);
            unPauseButton.Click += UnPauseButton_Click;
            unPauseButton.Paint += UnPauseButton_Paint;
            //SubscribeToEvent();
        }

        private void UnPauseButton_Paint(object? sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            Graphics g = e.Graphics;
     
            SolidBrush brush = new SolidBrush(Color.Black);

            int triangleWidth = btn.Width / 2;  
            int triangleHeight = btn.Height / 2;  
            int left = (btn.Width - triangleWidth) / 2;  
            int top = (btn.Height - triangleHeight) / 2;  

            
            Point[] points = new Point[]
            {
                new Point(left, top + 10),  
                new Point(left, top + triangleWidth - 10),  
                new Point(left + triangleHeight, top + triangleWidth / 2)
            };
            g.FillPolygon(brush,points);
        }

        private void UnPauseButton_Click(object? sender, EventArgs e)
        {
            Onresume?.Invoke(this);
        }

        //public void SubscribeToEvent()
        //{
        //    MainMenu.OnPause += PauseForm_OnPauseOpened;
        //}
        //private void PauseForm_OnPauseOpened(object sender)
        //{
        //    this.BackgroundImage?.Dispose();
        //    Hide();
        //    Invalidate();
        //}

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                Onresume?.Invoke(this);
                //Dispose();

            }
        }
        public delegate void OnResume(object sender);
        public event OnResume Onresume;


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height;
            int barWidth = 30;
            int barSpacing = 30;
            int totalWidth = 2 * barWidth + barSpacing;
            int startX = (width - totalWidth) / 2;
            int startY = (height - barWidth) / 2;
            Span<byte> rgb = stackalloc byte[] { (byte)215, (byte)232, (byte)186 };
            Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);

            g.FillRectangle(new SolidBrush(color), startX, startY, barWidth, barWidth * 2 + barSpacing);
            g.FillRectangle(new SolidBrush(color), startX + barWidth + barSpacing, startY, barWidth, barWidth * 2 + barSpacing);


        }

        //protected override void OnFormClosing(FormClosingEventArgs e)
        //{
           
        //    if (!ConfirmExitMenu())
        //    {
        //        e.Cancel = true;
        //    }

        //}
        //private bool ConfirmExitMenu()
        //{
        //    return MessageBox.Show("Are you sure you want to exit out of the game?",
        //        "Confirm",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Warning) == DialogResult.Yes;
        //}

    }
}
