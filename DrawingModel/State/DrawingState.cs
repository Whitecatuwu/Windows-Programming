using DrawingModel;
using DrawingShape;
using System;

namespace DrawingState
{
    internal class DrawingState : IState
    {
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler SelectingEvent = delegate { };
        public event ModelChangedEventHandler SelectingCompletedEvent = delegate { };

        int _firstX;
        int _firstY;
        int _lastX;
        int _lastY;
        bool _isPressed = false;

        Shape _hint = null;
        PointerState _pointerState;
        ShapeFactory _shapeFactory = new ShapeFactory();
        ShapeType _hintShapeType = ShapeType.NULL;

        public DrawingState(PointerState pointerState)
        {
            this._pointerState = pointerState;
        }

        public ShapeType HintShapeType
        {
            set { _hintShapeType = value; }
        }


        public void Initialize(Model m)
        {
            _isPressed = false;
            _hint = null;
            _hintShapeType = ShapeType.NULL;
        }

        public void MouseDown(Model m, int x, int y)
        {
            _isPressed = true;
            _firstX = _lastX = x;
            _firstY = _lastY = y;
        }

        public void MouseMove(Model m, int x, int y)
        {
            if (!_isPressed) return;

            if (_hint == null && _hintShapeType != ShapeType.NULL)
            {
                string[] shapeData = new string[] { new Random().Next().ToString(), x.ToString(), y.ToString(), "1", "1" };
                _hint = _shapeFactory.CreateShape(_hintShapeType, shapeData);
            }
            _lastX = x;
            _lastY = y;
            _hint.Width = _lastX - _firstX;
            _hint.Height = _lastY - _firstY;  
            SelectingEvent();
        }

        public void MouseUp(Model m, int x, int y)
        {
            if (!_isPressed) return;
            _isPressed = false;

            if (_hint == null || _hint.Height == 0 || _hint.Width == 0)
            {
                SelectingCompletedEvent();
                return;
            }           
            _hint.Normalize();
            m.AddShape(_hint);
            SelectingCompletedEvent();
            _pointerState.AddSelectedShape(_hint);
        }

        public void OnPaint(Model m, IGraphics g)
        {
            g.ClearAll();
            foreach (Shape shape in m.Shapes)
            {
                ((IDrawable)shape).Draw(g);
                shape.DrawText(g);
            }
            ((IDrawable)_hint).Draw(g);
        }

        public void KeyDown(Model m, int keyValue)
        {
            // do nothing
        }

        public void KeyUp(Model m, int keyValue)
        {
            // do nothing
        }
    }
}
