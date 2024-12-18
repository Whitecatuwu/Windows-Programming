using DrawingShape;
using DrawingModel;

namespace DrawingCommand
{
    public class AddLineCommand : ICommand
    {
        Model _model;
        Line _line;
        public AddLineCommand(in Model model, in Line line)
        {
            _model = model;
            _line = line;
        }

        public void Execute()
        {
            _model.AddLine(_line);
        }

        public void UnExecute()
        {
            _model.RemoveLine(_line);
        }
    }
}
