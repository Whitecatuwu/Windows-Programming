using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingCommand;
using System;
using DrawingShape;
using DrawingModel;
namespace DrawingCommand.Tests
{
    [TestClass()]
    public class TextMoveCommendTests
    {
        [TestMethod()]
        public void TextMoveCommendTest()
        {
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            var tm = new TextMoveCommend(new DrawingModel.Model(), shape, new Tuple<int, int>(114,514));
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            Model model = new Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            var tm = new TextMoveCommend(model, shape, new Tuple<int, int>(114, 514));

            int textX = shape.TextBox_X;
            int textY = shape.TextBox_Y;

            model.AddShape(shape);
            tm.Execute();
            Assert.AreEqual(textX, shape.TextBox_X);
            Assert.AreEqual(textY, shape.TextBox_Y);
            tm.UnExecute();
            Assert.AreEqual(shape.TextBox_X, 114);
            Assert.AreEqual(shape.TextBox_Y, 514);
        }

        [TestMethod()]
        public void UnExecuteTest()
        {
            Model model = new Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            var tm = new TextMoveCommend(model, shape, new Tuple<int, int>(114, 514));

            int textX = shape.TextBox_X;
            int textY = shape.TextBox_Y;

            model.AddShape(shape);
            tm.Execute();
            Assert.AreEqual(textX, shape.TextBox_X);
            Assert.AreEqual(textY, shape.TextBox_Y);
            tm.UnExecute();
            Assert.AreEqual(shape.TextBox_X, 114);
            Assert.AreEqual(shape.TextBox_Y, 514);
        }
    }
}