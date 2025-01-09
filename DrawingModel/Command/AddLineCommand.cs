using DrawingShape;
using DrawingModel;
using System.Runtime.InteropServices;

namespace DrawingCommand
{
    public class AddLineCommand : ICommand
    {
        ConnectionPoint _first;
        ConnectionPoint _second;
        Line _line;
        Model _model;

        public AddLineCommand(Model model, ConnectionPoint first, ConnectionPoint second, in Line line)
        {
            _model = model;
            _first = first;
            _second = second;
            _line = line;
        }

        public void Execute()
        {
            _model.AddLine(_first, _second, _line);
        }

        public void UnExecute()
        {
            _model.RemoveLine(_line);
        }
    }
}
