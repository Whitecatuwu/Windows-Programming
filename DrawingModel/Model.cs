using System;
using System.Collections.Generic;
using System.Drawing;
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
        private HashSet<int> _selectedShapeIndexs = new HashSet<int>();

        ShapeType _hintType = ShapeType.NULL;
        Shape _hint;
        int _firstPointX;
        int _firstPointY;
        int _lastPointX;
        int _lastPointY;

        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _modelAddedShape = delegate { };
        public event ModelChangedEventHandler _modelRemovedShape = delegate { };
        public event ModelChangedEventHandler _modelDrawing = delegate { };
        public event ModelChangedEventHandler _modelDrawingCompleted = delegate { };
        public event ModelChangedEventHandler _modelSelectedShape = delegate { };
        public event ModelChangedEventHandler _modelMovingShapes = delegate { };

        public Model() { }
        public List<Shape> Shapes
        {
            get { return _shapes; }
        }

        public HashSet<int> SelectedShapeIndexs
        {
            get { return _selectedShapeIndexs; }
        }

        public int ShapesSize
        {
            get { return _shapes.Count(); }
        }

        public ShapeType HintType
        {
            set { _hintType = value; }
            get { return _hintType; }
        }

        public void ClearSelectedShapes() { _selectedShapeIndexs.Clear(); }

        public void AddShape(in ShapeType shapeType, in string[] inputDatas)
        {
            Shape shape = _shapeFactory.CreateShape(shapeType, inputDatas);
            _shapes.Add(shape);
            _modelAddedShape();
        }

        public void RemoveShape(in int index)
        {
            _shapes.RemoveAt(index);
            _modelRemovedShape();
        }

        public void SelectShape(in int x, in int y)
        {
            for (int i = 0; i < _shapes.Count(); i++)
            {
                Shape shape = _shapes[i];
                bool isXInRange = (x >= shape.X) && (x <= shape.X + shape.Width);
                bool isYInRange = (y >= shape.Y) && (y <= shape.Y + shape.Height);
                if (isXInRange && isYInRange)
                    _selectedShapeIndexs.Add(i);
            }
            _modelSelectedShape();
        }

        public void MoveSelectedShapes(in int dx, in int dy)
        {
            foreach (int index in _selectedShapeIndexs)
            {
                _shapes[index].X += dx;
                _shapes[index].Y += dy;
            }
            _modelMovingShapes();
        }

        public void SelectFirstPoint(in int x, in int y)
        {
            _hint = _shapeFactory.CreateShape(_hintType, new string[] { new Random().Next().ToString(), x.ToString(), y.ToString(), "1", "1" });
        }

        public void SelectSecondPoint(in int x, int y)
        {
            if (_hint == null)
                return;
            _hint.Width = Math.Abs(x - _hint.X);
            _hint.Height = Math.Abs(y - _hint.Y);
            _modelDrawing();
        }

        public void SelectPointCompleted()
        {
            _shapes.Add(_hint);
            _modelAddedShape();
            _modelDrawingCompleted();
        }

        public void DrawAll(in IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape shape in _shapes)
            {
                ((IDrawable)shape).Draw(graphics);
                shape.DrawText(graphics);
            }
        }

        public void DrawOne(in IGraphics graphics)
        {
            graphics.ClearAll();
            ((IDrawable)_hint).Draw(graphics);
        }

        public void DrawFrames(in IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (int index in _selectedShapeIndexs)
            {
                _shapes[index].DrawFrame(graphics);
            }
        }
    }
}
