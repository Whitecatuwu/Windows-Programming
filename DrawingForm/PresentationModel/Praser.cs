using DrawingShape;

using System.Collections.Generic;

namespace DrawingForm.PresentationModel
{
    class Praser
    {
        Dictionary<string, ShapeType> _stringToShapeType = new Dictionary<string, ShapeType>() { };
        Dictionary<ShapeType, string> _shapeTypeToString = new Dictionary<ShapeType, string>() { };
        public Praser()
        {
            _stringToShapeType.Add("Start", ShapeType.START);
            _stringToShapeType.Add("Terminator", ShapeType.TERMINATOR);
            _stringToShapeType.Add("Process", ShapeType.PROCESS);
            _stringToShapeType.Add("Decision", ShapeType.DECISION);

            _shapeTypeToString.Add(ShapeType.START, "Start");
            _shapeTypeToString.Add(ShapeType.TERMINATOR, "Terminator");
            _shapeTypeToString.Add(ShapeType.PROCESS, "Process");
            _shapeTypeToString.Add(ShapeType.DECISION, "Decision");
        }

        public string ToString(in ShapeType shapeType)
        {
            return _shapeTypeToString[shapeType];
        }


        public ShapeType ToShapeType(in string str)
        {
            return _stringToShapeType[str];
        }
    }
}
