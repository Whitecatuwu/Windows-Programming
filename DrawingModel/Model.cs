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
        private List<Shape> _selectedShapes = new List<Shape>();

        ShapeType _hintType = ShapeType.NULL;
        Shape _hint;

        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _modelAddedShape = delegate { };
        public event ModelChangedEventHandler _modelRemovedShape = delegate { };
        public event ModelChangedEventHandler _modelDrawing = delegate { };
        public event ModelChangedEventHandler _modelDrawingCompleted = delegate { };

        public Model() { }

        public List<Shape> Shapes { get { return _shapes; } }

        public List<Shape> SelectedShapes { get { return _selectedShapes; } }

        public ShapeType HintType
        {
            set { _hintType = value; }
            get { return _hintType; }
        }

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
            //
        }

        public void SelectFirstPoint(in int x, in int y)
        {
            _hint = _shapeFactory.CreateShape(_hintType, new string[] { new Random().Next().ToString(), x.ToString(), y.ToString(), "1", "1" });    
        }

        public void SelectSecondPoint(in int x,int y)
        {
            _hint.Width = Math.Abs(x - _hint.X);
            _hint.Height = Math.Abs(y - _hint.Y);
            _modelDrawing();
        }

        public void SelectPointCompleted(in int x, in int y) 
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
            foreach (Shape selectedShape in _selectedShapes)
            {
                selectedShape.DrawFrame(graphics);
            }
        }
    }
}
