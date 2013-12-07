using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Testbed.Physics
{
    static class Extensions
    {
        public static void DrawLine(this PrimitiveBatch primBatch, Line line, Color color)
        {
            primBatch.Begin(PrimitiveType.LineList);

            primBatch.AddVertex((Vector2)line.Start, color);
            primBatch.AddVertex((Vector2)line.End, color);

            primBatch.End();
        }
    }
}
