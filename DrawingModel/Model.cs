using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingShape;

namespace DrawingModel
{
    internal class Model
    {
        private ShapeFactory _shapeFactory = new ShapeFactory();
        private List<Shape> _shapes = new List<Shape>();
        private List<string[]> _shapeDatas = new List<string[]>();
        private Dictionary<string, ShapeType> _stringToShapeType = new Dictionary<string, ShapeType>() { };
        private Dictionary<ShapeType, string> _shapeTypeToString = new Dictionary<ShapeType, string>() { };

        int _firstPointX;
        int _firstPointY;
        bool _isPressed = false;
        ShapeType _hintType = ShapeType.NULL;
        Shape _hint;

        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _modelAddedShape = delegate { };
        public event ModelChangedEventHandler _modelInputError = delegate { };
        public event ModelChangedEventHandler _modelNullShapeType = delegate { };
        public event ModelChangedEventHandler _modelRemovedShape = delegate { };
        public event ModelChangedEventHandler _modelChanged = delegate { };
        public event ModelChangedEventHandler _modelPointerDragging = delegate { };

        public Model()
        {
            _stringToShapeType.Add("Start", ShapeType.START);
            _stringToShapeType.Add("Terminator", ShapeType.TERMINATOR);
            _stringToShapeType.Add("Process", ShapeType.PROCESS);
            _stringToShapeType.Add("Decision", ShapeType.DECISION);

            _shapeTypeToString.Add(ShapeType.START, "Start");
            _shapeTypeToString.Add(ShapeType.TERMINATOR, "Terminator");
            _shapeTypeToString.Add(ShapeType.PROCESS, "Process");
            _shapeTypeToString.Add(ShapeType.DECISION, "Decision");
        }

        public List<Shape> Shapes
        {
            get { return _shapes; }
        }

        public List<string[]> ShapeDatas
        {
            get { return _shapeDatas; }
        }

        public ShapeType HintType
        {
            set { _hintType = value; }
            get { return _hintType; }
        }

        public void AddShape(object shapeType, string[] inputDatas)
        {
            if (shapeType == null)
            {
                _modelNullShapeType();
                return;
            }
            try
            {
                if (int.Parse(inputDatas[1].ToString()) < 0 || int.Parse(inputDatas[2].ToString()) < 0)
                {
                    _modelInputError();
                    return;
                }

                if (int.Parse(inputDatas[3].ToString()) <= 0 || int.Parse(inputDatas[4].ToString()) <= 0)
                {
                    _modelInputError();
                    return;
                }              
            }
            catch
            {
                _modelInputError();
                return;
            }
            Shape shape = _shapeFactory.CreateShape(_stringToShapeType[shapeType.ToString()], inputDatas);
            _shapes.Add(shape);
            AddShapeData(shape);
            _modelAddedShape();
        }

        private void AddShapeData(Shape shape)
        {
            string[] shapeData = new string[]
            {
                shape.Id,
                _shapeTypeToString[shape.ShapeType],
                shape.Text,
                shape.X.ToString(),
                shape.Y.ToString(),
                shape.Height.ToString(),
                shape.Width.ToString(),
            }.ToArray();
            _shapeDatas.Add(shapeData);
        }

        public void RemoveShape(int index)
        {
            _shapes.RemoveAt(index);
            _shapeDatas.RemoveAt(index);
            _modelRemovedShape();
        }

        public void PointerPressed(int x, int y)
        {
            if(_hintType == ShapeType.NULL)
            {
                _modelNullShapeType();
                return;
            }

            if (x > 0 && y > 0)
            {
                _hint = _shapeFactory.CreateShape(_hintType, new string[] { new Random().Next().ToString(), x.ToString(), y.ToString(), "1", "1" });
                _firstPointX = x;
                _firstPointY = y;
                _isPressed = true;
            }
        }

        public void PointerMoved(int x, int y)
        {

            if (_isPressed)
            {
                _hint.Width = Math.Max(Math.Abs(x - _firstPointX), 1);
                _hint.Height = Math.Max(Math.Abs(y - _firstPointY), 1);
                _modelPointerDragging();
                _modelChanged();
            }
        }

        public void PointerReleased(double x, double y)
        {
            if (_isPressed)
            {
                _isPressed = false;
                _shapes.Add(_hint);
                AddShapeData(_hint);
                _modelAddedShape();
            }
        }

        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (IDrawable shape in _shapes)
                shape.Draw(graphics);
            if (_isPressed)
                ((IDrawable)_hint).Draw(graphics);
        }
    }
}
