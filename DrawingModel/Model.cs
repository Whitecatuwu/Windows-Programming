using System.Collections.Generic;
using System.Linq;
using DrawingShape;
using DrawingState;
using DrawingCommand;
using System.Runtime.InteropServices.ComTypes;
using System;


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
        public event ModelChangedEventHandler _selectingFailedEvent = delegate { };
        public event ModelChangedEventHandler _editShapeTextEvent = delegate { };
        public event ModelChangedEventHandler _shapeTextEditedEvent = delegate { };
        public event ModelChangedEventHandler _insertedShapeEvent = delegate { };
        public event ModelChangedEventHandler _commandExecutedEvent = delegate { };
        public event ModelChangedEventHandler _addedLineEvent = delegate { };
        public event ModelChangedEventHandler _removedLineEvent = delegate { };
        public event ModelChangedEventHandler _touchedShapeEvent = delegate { };

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
        int _textEditShapeIndex = -1;

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
            _drawingState._selectingFailedEvent += delegate { _selectingFailedEvent(); };

            _lineState._selectingEvent += delegate { _selectingEvent(); };
            _lineState._selectingCompletedEvent += delegate { _selectingCompletedEvent(); };
            _lineState._touchShapeEvent += delegate { _touchedShapeEvent(); };
            _lineState._selectingFailedEvent += delegate { _selectingFailedEvent(); };

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

        public int TextEditShapeIndex
        {
            set { _textEditShapeIndex = value; }
            get { return _textEditShapeIndex; }
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
                foreach (var point in shape.ConnectionPoints)
                {
                    point.DrawLines(_shapes, g);
                }
            }
            /*foreach (Line line in _lines)
            {
                ((IDrawable)line).Draw(g);
            }*/
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

        public void MoveShape(in Shape shape, in int x, in int y)
        {
            int index = _shapes.IndexOf(shape);
            if (index < 0) return;
            shape.X = x;
            shape.Y = y;
            UpdatedShapeIndex = index;
            _movedShapesEvent();
        }

        public void MoveTextBox(in Shape shape, in int x, in int y)
        {
            int index = _shapes.IndexOf(shape);
            if (index < 0) return;
            shape.TextBox_X = x;
            shape.TextBox_Y = y;
            UpdatedShapeIndex = index;
            _movedShapesEvent();
        }

        public void AddLine(in ConnectionPoint first, in ConnectionPoint second, in Line line)
        {
            line.ConnectedFirstPoint = first;
            line.ConnectedSecondPoint = second;
            _addedLineEvent();
        }

        public void RemoveLine(in Line line)
        {
            line.Disconnect();
            _removedLineEvent();
        }

        public void EditShapeText(in string text)
        {
            if (_textEditShapeIndex < 0) return;
            _shapes[_textEditShapeIndex].Text = text;
            _updatedShapeIndex = _textEditShapeIndex;
            _shapeTextEditedEvent();
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

        public void Save()
        {
            foreach (var shape in _shapes)
            {
                Console.WriteLine
                    ($"id:{shape.Id}, text:{shape.Text}, x:{shape.X}, y:{shape.Y}, h:{shape.Height}, w:{shape.Width}");
                foreach (var point in shape.ConnectionPoints)
                {
                    foreach (var line in point.ConnectedLines)
                    {
                        if (_shapes.IndexOf(line.ConnectedShape(point)) == -1) continue;
                        if (line.ConnectedShape(point).Id == shape.Id)
                        {
                            Console.WriteLine
                                ($"point:{line.ConnectedShape(point).Id}");
                        }
                        else
                        {
                            Console.WriteLine
                                ($"point:{line.ConnectedShape(point).Id}");
                        }

                    }
                }
            }
        }
    }
}
