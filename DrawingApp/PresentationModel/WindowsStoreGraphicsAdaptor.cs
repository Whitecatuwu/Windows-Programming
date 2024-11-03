using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using DrawingModel;

namespace DrawingApp.PresentationModel
{
    class WindowsStoreGraphicsAdaptor : IGraphics
    {
        Canvas _canvas;
        public WindowsStoreGraphicsAdaptor(Canvas canvas)
        {
            this._canvas = canvas;
        }
        public void ClearAll()
        {
            _canvas.Children.Clear();
        }
        public void DrawProcess(int x, int y, int height, int width)
        {
        }
        public void DrawTerminator(int x, int y, int height, int width)
        {
        }
        public void DrawStart(int x, int y, int height, int width)
        {
        }
        public void DrawDecision(int x, int y, int height, int width)
        {
        }

    }
}