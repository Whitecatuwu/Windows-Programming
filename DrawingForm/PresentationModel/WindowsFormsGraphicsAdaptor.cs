using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using DrawingModel;

namespace DrawingForm.PresentationModel
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        Pen _pen = new Pen(Color.DarkBlue, 1);
        Pen _framePen = new Pen(Color.Red, 2);
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }
        public void DrawTerminator(in int[] datas)
        {
            Normalize(datas, out int[] result);
            ref int x = ref result[0];
            ref int y = ref result[1];
            ref int height = ref result[2];
            ref int width = ref result[3];

            _graphics.DrawArc(_pen, x, y, height, height, 90, 180);
            _graphics.DrawArc(_pen, x + width - height, y, height, height, 270, 180);
            _graphics.DrawLine(_pen, x + height / 2, y, x + width - height / 2, y);
            _graphics.DrawLine(_pen, x + height / 2, y + height, x + width - height / 2, y + height);
        }
        public void DrawProcess(in int[] datas)
        {
            Normalize(datas, out int[] result);
            ref int x = ref result[0];
            ref int y = ref result[1];
            ref int height = ref result[2];
            ref int width = ref result[3];

            _graphics.DrawRectangle(_pen, x, y, width, height);
        }
        public void DrawStart(in int[] datas)
        {
            Normalize(datas, out int[] result);
            ref int x = ref result[0];
            ref int y = ref result[1];
            ref int height = ref result[2];
            ref int width = ref result[3];

            _graphics.DrawEllipse(_pen, x, y, width, height);
        }
        public void DrawDecision(in int[] datas)
        {
            Normalize(datas, out int[] result);
            ref int x = ref result[0];
            ref int y = ref result[1];
            ref int height = ref result[2];
            ref int width = ref result[3];

            Point[] points = new Point[4];
            points[0] = new Point(x + width / 2, y);
            points[1] = new Point(x + width, y + height / 2);
            points[2] = new Point(x + width / 2, y + height);
            points[3] = new Point(x, y + height / 2);
            _graphics.DrawPolygon(_pen, points);
        }
        public void DrawFrame(in int[] datas)
        {
            Normalize(datas, out int[] result);
            ref int x = ref result[0];
            ref int y = ref result[1];
            ref int height = ref result[2];
            ref int width = ref result[3];

            _graphics.DrawRectangle(_framePen, x, y, width, height);
        }

        public void DrawText(in int[] datas, in string text)
        {
            Normalize(datas, out int[] result);
            ref int x = ref result[0];
            ref int y = ref result[1];
            ref int height = ref result[2];
            ref int width = ref result[3];

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            _graphics.DrawString(text, new Font("Arial", 7), Brushes.Black, x + width / 2, y + height / 2, format);
        }

        private void Normalize(in int[] datas, out int[] result)
        {
            ref int x = ref datas[0];
            ref int y = ref datas[1];
            ref int height = ref datas[2];
            ref int width = ref datas[3];

            if (height < 0)
            {
                y += height;
                height = -height;
            }
            if (width < 0)
            {
                x += width;
                width = -width;
            }

            result = new int[4] { x, y, height, width };
        }
    }
}
