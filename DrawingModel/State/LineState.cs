using DrawingModel;

namespace DrawingState
{
    public class LineState : IState
    {
        public void Initialize(Model m) { }
        public void OnPaint(Model m, IGraphics g) { }
        public void MouseDown(Model m, int x, int y) { }
        public void MouseMove(Model m, int x, int y) { }
        public void MouseUp(Model m, int x, int y) { }
        public void MouseDoubleClick(Model m, int x, int y) { }
        public void KeyDown(Model m, int keyValue) { }
        public void KeyUp(Model m, int keyValue) { }
    }
}
