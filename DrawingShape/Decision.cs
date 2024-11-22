using System;

namespace DrawingShape
{
    internal class Decision : Shape, IDrawable
    {
        public Decision(ShapeType shapeType, string[] shapeDatas) : base(shapeType, shapeDatas) { }
        public void Draw(DrawingModel.IGraphics graphics)
        {
            graphics.DrawDecision(ShapeDatas);
        }
        public bool IsPointInRange(int x, int y)
        {
            float d1 =  Width;
            float d2 = Height;
            float h = this._x + d1/2.0f;
            float k = this._y + d2/2.0f;
            return Math.Abs(x-h)/(d1/2.0f) + Math.Abs(y-k)/(d2/2.0f) <= 1;
        }
    }
}
