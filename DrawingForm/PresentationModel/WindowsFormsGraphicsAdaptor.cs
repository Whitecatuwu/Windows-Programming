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
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }
        public void DrawTerminator(int x, int y, int height, int width, string text)
        {
            _graphics.DrawArc(_pen, x, y, height, height, 90, 180);
            _graphics.DrawArc(_pen, x + width - height, y, height, height, 270, 180);
            _graphics.DrawLine(_pen, x + height / 2, y, x + width - height / 2, y);
            _graphics.DrawLine(_pen, x + height / 2, y + height, x + width - height / 2, y + height);
            _graphics.DrawString(text, new Font("Arial", 7), Brushes.Black, x + width / 2, y + height / 2);
        }
        public void DrawProcess(int x, int y, int height, int width, string text)
        {
            _graphics.DrawRectangle(_pen, x, y, width, height);
            _graphics.DrawString(text, new Font("Arial", 7), Brushes.Black, x + width / 2, y + height / 2);
        }
        public void DrawStart(int x, int y, int height, int width, string text)
        {
            _graphics.DrawEllipse(_pen, x, y, width, height);
            _graphics.DrawString(text, new Font("Arial", 7), Brushes.Black, x + width / 2, y + height / 2);
        }
        public void DrawDecision(int x, int y, int height, int width, string text)
        {
            Point[] points = new Point[4];
            points[0] = new Point(x + width / 2, y);
            points[1] = new Point(x + width, y + height / 2);
            points[2] = new Point(x + width / 2, y + height);
            points[3] = new Point(x, y + height / 2);
            _graphics.DrawPolygon(_pen, points);
            _graphics.DrawString(text, new Font("Arial", 7), Brushes.Black, x + width / 2, y + height / 2);
        }
    }
}
