using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

/// <summary>
/// Summary description for MainFormUITest
/// </summary>
namespace DrawingForm.Tests
{
    [TestClass()]
    public class DrawingFormUITest
    {
        string[] _toolBarBtns = new string[5] { "Start", "Terminator", "Process", "Line", "Select" };

        private Robot _robot;
        //private const string APP_NAME =
        //"Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        private const string CALCULATOR_TITLE = "HW7";
        //private const string EXPECTED_VALUE = "顯示是 444";
        //private const string RESULT_CONTROL_NAME = "CalculatorResults";



        /// <summary>
        /// Launches the Calculator
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            //var projectName = "hw3";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var targetAppPath = Path.Combine(solutionPath, "DrawingForm", "bin", "Debug", "hw2.exe");
            File.WriteAllText("abc.txt", targetAppPath.ToString());
            //Console.WriteLine(targetAppPath);
            _robot = new Robot(targetAppPath, CALCULATOR_TITLE);
        }
        /// <summary>
        /// Closes the launched program
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            _robot.CleanUp();
        }

        [TestMethod]
        public void TestBasisRedoUndo()
        {
            BasisRedoUndo();
        }

        //[TestMethod]
        public void TestShapeMoveRedoUndo()
        {
            ShapeMoveRedoUndo();
        }

        //[TestMethod]
        public void TestTextMoveRedoUndo()
        {
            TextMoveRedoUndo();
        }

        //[TestMethod]
        public void TestTextEditUndoRedoUndo()
        {
            TextEditRedoUndo();
        }

        //[TestMethod]
        public void TestGridViewRedoUndo()
        {
            GridViewRedoUndo();
        }

        //[TestMethod]
        public void TestMeta()
        {
            _robot.Sleep(0.2);
            BasisRedoUndo();
            _robot.UndoUntilDisable();

            _robot.Sleep(0.2);
            ShapeMoveRedoUndo();
            _robot.UndoUntilDisable();

            _robot.Sleep(0.2);
            TextMoveRedoUndo();
            _robot.UndoUntilDisable();

            _robot.Sleep(0.2);
            TextEditRedoUndo();
            _robot.UndoUntilDisable();

            _robot.Sleep(0.2);
            GridViewRedoUndo();
            //_robot.UndoUntilDisable();

        }

        private void AssertToolBar(string name)
        {
            foreach (var btn in _toolBarBtns)
            {
                if (btn == name)
                    _robot.AssertChecked(btn, true);
                else
                    _robot.AssertChecked(btn, false);
            }
        }
        private void DrawStart(int index)
        {
            _robot.ClickButton("Start");
            AssertToolBar("Start");

            _robot.MouseMove(200, 100);
            _robot.MouseDown();
            _robot.MouseMove(200, 200);
            _robot.MouseUp();
            _robot.AssertDataGridViewRowDataBy("shapeGridView", index, new string[] { "Start", "220", "139", "200", "200" });
        }
        private void DrawTerminator(int index)
        {
            _robot.ClickButton("Terminator");
            AssertToolBar("Terminator");

            _robot.MouseMove(200, 200);
            _robot.MouseDown();
            _robot.MouseMove(410, 210);
            _robot.MouseUp();
            _robot.AssertDataGridViewRowDataBy("shapeGridView", index, new string[] { "Terminator", "245", "239", "210", "410" });
        }
        private void DrawProcess(int index)
        {
            _robot.ClickButton("Process");
            AssertToolBar("Process");

            _robot.MouseMove(200, 300);
            _robot.MouseDown();
            _robot.MouseMove(230, 230);
            _robot.MouseUp();
            _robot.AssertDataGridViewRowDataBy("shapeGridView", index, new string[] { "Process", "269", "339", "230", "230" });
        }
        private void DrawDecision(int index)
        {
            _robot.ClickButton("Decision");
            AssertToolBar("Decision");

            _robot.MouseMove(400, 300);
            _robot.MouseDown();
            _robot.MouseMove(240, 240);
            _robot.MouseUp();
            //
            _robot.AssertDataGridViewRowDataBy("shapeGridView", index, new string[] { "Decision", "492", "339", "240", "240" });
        }
        private void DrawLine()
        {
            _robot.ClickButton("Line");
            AssertToolBar("Line");

            _robot.MouseMove(202, 101);
            _robot.MouseDown();
            _robot.MouseMove(1, 1);
            _robot.MouseMove(130, 100);
            _robot.MouseUp();
        }

        private void BasisRedoUndo()
        {
            DrawStart(0);
            DrawTerminator(1);
            DrawLine();
            DrawProcess(2);
            DrawDecision(3);

            _robot.ClickButton("Undo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 3);
            _robot.ClickButton("Redo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 4);
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 3, new string[] { "Decision", "492", "339", "240", "240" });
            _robot.ClickButton("Undo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 3);
            _robot.ClickButton("Undo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 2);
            _robot.ClickButton("Undo"); //undo line
            _robot.AssertDataGridViewRowCount("shapeGridView", 2);
            _robot.ClickButton("Undo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 1);
            _robot.ClickButton("Redo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 2);
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 1, new string[] { "Terminator", "245", "239", "210", "410" });
            _robot.ClickButton("Redo"); //redo line
            _robot.AssertDataGridViewRowCount("shapeGridView", 2);
            _robot.ClickButton("Redo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 3);
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 2, new string[] { "Process", "269", "339", "230", "230" });
            _robot.ClickButton("Redo");
            _robot.AssertDataGridViewRowCount("shapeGridView", 4);
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 3, new string[] { "Decision", "492", "339", "240", "240" });

        }
        private void ShapeMoveRedoUndo()
        {
            DrawStart(0);
            DrawTerminator(1);
            DrawLine();
            _robot.MoveToElement("Start");

            _robot.MouseMove(400, 300);
            _robot.MouseMove(-150, -150);

            _robot.MouseDown();
            _robot.MouseMove(0, 50);
            _robot.MouseUp();
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, new string[] { "Start", "220", "189", "200", "200" });

            _robot.MouseDown();
            _robot.MouseMove(50, 0);
            _robot.MouseUp();
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, new string[] { "Start", "270", "189", "200", "200" });

            _robot.MouseDown();
            _robot.MouseMove(0, -70);
            _robot.MouseUp();
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, new string[] { "Start", "270", "119", "200", "200" });

            _robot.ClickButton("Undo");
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, new string[] { "Start", "270", "189", "200", "200" });

            _robot.ClickButton("Undo");
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, new string[] { "Start", "220", "189", "200", "200" });

            _robot.ClickButton("Redo");
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, new string[] { "Start", "270", "189", "200", "200" });

            _robot.ClickButton("Redo");
            _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, new string[] { "Start", "270", "119", "200", "200" });
        }

        private void TextMoveRedoUndo()
        {
            DrawStart(0);
            _robot.MouseMove(-100, -110);

            _robot.MouseDown();
            _robot.MouseMove(0, 1);
            _robot.MouseMove(0, 49);
            _robot.MouseUp();
            _robot.MouseDoubleClick();
            _robot.AssertTextEditPopUp();

            _robot.ClickButton("Undo");
            _robot.MoveToElement("Start");
            _robot.MouseMove(200, 100);
            _robot.MouseMove(100, 90);
            _robot.MouseDoubleClick();
            _robot.AssertTextEditPopUp();

            _robot.ClickButton("Redo");
            _robot.MoveToElement("Start");
            _robot.MouseMove(200, 100);
            _robot.MouseMove(100, 90 + 50);
            _robot.MouseDoubleClick();
            _robot.AssertTextEditPopUp();
        }

        private void TextEditRedoUndo()
        {
            DrawStart(0);
            string preText = _robot.GetTextFormGridView("shapeGridView", 0);
            _robot.MouseMove(-100, -110);
            _robot.MouseDoubleClick();
            _robot.InputText("qwertyuiop");
            _robot.AssertTextFromGridView("shapeGridView", 0, "qwertyuiop");
            _robot.ClickButton("Undo");
            _robot.AssertTextFromGridView("shapeGridView", 0, preText);
            _robot.ClickButton("Redo");
            _robot.AssertTextFromGridView("shapeGridView", 0, "qwertyuiop");
        }

        private void GridViewRedoUndo()
        {
            string[][] shapeDataList = new string[4][]
            {
                new string[] { "Start", "220", "139", "200", "200" },
                new string[] { "Terminator", "245", "239", "210", "410" },
                new string[] { "Process", "269", "339", "230", "230" },
                new string[] { "Decision", "492", "339", "240", "240" }
            };
            int i = 0;
            foreach (var shapeData in shapeDataList)
            {
                _robot.SelectShape("selectedShapeType", shapeData[0]);
                _robot.InputShapeDatas(shapeData);
                _robot.ClickButton("新增");
                _robot.AssertDataGridViewRowDataBy("shapeGridView", i, shapeData);
                i++;
            }
            for(int j = 0; j < 4; j++)
            {
                _robot.ClickButton("Undo");
                _robot.AssertDataGridViewRowCount("shapeGridView", 3-j);
            }
            for (int j = 0; j < 4; j++)
            {
                _robot.ClickButton("Redo");
                _robot.AssertDataGridViewRowDataBy("shapeGridView", j, shapeDataList[j]);
            }
            for(int j = 0; j < 4; j++)
            {
                _robot.ClickDataGridViewCellBy("shapeGridView", 0, "刪除");
                _robot.AssertDataGridViewRowCount("shapeGridView", 3 - j);
            }
            for (int j = 3; j >= 0; j--)
            {
                _robot.ClickButton("Undo");
                _robot.AssertDataGridViewRowDataBy("shapeGridView", 0, shapeDataList[j]);
            }
        }
    }
}