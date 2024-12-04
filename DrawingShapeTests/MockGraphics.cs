using DrawingModel;
namespace DrawingShapeTests
{
    internal class MockGraphics : IGraphics
    {
        public int[] testDatas;
        public string text;
        public void ClearAll() { }
        public void DrawProcess(in int[] datas)
        {
            testDatas = datas;
        }
        public void DrawTerminator(in int[] datas)
        {
            testDatas = datas;
        }
        public void DrawStart(in int[] datas) 
        {
            testDatas = datas;
        }
        public void DrawDecision(in int[] datas)
        {
            testDatas = datas;
        }
        public void DrawFrame(in int[] datas) 
        {
            testDatas = datas;
        }
        public void DrawText(in int[] datas, in string text) 
        {
            testDatas = datas;
            this.text = text;
        }

        public void DrawPoint(in int x, in int y,in int r)
        {
            testDatas = new int[] { x, y, r};
        }
    }
}
