

using System;

namespace DrawingShape
{
    public class Line : IDrawable, IComparable<Line>
    {
        int _firstX = 0;
        int _firstY = 0;
        int _secondX = 0;
        int _secondY = 0;

        ConnectionPoint _connectedFirstPoint = null;
        ConnectionPoint _connectedSecondPoint = null;
        public int FirstX
        {
            set { _firstX = value; }
            get { return _firstX; }
        }
        public int FirstY
        {
            set { _firstY = value; }
            get { return _firstY; }
        }
        public int SecondX
        {
            set { _secondX = value; }
            get { return _secondX; }
        }
        public int SecondY
        {
            set { _secondY = value; }
            get { return _secondY; }
        }
        public ConnectionPoint ConnectedFirstPoint
        {
            set
            {
                _connectedFirstPoint = value;
                FirstX = _connectedFirstPoint.X;
                FirstY = _connectedFirstPoint.Y;
                _connectedFirstPoint._pointChangeEvent += delegate
                {
                    FirstX = _connectedFirstPoint.X;
                    FirstY = _connectedFirstPoint.Y;
                };
                _connectedFirstPoint._pointRemovedEvent += delegate
                {
                    _connectedSecondPoint?.RemoveConnectionLine(this);
                };
                value.AddConnectionLine(this);
            }
            get { return _connectedFirstPoint; }
        }

        public ConnectionPoint ConnectedSecondPoint
        {
            set
            {
                _connectedSecondPoint = value;
                SecondX = _connectedSecondPoint.X;
                SecondY = _connectedSecondPoint.Y;
                _connectedSecondPoint._pointChangeEvent += delegate
                {
                    SecondX = _connectedSecondPoint.X;
                    SecondY = _connectedSecondPoint.Y;
                };
                _connectedSecondPoint._pointRemovedEvent += delegate
                {
                    _connectedFirstPoint?.RemoveConnectionLine(this);
                };
                value.AddConnectionLine(this);
            }
            get { return _connectedSecondPoint; }
        }
        public void Disconnect()
        {
            _connectedFirstPoint.RemoveConnectionLine(this);
            _connectedFirstPoint = null;
            _connectedSecondPoint.RemoveConnectionLine(this);
            _connectedSecondPoint = null;
        }

        public ConnectionPoint AnotherPoint(ConnectionPoint point)
        {
            if(_connectedFirstPoint == null) return null;
            var anotherPoint =
                (_connectedFirstPoint.CompareTo(point) == 0) ? _connectedSecondPoint : _connectedFirstPoint;
            return anotherPoint;
        }

        public void Draw(DrawingModel.IGraphics graphics)
        {
            graphics.DrawLine(_firstX, _firstY, _secondX, _secondY);
        }
        public bool IsPointInRange(int x, int y)
        {
            return false;
        }

        public int CompareTo(Line other)
        {
            if (System.Object.ReferenceEquals(this, other)) return 0;
            if (other == null) return 1;
            if (this.ConnectedFirstPoint == null) return -1;
            if (this.ConnectedSecondPoint == null) return -1;
            bool sameFirst =
                this.ConnectedFirstPoint.CompareTo(other.ConnectedFirstPoint) == 0 ||
                this.ConnectedFirstPoint.CompareTo(other.ConnectedSecondPoint) == 0;
            bool sameSecond =
                this.ConnectedSecondPoint.CompareTo(other.ConnectedFirstPoint) == 0 ||
                this.ConnectedSecondPoint.CompareTo(other.ConnectedSecondPoint) == 0;
            if (sameFirst && sameSecond) return 0;           
            return -1;
        }
    }
}