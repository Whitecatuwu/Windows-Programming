

namespace DrawingShape
{
    public class Line : IDrawable
    {
        int _firstX = 0;
        int _firstY = 0;
        int _secondX = 0;
        int _secondY = 0;

        Shape _connectedFirstShape = null;
        int _connectedFirstShapePointNumber = -1;
        Shape _connectedSecondShape = null;
        int _connectedSecondShapePointNumber = -1;

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
        public Shape ConnectedFirstShape
        {
            set { _connectedFirstShape = value; }
            get { return _connectedFirstShape; }
        }

        public Shape ConnectedSecondShape
        {
            set { _connectedSecondShape = value; }
            get { return _connectedSecondShape; }
        }
        public int ConnectedFirstShapePointNumber
        {
            set { _connectedFirstShapePointNumber = value; }
            get { return _connectedFirstShapePointNumber; }
        }
        public int ConnectedSecondShapePointNumber
        {
            set { _connectedSecondShapePointNumber = value; }
            get { return _connectedSecondShapePointNumber; }
        }


        public Line() { }
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