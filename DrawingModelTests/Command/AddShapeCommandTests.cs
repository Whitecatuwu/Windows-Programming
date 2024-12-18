using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using DrawingShape;
using System.Linq;

namespace DrawingCommand.Tests
{

    [TestClass()]
    public class AddShapeCommandTests
    {
        [TestMethod()]
        public void AddShapeCommandTest()
        {
            Model model = new DrawingModel.Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "1", "100", "100" });
            var add = new AddShapeCommand(model, shape);
        }

        [TestMethod()]
        public void AddShapeCommandTest1()
        {
            Model model = new DrawingModel.Model();
            var add = new AddShapeCommand(model, ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            Model model = new DrawingModel.Model();
            var add = new AddShapeCommand(model, ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            add.Execute();
            PrivateObject privateObject = new PrivateObject(add);
            Model m = (Model)privateObject.GetFieldOrProperty("_model");
            Assert.AreEqual(1, m.ShapesSize);
        }

        [TestMethod()]
        public void UnExecuteTest()
        {
            Model model = new DrawingModel.Model();
            var add = new AddShapeCommand(model, ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            PrivateObject privateObject = new PrivateObject(add);
            Model m = (Model)privateObject.GetFieldOrProperty("_model");

            add.Execute();
            add.UnExecute();
            Assert.AreEqual(0, m.ShapesSize);
        }
    }
}