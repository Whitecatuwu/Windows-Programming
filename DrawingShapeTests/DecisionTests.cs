using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingShapeTests;

namespace DrawingShape.Tests
{
    [TestClass()]
    public class DecisionTests
    {
        Decision decision = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
        [TestMethod()]
        public void DecisionTest()
        {
            string[] testShapeData = new string[] { "DecisionTest", "1", "1", "1", "1" };
            Decision test = new Decision(ShapeType.DECISION, testShapeData);

            Assert.AreEqual(test.ShapeType, ShapeType.DECISION);
            Assert.AreEqual(test.Text, testShapeData[0]);
            Assert.AreEqual(test.X, int.Parse(testShapeData[1]));
            Assert.AreEqual(test.Y, int.Parse(testShapeData[2]));
            Assert.AreEqual(test.Height, int.Parse(testShapeData[3]));
            Assert.AreEqual(test.Width, int.Parse(testShapeData[4]));
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void DrawTest()
        {
            MockGraphics mock = new MockGraphics();
            decision.Draw(mock);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(mock.testDatas[i], decision.ShapeDatas[i]);
            }
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void IsPointInRangeTest()
        {
            Assert.IsFalse(decision.IsPointInRange(101, 0));
            Assert.IsTrue(decision.IsPointInRange(50, 50));
            Assert.IsTrue(true);
        }
    }
}