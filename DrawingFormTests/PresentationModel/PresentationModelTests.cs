using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using DrawingShape;

namespace DrawingForm.PresentationModel.Tests
{
    [TestClass()]
    public class PresentationModelTests
    {
        Model model = new Model();
        [TestMethod()]
        public void PresentationModelTest()
        {
            PresentationModel presentationModel = new PresentationModel(model);
        }

        [TestMethod()]
        public void GetShapeDataTest()
        {
            PresentationModel presentationModel = new PresentationModel(model);
            string[] shapeData = new string[] { "DecisionTest", "1", "1", "100", "100" };
            model.AddShape(ShapeType.START, shapeData);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(presentationModel.GetShapeData(0)[i + 2], shapeData[i]);
            }
        }

        [TestMethod()]
        public void SetDrawingModeTest()
        {
            PresentationModel presentationModel = new PresentationModel(model);
            presentationModel.SetDrawingMode(DrawingMode.NULL);
            presentationModel.SetDrawingMode(DrawingMode.TERMINATOR);
            Assert.IsTrue(presentationModel.IsTerminatorEnable);

            presentationModel.SetDrawingMode(DrawingMode.START);
            Assert.IsTrue(presentationModel.IsStartEnable);

            presentationModel.SetDrawingMode(DrawingMode.POINTER);
            Assert.IsTrue(presentationModel.IsSelectEnable);

            presentationModel.SetDrawingMode(DrawingMode.DECISION);
            Assert.IsTrue(presentationModel.IsDecisionEnable);

            presentationModel.SetDrawingMode(DrawingMode.PROCESS);
            Assert.IsTrue(presentationModel.IsProcessEnable);

            presentationModel.SetDrawingMode(DrawingMode.LINE);
            Assert.IsTrue(presentationModel.IsLineEnable);
        }

        [TestMethod()]
        public void InputDataTest()
        {
            PresentationModel presentationModel = new PresentationModel(model);
            presentationModel.InputData(0, "null");
            presentationModel.InputData(114514, "null");

            presentationModel.InputData(1, "-1");
            Assert.AreEqual(presentationModel.GetXStateColor, 0x78FF0000);
            presentationModel.InputData(1, "***");
            Assert.AreEqual(presentationModel.GetXStateColor, 0x78FF0000);
            presentationModel.InputData(1, "1");
            Assert.AreEqual(presentationModel.GetXStateColor, 0x78000000);
            Assert.IsFalse(presentationModel.IsAddButtonEnabled);

            presentationModel.InputData(2, "-1");
            Assert.AreEqual(presentationModel.GetYStateColor, 0x78FF0000);
            presentationModel.InputData(2, "***");
            Assert.AreEqual(presentationModel.GetYStateColor, 0x78FF0000);
            presentationModel.InputData(2, "1");
            Assert.AreEqual(presentationModel.GetYStateColor, 0x78000000);
            Assert.IsFalse(presentationModel.IsAddButtonEnabled);

            presentationModel.InputData(3, "-1");
            Assert.AreEqual(presentationModel.GetHStateColor, 0x78FF0000);
            presentationModel.InputData(3, "***");
            Assert.AreEqual(presentationModel.GetHStateColor, 0x78FF0000);
            presentationModel.InputData(3, "1");
            Assert.AreEqual(presentationModel.GetHStateColor, 0x78000000);
            Assert.IsFalse(presentationModel.IsAddButtonEnabled);

            presentationModel.InputData(4, "-1");
            Assert.AreEqual(presentationModel.GetWStateColor, 0x78FF0000);
            presentationModel.InputData(4, "***");
            Assert.AreEqual(presentationModel.GetWStateColor, 0x78FF0000);
            presentationModel.InputData(4, "1");
            Assert.AreEqual(presentationModel.GetWStateColor, 0x78000000);

            Assert.IsTrue(presentationModel.IsAddButtonEnabled);

            for (int i = 1; i <= 4; i++)
            {
                Assert.AreEqual(presentationModel.InputDatas[i], "1");
            }
            presentationModel.InputDatas = new string[] { "test", "2", "2", "2", "2" };
            for (int i = 1; i <= 4; i++)
            {
                Assert.AreEqual(presentationModel.InputDatas[i], "2");
            }

        }

        [TestMethod()]
        public void AddShapeTest()
        {
            PresentationModel presentationModel = new PresentationModel(model);
            presentationModel.AddShape(null);
            presentationModel.InputData(1, "1");
            presentationModel.InputData(2, "1");
            presentationModel.InputData(3, "1");
            presentationModel.InputData(4, "1");
            presentationModel.AddShape("Start");
        }

        [TestMethod()]
        public void NotifyTest()
        {
            PresentationModel presentationModel = new PresentationModel(model);
            PrivateObject privateObject = new PrivateObject(presentationModel);
            privateObject.Invoke("Notify", "");
           
        }
    }
}