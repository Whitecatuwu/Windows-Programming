using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
/// <summary>
/// Summary description for MainFormUITest
/// </summary>
namespace MainFormUITest
{
    [TestClass()]
    public class DrawingFormUITest
    {
        private Robot _robot;
        private const string APP_NAME =
       "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        private const string CALCULATOR_TITLE = "HW7";
        private const string EXPECTED_VALUE = "顯示是 444";
        private const string RESULT_CONTROL_NAME = "CalculatorResults";
        /// <summary>
        /// Launches the Calculator
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            var projectName = "Hw3";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var targetAppPath = Path.Combine(solutionPath, projectName,"DrawingForm", "bin", "Debug","hw2.exe");
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

        /// <summary>
        /// Runs the script: 123 + 321 =
        /// </summary>
        private void RunScriptAdd()
        {
            _robot.ClickButton("Select");
            /*_robot.ClickButton("一");
            _robot.ClickButton("二");
            _robot.ClickButton("三");
            _robot.ClickButton("加");
            _robot.ClickButton("三");
            _robot.ClickButton("二");
            _robot.ClickButton("一");
            _robot.ClickButton("等於");
            */
        }

        /// <summary>
        /// Tests that the result of 123 + 321 should be 444
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            RunScriptAdd();
            _robot.AssertText(RESULT_CONTROL_NAME, EXPECTED_VALUE);
        }
    }
}