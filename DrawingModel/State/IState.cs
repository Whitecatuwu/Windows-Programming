using DrawingModel;

namespace DrawingState
{
    public interface IState
    {
        void Initialize(Model m);
        void OnPaint(Model m, IGraphics g);
        void MouseDown(Model m, int x,int y);
        void MouseMove(Model m, int x, int y);
        void MouseUp(Model m, int x, int y);
        void MouseDoubleClick(Model m, int x, int y);
        void KeyDown(Model m, int keyValue);
        void KeyUp(Model m, int keyValue);
    }
}
