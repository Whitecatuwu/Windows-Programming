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

        protected int[] _connectionPointXs = new int[4]; // up, right, down, left
        protected int[] _connectionPointYs = new int[4]; // up, right, down, left
        protected bool _touched = false;

        //protected Line[] _connectedLines = new Line[4];

        const int POINT_RADIUS = 10;

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
                _x = value;
                Abjust();
            }
            get { return _x; }
        }
        public int Y
        {
            set
            {
                TextBox_Y += value - _y;
                _y = value;
                Abjust();
            }
            get { return _y; }
        }
        public int Height
        {
            set
            {
                _height = value;
                TextBox_Y = _y + _height / 2;
                Abjust();
            }
            get { return _height; }
        }
        public int Width
        {
            set
            {
                _width = value;
                TextBox_X = _x + _width / 2;
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
            for (int i = 0; i < 4; i++)
            {
                graphics.DrawPoint(_connectionPointXs[i], _connectionPointYs[i], POINT_RADIUS);
            }
        }
        public int TouchConnectPoint(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                ref int cx = ref _connectionPointXs[i];
                ref int cy = ref _connectionPointYs[i];
                if ((x - cx) * (x - cx) + (y - cy) * (y - cy) <= POINT_RADIUS * POINT_RADIUS)
                {
                    return i;
                }
            }
            return -1;
        }

        /*public void SetConnectLine(in int number,Line)
        {
            _connectedLines[number]
        }*/

        private void Abjust()
        {
            /*_textBox_X = _x + _width / 2;
            _textBox_Y = _y + _height / 2;
            _textBox_H = 15;
            _textBox_W = 120;*/

            _textBoxMovePoint_X = _textBox_X;
            _textBoxMovePoint_Y = _textBox_Y - _textBox_H / 2;

            _connectionPointXs[0] = _x + _width / 2 - POINT_RADIUS / 2;
            _connectionPointXs[1] = _x + _width - POINT_RADIUS / 2;
            _connectionPointXs[2] = _x + _width / 2 - POINT_RADIUS / 2;
            _connectionPointXs[3] = _x - POINT_RADIUS / 2;

            _connectionPointYs[0] = _y - POINT_RADIUS / 2;
            _connectionPointYs[1] = _y + _height / 2 - POINT_RADIUS / 2;
            _connectionPointYs[2] = _y + _height - POINT_RADIUS / 2;
            _connectionPointYs[3] = _y + _height / 2 - POINT_RADIUS / 2;
        }
    }
}


