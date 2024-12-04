using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingShapeTests;

namespace DrawingShape.Tests
{

    [TestClass()]
    public class TerminatorTests
    {
        Terminator terminator = new Terminator(ShapeType.TERMINATOR, new string[] { "terminatorTest", "1", "1", "100", "300" });
        [TestMethod()]
        public void TerminatorTest()
        {
            string[] testShapeData = new string[] { "terminatorTest", "1", "1", "1", "1" };
            Terminator test = new Terminator(ShapeType.TERMINATOR, testShapeData);

            Assert.AreEqual(test.ShapeType, ShapeType.TERMINATOR);
            Assert.AreEqual(test.Text, testShapeData[0]);
            Assert.AreEqual(test.X, int.Parse(testShapeData[1]));
            Assert.AreEqual(test.Y, int.Parse(testShapeData[2]));
            Assert.AreEqual(test.Height, int.Parse(testShapeData[3]));
            Assert.AreEqual(test.Width, int.Parse(testShapeData[4]));
        }

        [TestMethod()]
        public void DrawTest()
        {
            MockGraphics mock = new MockGraphics();
            terminator.Draw(mock);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(mock.testDatas[i], terminator.ShapeDatas[i]);
            }
        }

        [TestMethod()]
        public void IsPointInRangeTest()
        {


            Assert.IsTrue(terminator.IsPointInRange(150, 50));
            Assert.IsTrue(terminator.IsPointInRange(2, 50));
            Assert.IsTrue(terminator.IsPointInRange(50, 2));
            Assert.IsTrue(terminator.IsPointInRange(300, 50));
            Assert.IsTrue(terminator.IsPointInRange(50, 50));
            Assert.IsTrue(terminator.IsPointInRange(250, 50));

            Assert.IsFalse(terminator.IsPointInRange(300, 2));
            Assert.IsFalse(terminator.IsPointInRange(2, 2));
            Assert.IsFalse(terminator.IsPointInRange(0, 0));
            Assert.IsFalse(terminator.IsPointInRange(350, 50));
            Assert.IsFalse(terminator.IsPointInRange(50, 350));
            Assert.IsFalse(terminator.IsPointInRange(50, 0));
            Assert.IsFalse(terminator.IsPointInRange(0, 50));
            Assert.IsFalse(terminator.IsPointInRange(150, 0));
            Assert.IsFalse(terminator.IsPointInRange(150, 350));


        }
    }
}