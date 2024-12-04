using System;
using DrawingShape;

namespace DrawingModel
{
    public class ShapeFactory
    {
        public Shape CreateShape(ShapeType shapeType, string[] shapeDatas)
        {
            switch (shapeType)
            {
                case ShapeType.START:
                    return new Start(shapeType, shapeDatas);
                case ShapeType.TERMINATOR:
                    return new Terminator(shapeType, shapeDatas);
                case ShapeType.PROCESS:
                    return new Process(shapeType, shapeDatas);
                case ShapeType.DECISION:
                    return new Decision(shapeType, shapeDatas);
                default:
                    throw new ArgumentException("Invalid shape type");
            }
        }
    }
}
