using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrawingModelTests.Command
{
    internal class MockCommand : DrawingCommand.ICommand
    {
        public void Execute() { }
        public void UnExecute() { }
    }
}
