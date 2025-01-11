using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using DrawingShape;
using System.Collections.Generic;
using System.Linq;

namespace DrawingCommand.Tests
{
    [TestClass()]
    public class AddLineCommandTests
    {
        [TestMethod()]
        public void AddLineCommandTest()
        {
            Model model = new Model();
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            //var add = new AddLineCommand(model, line);
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            /*Model model = new Model();
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            var add = new AddLineCommand(model, line);
            add.Execute();
            PrivateObject privateObject = new PrivateObject(model);
            var lines = (List<Line>)privateObject.GetFieldOrProperty("_lines");
            Assert.AreEqual(1, lines.Count());
            add.UnExecute();
            Assert.AreEqual(0, lines.Count());*/
        }

        [TestMethod()]
        public void UnExecuteTest()
        {
            /*Model model = new Model();
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            var add = new AddLineCommand(model, line);
            add.Execute();
            PrivateObject privateObject = new PrivateObject(model);
            var lines = (List<Line>)privateObject.GetFieldOrProperty("_lines");
            Assert.AreEqual(1, lines.Count());
            add.UnExecute();
            Assert.AreEqual(0, lines.Count());*/
        }
    }
}