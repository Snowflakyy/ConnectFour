using CourseWork_OOP_Phase1_.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_OOP_Phase1_
{
    public partial class StartMenu : Form
    {
        public Color _selectedColor1 { get; private set; } = Color.Red;
        public Color _selectedColor2 { get; private set; } = Color.Yellow;
        private List<Shapes.Shapes> _shapes = new List<Shapes.Shapes>();
        //private Type _selectedShape = typeof(Circle);
        public ShapesType _selectedShapeType { get; private set; } = ShapesType.Circle;
        //private ConnectFourMockUp ConnectFourGui;
        //public FormSettings formsettings { get; set; }
        public FormSetting formsetting { get; set; }

        public int StartVerticalPadding { get; private set; }
        public int StartHorizontalPadding { get; private set; }
        public int padding { get; private set; } = 40;

        public int buttonWidth { get; set; } = 150;
        public int buttonHeight { get; set; } = 40;
        public PictureBox pictureBoxPl1 { get; private set; }
        public PictureBox pictureBoxPl2 { get; private set; }
        public Point pointPl1 { get; private set; }
        public Point pointPl2 { get; private set; }
        public Point pointContextMeny { get; private set; }



        public delegate void OnGameStartHandler(object sender);
        public event OnGameStartHandler OnGameStart;


        public StartMenu()
        {
            InitializeComponent();
            OnResize(this, null);
            this.Resize += OnResize;
            Shown += StartMenu_Shown;
            //this.BackgroundImage = Image.FromFile("../Images/Untitled.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Font = new Font("Segoe UI", 12, FontStyle.Bold);



        }

        private void StartMenu_Shown(object? sender, EventArgs e)
        {
            Reshaper(pointContextMeny);
        }
        private Color PictureBoxColorChange(PictureBox pictureBox, Point point, Color selectedColor)
        {
            formsetting = new FormSetting(selectedColor, color => selectedColor = color)
            {
                TopMost = true,
                StartPosition = FormStartPosition.Manual,
                Size = new System.Drawing.Size(400, 400)

            };

            formsetting.ColorPickedCallBack = color =>
            {
                pictureBox = new PictureBox();
                pictureBox.Location = point;
                pictureBox.Size = new Size(40, 40);
                pictureBox.BackColor = color;
                pictureBox.Paint += PictureBox_Paint;
                this.Controls.Add(pictureBox);
            };
            formsetting.Show();
            Refresh();
            return selectedColor;
        }

        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            using (var pencil = new Pen(Color.Black, 5))
            {
                e.Graphics.DrawRectangle(pencil, 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            }
        }

        //private void pictureBoxCreation(Point point1,Point point2)
        //{
        //    pictureBox1 = new PictureBox();
        //    pictureBox1.Location = point1;
        //    pictureBox1.Size = new Size(15, 15);

        //    pictureBox2 = new PictureBox();
        //    pictureBox2.Location = point2;
        //    pictureBox2.Size = new Size(15, 15);

        //    this.Controls.Add(pictureBox1);
        //    this.Controls.Add(pictureBox2);

        //}
        private void Reshaper(Point contextPos)
        {
            ToolStripLabel toolStripLabel = new ToolStripLabel("Choose a figure");
            toolStripLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            toolStripLabel.ForeColor = Color.Black;
            toolStripLabel.Enabled = false;

            actionMenu.Items.Add(toolStripLabel);
            actionMenu.Items.Add(new ToolStripSeparator());

            ToolStripButton circle = new ToolStripButton(" Circle");
            circle.Click += (_, _) =>
            {
                _selectedShapeType = ShapesType.Circle;
                Refresh();
            };
            actionMenu.Items.Add(circle);

            ToolStripButton rectangle = new ToolStripButton("Square");
            rectangle.Click += (_, _) =>
            {
                _selectedShapeType = ShapesType.Square;
                Refresh();
            };
            actionMenu.Items.Add(rectangle);

            ToolStripButton triangle = new ToolStripButton("Triangle");
            triangle.Click += (_, _) =>
            {
                _selectedShapeType = ShapesType.Triangle;
                Refresh();
            };
            actionMenu.Items.Add(triangle);
            actionMenu.Show(this, contextPos);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (formsetting != null && formsetting.Visible)
                formsetting.Hide();
            //if (e.Button.HasFlag(MouseButtons.Right))
            //{
            //    Reshaper(e);
            //}
        }


        protected void OnResize(object sender, EventArgs e)
        {

            StartHorizontalPadding = (int)(Width * 0.25) - buttonWidth / 2;
            StartVerticalPadding = (int)(Height * 0.35) - buttonHeight / 2;

            int centerHorizontal = ClientSize.Width / 2 - buttonWidth / 2;

            int centerVertical = ClientSize.Height / 3;

            Start_game.Location = new Point(centerHorizontal, centerVertical - buttonHeight / 3);
            Start_game.Size = new Size(buttonWidth, buttonHeight);

            Player1_color.Location = new Point(centerHorizontal, centerVertical + padding + buttonHeight / 3);
            Player1_color.Size = new Size(buttonWidth, buttonHeight);

            Player2_color.Location = new Point(centerHorizontal, centerVertical + 2 * (padding + buttonHeight / 3));
            Player2_color.Size = new Size(buttonWidth, buttonHeight);

            pointPl1 = new Point(centerHorizontal + buttonWidth * 4 / 3, centerVertical + padding + buttonHeight * 1 / 3);
            pointPl2 = new Point(centerHorizontal + buttonWidth * 4 / 3, centerVertical + padding * 9 / 4 + buttonHeight * 1 / 3);
            pointContextMeny = new Point(centerHorizontal, centerVertical + 4 * padding);



            if (pictureBoxPl1 != null)
            {
                pictureBoxPl1.Dispose();
            }
            if (pictureBoxPl2 != null)
            {
                pictureBoxPl2.Dispose();
            }


            //pictureBoxCreation(pointPl1, pointPl2);


            Invalidate();
        }

        private void Player1_color_Click(object sender, EventArgs e)
        {
            _selectedColor1 = PictureBoxColorChange(pictureBoxPl1, pointPl1, _selectedColor1);
        }

        private void Player2_color_Click(object sender, EventArgs e)
        {
            _selectedColor2 = PictureBoxColorChange(pictureBoxPl2, pointPl2, _selectedColor2);

        }

        private void Start_game_Click_1(object sender, EventArgs e)
        {
            OnGameStart?.Invoke(this);
            this.Hide();
        }
    }
}
