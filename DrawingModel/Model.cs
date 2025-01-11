using System.Collections.Generic;
using System.Linq;
using DrawingShape;
using DrawingState;
using DrawingCommand;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;


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
        public event ModelChangedEventHandler _saveEvent = delegate { };
        public event ModelChangedEventHandler _saveFailedEvent = delegate { };
        public event ModelChangedEventHandler _autoSaveEvent = delegate { };
        public event ModelChangedEventHandler _autoSaveFinishEvent = delegate { };
        public event ModelChangedEventHandler _loadEvent = delegate { };

        ShapeFactory _shapeFactory = new ShapeFactory();
        CommandManager _commandManager = new CommandManager();

        List<Shape> _shapes = new List<Shape>();

        PointerState _pointerState;
        DrawingState.DrawingState _drawingState;
        LineState _lineState;
        IState _currentState;

        int _removedShapeIndex = -1;
        int _updatedShapeIndex = -1;
        int _insertedShapeIndex = -1;
        int _textEditShapeIndex = -1;

        bool _saving = false;
        bool _changed = false;

        const int MAX_BACKUP_COUNT = 5;

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

        public bool Changed
        {
            set { _changed = value; }
            get { return _changed; }
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

        public bool IsSaveEnabled
        {
            get { return !_saving; }
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

        async public void Save(string path)
        {
            this._saving = true;
            _saveEvent();

            Task<bool> task = Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    File.WriteAllText(path, GetContent());
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            bool saveSuccess = await task;
            this._saving = false;

            if (saveSuccess) _saveEvent();
            else _saveFailedEvent();
        }

        async public void AutoSave()
        {
            if (!_changed) return;
            _changed = false;
            _autoSaveEvent();

            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var time = DateTime.Now.ToString("yyyyMMddHHmmss");
            var targetFolderPath = Path.Combine(solutionPath, "DrawingForm", "bin", "Debug", "drawing_backup");         

            if (!Directory.Exists(targetFolderPath))
            {
                try { Directory.CreateDirectory(targetFolderPath); }
                catch (Exception) { return; }
            }
            
            var targetPath = Path.Combine(targetFolderPath, $"{time}_bak.mydrawing");     

            Task<bool> task = Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    File.WriteAllText(targetPath, GetContent());
                    DeleteOldestBackup(targetFolderPath);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            bool saveSuccess = await task;
            _autoSaveFinishEvent();
        }

        public void Load(string path)
        {
            _pointerState.ClearSelectedShapes(false);
            _shapes.Clear();
            _commandManager.Reload();
            _loadEvent();

            Thread.Sleep(TimeSpan.FromSeconds(3));

            string contents = File.ReadAllText(path);
            List<string[]> connectionInfos = new List<string[]> { };
            foreach (var content in contents.Split('\n'))
            {
                if (content == "") continue;
                var result = AnalyzeShapeData(content);
                ShapeType shapeType = (ShapeType)int.Parse(result[0]);
                string shapeId = result[1];
                string[] shapeData = new string[]
                {
                    result[2], // text
                    result[3], // x
                    result[4], // y
                    result[5], // h
                    result[6], // w
                };
                var shape = _shapeFactory.CreateShape(shapeType, shapeData);
                shape.Id = shapeId;
                AddShape(shape);
                connectionInfos.Add(new string[]
                {
                    result[7],
                    result[8],
                    result[9],
                    result[10],
                });
            }
            for (int i = 0; i < connectionInfos.Count; i++)
            {
                var points = _shapes[i].ConnectionPoints;
                var connectionInfo = connectionInfos[i];
                for (int j = 0; j < points.Length; j++)
                {
                    if (connectionInfo[j] == "") continue;
                    foreach (var line in connectionInfo[j].Split(','))
                    {
                        if (line == "") continue;
                        string targetID = line.Split('_')[0].Trim();
                        int anotherPointSeq = int.Parse(line.Split('_')[1].Trim());
                        var targetShape = _shapes.Find(s => s.Id == targetID);
                        var anotherPoint = targetShape.ConnectionPoints[anotherPointSeq];
                        AddLine(points[j], anotherPoint, new Line());
                    }
                }
            }
        }

        private string GetContent()
        {
            string content = "";
            foreach (var shape in _shapes)
            {
                content += $"{(int)shape.ShapeType}, {shape.Id}, {shape.Text}, {shape.X}, {shape.Y}, {shape.Height}, {shape.Width}";
                foreach (var point in shape.ConnectionPoints)
                {
                    string connectedShapeIds = "";
                    foreach (var line in point.ConnectedLines)
                    {
                        var anotherPoint = line.AnotherPoint(point);
                        var connectedShape = anotherPoint.ParantShape;
                        if (_shapes.IndexOf(connectedShape) == -1) continue;
                        if (connectedShape.Id == shape.Id) continue;

                        connectedShapeIds += $"{connectedShape.Id}_{anotherPoint.Seq}, ";
                    }
                    content += $", [{connectedShapeIds}]";
                }
                content += "\n";
            }
            return content;
        }

        private List<string> AnalyzeShapeData(string input)
        {
            var regex = new Regex(@"([^,\[\]\s]+)(?=\s*,|\s*\[)|\[(.*?)\]");
            MatchCollection matches = regex.Matches(input);
            List<string> result = new List<string>();

            foreach (Match match in matches)
            {
                if (match.Value.StartsWith("["))
                {
                    result.Add(match.Groups[2].Value.Trim());
                }
                else
                {
                    result.Add(match.Value.Trim());
                }
            }
            return result;
        }
        private void DeleteOldestBackup(string backUpDirPath)
        {
            if (!Directory.Exists(backUpDirPath)) { return; }

            string[] files = Directory.GetFiles(backUpDirPath);
            if (files.Length <= MAX_BACKUP_COUNT) { return; }

            var oldestFile = files
                .Select(file => new FileInfo(file))  // 轉換為 FileInfo 對象
                .OrderBy(fileInfo => fileInfo.LastWriteTime)  // 按照 LastWriteTime 排序
                .First();

            try
            {
                File.Delete(oldestFile.FullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"刪除{oldestFile.FullName}時發生錯誤: {ex.Message}");
            }
        }
    }
}
