namespace CourseWork_OOP_Phase1_.Shapes
{
    public abstract class Shapes:IShapeable
    {
        public abstract void Fill(Graphics g, SolidBrush brush);
        public abstract void Fill(Graphics g, int linethickness);
        public abstract string Name { get; }
        public Color _color { get; set; }


        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Area { get; }
    }
}
