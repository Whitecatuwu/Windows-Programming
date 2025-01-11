using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingShape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingShapeTests;

namespace DrawingShape.Tests
{
    [TestClass()]
    public class LineTests
    {
        [TestMethod()]
        public void DisconnectTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            var line = new Line();
            line.ConnectedFirstPoint = point;
            line.ConnectedSecondPoint = shape.ConnectionPoints[1];
            line.Disconnect();
            Assert.IsNull(line.ConnectedFirstPoint);
            Assert.IsNull(line.ConnectedSecondPoint);
        }

        [TestMethod()]
        public void AnotherPointTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            var line = new Line();
            line.ConnectedFirstPoint = point;
            line.ConnectedSecondPoint = shape.ConnectionPoints[1];
            var result = line.AnotherPoint(point).CompareTo(line.ConnectedSecondPoint);
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void DrawTest()
        {
            var line = new Line();
            line.Draw(new MockGraphics());
        }

        [TestMethod()]
        public void IsPointInRangeTest()
        {
            Assert.IsFalse(new Line().IsPointInRange(0, 0));
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            var point1 = shape.ConnectionPoints[1];
            var line = new Line();
            var line1 = new Line();
            var line2 = new Line();
            line.ConnectedFirstPoint = point;
            line.ConnectedSecondPoint = point1;
            line1.ConnectedFirstPoint = point1;
            line1.ConnectedSecondPoint = point;
            line2.ConnectedFirstPoint = shape.ConnectionPoints[2];
            line2.ConnectedSecondPoint = shape.ConnectionPoints[3];

            Assert.AreEqual(line.CompareTo(line1), 0);
            Assert.AreNotEqual(line.CompareTo(line2), 0);
        }
    }
}