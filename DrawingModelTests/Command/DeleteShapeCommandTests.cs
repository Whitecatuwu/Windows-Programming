using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using DrawingShape;
using System.Diagnostics;

namespace DrawingCommand.Tests
{
    [TestClass()]
    public class DeleteShapeCommandTests
    {

        Decision decision = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
        DrawingShape.Process process = new DrawingShape.Process(ShapeType.PROCESS, new string[] { "ProcessTest", "400", "400", "100", "100" });
        Start start = new Start(ShapeType.START, new string[] { "StartTest", "400", "400", "100", "100" });
        Terminator terminator = new Terminator(ShapeType.TERMINATOR, new string[] { "terminatorTest", "1", "1", "100", "300" });

        [TestInitialize()]
        public void Initialize()
        {

        }

        [TestMethod()]
        public void DeleteShapeCommandTest()
        {
            Model model = new DrawingModel.Model();
            var del = new DeleteShapeCommand(model, start);
        }

        [TestMethod()]
        public void DeleteShapeCommandTest1()
        {
            Model m = new DrawingModel.Model();
            m.AddShape(decision);
            m.AddShape(process);
            m.AddShape(start);
            m.AddShape(terminator);
            var del = new DeleteShapeCommand(m, 2);
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            Model m = new DrawingModel.Model();
            m.AddShape(decision);
            m.AddShape(process);
            m.AddShape(start);
            m.AddShape(terminator);
            var del = new DeleteShapeCommand(m, 2);
            del.Execute();

            Assert.AreNotEqual(m.Shapes[2], start);
        }

        [TestMethod()]
        public void UnExecuteTest()
        {
            Model m = new DrawingModel.Model();
            m.AddShape(decision);
            m.AddShape(process);
            m.AddShape(start);
            m.AddShape(terminator);
            var del = new DeleteShapeCommand(m, 2);
            del.Execute();
            Assert.AreNotEqual(m.Shapes[2], start);
            del.UnExecute();
            Assert.AreEqual(m.Shapes[2], start);
        }
    }
}