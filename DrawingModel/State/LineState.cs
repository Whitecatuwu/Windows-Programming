using DrawingModel;
using DrawingShape;

namespace DrawingState
{
    public class LineState : IState
    {
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _selectingEvent = delegate { };
        public event ModelChangedEventHandler _selectingCompletedEvent = delegate { };

        int _firstX;
        int _firstY;
        int _secondX;
        int _secondY;
        bool _isPressed = false;
        Line _hint = null;

        public void Initialize(Model m)
        {
            _firstX = _secondX = 0;
            _firstY = _secondY = 0;
            _isPressed = false;
            _hint = null;
        }
        public void OnPaint(Model m, IGraphics g)
        {
            if (_hint != null)
            {
                ((IDrawable)_hint).Draw(g);
            }
        }
        public void MouseDown(Model m, int x, int y)
        {
            _isPressed = true;
            _firstX = _secondX = x;
            _firstY = _secondY = y;
        }
        public void MouseMove(Model m, int x, int y)
        {
            if (!_isPressed) return;
            if (_secondX == x && _secondY == y) return;
            if (_hint == null)
            {
                _hint = new Line();
                _hint.FirstX = _firstX;
                _hint.FirstY = _firstY;
                return;
            }
            _hint.SecondX = x;
            _hint.SecondY = y;
            _selectingEvent();
        }
        public void MouseUp(Model m, int x, int y)
        {
            if (!_isPressed) return;
            _isPressed = false;

            if (_hint == null || (_hint.FirstX == _hint.SecondX && _hint.FirstY == _hint.SecondY))
            {
                _selectingCompletedEvent();
                return;
            }
            m.AddLine(_hint);
            _selectingCompletedEvent();
        }

        public void MouseDoubleClick(Model m, int x, int y)
        {
            //nothing
        }
        public void KeyDown(Model m, int keyValue)
        {
            //nothing
        }
        public void KeyUp(Model m, int keyValue)
        {
            //nothing
        }
    }
}
