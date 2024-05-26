
using System.Reflection.Metadata;

namespace CourseWork_OOP_Phase1_.Shapes
{
    internal class Circle : Shapes,IShapeable
    {
        //private readonly float CenterX;
        //private readonly float CenterY;
        private readonly float R;
        public override string Name { get; }

       // public int level { get; }
        public PointF Center => new(CenterX, CenterY);

        public  float CenterX { get;private set; }
        public  float CenterY { get;private set; }
        public Color _color { get; private set; }
        public float Area { get; private set; }
        public RectangleF Rectangle => new(CenterX - R, CenterY - R, R * 2, R * 2);

        public Circle(PointF center, float r, Color color)
        {
            CenterX = center.X;
            CenterY = center.Y;
            R = r;
            _color = color;
            Name = "Circle";
            Area = (float)(Math.PI * r * r);
        }
        public Circle()
        {
            Name = "Circle";
        }

        public override void Fill(Graphics g, SolidBrush brush)
        {
            brush.Color = _color;
            g.FillEllipse(brush, Rectangle);
        }
        public override void Fill(Graphics g, int linethickness)
        {
            var pen = new Pen(Color.Black, linethickness);
            g.DrawEllipse(pen, Rectangle);
        }


    }
}
