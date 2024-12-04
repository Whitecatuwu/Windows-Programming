

namespace DrawingShape
{
    public class Process : Shape, IDrawable
    {
        public Process(ShapeType shapeType, string[] shapeDatas) : base(shapeType, shapeDatas) { }
        public void Draw(DrawingModel.IGraphics graphics)
        {
            graphics.DrawProcess(ShapeDatas);
        }
        public bool IsPointInRange(int x, int y)
        {
            int rx = this._x + Width;
            int ry = this._y + Height;
            return (x <= rx && x >= this._x) && (y <= ry && y >= this._y);
        }
    }
}
