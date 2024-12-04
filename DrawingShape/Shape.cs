using DrawingModel;
using System;

namespace DrawingShape
{
    public enum ShapeType
    {
        NULL = -1,
        START = 0,
        TERMINATOR = 1,
        PROCESS = 2,
        DECISION = 3
    }
    abstract public class Shape : IComparable<Shape>
    {
        public Shape(ShapeType shapeType, string[] shapeDatas)
        {
            _shapeType = shapeType;
            _id = new Random().Next().ToString();
            _text = shapeDatas[0];
            _x = int.Parse(shapeDatas[1]);
            _y = int.Parse(shapeDatas[2]);
            _height = int.Parse(shapeDatas[3]);
            _width = int.Parse(shapeDatas[4]);

            _textBox_X = _x + _width / 2;
            _textBox_Y = _y + _height / 2;
            _textBox_H = 15;
            _textBox_W = 120;

            _textBoxMovePoint_X = _textBox_X;
            _textBoxMovePoint_Y = _textBox_Y - _textBox_H / 2;
        }

        protected ShapeType _shapeType;
        protected string _text;
        protected string _id;
        protected int _x;
        protected int _y;
        protected int _height;
        protected int _width;

        protected int _textBox_X;
        protected int _textBox_Y;
        protected int _textBox_H;
        protected int _textBox_W;
        protected int _textBoxMovePoint_X;
        protected int _textBoxMovePoint_Y;

        const int POINT_RADIUS = 10;

        public ShapeType ShapeType
        {
            get { return _shapeType; }
        }
        public string Text
        {
            get { return _text; }
        }
        public string Id
        {
            get { return _id; }
        }
        public int X
        {
            set
            {
                _textBox_X += value - _x;
                _textBoxMovePoint_X += value - _x;
                _x = value;
            }
            get { return _x; }
        }
        public int Y
        {
            set
            {
                _textBox_Y += value - _y;
                _textBoxMovePoint_Y += value - _y;
                _y = value;
            }
            get { return _y; }
        }
        public int Height
        {
            set
            {
                _height = value;
                TextBox_Y = _y + _height / 2;
                _textBoxMovePoint_Y = _textBox_Y - _textBox_H / 2;
            }
            get { return _height; }
        }
        public int Width
        {
            set
            {
                _width = value;
                TextBox_X = _x + _width / 2;
                _textBoxMovePoint_X = _textBox_X;
            }
            get { return _width; }
        }

        public int TextBox_X
        {
            set
            {
                _textBoxMovePoint_X += value - _textBox_X;
                _textBox_X = value;
            }
            get { return _textBox_X; }
        }

        public int TextBox_Y
        {
            set
            {
                _textBoxMovePoint_Y += value - _textBox_Y;
                _textBox_Y = value;
            }
            get { return _textBox_Y; }
        }

        public int[] ShapeDatas
        {
            get { return new int[] { _x, _y, _height, _width }; }
        }

        public void DrawFrame(IGraphics graphics)
        {
            graphics.DrawFrame(ShapeDatas);
        }

        public void DrawText(IGraphics graphics)
        {
            graphics.DrawText(new int[] { _textBox_X , _textBox_Y , _textBox_H, _textBox_W }, _text);
        }

        public void Normalize()
        {
            if (_width < 0)
            {
                _x += _width;
                _width = -_width;
            }
            if (_height < 0)
            {
                _y += _height;
                _height = -_height;
            }

            if (_x < 0) _x = 0;
            if (_y < 0) _y = 0;
        }

        public int CompareTo(Shape other)
        {
            if (System.Object.ReferenceEquals(this, other)) return 0;
            if (other == null) return 1;
            return -1;
        }

        public void DrawTextBoxFrame(IGraphics graphics)
        {
            graphics.DrawFrame(new int[] { _textBox_X - _textBox_W / 2, _textBox_Y , _textBox_H, _textBox_W });
        }

        public bool IsTouchMovePoint(int x, int y)
        {

            ref int cx = ref _textBoxMovePoint_X;
            ref int cy = ref _textBoxMovePoint_Y;
            return (x - cx) * (x - cx) + (y - cy) * (y - cy) <= POINT_RADIUS * POINT_RADIUS;
        }

        public void DrawTextBoxMovePoint(IGraphics graphics)
        {
            graphics.DrawPoint(_textBoxMovePoint_X, _textBoxMovePoint_Y, POINT_RADIUS);
        }
    }
}


