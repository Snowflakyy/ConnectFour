namespace CourseWork_OOP_Phase1_.Shapes
{
    internal class Square : Shapes,IShapeable
    {
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Area { get; set; }
        public float _a { get; set; }
        //public PointF center => new(CenterX, CenterY);
        public override string Name { get; }
        public Color _color { get; set; }
        private readonly float R;

        public RectangleF Rect => new(CenterX - R, CenterY - R, R * 2, R * 2);
        public Square(PointF center, float diag, Color color)
        {
            (CenterX, CenterY) = (center.X, center.Y);
            R = diag / 2;
            _a = 2 * R / (float)Math.Sqrt(2);
            Area = _a * _a;
            _color = color;
            Name = "Square";
        }
        public Square()
        {
            Name = "Square";
        }

        public override void Fill(Graphics g, SolidBrush brush)
        {
            brush.Color = _color;
            g.FillRectangle(brush, Rect);
        }

        public override void Fill(Graphics g, int linethickness)
        {
            var pen = new Pen(Color.Black, linethickness);
            g.DrawRectangle(pen, Rect);
        }

    }
}
