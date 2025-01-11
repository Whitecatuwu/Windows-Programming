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
    public class ConnectionPointTests
    {
        [TestMethod()]
        public void ConnectionPointTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            new ConnectionPoint(shape, new int[] { 1, 1, 1, 1 });
        }

        [TestMethod()]
        public void DrawTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            point.Draw(new MockGraphics());
        }

        [TestMethod()]
        public void DrawLinesTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            var line = new Line();
            line.ConnectedFirstPoint = point;
            line.ConnectedSecondPoint = shape.ConnectionPoints[1];
            point.AddConnectionLine(line);
            point.DrawLines(new List<Shape> { shape }  ,new MockGraphics());
        }

        [TestMethod()]
        public void IsPointInRangeTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            Assert.IsTrue(point.IsPointInRange(50,1));
        }

        [TestMethod()]
        public void AddConnectionLineTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            var line = new Line();
            line.ConnectedFirstPoint = point;
            line.ConnectedSecondPoint = shape.ConnectionPoints[1];
            point.AddConnectionLine(line);
            Assert.IsTrue(point.ConnectedLines.Count == 1);
        }

        [TestMethod()]
        public void RemoveConnectionLineTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            var line = new Line();
            line.ConnectedFirstPoint = point;
            line.ConnectedSecondPoint = shape.ConnectionPoints[1];
            point.AddConnectionLine(line);
            Assert.IsTrue(point.ConnectedLines.Count == 1);
            point.RemoveConnectionLine(line);
            Assert.IsTrue(point.ConnectedLines.Count == 0);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Shape shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var point = shape.ConnectionPoints.First();
            var point2 = shape.ConnectionPoints[1];
            Assert.AreEqual(point.CompareTo(point),0);
            Assert.AreNotEqual(point.CompareTo(point2), 0);
        }
    }
}