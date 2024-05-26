using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseWork_OOP_Phase1_.Enums;
using CourseWork_OOP_Phase1_.Shapes;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace CourseWork_OOP_Phase1_
{
    [Serializable]
    public partial class ConnectFourMockUp : Form,IShapesOperations, ISerializable
    {
        public int BoardHorizontalPadding { get; set; }
        public int BoardVerticalPadding { get; set; }
        public int GridSquareHeight { get; set; }
        public int GridSquareWidth { get; set; }
        //public int LastPlacedRow { get;private set; }
        public int LastPlacedCol { get; private set; }
        public int ChipRadius { get; private set; } = 20;
        public ShapesType _shapes { get; private set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public const int LineThickness = 8;
        public GameLogic GameBoard { get; private set; }
        public Color player1Col { get; private set; }
        public Color player2Col { get; private set; }
        public int CurrentColumn { get; private set; }
        public int CurrentRow { get; private set; }
        public int padding { get; private set; } =20;
        public bool isExited { get; private set; } = false;

        public PauseForm _pauseform { get; private set; }
        public bool isPaused { get; set; } = false;

        public List<Button> _buttonsStat;
        public List<Shapes.Shapes> _shapesList;
        public List<int> FirstPlacedChipColumn;
        public List<Chip> ChipsPlaced;
        public PictureBox pictBackGr;
        public PopUp _popup;

        public _ResultRow _resultRow; 
        public Pen pencilBounds { get; private set; }= new Pen(Color.Black, LineThickness);
        public OrderedDictionary _operationUndo { get; private set; }
        public OrderedDictionary _operationLastPlacedChips { get; private set; }
        public Button undoButton { get; private set; }
        public Button redoButton { get; private set; }

        public Button pauseButton { get;private set; }
        public Button resButton { get;private set; }

        public Button ResetButton { get; private set; }
        public Button DeserialButton { get; private set; }


        public delegate void OnAppClosing(object sender);

        public event OnAppClosing OnAppClosingHandler;
        
        
       

        public ConnectFourMockUp() : this(Chip.Player1, ShapesType.Circle, Color.Red, Color.Yellow)
        {

        }

        public ConnectFourMockUp(Chip playeroneChip, ShapesType shapeChosen, Color player1Color, Color player2Color)
        {
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Font = new Font("Segoe UI", 12, FontStyle.Bold);
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            GameBoard = new GameLogic(7, 6, playeroneChip);
            this.ClientSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            

            _shapes = shapeChosen;
            player1Col = player1Color;
            player2Col = player2Color;
           
            _buttonsStat = new List<Button>();
            _pauseform = new PauseForm(this);
            _operationUndo = new OrderedDictionary();
            FirstPlacedChipColumn = new List<int>();
            _operationLastPlacedChips = new OrderedDictionary();

            pictBackGr = new PictureBox();
            pictBackGr.Size = this.Size;
            pictBackGr.BackColor = Color.FromArgb(169, 27, 53, 95);

             undoButton = new Button();
            undoButton.Location = new Point(Width - BoardHorizontalPadding/6 - padding, padding);
            undoButton.Text = "Undo";
            undoButton.Size = new Size(70, 50);
            undoButton.Visible = _operationLastPlacedChips.Count > 0 ? true: false;
            this.Controls.Add(undoButton);
            undoButton.Click += UndoButton_Click;

             redoButton = new Button();
            redoButton.Location = new Point(Width - BoardHorizontalPadding, padding);
            redoButton.Text = "Redo";
            redoButton.Size = new Size(75, 50);
            redoButton.Visible = _operationLastPlacedChips.Count > 0 ? true : false;
            this.Controls.Add(redoButton);
            redoButton.Click += RedoButton_Click;

            pauseButton = new Button();
            pauseButton.Location = new Point(0, 0);
            pauseButton.Size = new Size(100, 100);
            pauseButton.FlatStyle = FlatStyle.Flat;
            pauseButton.BackColor = Color.Transparent;
            pauseButton.FlatAppearance.BorderSize = 0;
            pauseButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            pauseButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            pauseButton.Visible = true;
            this.Controls.Add(pauseButton);
            pauseButton.Click += PauseButton_Click;
            pauseButton.Paint += PauseButton_Paint;

            ResetButton = new Button();
            ResetButton.Location = new Point(Width-padding*5, Height-padding*5);
            ResetButton.Text = "Reset";
            ResetButton.Size = new Size(75, 50);
            ResetButton.Visible = true;
            this.Controls.Add(ResetButton);
            ResetButton.Click += ResetButton_Click;

            resButton = new Button();
            resButton.Location = new Point( padding * 5, Height - padding * 5);
            resButton.Size = new Size(75, 50);
            resButton.Visible = true;
            this.Controls.Add(resButton);
            resButton.MouseDown += BtnRes_MouseDown;

            DeserialButton = new Button();
            DeserialButton.Location = new Point(Width - padding * 10, Height - padding * 5);
            DeserialButton.Size = new Size(75, 50);
            DeserialButton.Visible = true;
            this.Controls.Add(DeserialButton);
            DeserialButton.Click += DeserialButton_Click;


            //int x = GridSquareWidth * -1 + BoardHorizontalPadding + LineThickness + padding;
            //int y = GridSquareHeight * i + BoardVerticalPadding + LineThickness + padding;
            //var button = new Button();
            //button.Location = new Point(x, y);
            //button.Size = new System.Drawing.Size(50, 50);
            //button.Text = $"Row{i + 1}";
            //button.Visible = false;
            //this.Controls.Add(button);
            //button.MouseDown += new MouseEventHandler(Btn_MouseDown);
            //_buttonsStat.Add(button);


            isPaused = false;
          

            OnResize(null);
            SubscribeToEvents();
            StatisticButtons();

            

        }

        public delegate void OnDeserializeOpenedHandler(object sender);

        public event OnDeserializeOpenedHandler OnDeserializeOpened;

        private void ResetButton_Click(object? sender, EventArgs e)
        {
            if (ConfirmReset())
            {
                GameBoard.StartNewGame(true);
                Invalidate();
            }
        }
        private void DeserialButton_Click(object? sender, EventArgs e)
        {
           OnDeserializeOpened?.Invoke(this);
        }

        private void PauseButton_Paint(object? sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            Graphics g = e.Graphics;
            Brush brush = Brushes.Black;

            // Drawing two rectangles for "pause" symbol
            int rectWidth = btn.Width / 6;
            int space = btn.Width / 10;
            int rectHeight = btn.Height - 50;  // 10 pixels padding top and bottom

            Rectangle rect1 = new Rectangle((btn.Width / 2) - rectWidth - space / 2, 30, rectWidth, rectHeight);
            Rectangle rect2 = new Rectangle((btn.Width / 2) + space / 2, 30, rectWidth, rectHeight);

            g.FillRectangle(brush, rect1);
            g.FillRectangle(brush, rect2);
        }

        private void PauseButton_Click(object? sender, EventArgs e)
        {
            PauseGame();
        }


        private void UndoButton_Click(object sender, EventArgs e)
        {
            _ChipsToBeUndone();
            redoButton.Visible = true;
            if (_operationLastPlacedChips.Count == 0)
            {
                undoButton.Visible = false;
            }
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            _ChipsToBeRedone();
            undoButton.Visible = true;
            if (_operationUndo.Count == 0)
            {
                redoButton.Visible = false;
            }
        }

        private void GameReset(object sender)
        {
            _operationUndo.Clear();
            _operationLastPlacedChips.Clear();
            
            
        }


        private bool ConfirmReset()
        {
            return MessageBox.Show("Are you sure you want to reset the game?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        private bool ConfirmExitMenu()
        {
            return MessageBox.Show("Are you sure you want to exit out of the game?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private void StatisticButtons()
        {
            
            for (int i = 0; i < GameBoard.Rows; i++)
            {
                int x = GridSquareWidth * -1 + BoardHorizontalPadding + LineThickness + padding;
                int y = GridSquareHeight * i + BoardVerticalPadding + LineThickness +padding;
                var button = new Button();
                button.Location= new Point(x, y);
                button.Size = new System.Drawing.Size(50, 50);
                button.Text = $"Row{i + 1}";
                button.Visible = false;
                this.Controls.Add(button);
                button.MouseDown += new MouseEventHandler(Btn_MouseDown);
                _buttonsStat.Add(button);
        }
            Update();
    }

        private void BtnRes_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (!GameBoard.IsGameOver)
                {
                    MessageBox.Show("The game is not over yet.");
                    return;
                }

                GetChipsPlacedFrequency();
                var sortedChips = SortFirstPlacedChipColumn();
                _resultRow = new _ResultRow(sortedChips);
                _resultRow.StartPosition = FormStartPosition.Manual;
                _resultRow.Location = new Point(this.Location.X + e.X, this.Location.Y + e.Y);
                _resultRow.Show();
                resButton.Hide();
            }
        }

        private void Btn_MouseDown(object sender,MouseEventArgs e)
        {
            
            IEnumerable<string> _results = GetResultsForRows(CurrentRow);
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.Visible =false)
                {
                    _popup.Hide();
                    return;
                }
                _popup = new PopUp(_results);
                
                _popup.Location =new Point(e.X, e.Y);
                _popup.Show();
            }

        }

        private IEnumerable<string> GetResultsForRows(int row)
        {
            List<string> _results = new List<string>();
            
            _results.Add($"Player1 Chips Placed On Row {row+1}: {GameBoard.GetChipsEachRow(row, Chip.Player1)}");
            _results.Add($"Player2 Chips Placed On Row {row+1}: {GameBoard.GetChipsEachRow(row, Chip.Player2)}");
            _results.Add($"Vacant Places On Row {row+1}: {GameBoard.GetChipsEachRow(row, Chip.None)}");
            
            return _results;
        }

  

    
        public void _ChipsToBeUndone()
        {
            if (_operationLastPlacedChips.Count == 0)
            {
                MessageBox.Show("No chips to undo");
                return;
            }

            var lastEntry = _operationLastPlacedChips.Cast<DictionaryEntry>().Last();
            var lastKey = (Tuple<int, int>)lastEntry.Key;
            var lastValue = (Chip)lastEntry.Value;

            _operationUndo.Add(new Tuple<int, int>(lastKey.Item1, lastKey.Item2), lastValue);

            
            GameBoard.ChipOperations(Chip.None, lastKey.Item1, lastKey.Item2);

           
            _operationLastPlacedChips.Remove(lastKey);
            Invalidate();
        }

        public void _ChipsToBeRedone()
        {
            if (_operationUndo.Count == 0)
            {
                MessageBox.Show("No chips to redo");
                return;
            }
            var lastEntry = _operationUndo.Cast<DictionaryEntry>().Last();
            var lastKey = (Tuple<int, int>)lastEntry.Key;
            var lastValue = (Chip)lastEntry.Value;

            _operationLastPlacedChips.Add(new Tuple<int, int>(lastKey.Item1, lastKey.Item2), lastValue);

          
            GameBoard.ChipOperations(lastValue, lastKey.Item1, lastKey.Item2);

           
            _operationUndo.Remove(lastKey);
            Invalidate();
        }


        public delegate void OnVisibilityChangedHandler(object sender);

        public event OnVisibilityChangedHandler OnVisibilityChanged;

        public delegate void OnFormClosedHandler(object sender);

        public event OnFormClosedHandler OnFormClosed;
       

        private void SubscribeToEvents()
        {
            _pauseform.Onresume += PauseForm_OnPauseClosed;
            GameBoard.OnNewGame += GameBoard_OnNewGame;
            GameBoard.OnGameReset += GameReset;
        }
        private void GameBoard_OnNewGame(object sender)
        {
            Invalidate();
            _operationUndo.Clear();
            _operationLastPlacedChips.Clear();
        }
        private void PauseForm_OnPauseClosed(object sender)
        {
            try
            {
                _pauseform.Hide();
                this.Opacity = 1.0;
                isPaused = false;

            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured:" + e.Message);
            }

        }
        private void PauseGame()
        {
            Bitmap screenshot = CaptureScreen();
            _pauseform.BackgroundImage = screenshot;
            _pauseform.Show();

            this.Opacity = 0;
          
            //isPaused = true;
           
        }

        private Bitmap CaptureScreen()
        {
            this.Controls.Add(pictBackGr);
            pictBackGr.BringToFront();
            this.Refresh();


            
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
              
                var pt = this.PointToScreen(Point.Empty);
                gfx.CopyFromScreen(pt.X, pt.Y, 0, 0, this.Size);

            }

           
            this.Controls.Remove(pictBackGr);
            pictBackGr.SendToBack();

            return bmp;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
             
                    PauseGame();
                

            }

            if (e.KeyCode == Keys.S)
            {
                OnDeserializeOpened?.Invoke(this);
                //Deserialize();
                Invalidate();
             Refresh();
            }


        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_popup != null && _popup.Visible)
            {
                _popup.Close();
                Invalidate();
            }
            if (GameBoard.IsGameOver)
            {
               
                GameBoard.StartNewGame(false);
                resButton.Show();
                if(_resultRow!=null)
                _resultRow.Close();
            }
            else if (!GameBoard.IsColumnAvailable(CurrentColumn))
            {
                MessageBox.Show("That column is full!Try another one.");
            }


            GameBoard.PlaceChipOnCol(CurrentColumn, GameBoard.CurrentChipTurn);
            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            HoverColumnOnMove(e);
            HoverRowOnMove(e);

        }

        private void HoverColumnOnMove(MouseEventArgs e)
        {
            int hoverColumn = (e.X - BoardHorizontalPadding) / GridSquareWidth;
            
            if (hoverColumn < 0 || hoverColumn >= GameBoard.Columns)
            {
                hoverColumn = hoverColumn < 0 ? 0 : GameBoard.Columns - 1;
            }

            if (CurrentColumn != hoverColumn)
            {
                CurrentColumn = hoverColumn;
                Invalidate();
            }
        }

        private void HoverRowOnMove(MouseEventArgs e)
        {
            int hoverRow = (e.Y - BoardVerticalPadding) / GridSquareHeight;
            if(hoverRow<0 || hoverRow>=GameBoard.Rows)
            {
                hoverRow=hoverRow< 0 ? 0 : GameBoard.Rows - 1;

            }

            if (CurrentRow != hoverRow)
            {
                CurrentRow=hoverRow;
      
                foreach (var button in _buttonsStat)
                    button.Visible = false;

                _buttonsStat[CurrentRow].Visible= true;
                    
                
                
                Invalidate();
            }

        }
        

       
        private void DrawSelectedChip(Graphics g, Chip chip, int column, int row, Point center)
        {
            int chipPaddindX = (int)(GridSquareWidth * 0.350);
            int chipPaddingY = (int)(GridSquareHeight * 0.350);
            Color currentcolor;
            Shapes.Shapes shape;
            switch (chip)
            {
                case Chip.None:
                    shape = CreateShape(g, Color.FromArgb(215, 215, 215), center);
                    currentcolor = Color.FromArgb(215, 215, 215);
                    break;
                case Chip.Player1:
                    shape = CreateShape(g, player1Col, center);
                    currentcolor = player1Col;
                    break;

                case Chip.Player2:
                    shape = CreateShape(g, player2Col, center);
                    currentcolor = player2Col;
                    break;
                default: throw new Exception("No such shape");

            }

            using (var br = new SolidBrush(currentcolor))
            {
                shape.Fill(g, br);
                
                using (var pencil = new Pen(Color.Black, 10))
                {
                    shape.Fill(g, LineThickness);
                }
            }
            

        }

        private Shapes.Shapes CreateShape(Graphics g, Color color, Point center)
        {
            Shapes.Shapes shape;
            switch (_shapes)
            {
                case ShapesType.Circle:
                    shape = (new Circle(center, ChipRadius, color));
                    break;
                case ShapesType.Square:
                    shape = (new Square(center, (float)(ChipRadius * 2 * Math.Sqrt(2)), color));
                    break;
                case ShapesType.Triangle:
                    shape = (new Triangle(center, ChipRadius * 3, color));
                    break;
                default:
                    throw new Exception("Invalid shape");
                    break;
            }

            return shape;
        }


        private void GetChipsPlacedFrequency()
        {
            var firstEntry = _operationLastPlacedChips.Cast<DictionaryEntry>().First();
            var firstKey = (Tuple<int,int>)firstEntry.Key;
            FirstPlacedChipColumn.Add(firstKey.Item2);
            //    var lastEntry = _operationLastPlacedChips.Cast<DictionaryEntry>().Last();
            //var lastKey = (Tuple<int, int>)lastEntry.Key;
        }

        private IEnumerable<string> SortFirstPlacedChipColumn()
        {
            //var sortedGroups = FirstPlacedChipColumn
            //    .GroupBy(b => b > 0)
            //    .OrderByDescending(g => g.Key)
            //    .SelectMany(g => g)
            //    .Select(row => $"Number placed on row {row}");
            var sortedGroups = FirstPlacedChipColumn
                .GroupBy(col => col)
                .OrderByDescending(group => group.Count())
                .Select(group => $"Column {group.Key+1}: {group.Count()} times");
            return sortedGroups;
        }

        private void Deserialize()
        {
            MessageBox.Show("Deseriazlization");
            var openFile = new OpenFileDialog();
            openFile.Filter = "JSON files (.json)|.json|Text files (.txt)|.txt";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string jsonString = System.IO.File.ReadAllText(openFile.FileName);
                var shapes = JsonConvert.DeserializeObject<OrderedDictionary>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                //_gameManager.Shapes.Clear();
                //_gameManager.Shapes.AddRange(shapes);
                //connectFourGUI._operationLastPlacedChips.Clear();
                //connectFourGUI._operationUndo.Clear();
                GameBoard.StartNewGame(false);

                foreach (DictionaryEntry entry in shapes)
                {
                    // Assuming the key is Tuple<int, int> and the value is int
                    var key = (Tuple<int, int>)entry.Key;
                    var value = (Chip)entry.Value;
                    _operationLastPlacedChips.Add(key, value);
                }

                MessageBox.Show("Restored shapes from " + openFile.FileName);
            }
        }

        protected override void OnPaint(PaintEventArgs e)

        {
            if (GridSquareHeight == 0 || GridSquareWidth == 0)
            {
                return;
            }
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle gameBoardBounds = new Rectangle(BoardHorizontalPadding, BoardVerticalPadding,
                (GridSquareWidth * GameBoard.Columns), (GridSquareHeight * GameBoard.Rows));


          
            int chipPaddindX = (int)(GridSquareWidth * 0.350);
            int chipPaddingY = (int)(GridSquareHeight * 0.350);

      
            for (int i = 1; i < GameBoard.Rows; i++)
            {
                g.DrawLine(pencilBounds,
                    BoardHorizontalPadding,
                    BoardVerticalPadding + (i * GridSquareHeight),
                    BoardHorizontalPadding + (GridSquareWidth * GameBoard.Columns),
                    BoardVerticalPadding + (i * GridSquareHeight));
            }

          
            for (int i = 1; i < GameBoard.Columns; i++)
            {
                g.DrawLine(pencilBounds,
                    BoardHorizontalPadding + (i * GridSquareWidth),
                    BoardVerticalPadding,
                    BoardHorizontalPadding + (i * GridSquareWidth),
                    BoardVerticalPadding + (GridSquareHeight * GameBoard.Rows));
            }
          

            g.DrawRectangle(pencilBounds, gameBoardBounds);


            for (int row = 0; row < GameBoard.Rows; row++)
            {
                for (int col = 0; col < GameBoard.Columns; col++)
                {
                    int x = GridSquareWidth * col + BoardHorizontalPadding + LineThickness + padding / 2;
                    int y = GridSquareHeight * row + BoardVerticalPadding + LineThickness + padding / 2;

                    Point center = new Point(x + chipPaddindX, y + chipPaddingY);

                    DrawSelectedChip(g, GameBoard[row, col], col, row, center);
                    if (GameBoard[row, col] != Chip.None && !_operationLastPlacedChips.Contains(new Tuple<int,int>(row,col)))
                    {
                        _operationLastPlacedChips.Add(new Tuple<int, int>(row, col),GameBoard[row,col]);
                        if (_operationLastPlacedChips.Count == 1)
                        {
                            undoButton.Visible = true;
                        }
                      
                    }

                }

            }

            if (!GameBoard.IsGameOver)
            {
                int x = GridSquareWidth * CurrentColumn + BoardHorizontalPadding + LineThickness + padding / 2;
                int y = GridSquareHeight * -1 + BoardVerticalPadding + LineThickness + padding / 2;
                Point center = new Point(x + chipPaddindX, y + chipPaddingY);

             
                DrawSelectedChip(g,GameBoard.CurrentChipTurn,CurrentColumn,-1,center);
            }

     


            HashSet<Point> winLocations = GameBoard.GetWinLoc();
            if (winLocations != null)
            {
                foreach (Point winLocation in winLocations)
                {
                    Rectangle winGridBoxBounds = new Rectangle(
                        BoardHorizontalPadding + winLocation.X * GridSquareWidth,
                        BoardVerticalPadding + winLocation.Y * GridSquareHeight,
                        GridSquareWidth,
                        GridSquareHeight);

                  
                    using (var br = new Pen(Color.LimeGreen, LineThickness))
                    {
                        g.DrawRectangle(br, winGridBoxBounds);
                    };
                }

            }

            using (StringFormat sf = new StringFormat())
            {
                Rectangle tetxBounds = new Rectangle(0,BoardVerticalPadding+ (GridSquareHeight*GameBoard.Rows),Width,(int)(GridSquareHeight * 1.5) - ((int)Font.Size / 2));
                if (GameBoard._score != null)
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                g.DrawString(
                    GameBoard.IsGameOver
                        ? "Click anywhere to start a new round..."
                        : $"{GameBoard._score.Count(score => score == Chip.Player1)} : {GameBoard._score.Count(score => score == Chip.Player2)} ",
                    Font, Brushes.Black, tetxBounds, sf);

                }
            }
            
         

        }


        protected override void OnResize(EventArgs e)
        {

            BoardHorizontalPadding = (int)(Width * 0.25);
            BoardVerticalPadding = (int)(Height * 0.15);

            StartX = Width - BoardHorizontalPadding * 2;
            StartY = Height - BoardVerticalPadding * 2;

            GridSquareWidth = StartX / GameBoard.Columns;
            GridSquareHeight = StartY / GameBoard.Rows;
            Invalidate();
        }

        private void ConnectFourMockUp_Load(object sender, EventArgs e)
        {
          
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!ConfirmExitMenu())
                e.Cancel = true;
            else
                OnAppClosingHandler?.Invoke(this);


        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
