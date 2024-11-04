using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using DrawingModel;
using DrawingShape;
using System.Reflection;

namespace DrawingForm.PresentationModel
{
    public enum DrawingMode
    {
        NULL = -1,
        START = 0,
        TERMINATOR = 1,
        PROCESS = 2,
        DECISION = 3,
        SELECT = 4
    }
    class PresentationModel
    {
        Model _model;

        private string[] _inputDatas = new string[5]; // Text, X, Y, H, W
        bool[] _drawingModeSwitch = new bool[5] { false, false, false, false, true };

        private Dictionary<string, ShapeType> _stringToShapeType = new Dictionary<string, ShapeType>() { };
        private Dictionary<ShapeType, string> _shapeTypeToString = new Dictionary<ShapeType, string>() { };

        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _pModelChangedMode = delegate { };
        public event ModelChangedEventHandler _pModelGetNullShapeType = delegate { };
        public event ModelChangedEventHandler _pModelGetErrorInput = delegate { };

        DrawingMode _currentDrawingMode = DrawingMode.SELECT;
        int _removedShapeIndex;
        bool _isPressed = false;
        bool _isMoved = false;

        public PresentationModel(Model model, Control canvas)
        {
            this._model = model;
            _model._modelDrawingCompleted += delegate { SetDrawingMode(DrawingMode.SELECT); };

            _stringToShapeType.Add("Start", ShapeType.START);
            _stringToShapeType.Add("Terminator", ShapeType.TERMINATOR);
            _stringToShapeType.Add("Process", ShapeType.PROCESS);
            _stringToShapeType.Add("Decision", ShapeType.DECISION);

            _shapeTypeToString.Add(ShapeType.START, "Start");
            _shapeTypeToString.Add(ShapeType.TERMINATOR, "Terminator");
            _shapeTypeToString.Add(ShapeType.PROCESS, "Process");
            _shapeTypeToString.Add(ShapeType.DECISION, "Decision");
        }

        public string[] InputDatas
        {
            set { _inputDatas = value; }
            get { return _inputDatas; }
        }

        public bool IsStartEnable
        {
            get { return _drawingModeSwitch[0]; }
        }

        public bool IsTerminatorEnable
        {
            get { return _drawingModeSwitch[1]; }
        }

        public bool IsProcessEnable
        {
            get { return _drawingModeSwitch[2]; }
        }

        public bool IsDecisionEnable
        {
            get { return _drawingModeSwitch[3]; }
        }

        public bool IsSelectEnable
        {
            get { return _drawingModeSwitch[4]; }
        }

        public int RemovedShapeIndex
        {
            get { return _removedShapeIndex; }
        }

        public string[] LastShapeData
        {
            get 
            {
                Shape shape = _model.Shapes.Last();
                string[] shapeData = new string[]{
                    shape.Id,
                    _shapeTypeToString[shape.ShapeType],
                    shape.Text,
                    shape.X.ToString(),
                    shape.Y.ToString(),
                    shape.Height.ToString(),
                    shape.Width.ToString(),
                }.ToArray();
                return shapeData;
            }
        }

        public void SetDrawingMode(in DrawingMode drawingMode)
        {
            _drawingModeSwitch[(int)_currentDrawingMode] = false;
            _drawingModeSwitch[(int)drawingMode] = true;
            _currentDrawingMode = drawingMode;
            if ((int)drawingMode >= 0 && (int)drawingMode <= 3)
                _model.HintType = (ShapeType)drawingMode;
            _pModelChangedMode();
        }

        public void ClickedAt(in int r, in int c)
        {
            if (r >= 0 && c == 0)
            {
                _removedShapeIndex = r;
                _model.RemoveShape(r);
            }
        }

        private bool IsInputsCurrently()
        {
            for (int i = 1; i < 5; i++)
            {
                try
                {
                    if (int.Parse(_inputDatas[i]) <= 0)
                        return false;
                }
                catch { return false; }
            }
            return true;
        }

        public void AddShape(in object shapeType)
        {
            if (shapeType == null)
            {
                _pModelGetNullShapeType();
                return;
            }
            if (!IsInputsCurrently())
            {
                _pModelGetErrorInput();
                return;
            }
            _model.AddShape(_stringToShapeType[shapeType.ToString()], _inputDatas);
        }

        public void PointerPressed(in int x, in int y)
        {
            _isPressed = true;
            _isMoved = false;
            if (_currentDrawingMode == DrawingMode.NULL)
                return;
            if (x <= 0 || y <= 0)
                return;          
            if (_currentDrawingMode == DrawingMode.SELECT)
            {
                _model.SelectShape(x,y);
            }
            else
                _model.SelectFirstPoint(x, y);
        }

        public void PointerMoved(in int x, in int y)
        {
            _isMoved = true;
            if (_isPressed && _currentDrawingMode != DrawingMode.SELECT)
                _model.SelectSecondPoint(x, y);
        }

        public void PointerReleased(in int x, in int y)
        {
            if (_isPressed)
            {
                _isPressed = false;
                if (_isMoved && _currentDrawingMode != DrawingMode.SELECT)
                    _model.SelectPointCompleted(x, y);
            }
        }

        public void Draw(in IGraphics graphics)
        {
            if (graphics == null) { return; }
            _model.DrawAll(graphics);
            if (_currentDrawingMode == DrawingMode.SELECT)
            {
                _model.DrawFrames(graphics);
            }
            else if (_currentDrawingMode != DrawingMode.NULL)
            {
                _model.DrawOne(graphics);
            }         
        }
    }
}
