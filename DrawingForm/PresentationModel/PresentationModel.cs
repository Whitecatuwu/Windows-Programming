using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;
using DrawingShape;
using System.Reflection;
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
        POINTER = 4
    }
    class PresentationModel : INotifyPropertyChanged
    {
        Model _model;

        private string[] _inputDatas = new string[5]; // Text, X, Y, H, W
        bool[] _inputCorrectlyBools = new bool[5] { true, false, false, false, false };// Text, X, Y, H, W
        bool[] _drawingModeSwitch = new bool[5] { false, false, false, false, true };

        private Dictionary<string, ShapeType> _stringToShapeType = new Dictionary<string, ShapeType>() { };
        private Dictionary<ShapeType, string> _shapeTypeToString = new Dictionary<ShapeType, string>() { };

        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler ChangedModeEvent = delegate { };
        public event ModelChangedEventHandler GotNullShapeTypeEvent = delegate { };
        public event ModelChangedEventHandler GotErrorInputEvent = delegate { };

        DrawingMode _currentDrawingMode = DrawingMode.POINTER;

        public PresentationModel(Model model)
        {
            this._model = model;
            _model.SelectingCompletedEvent += delegate { SetDrawingMode(DrawingMode.POINTER); };

            /*來自view輸入的資料型態為string，而model創建形狀所需的參數不全為string，
             資料型態轉換及判斷輸入的部分在PresentationModel實現。*/
            _stringToShapeType.Add("Start", ShapeType.START);
            _stringToShapeType.Add("Terminator", ShapeType.TERMINATOR);
            _stringToShapeType.Add("Process", ShapeType.PROCESS);
            _stringToShapeType.Add("Decision", ShapeType.DECISION);

            _shapeTypeToString.Add(ShapeType.START, "Start");
            _shapeTypeToString.Add(ShapeType.TERMINATOR, "Terminator");
            _shapeTypeToString.Add(ShapeType.PROCESS, "Process");
            _shapeTypeToString.Add(ShapeType.DECISION, "Decision");
        }
        //------------Switch------------
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
        //------------------------

        public bool IsAddButtonEnabled
        {
            get { return _inputCorrectlyBools.All(b => b); }
        }

        public string[] GetShapeData(in int index)
        {
            Shape shape = _model.Shapes.ElementAt(index);
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

        public void SetDrawingMode(in DrawingMode drawingMode)
        {
            _drawingModeSwitch[(int)_currentDrawingMode] = false;
            _drawingModeSwitch[(int)drawingMode] = true;
            _currentDrawingMode = drawingMode;

            if (drawingMode == DrawingMode.POINTER)
            {
                _model.EnterPointerState();
            }
            else if (drawingMode != DrawingMode.NULL)
            {
                _model.EnterDrawingState((ShapeType)drawingMode);
            }

            ChangedModeEvent();
        }

        public bool CheckInput(in int index, in string data)
        {
            if (index < 1 || index > 4) return false;
            _inputDatas[index] = data;
            try
            {
                _inputCorrectlyBools[index] = (int.Parse(_inputDatas[index]) > 0);
            }
            catch (Exception)
            {
                return false;
            }
            Notify("IsAddButtonEnabled");    
            return _inputCorrectlyBools[index];
        }

        public void AddShape(in object shapeType)
        {
            //for DataGridView adding shape.
            if (shapeType == null)
            {
                GotNullShapeTypeEvent();
                return;
            }
         
            _model.AddShape(_stringToShapeType[shapeType.ToString()], _inputDatas);
        }
        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
