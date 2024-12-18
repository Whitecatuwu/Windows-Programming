using System;
using System.Collections.Generic;
using System.Linq;
using DrawingCommand;
using DrawingModel;
using DrawingShape;

namespace DrawingState
{
    public class PointerState : IState
    {
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _movingShapesEvent = delegate { };
        public event ModelChangedEventHandler _movedShapesEvent = delegate { };
        public event ModelChangedEventHandler _selectedShapeEvent = delegate { };
        public event ModelChangedEventHandler _editShapeTextEvent = delegate { };

        SortedSet<Shape> _selectedShapes = new SortedSet<Shape>();
        SortedDictionary<Shape, Tuple<int, int>> _selectedShapesPrePositions = new SortedDictionary<Shape, Tuple<int, int>> { }; // {shape:(X,Y)}

        const int CTRL_KEY = 17;
        bool _isCtrlKeyDown = false;
        bool _isPressed = false;
        bool _isMoved = false;
        int _preX = 0;
        int _preY = 0;

        Shape _touchedTextBoxShape = null;
        Tuple<int, int> _touchedTextBoxShapePos = null;

        public void Initialize(Model m)
        {
            _selectedShapes.Clear();
            _isCtrlKeyDown = false;
            _isPressed = false;
            _isMoved = false;
            _preX = 0;
            _preY = 0;
        }

        public void MouseDown(Model m, int x, int y)
        {
            _isPressed = true;
            _isMoved = false;
            _preX = x;
            _preY = y;

            foreach (Shape shape in m.Shapes.Reverse())
            {
                if (_touchedTextBoxShape == null && shape.IsTouchMovePoint(x, y))
                {
                    _touchedTextBoxShape = shape;
                    _touchedTextBoxShapePos = new Tuple<int, int>(shape.TextBox_X, shape.TextBox_Y);
                    return;
                }

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
            _selectedShapesPrePositions.Add(shape, new Tuple<int, int>(shape.X, shape.Y));
            _selectedShapeEvent();
        }

        public void RemoveSelectedShape(Shape shape)
        {
            if (_selectedShapes.Remove(shape))
            {
                _selectedShapeEvent();
                _selectedShapesPrePositions.Remove(shape);
            }
        }

        public void ClearSelectedShapes()
        {
            _selectedShapes.Clear();
            _selectedShapesPrePositions.Clear();
            _selectedShapeEvent();
        }

        public void MouseMove(Model m, int x, int y)
        {
            if (!_isPressed) return;
            if (_preX == x && _preY == y) return;
            _isMoved = true;

            if (_touchedTextBoxShape != null)
            {
                _touchedTextBoxShape.TextBox_X += x - _preX;
                _touchedTextBoxShape.TextBox_Y += y - _preY;
            }

            else
            {
                foreach (Shape selectedShape in _selectedShapes)
                {
                    selectedShape.X += x - _preX;
                    selectedShape.Y += y - _preY;
                    selectedShape.Normalize();
                }
            }

            _preX = x;
            _preY = y;
            _movingShapesEvent();
        }

        public void MouseUp(Model m, int x, int y)
        {
            if (!_isPressed) return;
            _isPressed = false;

            if (_touchedTextBoxShape != null && _isMoved)
            {
                _isMoved = false;
                m.ExeCommand(new TextMoveCommend(m, _touchedTextBoxShape, _touchedTextBoxShapePos));
                _touchedTextBoxShape = null;
                return;
            }

            if (_selectedShapes.Count() <= 0) return;
            if (!_isMoved) return;
            m.ExeCommand(new ShapeMoveCommand(m, _selectedShapes, _selectedShapesPrePositions));
            _isMoved = false;

            /*foreach (Shape selectedShape in _selectedShapes)
            {
                m.UpdatedShapeIndex = m.Shapes.IndexOf(selectedShape);
                _movedShapesEvent();
            }*/
        }

        public void MouseDoubleClick(Model m, int x, int y)
        {
            foreach (Shape shape in m.Shapes.Reverse())
            {
                if (shape.IsTouchMovePoint(x, y))
                {
                    m.TextEditShapeIndex = m.Shapes.IndexOf(shape);
                    _editShapeTextEvent();
                    return;
                }
            }
        }

        public void OnPaint(Model m, IGraphics g)
        {
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