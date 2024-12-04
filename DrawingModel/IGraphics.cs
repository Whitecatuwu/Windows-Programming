

namespace DrawingModel
{
    public interface IGraphics
    {
        void ClearAll();
        void DrawProcess(in int[] datas);
        void DrawTerminator(in int[] datas);
        void DrawStart(in int[] datas);
        void DrawDecision(in int[] datas);
        void DrawFrame(in int[] datas);
        void DrawText(in int[] datas, in string text);
        void DrawPoint(in int x, in int y,in int r);
    }
}
