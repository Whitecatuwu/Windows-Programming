using DrawingModel;
using DrawingShape;

namespace DrawingCommand
{
    public class DeleteShapeCommand : ICommand
    {
        Shape _shape;
        DrawingModel.ShapeFactory _shapeFactory = new DrawingModel.ShapeFactory();
        Model _model;
        int removeShapeIndex = -1;

        public DeleteShapeCommand(in Model model, in Shape shape)
        {
            _shape = shape;
            _model = model;
            removeShapeIndex = _model.Shapes.IndexOf(shape);
        }
        public DeleteShapeCommand(in Model model, in int index)
        {
            _model = model;
            removeShapeIndex = index;
            _shape = _model.Shapes[index];
        }

        public void Execute()
        {
            _model.RemoveShape(removeShapeIndex);
        }
        public void UnExecute()
        {
            _model.InsertShape(removeShapeIndex, _shape);
        }
    }
}
