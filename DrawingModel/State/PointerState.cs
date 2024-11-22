using System.Collections.Generic;
using DrawingModel;
using DrawingShape;

namespace DrawingState
{
    public class PointerState : IState
    {
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler MovingShapesEvent = delegate { };
        public event ModelChangedEventHandler MovedShapesEvent = delegate { };
        public event ModelChangedEventHandler SelectedShapeEvent = delegate { };

        SortedSet<Shape> _selectedShapes = new SortedSet<Shape>();
        const int CTRL_KEY = 17;
        bool _isCtrlKeyDown = false;
        bool _isPressed = false;
        int _preX = 0;
        int _preY = 0;

        public void Initialize(Model m)
        {
            _selectedShapes.Clear();
            _isCtrlKeyDown = false;
            _isPressed = false;
            _preX = 0;
            _preY = 0;
        }

        public void MouseDown(Model m, int x, int y)
        {
            _isPressed = true;
            _preX = x;
            _preY = y;
            foreach (Shape shape in m.Shapes)
            {
                if (((IDrawable)shape).IsPointInRange(x, y))
                {
                    if (!_isCtrlKeyDown)
                        ClearSelectedShapes();
                    AddSelectedShape(shape);
                    return;
                }
            }
            if (!_isCtrlKeyDown)
                ClearSelectedShapes();
        }

        public void AddSelectedShape(Shape shape)
        {
            _selectedShapes.Add(shape);
            SelectedShapeEvent();
        }

        public void RemoveSelectedShape(Shape shape)
        {
            if (_selectedShapes.Remove(shape))
                SelectedShapeEvent();
        }

        public void ClearSelectedShapes()
        {
            _selectedShapes.Clear();
            SelectedShapeEvent();
        }

        public void MouseMove(Model m, int x, int y)
        {
            if (!_isPressed) return;

            foreach (Shape selectedShape in _selectedShapes)
            {
                selectedShape.X += x - _preX;
                selectedShape.Y += y - _preY;
                selectedShape.Normalize();
            }

            _preX = x;
            _preY = y;
            MovingShapesEvent();
        }

        public void MouseUp(Model m, int x, int y)
        {
            if (!_isPressed) return;
            _isPressed = false;

            foreach (Shape selectedShape in _selectedShapes)
            {
                m.UpdatedShapeIndex = m.Shapes.IndexOf(selectedShape);
                MovedShapesEvent();
            }
        }

        public void OnPaint(Model m, IGraphics g)
        {
            g.ClearAll();
            foreach (Shape shape in m.Shapes)
            {
                ((IDrawable)shape).Draw(g);
                shape.DrawText(g);
            }
            foreach (Shape selectedShape in _selectedShapes)
            {
                selectedShape.DrawFrame(g);
            }
        }

        public void KeyDown(Model m, int keyValue)
        {
            if (_isCtrlKeyDown) return;
            if (keyValue == CTRL_KEY)
                _isCtrlKeyDown = true;
        }

        public void KeyUp(Model m, int keyValue)
        {
            if (keyValue == CTRL_KEY)
                _isCtrlKeyDown = false;
        }

    }
}
