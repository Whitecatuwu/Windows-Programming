﻿using DrawingModel;
using DrawingShape;
using DrawingCommand;

using System;

namespace DrawingState
{
    public class DrawingState : IState
    {
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _selectingEvent = delegate { };
        public event ModelChangedEventHandler _selectingCompletedEvent = delegate { };
        public event ModelChangedEventHandler _selectingFailedEvent = delegate { };

        const int SHIFT_KEY = 160;
        int _firstX;
        int _firstY;
        int _lastX;
        int _lastY;
        bool _isPressed = false;
        bool _isShiftKeyDown = false;

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
            _isShiftKeyDown = false;
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
            if (_lastX == x && _lastY == y) return;
            SelectHintRange(x, y);
        }

        public void MouseUp(Model m, int x, int y)
        {
            if (!_isPressed) return;
            _isPressed = false;

            if (_hint == null || _hint.Height == 0 || _hint.Width == 0)
            {
                _selectingFailedEvent();
                return;
            }
            _hint.Normalize();
            //m.AddShape(_hint);
            m.ExeCommand(new AddShapeCommand(m,_hint));
            _selectingCompletedEvent();
            _pointerState.AddSelectedShape(_hint);
        }
        public void MouseDoubleClick(Model m, int x, int y)
        {
            //nothing
        }
        public void OnPaint(Model m, IGraphics g)
        {
            if (_hint != null)
            {
                ((IDrawable)_hint).Draw(g);
            }
        }

        public void KeyDown(Model m, int keyValue)
        {
            if (_isShiftKeyDown) return;
            if (keyValue == SHIFT_KEY)
            {
                SelectHintRange(_lastX, _lastY);
                _isShiftKeyDown = true;
            }
        }

        public void KeyUp(Model m, int keyValue)
        {
            if (keyValue == SHIFT_KEY)
            {
                SelectHintRange(_lastX, _lastY);
                _isShiftKeyDown = false;
            }
        }

        private void SelectHintRange(int x, int y)
        {
            if (_hint == null)
            {
                if (_hintShapeType == ShapeType.NULL) return;
                string[] shapeData = new string[] { new Random().Next().ToString(), _firstX.ToString(), _firstY.ToString(), "1", "1" };
                _hint = _shapeFactory.CreateShape(_hintShapeType, shapeData);
            }
         
            _lastX = x;
            _lastY = y;

            int dx = _lastX - _firstX;
            int dy = _lastY - _firstY;

            if (_isShiftKeyDown)
            {
                int width = Math.Min(Math.Abs(dx), Math.Abs(dy));
                _hint.Width = (dx < 0) ? -width : width;
                _hint.Height = (dy < 0) ? -width : width;
            }
            else
            {
                _hint.Width = dx;
                _hint.Height = dy;
            }
            _selectingEvent();
        }
    }
}
