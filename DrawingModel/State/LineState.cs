using DrawingCommand;
using DrawingModel;
using DrawingShape;
using System.Linq;

namespace DrawingState
{
    public class LineState : IState
    {
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _selectingEvent = delegate { };
        public event ModelChangedEventHandler _selectingCompletedEvent = delegate { };
        public event ModelChangedEventHandler _selectingFailedEvent = delegate { };
        public event ModelChangedEventHandler _touchShapeEvent = delegate { };

        int _firstX;
        int _firstY;
        int _secondX;
        int _secondY;
        bool _isPressed = false;
        Line _hint = null;

        Shape _touchedShape = null;

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
            if (_touchedShape != null)
            {
                _touchedShape.DrawConnectionPoints(g);
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
            DisplayTouchedShape(m, x, y);

            if (!_isPressed) return;
            if (_secondX == x && _secondY == y) return;
            if (_hint == null)
            {
                foreach (Shape shape in m.Shapes.Reverse())
                {
                    var point = shape.TouchConnectPoint(x, y);
                    if (point == null) continue;

                    _hint = new Line();
                    _hint.FirstX = _firstX;
                    _hint.FirstY = _firstY;
                    _hint.ConnectedFirstPoint = point;
                    return;
                }
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
                _selectingFailedEvent();
                return;
            }

            foreach (Shape shape in m.Shapes.Reverse())
            {
                var point = shape.TouchConnectPoint(x, y);
                if (point == null) continue;
                if (_hint.ConnectedFirstPoint.CompareTo(point) == 0)
                {
                    break;
                };

                m.ExeCommand(new AddLineCommand(m, _hint.ConnectedFirstPoint, point, _hint));
                //_hint.ConnectedSecondPoint = point;
                _hint = null;
                _selectingCompletedEvent();
                return;
            }
            _hint.ConnectedFirstPoint.RemoveConnectionLine(_hint);
            _hint = null;
            _selectingFailedEvent();
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

        private void DisplayTouchedShape(Model m, int x, int y)
        {
            foreach (Shape shape in m.Shapes.Reverse())
            {
                if (((IDrawable)shape).IsPointInRange(x, y))
                {
                    _touchedShape = shape;
                    _touchedShape.Touched = true;
                    _touchShapeEvent();
                    break;
                }
                if (_touchedShape != null)
                {
                    _touchedShape.Touched = false;
                    _touchedShape = null;
                    _touchShapeEvent();
                }
            }
        }
    }
}
