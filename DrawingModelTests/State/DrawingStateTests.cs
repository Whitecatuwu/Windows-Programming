using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;
using DrawingShapeTests;
using DrawingShape;
using System.Collections;
using System.Diagnostics;

namespace DrawingState.Tests
{
    [TestClass()]
    public class DrawingStateTests
    {
        Model m = new Model();
        Decision decision = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
        DrawingShape.Process process = new DrawingShape.Process(ShapeType.PROCESS, new string[] { "ProcessTest", "400", "400", "100", "100" });
        Start start = new Start(ShapeType.START, new string[] { "StartTest", "400", "400", "100", "100" });
        Terminator terminator = new Terminator(ShapeType.TERMINATOR, new string[] { "terminatorTest", "1", "1", "100", "300" });
        PointerState pointerState = new PointerState();

        [TestInitialize()]
        public void Initialize()
        {
            m.AddShape(decision);
            m.AddShape(process);
            m.AddShape(start);
            m.AddShape(terminator);
        }

        [TestMethod()]
        public void DrawingStateTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
        }

        [TestMethod()]
        public void InitializeTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            drawingState.Initialize(m);
        }

        [TestMethod()]
        public void MouseDownTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            drawingState.Initialize(m);
            drawingState.MouseDown(m, 0, 0);
        }

        [TestMethod()]
        public void MouseMoveTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            drawingState.Initialize(m);
            drawingState.MouseMove(m, 0, 0); //do nothing

            drawingState.MouseDown(m, 0, 0);
            drawingState.MouseMove(m, 0, 0); //do nothing

            drawingState.MouseDown(m, 0, 0);
            drawingState.MouseMove(m, 114, 114); //call privateObject();
        }

        [TestMethod()]
        public void MouseUpTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            drawingState.Initialize(m);
            drawingState.HintShapeType = ShapeType.TERMINATOR;
            drawingState.MouseUp(m, 0, 0); //do nothing

            drawingState.MouseDown(m, 0, 0);
            drawingState.MouseMove(m, 0, 0);
            drawingState.MouseUp(m, 0, 0); // null hint

            drawingState.MouseDown(m, 1, 1);
            drawingState.MouseMove(m, 100, 100);
            drawingState.MouseMove(m, 100, 1);
            drawingState.MouseUp(m, 100, 1); //width == 0

            drawingState.MouseDown(m, 1, 1);
            drawingState.MouseMove(m, 100, 100);
            drawingState.MouseMove(m, 1, 100);
            drawingState.MouseUp(m, 1, 100); // height == 0

            drawingState.MouseDown(m, 1, 1);
            drawingState.MouseMove(m, 100, 100);
            drawingState.MouseUp(m, 100, 100);

        }

        [TestMethod()]
        public void OnPaintTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            drawingState.Initialize(m);
            drawingState.HintShapeType = ShapeType.TERMINATOR;

            drawingState.OnPaint(m, new MockGraphics());
            drawingState.MouseDown(m, 1, 1);
            drawingState.MouseMove(m, 100, 100);
            drawingState.OnPaint(m, new MockGraphics());
            drawingState.MouseUp(m, 100, 100);
            drawingState.OnPaint(m, new MockGraphics());
        }

        [TestMethod()]
        public void KeyDownTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            drawingState.KeyDown(m, 0);
            drawingState.KeyDown(m, 160);
            drawingState.KeyDown(m, 160);
        }

        [TestMethod()]
        public void KeyUpTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            drawingState.KeyUp(m, 0);
            drawingState.KeyUp(m, 160);
            drawingState.KeyUp(m, 160);
        }
        [TestMethod()]
        public void SelectHintRangeTest()
        {
            DrawingState drawingState = new DrawingState(pointerState);
            PrivateObject privateObject = new PrivateObject(drawingState);

            privateObject.Invoke("SelectHintRange", 1, 1);

            drawingState.HintShapeType = DrawingShape.ShapeType.PROCESS;
            privateObject.Invoke("SelectHintRange", 1, 1);

            drawingState.KeyDown(m, 160);
            privateObject.Invoke("SelectHintRange", 100, 1000);
            privateObject.Invoke("SelectHintRange", -100, 1000);
            privateObject.Invoke("SelectHintRange", 100, -1000);
            privateObject.Invoke("SelectHintRange", -100, -1000);

            drawingState.KeyUp(m, 160);
            privateObject.Invoke("SelectHintRange", 200, 280);

        }
    }
}