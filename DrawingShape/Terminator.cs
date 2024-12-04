

namespace DrawingShape
{
    public class Terminator : Shape, IDrawable
    {
        public Terminator(ShapeType shapeType, string[] shapeDatas) : base(shapeType, shapeDatas) { }
        public void Draw(DrawingModel.IGraphics graphics)
        {
            graphics.DrawTerminator(ShapeDatas);
        }
        public bool IsPointInRange(int x, int y)
        {
            float r =  Height / 2.0f;
            float h1 = this._x + r;
            float k1 = this._y + r;
            float h2 = this._x + Width - r;
            float k2 = k1;
            bool isInLeftHalfCircle = (x - h1) * (x - h1) + (y - k1) * (y - k1) <= r * r;
            bool isInRighttHalfCircle = (x - h2) * (x - h2) + (y - k2) * (y - k2) <= r * r;
            bool isInMiddleRectangle = (x >= h1 && x <= h2) && (y >= this._y && y <= this._y + Height);
            return isInMiddleRectangle  || isInLeftHalfCircle || isInRighttHalfCircle;
        }
    }
}
