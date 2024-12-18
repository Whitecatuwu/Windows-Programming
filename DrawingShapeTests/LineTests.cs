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
        public void LineTest()
        {
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            Assert.AreEqual(line.FirstX, 114);
            Assert.AreEqual(line.FirstY, 514);
            Assert.AreEqual(line.SecondX, 191);
            Assert.AreEqual(line.SecondY, 861);

            line.ConnectedFirstShape = null;
            line.ConnectedFirstShapePointNumber = -1;
            line.ConnectedSecondShape = null;
            line.ConnectedSecondShapePointNumber = -1;

            line.ConnectedFirstShape = line.ConnectedFirstShape;
            line.ConnectedFirstShapePointNumber = line.ConnectedFirstShapePointNumber;
            line.ConnectedSecondShape = line.ConnectedSecondShape;
            line.ConnectedSecondShapePointNumber = line.ConnectedSecondShapePointNumber;

        }

        [TestMethod()]
        public void DrawTest()
        {
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            line.Draw(new MockGraphics());
        }

        [TestMethod()]
        public void IsPointInRangeTest()
        {
            Line line = new Line();
            Assert.IsFalse(line.IsPointInRange(0,0));
        }
    }
}