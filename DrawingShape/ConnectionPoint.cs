using System;
using System.Collections.Generic;

namespace DrawingShape
{
    public class ConnectionPoint : IDrawable, IComparable<ConnectionPoint>
    {
        int _x;
        int _y;
        int _r;
        Shape _shape;
        HashSet<Line> _connectedLines = new HashSet<Line>();

        public delegate void ConnectionPointChangedEventHandler();
        public event ConnectionPointChangedEventHandler _pointChangeEvent = delegate { };
        public event ConnectionPointChangedEventHandler _pointRemovedEvent = delegate { };

        public Shape ParantShape
        {
            get { return _shape; }
        }
        public IReadOnlyCollection<Line> ConnectedLines
        {
            get { return _connectedLines; }
        }
        public int X
        {
            set
            {
                _x = value;
                _pointChangeEvent();
            }
            get { return _x; }
        }
        public int Y
        {
            set
            {
                _y = value;
                _pointChangeEvent();
            }
            get { return _y; }
        }

        public ConnectionPoint(Shape shape, int x, int y, int r)
        {
            _shape = shape;
            this._x = x;
            this._y = y;
            this._r = r;
        }
        ~ConnectionPoint()
        {
            _pointRemovedEvent();
            this._connectedLines.Clear();   
        }

        public void Draw(DrawingModel.IGraphics graphics)
        {
            graphics.DrawPoint(_x, _y, _r);
        }

        public void DrawLines(List<Shape> shapes, DrawingModel.IGraphics graphics)
        {
            foreach (Line line in _connectedLines)
            {
                if (shapes.IndexOf(line.ConnectedFirstPoint?._shape) == -1) return;
                if (shapes.IndexOf(line.ConnectedSecondPoint?._shape) == -1) return;
                line.Draw(graphics);
            }
        }

        public bool IsPointInRange(int x, int y)
        {
            ref int cx = ref _x;
            ref int cy = ref _y;
            ref int r = ref _r;
            return (x - cx) * (x - cx) + (y - cy) * (y - cy) <= r * r;
        }

        public void AddConnectionLine(Line line)
        {
            this._connectedLines.Add(line);
            _pointChangeEvent();
        }
        public void RemoveConnectionLine(Line line)
        {
            this._connectedLines.Remove(line);
            _pointChangeEvent();
        }

        public int CompareTo(ConnectionPoint other)
        {
            if (System.Object.ReferenceEquals(this, other)) return 0;
            if (other == null) return 1;
            return -1;
        }
    }
}
