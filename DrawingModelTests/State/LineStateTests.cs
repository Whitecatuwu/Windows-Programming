using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using DrawingShapeTests;
using DrawingShape;

namespace DrawingState.Tests
{
    [TestClass()]
    public class LineStateTests
    {
        Model m = new Model();
        PointerState pointerState = new PointerState();

        [TestInitialize()]
        public void Initialize()
        {
            //
        }

        [TestMethod()]
        public void LineStateTest()
        {
            LineState lineState = new LineState();
        }

        [TestMethod()]
        public void InitializeTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);
        }

        [TestMethod()]
        public void OnPaintTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);

            lineState.OnPaint(m, new MockGraphics());
            lineState.MouseDown(m, 1, 1);
            lineState.MouseMove(m, 100, 100);
            lineState.OnPaint(m, new MockGraphics());
            lineState.MouseUp(m, 100, 100);
            lineState.OnPaint(m, new MockGraphics());
        }

        [TestMethod()]
        public void MouseDownTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);
            lineState.MouseDown(m, 0, 0);
        }

        [TestMethod()]
        public void MouseMoveTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);
            lineState.MouseMove(m, 0, 0); //do nothing

            lineState.MouseDown(m, 0, 0);
            lineState.MouseMove(m, 0, 0); //do nothing

            lineState.MouseDown(m, 0, 0);
            lineState.MouseMove(m, 114, 114);
            lineState.MouseMove(m, 191, 986);
        }

        [TestMethod()]
        public void MouseUpTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);
            lineState.MouseUp(m, 0, 0); //do nothing

            lineState.MouseDown(m, 0, 0);
            lineState.MouseMove(m, 0, 0);
            lineState.MouseUp(m, 0, 0); // null hint

            lineState.MouseDown(m, 1, 1);
            lineState.MouseMove(m, 100, 100);
            lineState.MouseMove(m, 100, 1);
            lineState.MouseUp(m, 100, 1); //width == 0

            lineState.MouseDown(m, 1, 1);
            lineState.MouseMove(m, 100, 100);
            lineState.MouseMove(m, 1, 100);
            lineState.MouseUp(m, 1, 100); // height == 0

            lineState.MouseDown(m, 1, 1);
            lineState.MouseMove(m, 100, 100);
            lineState.MouseUp(m, 100, 100);
        }

        [TestMethod()]
        public void MouseDoubleClickTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);
            lineState.MouseDoubleClick(m, 114, 514);
        }

        [TestMethod()]
        public void KeyDownTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);
            lineState.KeyDown(m, 0);
        }

        [TestMethod()]
        public void KeyUpTest()
        {
            LineState lineState = new LineState();
            lineState.Initialize(m);
            lineState.KeyUp(m, 0);
        }

        [TestMethod()]
        public void DisplayTouchedShapeTest()
        {
            LineState lineState = new LineState();
            Model model = new Model();
            lineState.Initialize(model);
            var shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(shape);

            PrivateObject privateObject = new PrivateObject(lineState);
            Shape touchedShape = null;

            privateObject.Invoke("DisplayTouchedShape", model, 70, 70); //touched
            touchedShape = (Shape)privateObject.GetFieldOrProperty("_touchedShape");
            Assert.IsNotNull(touchedShape);

            privateObject.Invoke("DisplayTouchedShape", model, 60, 60); //touched
            touchedShape = (Shape)privateObject.GetFieldOrProperty("_touchedShape");
            Assert.IsNotNull(touchedShape);

            privateObject.Invoke("DisplayTouchedShape", model, 200, 200);//untouched
            touchedShape = (Shape)privateObject.GetFieldOrProperty("_touchedShape");
            Assert.IsNull(touchedShape);
        }
    }
}