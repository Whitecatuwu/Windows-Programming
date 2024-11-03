using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingShape
{
    internal interface IDrawable
    {
        void Draw(DrawingModel.IGraphics graphics);
    }
}
