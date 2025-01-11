using System;
using System.Collections.Generic;
namespace DrawingCommand
{
    public class CommandManager
    {
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();

        public void Execute(ICommand cmd)
        {
            cmd.Execute();
            _undo.Push(cmd);    // push command 進 undo stack
            _redo.Clear();      // 清除redo stack
        }

        public void Undo()
        {
            if (_undo.Count <= 0)
                throw new Exception("Cannot Undo exception\n");
            ICommand cmd = _undo.Pop();
            _redo.Push(cmd);
            cmd.UnExecute();
        }

        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception("Cannot Redo exception\n");
            ICommand cmd = _redo.Pop();
            _undo.Push(cmd);
            cmd.Execute();
        }

        public void Reload()
        {
            _redo.Clear();
            _undo.Clear();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _undo.Count != 0;
            }
        }
    }
}
