


namespace DrawingShape
{
    internal class Start : Shape, IDrawable
    {
        public Start(ShapeType shapeType, string[] shapeDatas) : base(shapeType, shapeDatas) { }
        public void Draw(DrawingModel.IGraphics graphics)
        {
            graphics.DrawStart(ShapeDatas);
        }
        public bool IsPointInRange(int x, int y)
        {
            float a = Width / 2.0f;
            float b = Height / 2.0f;
            float h = this._x + a;
            float k = this._y + b;  
            return (x - h) * (x - h) / (a * a) + (y - k) * (y - k) / (b * b) <= 1;
        }
    }
}
