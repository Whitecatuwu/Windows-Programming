using System.Collections.Generic;
using System.Linq;
using DrawingShape;
using DrawingState;


namespace DrawingModel
{
    public class Model //: INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler AddedShapeEvent = delegate { };
        public event ModelChangedEventHandler RemovedShapeEvent = delegate { };
        public event ModelChangedEventHandler MovingShapesEvent = delegate { };
        public event ModelChangedEventHandler MovedShapesEvent = delegate { };
        public event ModelChangedEventHandler SelectedShapeEvent = delegate { };
        public event ModelChangedEventHandler SelectingCompletedEvent = delegate { };
        public event ModelChangedEventHandler SelectingEvent = delegate { };

        ShapeFactory _shapeFactory = new ShapeFactory();
        List<Shape> _shapes = new List<Shape>();

        PointerState _pointerState;
        DrawingState.DrawingState _drawingState;
        IState _currentState;

        int _removedShapeIndex;
        int _updatedShapeIndex;

        public Model()
        {
            _pointerState = new PointerState();
            _drawingState = new DrawingState.DrawingState(_pointerState);

            _pointerState.SelectedShapeEvent += delegate { SelectedShapeEvent(); };
            _pointerState.MovingShapesEvent += delegate { MovingShapesEvent(); };
            _pointerState.MovedShapesEvent += delegate { MovedShapesEvent(); };

            _drawingState.SelectingEvent += delegate { SelectingEvent(); };
            _drawingState.SelectingCompletedEvent += delegate { SelectingCompletedEvent(); };

            EnterPointerState();
        }

        public IList<Shape> Shapes
        {
            get { return _shapes.AsReadOnly(); }
        }

        public int ShapesSize
        {
            get { return _shapes.Count(); }
        }

        public int RemovedShapeIndex
        {
            get { return _removedShapeIndex; }
        }

        public int UpdatedShapeIndex
        {
            set { _updatedShapeIndex = value; }
            get { return _updatedShapeIndex; }
        }

        public void EnterPointerState()
        {
            _pointerState.Initialize(this);
            _currentState = _pointerState;
        }

        public void EnterDrawingState(ShapeType shapeType)
        {
            _drawingState.Initialize(this);
            _drawingState.HintShapeType = shapeType;
            _currentState = _drawingState;
        }

        public void MouseDown(int x, int y)
        {
            _currentState.MouseDown(this, x, y);
        }

        public void MouseMove(int x, int y)
        {
            _currentState.MouseMove(this, x, y);
        }

        public void MouseUp(int x, int y)
        {
            _currentState.MouseUp(this, x, y);
        }

        public void OnPaint(IGraphics g)
        {
            _currentState.OnPaint(this, g);
        }

        public void AddShape(in ShapeType shapeType, in string[] inputDatas)
        {
            Shape shape = _shapeFactory.CreateShape(shapeType, inputDatas);
            _shapes.Add(shape);
            AddedShapeEvent();
        }

        public void AddShape(in Shape shape)
        {
            _shapes.Add(shape);
            AddedShapeEvent();
        }

        public void RemoveShape(in int index)
        {
            _pointerState.RemoveSelectedShape(_shapes[index]);
            _shapes.RemoveAt(index);
            _removedShapeIndex = index;
            RemovedShapeEvent();
        }
    }
}
