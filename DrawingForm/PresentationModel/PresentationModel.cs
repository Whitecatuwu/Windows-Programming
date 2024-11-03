using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using DrawingModel;
using DrawingShape;

namespace DrawingForm.PresentationModel
{
    class PresentationModel
    {
        Model _model;
        private string[] _inputDatas = new string[5]; // Text, X, Y, H, W
        bool[] _switchBool = new bool[4] { false, false, false, false };
        ShapeType _lastBoolIndex = ShapeType.START;

        public PresentationModel(Model model, Control canvas)
        {
            this._model = model;
        }

        public string[] InputDatas
        {
            set { _inputDatas = value; }
            get { return _inputDatas; }
        }

        public bool IsStartEnable
        {
            get { return _switchBool[0]; }
        }

        public bool IsTerminatorEnable
        {
            get { return _switchBool[1]; }
        }

        public bool IsProcessEnable
        {
            get { return _switchBool[2]; }
        }

        public bool IsDecisionEnable
        {
            get { return _switchBool[3]; }
        }
        public void setDrewingShape(ShapeType shapeType)
        {
            _switchBool[(int)_lastBoolIndex] = false;
            _switchBool[(int)shapeType] = true;      
            _lastBoolIndex = shapeType;
            _model.HintType = shapeType;
        }

        public void AddShape(object shapeType)
        {
            _model.AddShape(shapeType, _inputDatas);
        }

        public void Draw(IGraphics graphics)
        {
            _model.Draw(graphics);
        }
    }
}
