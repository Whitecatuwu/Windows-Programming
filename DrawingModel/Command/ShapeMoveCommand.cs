using System;
using System.Collections.Generic;
using DrawingModel;
using DrawingShape;

namespace DrawingCommand
{
    public class ShapeMoveCommand : ICommand
    {
        Model _model;
        SortedSet<Shape> _shapes;
        SortedDictionary<Shape, Tuple<int, int>> _prePositions;
        SortedDictionary<Shape, Tuple<int, int>> _newPositions = new SortedDictionary<Shape, Tuple<int, int>> { };

        public ShapeMoveCommand(Model model, SortedSet<Shape> shapes, SortedDictionary<Shape, Tuple<int, int>> prePositions)
        {
            _model = model;
            _shapes = shapes;
            _prePositions = prePositions;
            foreach (var shape in shapes)
            {
                _newPositions.Add(shape, new Tuple<int, int>(shape.X, shape.Y));
            }
        }

        public void Execute()
        {
            foreach (var shape in _newPositions.Keys)
            {
                var pos = _newPositions[shape];
                _model.MoveShape(shape, pos.Item1, pos.Item2);
            }
        }

        public void UnExecute()
        {
            foreach (var shape in _prePositions.Keys)
            {
                var pos = _prePositions[shape];
                _model.MoveShape(shape, pos.Item1, pos.Item2);
            }
        }
    }
}
