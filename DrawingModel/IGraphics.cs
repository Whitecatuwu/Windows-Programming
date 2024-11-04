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
        void DrawProcess(in int[] datas);
        void DrawTerminator(in int[] datas);
        void DrawStart(in int[] datas);
        void DrawDecision(in int[] datas);
        void DrawFrame(in int[] datas);
        void DrawText(in int[] datas, in string text);
    }
}
