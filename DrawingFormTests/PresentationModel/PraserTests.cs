using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingForm.PresentationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingForm.PresentationModel.Tests
{
    [TestClass()]
    public class PraserTests
    {
        [TestMethod()]
        public void PraserTest()
        {
            Praser praser = new Praser();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Praser praser = new Praser();
            Assert.AreEqual(praser.ToString(DrawingShape.ShapeType.START), "Start");
        }

        [TestMethod()]
        public void ToShapeTypeTest()
        {
            Praser praser = new Praser();
            Assert.AreEqual(praser.ToShapeType("Start"), DrawingShape.ShapeType.START);
        }
    }
}