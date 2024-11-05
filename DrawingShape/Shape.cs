using DrawingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    abstract internal class Shape
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
        }

        protected ShapeType _shapeType;
        protected string _text;
        protected string _id;
        protected int _x;
        protected int _y;
        protected int _height;
        protected int _width;

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
            set { _x = value; }
            get { return _x; }
        }
        public int Y
        {
            set { _y = value; }
            get { return _y; }
        }
        public int Height
        {
            set { _height = value; }
            get { return _height; }
        }
        public int Width
        {
            set { _width = value; }
            get { return _width; }
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
            graphics.DrawText(ShapeDatas, _text);
        }
    }
}


