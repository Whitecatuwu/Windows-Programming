using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DrawingShape;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ShapeFactoryTests
    {
        ShapeFactory shapeFactory = new ShapeFactory();
        [TestMethod()]
        public void CreateShapeTest()
        {
            Shape decison = shapeFactory.CreateShape(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            Shape process = shapeFactory.CreateShape(ShapeType.PROCESS, new string[] { "ProcessTest", "400", "400", "100", "100" });
            Shape start = shapeFactory.CreateShape(ShapeType.START, new string[] { "StartTest", "410", "400", "100", "100" });
            Shape terminator = shapeFactory.CreateShape(ShapeType.TERMINATOR, new string[] { "terminatorTest", "114", "1", "100", "300" });

            Assert.AreEqual(decison.ShapeType, ShapeType.DECISION);
            Assert.AreEqual(process.ShapeType, ShapeType.PROCESS);
            Assert.AreEqual(start.ShapeType, ShapeType.START);
            Assert.AreEqual(terminator.ShapeType, ShapeType.TERMINATOR);
            Assert.ThrowsException<ArgumentException>(() => shapeFactory.CreateShape(ShapeType.NULL, new string[] { "NULL", "0", "0", "0", "0" }));
        }  
    }
}