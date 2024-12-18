using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using DrawingShape;
using System.Reflection;

namespace DrawingCommand.Tests
{
    [TestClass()]
    public class TextChangeCommandTests
    {
        [TestMethod()]
        public void TextChangeCommandTest()
        {
            Model m = new Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            m.AddShape(shape);
            m.TextEditShapeIndex = 0;
            var tc = new TextChangeCommand(m, "qwerttyu");
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            Model m = new Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            m.AddShape(shape);
            m.TextEditShapeIndex = 0;

            var tc = new TextChangeCommand(m, "qwerttyu");
            tc.Execute();
            Assert.AreEqual(shape.Text, "qwerttyu");
            tc.UnExecute();
            Assert.AreEqual(shape.Text, "STARTTest");
        }

        [TestMethod()]
        public void UnExecuteTest()
        {
            Model m = new Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            m.AddShape(shape);
            m.TextEditShapeIndex = 0;

            var tc = new TextChangeCommand(m, "qwerttyu");
            tc.Execute();
            Assert.AreEqual(shape.Text, "qwerttyu");
            tc.UnExecute();
            Assert.AreEqual(shape.Text, "STARTTest");
        }
    }
}