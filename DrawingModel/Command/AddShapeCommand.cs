using DrawingModel;
using DrawingShape;
namespace DrawingCommand
{
    public class AddShapeCommand : ICommand
    {
        Shape _shape;
        DrawingModel.ShapeFactory _shapeFactory = new DrawingModel.ShapeFactory();
        Model _model;

        public AddShapeCommand(in Model model, in Shape shape)
        {
            _shape = shape;
            _model = model;
        }
        public AddShapeCommand(in Model model, in ShapeType shapeType, in string[] inputDatas)
        {
            Shape shape = _shapeFactory.CreateShape(shapeType, inputDatas);
            _shape = shape;
            _model = model;
        }

        public void Execute()
        {
            _model.AddShape(_shape);
        }
        public void UnExecute()
        {
            _model.RemoveShape(_shape);
        }
    }
}
