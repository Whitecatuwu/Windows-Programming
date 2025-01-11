using DrawingModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DrawingShape;
using DrawingShapeTests;
using System.Collections.Generic;
using System.Linq;
using DrawingCommand;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

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
        public void EnterLineStateTest()
        {
            Model model = new Model();
            model.EnterLineState();
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
        public void MouseDoubleClickTest()
        {
            Model model = new Model();
            model.MouseDoubleClick(114, 514);
        }

        [TestMethod()]
        public void OnPaintTest()
        {
            Model model = new Model();
            model.AddShape(new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" }));
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            //model.AddLine(line);
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
        [TestMethod()]
        public void AddLineTest()
        {
            Model model = new Model();
            Line line = new Line();
            var shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point_1 = shape.ConnectionPoints[0];
            var point_2 = shape.ConnectionPoints[1];
            model.AddLine(point_1, point_2, line);
        }

        [TestMethod()]
        public void RemoveShapeTest1()
        {
            Model model = new Model();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => model.RemoveShape(114514));
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(shape);
            model.RemoveShape(shape);
            Assert.AreEqual(model.RemovedShapeIndex, 1);
            model.RemoveShape(shape);
            Assert.AreEqual(model.RemovedShapeIndex, 1);
        }

        [TestMethod()]
        public void InsertShapeTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            model.InsertShape(1, ShapeType.START, new string[] { "STARTTest", "2", "1", "100", "100" });

            Assert.AreEqual(4, model.ShapesSize);
            Assert.AreEqual(model.Shapes[1].Text, "STARTTest");

            Assert.AreEqual(1, model.InsertedShapeIndex);
        }

        [TestMethod()]
        public void InsertShapeTest1()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "1", "100", "100" });
            model.InsertShape(1, shape);

            Assert.AreEqual(4, model.ShapesSize);
            Assert.AreEqual(model.Shapes[1].Text, "STARTTest");
        }

        [TestMethod()]
        public void ExeCommandTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });

            model.ExeCommand(new AddShapeCommand(model, ShapeType.START, new string[] { "DecisionTest", "10", "10", "100", "100" }));
        }

        [TestMethod()]
        public void MoveShapeTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "1", "100", "100" });
            model.AddShape(shape);

            model.MoveShape(shape, 114, 514);
            Assert.AreEqual(shape.X, 114);
            Assert.AreEqual(shape.Y, 514);

            model.MoveShape(null, 114, 514);
        }

        [TestMethod()]
        public void RedoTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });

            model.ExeCommand(new AddShapeCommand(model, ShapeType.START, new string[] { "DecisionTest", "10", "10", "100", "100" }));
            Assert.IsTrue(model.IsUndoEnabled);
            model.Undo();
            Assert.IsTrue(model.IsRedoEnabled);
            model.Redo();
            Assert.IsFalse(model.IsRedoEnabled);
            Assert.IsTrue(model.IsUndoEnabled);
        }

        [TestMethod()]
        public void UndoTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });

            model.ExeCommand(new AddShapeCommand(model, ShapeType.START, new string[] { "DecisionTest", "10", "10", "100", "100" }));
            Assert.IsTrue(model.IsUndoEnabled);
            model.Undo();
            Assert.IsTrue(model.IsRedoEnabled);
        }

        [TestMethod()]
        public void EditShapeTextTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });

            model.EditShapeText("qwertyui");
            model.TextEditShapeIndex = 0;
            model.EditShapeText("qwertyui");
            Assert.AreEqual(model.Shapes[0].Text, "qwertyui");

        }

        [TestMethod()]
        public void MoveTextBoxTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            var shape = new Start(ShapeType.START, new string[] { "TERMINATORTest", "1", "1", "100", "100" });

            model.MoveTextBox(new Start(ShapeType.START, new string[] { "TERMINATORTest", "1", "1", "100", "100" }), 114, 514);
            model.AddShape(shape);
            model.MoveTextBox(shape, 114, 514);
            Assert.AreEqual(shape.TextBox_X, 114);
            Assert.AreEqual(shape.TextBox_Y, 514);

        }

        [TestMethod()]
        public void RemoveLineTest()
        {
            Model model = new Model();
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            /*model.AddLine(line);
            model.RemoveLine(line);
            PrivateObject privateObject = new PrivateObject(model);
            List<Line> lines = (List<Line>)privateObject.GetFieldOrProperty("_lines");
            Assert.AreEqual(0, lines.Count());*/
        }

        [TestMethod()]
        public void SaveTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            var targetPath = Path.Combine(path, "test.mydrawing");
            model.Save(targetPath);
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }

        [TestMethod()]
        public void AutoSaveTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            model.AutoSave();
        }

        [TestMethod()]
        public void LoadTest()
        {
            SaveTest();
            Model model = new Model();
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            var targetPath = Path.Combine(path, "test.mydrawing");
            model.Load(targetPath);
        }
    }
}