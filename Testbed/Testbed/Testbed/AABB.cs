using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testbed.Physics
{
    struct AABB
    {
        Vector2I UpperBound, LowerBound;

        public AABB(Vector2I upperBound, Vector2I lowerBound)
        {
            UpperBound = upperBound;
            LowerBound = lowerBound;
        }
        public int Width
        {
            get
            {
                return LowerBound.X - UpperBound.X;
            }
        }
        public int Height
        {
            get
            {
                return LowerBound.Y - UpperBound.Y;
            }
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(UpperBound.X, UpperBound.Y, Width, Height);
        }
        public Vector2I TopLeft
        {
            get
            {
                return UpperBound;
            }
            set
            {
                UpperBound = value;
            }
        }

        public Vector2I TopRight
        {
            get
            {
                return new Vector2I(UpperBound.X + Width, UpperBound.Y);
            }
        }
        public Vector2I BottomRight
        {
            get
            {
                return LowerBound;
            }
            set
            {
                LowerBound = value;
            }
        }

        public Vector2I BottomLeft
        {
            get
            {
                return new Vector2I(LowerBound.X - Width, LowerBound.Y);
            }
        }
    }
}
