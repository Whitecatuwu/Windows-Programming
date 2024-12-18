using System;
using System.Collections.Generic;
using System.Linq;


namespace DrawingCommand
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
