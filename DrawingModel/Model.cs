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
        public event ModelChangedEventHandler _addedShapeEvent = delegate { };
        public event ModelChangedEventHandler _removedShapeEvent = delegate { };
        public event ModelChangedEventHandler _movingShapesEvent = delegate { };
        public event ModelChangedEventHandler _movedShapesEvent = delegate { };
        public event ModelChangedEventHandler _selectedShapeEvent = delegate { };
        public event ModelChangedEventHandler _selectingCompletedEvent = delegate { };
        public event ModelChangedEventHandler _selectingEvent = delegate { };

        ShapeFactory _shapeFactory = new ShapeFactory();
        List<Shape> _shapes = new List<Shape>();

        PointerState _pointerState;
        DrawingState.DrawingState _drawingState;
        IState _currentState;

        int _removedShapeIndex = -1;
        int _updatedShapeIndex = -1;

        public Model()
        {
            _pointerState = new PointerState();
            _drawingState = new DrawingState.DrawingState(_pointerState);

            _pointerState._selectedShapeEvent += delegate { _selectedShapeEvent(); };
            _pointerState._movingShapesEvent += delegate { _movingShapesEvent(); };
            _pointerState._movedShapesEvent += delegate { _movedShapesEvent(); };

            _drawingState._selectingEvent += delegate { _selectingEvent(); };
            _drawingState._selectingCompletedEvent += delegate { _selectingCompletedEvent(); };

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
            _addedShapeEvent();
        }

        public void AddShape(in Shape shape)
        {
            _shapes.Add(shape);
            _addedShapeEvent();
        }

        public void RemoveShape(in int index)
        {
            _pointerState.RemoveSelectedShape(_shapes[index]);
            _shapes.RemoveAt(index);
            _removedShapeIndex = index;
            _removedShapeEvent();
        }
    }
}
