using DrawingModel;
using DrawingShape;
using System;
using System.Collections.Generic;

namespace DrawingState
{

    internal class DrawingState : IState
    {
        int _firstX;
        int _firstY;
        int _lastX;
        int _lastY;
        bool _ispressed = false;

        Shape _hint = null;
        PointerState pointerState;
        ShapeFactory _shapeFactory = new ShapeFactory();
        ShapeType _hintShapeType = ShapeType.NULL;

        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler SelectingEvent = delegate { };
        public event ModelChangedEventHandler SelectingCompletedEvent = delegate { };

        public DrawingState(PointerState pointerState)
        {
            this.pointerState = pointerState;
        }

        public ShapeType HintShapeType
        {
            set { _hintShapeType = value; }
        }


        public void Initialize(Model m)
        {
            _ispressed = false;
            _hint = null;
            _hintShapeType = ShapeType.NULL;
        }

        public void MouseDown(Model m, int x, int y)
        {
            _ispressed = true;
            _firstX = _lastX = x;
            _firstY = _lastY = y;
            //_hint = new Ellipse(new Rectangle(ul_point.X, ul_point.Y, 0, 0));
        }

        public void MouseMove(Model m, int x, int y)
        {
            if (!_ispressed) return;

            if (_hint == null)
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
            _ispressed = false;
            if (_hint == null) 
                return;
            _hint.Normalize();
            m.AddShape(_hint);   
            SelectingCompletedEvent();
            pointerState.AddSelectedShape(_hint);
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
