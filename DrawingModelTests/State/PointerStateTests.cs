using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;
using DrawingShape;


namespace DrawingState.Tests
{
    [TestClass()]
    public class PointerStateTests
    {
        Model m = new Model();
        Decision decision = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
        Process process = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "400", "400", "100", "100" });
        Start start = new Start(ShapeType.START, new string[] { "StartTest", "400", "400", "100", "100" });
        Terminator terminator = new Terminator(ShapeType.TERMINATOR, new string[] { "terminatorTest", "1", "1", "100", "300" });

        [TestInitialize()]
        public void Initialize()
        {
            m.AddShape(decision);
            m.AddShape(process);
            m.AddShape(start);
            m.AddShape(terminator);
        }

        [TestMethod()]
        public void InitializeTest()
        {
            PointerState pointerState = new PointerState();
            pointerState.Initialize(m);
        }

        [TestMethod()]
        public void MouseDownTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);

            SortedSet<Shape> selectedShapes = (SortedSet<Shape>)privateObject.GetFieldOrProperty("_selectedShapes");
            pointerState.MouseDown(m, 150, 80);
            Assert.IsTrue(selectedShapes.Contains(terminator));
            pointerState.MouseDown(m, 0, 0);
            Assert.AreEqual(0, selectedShapes.Count);

            pointerState.KeyDown(m, 17);
            pointerState.MouseDown(m, 150, 80);
            pointerState.MouseDown(m, 0, 0);
            Assert.AreEqual(1, selectedShapes.Count);

            pointerState.MouseDown(m, 450, 461);
            Assert.IsTrue(selectedShapes.Contains(start));
            Assert.AreEqual(2, selectedShapes.Count);

            pointerState.KeyUp(m, 17);
            pointerState.MouseDown(m, 450, 461);
            Assert.IsTrue(selectedShapes.Contains(start));
            Assert.AreEqual(1, selectedShapes.Count);

            pointerState.MouseDown(m, 450, 445);
            pointerState.MouseDown(m, 450, 455);

        }

        [TestMethod()]
        public void AddSelectedShapeTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);
            SortedSet<Shape> selectedShapes = (SortedSet<Shape>)privateObject.GetFieldOrProperty("_selectedShapes");

            pointerState.AddSelectedShape(process);
            Assert.IsTrue(selectedShapes.Contains(process));
        }

        [TestMethod()]
        public void RemoveSelectedShapeTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);
            SortedSet<Shape> selectedShapes = (SortedSet<Shape>)privateObject.GetFieldOrProperty("_selectedShapes");

            pointerState.AddSelectedShape(process);
            pointerState.RemoveSelectedShape(process);
            pointerState.AddSelectedShape(start);
            pointerState.AddSelectedShape(terminator);
            pointerState.AddSelectedShape(decision);


            Assert.IsFalse(selectedShapes.Contains(process));
            pointerState.RemoveSelectedShape(process);
        }

        [TestMethod()]
        public void ClearSelectedShapesTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);
            SortedSet<Shape> selectedShapes = (SortedSet<Shape>)privateObject.GetFieldOrProperty("_selectedShapes");

            pointerState.AddSelectedShape(process);
            pointerState.AddSelectedShape(start);
            pointerState.AddSelectedShape(terminator);
            pointerState.AddSelectedShape(decision);

            pointerState.ClearSelectedShapes();
            Assert.AreEqual(0, selectedShapes.Count);

        }

        [TestMethod()]
        public void MouseMoveTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);
            SortedSet<Shape> selectedShapes = (SortedSet<Shape>)privateObject.GetFieldOrProperty("_selectedShapes");

            pointerState.AddSelectedShape(process);
            pointerState.AddSelectedShape(start);
            pointerState.AddSelectedShape(terminator);
            pointerState.AddSelectedShape(decision);

            int preStartX = start.X;
            //int preStartY = start.Y;
            pointerState.MouseMove(m, 12345789, 123456789);
            Assert.AreEqual(start.X, preStartX);

            pointerState.MouseDown(m, 0, 0);
            pointerState.MouseMove(m, 0, 0);
            Assert.AreEqual(start.X, preStartX);

            pointerState.MouseDown(m, 470, 470);
            pointerState.MouseMove(m, 500, 500);
            Assert.AreEqual(start.X, preStartX + 30);

            pointerState.MouseDown(m, 50, 150);
            pointerState.MouseMove(m, 60, 200);

            pointerState.MouseDown(m, 480, 480);
            pointerState.MouseMove(m, 500, 500);

        }

        [TestMethod()]
        public void MouseUpTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);
            SortedSet<Shape> selectedShapes = (SortedSet<Shape>)privateObject.GetFieldOrProperty("_selectedShapes");

            pointerState.AddSelectedShape(process);
            pointerState.AddSelectedShape(start);
            pointerState.AddSelectedShape(terminator);
            pointerState.AddSelectedShape(decision);

            pointerState.MouseUp(m, 0, 0);
            Assert.AreEqual(m.UpdatedShapeIndex, -1);

            pointerState.MouseDown(m, 450, 450);
            pointerState.MouseMove(m, 450, 455);
            pointerState.MouseUp(m, 450, 455);

            pointerState.MouseDown(m, 450, 450);
            pointerState.MouseMove(m, 450, 455);
            pointerState.MouseUp(m, 450, 445);

            pointerState.MouseDown(m, 450, 471);
            pointerState.MouseUp(m, 450, 471);
            pointerState.MouseDown(m, 450, 471);
            pointerState.MouseMove(m, 450, 475);
            pointerState.MouseUp(m, 450, 475);

            pointerState.AddSelectedShape(decision);
            pointerState.MouseDown(m, 5, 5);
            pointerState.MouseUp(m, 5, 5);

            pointerState.ClearSelectedShapes();
            pointerState.MouseDown(m, 114514, 1919);
            pointerState.MouseMove(m, 114514, 1900);
            pointerState.MouseUp(m, 114514, 1900);
        }

        [TestMethod()]
        public void MouseDoubleClickTest()
        {
            PointerState pointerState = new PointerState();
            pointerState.Initialize(m);

            Terminator term = new Terminator(ShapeType.TERMINATOR, new string[] { "terminatorTest", "1", "1", "100", "300" });

            pointerState.AddSelectedShape(term);

            pointerState.MouseDoubleClick(m, 0, 0);
            pointerState.MouseDoubleClick(m, 151, 43);
        }

        [TestMethod()]
        public void OnPaintTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);
            SortedSet<Shape> selectedShapes = (SortedSet<Shape>)privateObject.GetFieldOrProperty("_selectedShapes");

            pointerState.AddSelectedShape(process);
            pointerState.AddSelectedShape(start);
            pointerState.AddSelectedShape(terminator);
            pointerState.AddSelectedShape(decision);

            pointerState.OnPaint(m, new DrawingShapeTests.MockGraphics());
        }

        [TestMethod()]
        public void KeyDownTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);

            pointerState.KeyDown(m, 0);
            pointerState.KeyDown(m, 17);
            Assert.IsTrue((bool)privateObject.GetFieldOrProperty("_isCtrlKeyDown"));
            pointerState.KeyDown(m, 17);
        }

        [TestMethod()]
        public void KeyUpTest()
        {
            PointerState pointerState = new PointerState();
            PrivateObject privateObject = new PrivateObject(pointerState);
            pointerState.Initialize(m);

            pointerState.KeyUp(m, 17);
            Assert.IsFalse((bool)privateObject.GetFieldOrProperty("_isCtrlKeyDown"));
            pointerState.KeyUp(m, 0);
        }
    }
}