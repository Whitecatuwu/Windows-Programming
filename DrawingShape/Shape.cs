using DrawingModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        /*
          如果需要生成大量隨機字符串，應避免每次調用時都初始化新的 Random 對象，
          因為其種子值基於時間，可能會導致生成的字符串重複。可以將 Random 設置為靜態字段
         */
        private static readonly Random _random = new Random();
        public Shape(ShapeType shapeType, string[] shapeDatas)
        {
            _shapeType = shapeType;
            _id = _random.Next().ToString();
            _text = shapeDatas[0];
            _x = int.Parse(shapeDatas[1]);
            _y = int.Parse(shapeDatas[2]);
            _height = int.Parse(shapeDatas[3]);
            _width = int.Parse(shapeDatas[4]);

            _textBox_X = _x + _width / 2;
            _textBox_Y = _y + _height / 2;
            _textBox_H = 15;
            _textBox_W = 120;

            _connectionPoints = new ConnectionPoint[4]
            {
                new ConnectionPoint(this, _x + _width/2, _y, POINT_RADIUS),// up
                new ConnectionPoint(this, _x + _width, _y + _height/2, POINT_RADIUS), // right
                new ConnectionPoint(this, _x + _width/2, _y + _height, POINT_RADIUS), // down
                new ConnectionPoint(this, _x, _y + _height/2, POINT_RADIUS) // left
            };
            Abjust();
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

        protected bool _touched;

        protected ConnectionPoint[] _connectionPoints;// up, right, down, left

        const int POINT_RADIUS = 6;
        public ConnectionPoint[] ConnectionPoints
        {
            get { return _connectionPoints; }
        }
        public ShapeType ShapeType
        {
            get { return _shapeType; }
        }
        public string Text
        {
            set { _text = value; }
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
                TextBox_X += value - _x;
                foreach (ConnectionPoint connectionPoint in _connectionPoints)
                {
                    connectionPoint.X += value - _x;
                }
                _x = value;
            }
            get { return _x; }
        }
        public int Y
        {
            set
            {
                TextBox_Y += value - _y;
                foreach (ConnectionPoint connectionPoint in _connectionPoints)
                {
                    connectionPoint.Y += value - _y;
                }
                _y = value;
            }
            get { return _y; }
        }
        public int Height
        {
            set
            {
                _height = value;
                Abjust();
            }
            get { return _height; }
        }
        public int Width
        {
            set
            {
                _width = value;
                Abjust();
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
        public bool Touched
        {
            set { _touched = value; }
            get { return _touched; }
        }

        public void DrawFrame(IGraphics graphics)
        {
            graphics.DrawFrame(ShapeDatas);
        }

        public void DrawText(IGraphics graphics)
        {
            graphics.DrawText(new int[] { _textBox_X, _textBox_Y, _textBox_H, _textBox_W }, _text);
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
            graphics.DrawFrame(new int[] { _textBox_X - _textBox_W / 2, _textBox_Y, _textBox_H, _textBox_W });
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

        public void DrawConnectionPoints(IGraphics graphics)
        {
            if (!_touched) return;
            foreach (var point in _connectionPoints)
            {
                point.Draw(graphics);
            }
        }

        public ConnectionPoint TouchConnectPoint(int x, int y)
        {
            foreach (var point in _connectionPoints)
            {
                if (point.IsPointInRange(x, y))
                {
                    return point;
                }
            }
            return null;
        }

        /*public void SetConnectLine(in int number,Line)
        {
            _connectedLines[number]
        }*/

        private void Abjust()
        {
            _textBox_X = _x + _width / 2;
            _textBox_Y = _y + _height / 2;
            /*_textBox_H = 15;
            _textBox_W = 120;*/

            _textBoxMovePoint_X = _textBox_X;
            _textBoxMovePoint_Y = _textBox_Y - _textBox_H / 2;

            int[] x = new int[4]
            {
                _x + _width/2,
                _x + _width,
                _x + _width/2,
                _x
            };
            int[] y = new int[4]
            {
                _y,
                _y + _height/2,
                _y + _height,
                _y + _height/2
            };
            for (int i = 0; i < _connectionPoints.Length; i++)
            {
                _connectionPoints[i].X = x[i];
                _connectionPoints[i].Y = y[i];
            }
        }
    }
}


