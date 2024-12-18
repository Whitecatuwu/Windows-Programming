using DrawingModel;
using System;
using System.Collections.Generic;
using DrawingShape;

namespace DrawingCommand
{
    public class TextChangeCommand : ICommand
    {
        Model _model;
        string _preText;
        string _newText;

        public TextChangeCommand(Model model, string newText)
        {
            _model = model;
            _preText = model.Shapes[model.TextEditShapeIndex].Text;
            _newText = newText;
        }
        public void Execute()
        {
            _model.EditShapeText(_newText);
        }
        public void UnExecute()
        {
            _model.EditShapeText(_preText);
        }
    }
}
