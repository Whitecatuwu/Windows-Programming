

namespace DrawingShape
{
    public interface IDrawable
    {
        void Draw(DrawingModel.IGraphics graphics);

        bool IsPointInRange(int x, int y);
    }
}
