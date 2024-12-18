using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModelTests.Command;

namespace DrawingCommand.Tests
{
    [TestClass()]
    public class CommandManagerTests
    {
        
        [TestMethod()]
        public void ExecuteTest()
        {
            CommandManager commandManager = new CommandManager();
            commandManager.Execute(new MockCommand());
            commandManager.Execute(new MockCommand());
        }

        [TestMethod()]
        public void UndoTest()
        {
            CommandManager commandManager = new CommandManager();
            commandManager.Execute(new MockCommand());
            commandManager.Execute(new MockCommand());
            commandManager.Undo();
            commandManager.Undo();
            Assert.ThrowsException<Exception>(commandManager.Undo);
        }

        [TestMethod()]
        public void RedoTest()
        {
            CommandManager commandManager = new CommandManager();
            commandManager.Execute(new MockCommand());
            commandManager.Execute(new MockCommand());
            Assert.ThrowsException<Exception>(commandManager.Redo);
        }
    }
}