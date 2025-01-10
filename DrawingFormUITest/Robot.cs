using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;
using System.Windows.Automation;
using System.Windows;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Windows.Input;
using System.Windows.Forms;
using OpenQA.Selenium.Interactions;
using System.Reflection;
using System.Drawing.Printing;

using System.IO;
using static System.Windows.Forms.AxHost;
using static System.Net.Mime.MediaTypeNames;

namespace DrawingForm.Tests
{
    public class Robot
    {
        private WindowsDriver<WindowsElement> _driver;
        private Dictionary<string, string> _windowHandles;
        private string _root;
        private const string CONTROL_NOT_FOUND_EXCEPTION = "The specific control is not found!!";
        private const string WIN_APP_DRIVER_URI = "http://127.0.0.1:4723";

        public int CoordX
        {
            get { return _driver.Manage().Window.Position.X; }
        }

        public int CoordY
        {
            get { return _driver.Manage().Window.Position.Y; }
        }

        // constructor
        public Robot(string targetAppPath, string root)
        {
            this.Initialize(targetAppPath, root);
        }

        // initialize
        public void Initialize(string targetAppPath, string root)
        {
            _root = root;
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", targetAppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri(WIN_APP_DRIVER_URI), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _windowHandles = new Dictionary<string, string>
            {
                { _root, _driver.CurrentWindowHandle }
            };
        }

        // clean up
        public void CleanUp()
        {
            SwitchTo(_root);
            _driver.CloseApp();
            _driver.Dispose();
        }

        // test
        public void SwitchTo(string formName)
        {
            if (_windowHandles.ContainsKey(formName))
            {
                _driver.SwitchTo().Window(_windowHandles[formName]);
            }
            else
            {
                foreach (var windowHandle in _driver.WindowHandles)
                {
                    _driver.SwitchTo().Window(windowHandle);
                    try
                    {
                        _driver.FindElementByAccessibilityId(formName);
                        _windowHandles.Add(formName, windowHandle);
                        return;
                    }
                    catch
                    {

                    }
                }
            }
        }

        // test
        public void Sleep(Double time)
        {
            Thread.Sleep(TimeSpan.FromSeconds(time));
        }

        // test
        public void ClickButton(string name)
        {
            _driver.FindElementByName(name).Click();
        }

        // test
        public void ClickTabControl(string name)
        {
            var elements = _driver.FindElementsByName(name);
            foreach (var element in elements)
            {
                if ("ControlType.TabItem" == element.TagName)
                    element.Click();
            }
        }

        // test
        public void CloseWindow()
        {
            SendKeys.SendWait("%{F4}");
        }

        // test
        public void CloseMessageBox()
        {
            _driver.FindElementByName("取消").Click();
        }

        public void ConfirmEditText()
        {
            _driver.FindElementByName("確定").Click();
        }

        // test
        public void ClickDataGridViewCellBy(string name, int rowIndex, string columnName)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            dataGridView.FindElementByName($"{columnName} 資料列 {rowIndex}").Click();
        }

        public void AssertTextEditPopUp()
        {
            try
            {
                WindowsElement element = _driver.FindElementByName("編輯文字");
                Sleep(1);
                CloseMessageBox();
            }
            catch (Exception)
            {
                Console.WriteLine("TextEditForm do not pop-up.");
                Assert.Fail();
            }
        }

        // test
        public void AssertChecked(string name, bool state)
        {
            WindowsElement element = _driver.FindElementByName(name);
            Assert.AreEqual(state ? "True" : "False", element.GetAttribute("HasKeyboardFocus"));
        }

        public void InputText(string text)
        {
            WindowsElement element = _driver.FindElementByAccessibilityId("TextEditForm");
            var textBox = element.FindElementByAccessibilityId("textBox");
            textBox.SendKeys(text);
            ConfirmEditText();
        }

        public void UndoUntilDisable()
        {
            WindowsElement element = _driver.FindElementByName("Undo");
            while (element.Enabled)
            {
                element.Click();
            }
        }

        // test
        public void AssertTextFromGridView(string name, int rowIndex, string text)
        {
            WindowsElement element = _driver.FindElementByAccessibilityId(name);
            var rowDatas = element.FindElementByName($"資料列 {rowIndex}").FindElementsByXPath("//*");
            Assert.AreEqual(text, rowDatas[5].Text.Replace("(null)", ""));
        }

        public string GetTextFormGridView(string name, int rowIndex)
        {
            WindowsElement element = _driver.FindElementByAccessibilityId(name);
            var rowDatas = element.FindElementByName($"資料列 {rowIndex}").FindElementsByXPath("//*");
            return rowDatas[5].Text.Replace("(null)", "");
        }

        // test
        public void AssertDataGridViewRowDataBy(string name, int rowIndex, string[] data)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            var rowDatas = dataGridView.FindElementByName($"資料列 {rowIndex}").FindElementsByXPath("//*");

            // FindElementsByXPath("//*") 會把 "row" node 也抓出來，因此 i 要從 1 開始以跳過 "row" node
            for (int i = 6; i < rowDatas.Count; i++)
            {
                /* 機器人在畫圖時有時會誤差1 */
                int diff = int.Parse(data[i - 5]) - int.Parse(rowDatas[i].Text.Replace("(null)", ""));
                Assert.IsTrue(Math.Abs(diff) <= 1);
                //Assert.AreEqual(data[i - 6], rowDatas[i].Text.Replace("(null)", ""));
            }
            Assert.AreEqual(data[0], rowDatas[4].Text.Replace("(null)", ""));
        }

        public void AssertDataGridViewRowCount(string name, int count)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            var rows = dataGridView.FindElementsByXPath(".//Custom"); // 假設每一行是 Custom 類型元素
            Assert.AreEqual(count, rows.Count - 1);
        }

        public void OutputXml()
        {
            File.WriteAllText(@"output.xml", _driver.PageSource);
            //"C:\Users\user\Desktop\material_list_2025-01-01_20.39.05.txt"
        }

        public void MouseDown()
        {
            var actions = new Actions(_driver);
            actions.ClickAndHold().Perform();
            Sleep(0.05d);
        }

        public void MouseUp()
        {
            var actions = new Actions(_driver);
            actions.Release().Perform();
            Sleep(0.05d);
        }

        public void MouseMove(int x, int y)
        {
            var actions = new Actions(_driver);
            Sleep(0.1d);
            actions.MoveByOffset(x, y).Perform();
            Sleep(0.1d);
        }

        public void MouseDoubleClick()
        {
            var actions = new Actions(_driver);
            actions.DoubleClick().Perform();

            Sleep(0.05d);
        }
        public void MoveToElement(string name)
        {
            var actions = new Actions(_driver);
            WindowsElement element = _driver.FindElementByName(name);
            actions.MoveToElement(element).Perform();
        }

        public void SelectShape(string name, string type)
        {
            var element = _driver.FindElementByAccessibilityId(name);
            new Actions(_driver).MoveToElement(element).Perform();
            MouseMove(25, 0);
            new Actions(_driver).Click().Perform();
            element.FindElementByName(type).Click();
        }

        public void InputShapeDatas(string[] shapeDatas)
        {
            string[] textBoxs = new string[5]
            {
                "TextBoxText",
                "TextBoxX",
                "TextBoxY",
                "TextBoxH",
                "TextBoxW"
            };
            int i = 0;
            foreach (var shapeData in shapeDatas) { 
                var element = _driver.FindElementByAccessibilityId(textBoxs[i]);
                element.Clear();
                element.SendKeys(shapeData);              
                i++;
            }
        }

        public void Test()
        {

        }
    }
}