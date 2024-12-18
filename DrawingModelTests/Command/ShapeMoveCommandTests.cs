using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingCommand;
using System;
using System.Collections.Generic;
using DrawingShape;

namespace DrawingCommand.Tests
{
    [TestClass()]
    public class ShapeMoveCommandTests
    {
        [TestMethod()]
        public void ShapeMoveCommandTest()
        {
            var move = new ShapeMoveCommand(new DrawingModel.Model(), new SortedSet<Shape> { }, new SortedDictionary<Shape, Tuple<int, int>> { });
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            var model = new DrawingModel.Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            var shapes = new SortedSet<Shape> { };
            var pos = new SortedDictionary<Shape, Tuple<int, int>> { };

            model.AddShape(shape);
            shapes.Add(shape);
            pos.Add(shape, new Tuple<int, int>(114, 514));

            var move = new ShapeMoveCommand(model, shapes, pos);
            move.UnExecute();
            Assert.AreEqual(shape.X, 114);
            Assert.AreEqual(shape.Y, 514);
            move.Execute();
            Assert.AreEqual(shape.X, 2);
            Assert.AreEqual(shape.Y, 222);
        }

        [TestMethod()]
        public void UnExecuteTest()
        {
            var model = new DrawingModel.Model();
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "222", "100", "100" });
            var shapes = new SortedSet<Shape> { };
            var pos = new SortedDictionary<Shape, Tuple<int, int>> { };

            model.AddShape(shape);
            shapes.Add(shape);
            pos.Add(shape, new Tuple<int, int>(114, 514));

            var move = new ShapeMoveCommand(model, shapes, pos);
            move.UnExecute();
            Assert.AreEqual(shape.X, 114);
            Assert.AreEqual(shape.Y, 514);
            move.Execute();
            Assert.AreEqual(shape.X, 2);
            Assert.AreEqual(shape.Y, 222);
        }
    }
}