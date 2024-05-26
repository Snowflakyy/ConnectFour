namespace CourseWork_OOP_Phase1_.Shapes
{
    internal class Triangle : Shapes,IShapeable
    {


        private static readonly (double sin, double cos) _sincos60 = Math.SinCos(Math.PI / 3);
        private PointF[] _points;
        private PointF[] _rotatedPoints;
        public override string Name { get; }


        private PointF _center { get; set; }
        private readonly float _a;
        private readonly float halfA;
        private readonly float rCirc;
        private readonly float rInscr;
        private float _height;
        public float Area { get; }
        public Color _color { get; set; }
        //public float CenterX { get; set; }
        //public float CenterY { get; set; }


        public Triangle(PointF center, float height, Color color)
        {
            _center = center;
            _color = color;
            _height = height;
            //CenterX = center.X;
            Name = "Triangle";
            rCirc = height * 2 / 3;
            rInscr = height * 1 / 3;
            _a = rCirc * 2 * (float)_sincos60.sin;
            halfA = _a / 2;
            float TrianS = _a * height / 2;
            Area = TrianS;
            _points = [
                new(_center.X + halfA, _center.Y + rInscr),
                new(_center.X, _center.Y - rCirc),
                new(_center.X - halfA, _center.Y + rInscr)
                ];
            _rotatedPoints = new PointF[3];
            Array.Copy(_points, _rotatedPoints, _rotatedPoints.Length);
        }
        public Triangle()
        {
            Name = "Triangle";
        }

        public override void Fill(Graphics g, SolidBrush brush)
        {
            brush.Color = _color;


            _points[0] = new PointF(_center.X - _a / 2, _center.Y + rInscr);
            _points[1] = new PointF(_center.X, _center.Y - rCirc);
            _points[2] = new PointF(_center.X + _a / 2, _center.Y + rInscr);

            for (int i = 0; i < _rotatedPoints.Length; i++)
            {

                _rotatedPoints = [
           new(_center.X + halfA, _center.Y + rInscr),
           new(_center.X, _center.Y - rCirc),
           new(_center.X - halfA, _center.Y + rInscr)
           ];


            }

            g.FillPolygon(brush, _rotatedPoints);
        }
        public override void Fill(Graphics g, int linethickness)
        {
            var pen = new Pen(Color.Black, linethickness);
            g.DrawPolygon(pen, _points);
        }



    }
}
