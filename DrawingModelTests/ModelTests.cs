using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DrawingShape;
using DrawingShapeTests;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ModelTests
    {
        [TestMethod()]
        public void ModelTest()
        {
            Model model = new Model();
        }

        [TestMethod()]
        public void EnterPointerStateTest()
        {
            Model model = new Model();
            model.EnterPointerState();
        }

        [TestMethod()]
        public void EnterDrawingStateTest()
        {
            Model model = new Model();
            model.EnterDrawingState(ShapeType.START);
        }

        [TestMethod()]
        public void MouseDownTest()
        {
            Model model = new Model();
            model.MouseDown(114, 514);
        }

        [TestMethod()]
        public void MouseMoveTest()
        {
            Model model = new Model();
            model.MouseMove(114, 514);
        }

        [TestMethod()]
        public void MouseUpTest()
        {
            Model model = new Model();
            model.MouseUp(114, 514);
        }

        [TestMethod()]
        public void OnPaintTest()
        {
            Model model = new Model();
            model.OnPaint(new MockGraphics());
        }

        [TestMethod()]
        public void AddShapeTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            Assert.AreEqual(1, model.ShapesSize);
            Assert.AreEqual(model.Shapes[0].Text, "DecisionTest");
        }

        [TestMethod()]
        public void AddShapeTest1()
        {
            Model model = new Model();
            model.AddShape(new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" }));
            Assert.AreEqual(1, model.ShapesSize);
        }

        [TestMethod()]
        public void RemoveShapeTest()
        {
            Model model = new Model();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => model.RemoveShape(114514));
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" }));
            model.RemoveShape(1);
            Assert.AreEqual(model.RemovedShapeIndex, 1);
        }
    }
}