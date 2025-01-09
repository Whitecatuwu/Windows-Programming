

namespace DrawingShape
{
    public class Line : IDrawable
    {
        int _firstX = 0;
        int _firstY = 0;
        int _secondX = 0;
        int _secondY = 0;
        //bool _isConnected = false;

        ConnectionPoint _connectedFirstPoint = null;
        ConnectionPoint _connectedSecondPoint = null;
        /*public bool IsConnected
        {
            get { return _connectedFirstPoint != null && _connectedSecondPoint != null; }
        }*/
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

        public Shape ConnectedShape(ConnectionPoint point)
        {
            var anotherPoint = 
                (_connectedFirstPoint.CompareTo(point) == 0) ? _connectedSecondPoint : _connectedFirstPoint;
            return anotherPoint.ParantShape;
        }

        //public Line() { }
        public void Draw(DrawingModel.IGraphics graphics)
        {
            graphics.DrawLine(_firstX, _firstY, _secondX, _secondY);
        }
        public bool IsPointInRange(int x, int y)
        {
            return false;
        }
    }
}