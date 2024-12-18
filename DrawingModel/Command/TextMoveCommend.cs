using System;
using System.Collections.Generic;
using DrawingModel;
using DrawingShape;

namespace DrawingCommand
{
    public class TextMoveCommend : ICommand
    {
        Model _model;
        Shape _shape;
        Tuple<int, int> _prePosition;
        Tuple<int, int> _newPosition;

        public TextMoveCommend(Model model, Shape shape, Tuple<int, int> prePosition)
        {
            _model = model;
            _shape = shape;
            _prePosition = prePosition;
            _newPosition = new Tuple<int, int>(shape.TextBox_X, shape.TextBox_Y);
        }

        public void Execute()
        {
            var pos = _newPosition;
            _model.MoveTextBox(_shape, pos.Item1, pos.Item2);
        }

        public void UnExecute()
        {
            var pos = _prePosition;
            _model.MoveTextBox(_shape, pos.Item1, pos.Item2);
        }
    }
}

