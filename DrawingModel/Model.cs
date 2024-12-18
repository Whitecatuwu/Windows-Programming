using System.Collections.Generic;
using System.Linq;
using DrawingShape;
using DrawingState;
using DrawingCommand;


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
        public event ModelChangedEventHandler _editShapeTextEvent = delegate { };
        public event ModelChangedEventHandler _insertedShapeEvent = delegate { };
        public event ModelChangedEventHandler _commandExecutedEvent = delegate { };

       ShapeFactory _shapeFactory = new ShapeFactory();
        CommandManager _commandManager = new CommandManager();

        List<Shape> _shapes = new List<Shape>();
        List<Line> _lines = new List<Line>();

        PointerState _pointerState;
        DrawingState.DrawingState _drawingState;
        LineState _lineState;
        IState _currentState;

        int _removedShapeIndex = -1;
        int _updatedShapeIndex = -1;
        int _insertedShapeIndex = -1;

        public Model()
        {
            _pointerState = new PointerState();
            _drawingState = new DrawingState.DrawingState(_pointerState);
            _lineState = new LineState();

            _pointerState._selectedShapeEvent += delegate { _selectedShapeEvent(); };
            _pointerState._movingShapesEvent += delegate { _movingShapesEvent(); };
            _pointerState._movedShapesEvent += delegate { _movedShapesEvent(); };
            _pointerState._editShapeTextEvent += delegate { _editShapeTextEvent(); };

            _drawingState._selectingEvent += delegate { _selectingEvent(); };
            _drawingState._selectingCompletedEvent += delegate { _selectingCompletedEvent(); };

            _lineState._selectingEvent += delegate { _selectingEvent(); };
            _lineState._selectingCompletedEvent += delegate { _selectingCompletedEvent(); };

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

        public int InsertedShapeIndex
        {
            get { return _insertedShapeIndex; }
        }

        public bool IsRedoEnabled
        {
            get { return _commandManager.IsRedoEnabled; }
        }

        public bool IsUndoEnabled
        {
            get { return _commandManager.IsUndoEnabled; }
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

        public void EnterLineState()
        {
            _lineState.Initialize(this);
            _currentState = _lineState;
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
        public void MouseDoubleClick(int x, int y)
        {
            _currentState.MouseDoubleClick(this, x, y);
        }

        public void OnPaint(IGraphics g)
        {
            g.ClearAll();
            foreach (Shape shape in _shapes)
            {
                ((IDrawable)shape).Draw(g);
                shape.DrawText(g);
                shape.DrawTextBoxFrame(g);
                shape.DrawTextBoxMovePoint(g);
            }
            foreach (Line line in _lines)
            {
                ((IDrawable)line).Draw(g);
            }
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

        public void RemoveShape(Shape shape)
        {
            int index = _shapes.IndexOf(shape);
            if (index >= 0)
            {
                RemoveShape(index);
            }
        }

        public void InsertShape(in int index, in ShapeType shapeType, in string[] inputDatas)
        {
            Shape shape = _shapeFactory.CreateShape(shapeType, inputDatas);
            _shapes.Insert(index, shape);
            _insertedShapeIndex = index;
            _insertedShapeEvent();
        }

        public void InsertShape(in int index, Shape shape)
        {
            _shapes.Insert(index, shape);
            _insertedShapeIndex = index;
            _insertedShapeEvent();
        }

        public void AddLine(in Line line)
        {
            _lines.Add(line);
        }

        public void ExeCommand(ICommand cmd)
        {
            _commandManager.Execute(cmd);
            _commandExecutedEvent();
        }

        public void Redo()
        {
            _commandManager.Redo();
            _commandExecutedEvent();
        }

        public void Undo()
        {
            _commandManager.Undo();
            _commandExecutedEvent();
        }
    }
}
