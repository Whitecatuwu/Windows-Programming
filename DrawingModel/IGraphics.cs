using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    internal interface IGraphics
    {
        void ClearAll();
        void DrawProcess(int x, int y, int height, int width, string text);
        void DrawTerminator(int x, int y, int height, int width, string text);
        void DrawStart(int x, int y, int height, int width, string text);
        void DrawDecision(int x, int y, int height, int width, string text);
    }
}
