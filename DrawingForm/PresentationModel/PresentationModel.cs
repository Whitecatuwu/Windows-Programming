using System.Collections.Generic;
using System.Linq;
using DrawingModel;
using DrawingShape;
using System.ComponentModel;

namespace DrawingForm.PresentationModel
{
    public enum DrawingMode
    {
        NULL = -1,
        START = 0,
        TERMINATOR = 1,
        PROCESS = 2,
        DECISION = 3,
        POINTER = 4,
        LINE = 5
    }
    public class PresentationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _changedModeEvent = delegate { };
        public event ModelChangedEventHandler _gotNullShapeTypeEvent = delegate { };
        public event ModelChangedEventHandler _gotErrorInputEvent = delegate { };

        Model _model;
        Praser _praser = new Praser();

        const int RED = 0x78FF0000;
        const int BLACK = 0x78000000;

        string[] _inputedDatas = new string[5]; // Text, X, Y, H, W
        bool[] _inputCorrectlyBools = new bool[5] { true, false, false, false, false };// Text, X, Y, H, W
        bool[] _drawingModeSwitch = new bool[6] { false, false, false, false, true, false };
        DrawingMode _currentDrawingMode = DrawingMode.POINTER;

        public PresentationModel(Model model)
        {
            this._model = model;
            _model._selectingCompletedEvent += delegate { SetDrawingMode(DrawingMode.POINTER); };
        }
        //------------Switch------------
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
        //------------Inputs State------------

        public int GetXStateColor
        {
            get { return _inputCorrectlyBools[1] ? BLACK : RED; }
        }

        public int GetYStateColor
        {
            get { return _inputCorrectlyBools[2] ? BLACK : RED; }
        }

        public int GetHStateColor
        {
            get { return _inputCorrectlyBools[3] ? BLACK : RED; }
        }

        public int GetWStateColor
        {
            get { return _inputCorrectlyBools[4] ? BLACK : RED; }
        }
        //------------------------
        public string[] InputDatas
        {
            set { _inputedDatas = value; }
            get { return _inputedDatas; }
        }

        public bool IsAddButtonEnabled
        {
            get { return _inputCorrectlyBools.All(b => b); }
        }

        public string[] GetShapeData(in int index)
        {
            Shape shape = _model.Shapes.ElementAt(index);
            string[] shapeData = new string[]{
                shape.Id,
                _praser.ToString(shape.ShapeType),
                shape.Text,
                shape.X.ToString(),
                shape.Y.ToString(),
                shape.Height.ToString(),
                shape.Width.ToString(),
            }.ToArray();
            return shapeData;
        }

        public void SetDrawingMode(in DrawingMode drawingMode)
        {
            if (drawingMode == DrawingMode.NULL) return;

            _drawingModeSwitch[(int)_currentDrawingMode] = false;
            _drawingModeSwitch[(int)drawingMode] = true;
            _currentDrawingMode = drawingMode;

            if (drawingMode == DrawingMode.POINTER)
            {
                _model.EnterPointerState();
            }
            else if(drawingMode == DrawingMode.LINE)
            {
                _model.EnterLineState();
            }
            else
            {
                _model.EnterDrawingState((ShapeType)drawingMode);
            }

            _changedModeEvent();
        }

        public void InputData(in int index, in string data)
        {
            if (index < 1 || index > 4) return;
            _inputedDatas[index] = data;
            _inputCorrectlyBools[index] = int.TryParse(data, out int result) && result > 0;
            Notify("IsAddButtonEnabled");
        }

        public void AddShape(in object shapeType)
        {
            //for DataGridView adding shape.
            if (shapeType == null)
            {
                _gotNullShapeTypeEvent();
                return;
            }
            _model.AddShape(_praser.ToShapeType(shapeType.ToString()), _inputedDatas);
        }
        private void Notify(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
