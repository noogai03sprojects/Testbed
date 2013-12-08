using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Testbed.Physics
{
    struct AABB
    {
        enum SunderlandCell
        {
            TopLeft = 0,
            TopCentre =1,
            TopRight = 2,
            CentreLeft = 3,
            Centre = 4,
            CentreRight = 5,
            BottomLeft = 6,
            BottomCentre = 7,
            BottomRight = 8,
            Error = -1
        }
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
        public Vector2I Size
        {
            get
            {
                return new Vector2I(Width, Height);
            }
        }

        public Vector2I Centre
        {
            get
            {
                return UpperBound + Size / 2;
            }
            set
            {
                Vector2I tempSize = Size;
                UpperBound = value - tempSize / 2;
                LowerBound = value + tempSize / 2;
            }
        }

        public void SetPosition(Vector2I topLeft)
        {
            Vector2I tempSize = Size;
            this.UpperBound = topLeft;
            this.LowerBound = topLeft + tempSize;
        }
        public void Move(Vector2I delta)
        {
            //delta.Y *= -1;
            UpperBound += delta;
            LowerBound += delta;
        }

        public bool Contains(Vector2I point)
        {
            if (point.X > LowerBound.X)
                return false;
            if (point.X < UpperBound.X)
                return false;
            if (point.Y > LowerBound.Y)
                return false;
            if (point.Y < UpperBound.Y)
                return false;

            return true;
        }
        private SunderlandCell CalculateSunderlandCell(Vector2I v)
        {
            if (v.X < UpperBound.X)
            {
                if (v.Y < UpperBound.Y)
                    return SunderlandCell.TopLeft;
                if (v.Y >= UpperBound.Y && v.Y <= LowerBound.Y)
                    return SunderlandCell.CentreLeft;
                if (v.Y > LowerBound.Y)
                    return SunderlandCell.BottomLeft;
            }
            if (v.X >= UpperBound.X && v.X <= LowerBound.X)
            {
                if (v.Y < UpperBound.Y)
                    return SunderlandCell.TopCentre;
                if (v.Y >= UpperBound.Y && v.Y <= LowerBound.Y)
                    return SunderlandCell.Centre;
                if (v.Y > LowerBound.Y)
                    return SunderlandCell.BottomCentre;
            }
            if (v.X > LowerBound.X)
            {
                if (v.Y < UpperBound.Y)
                    return SunderlandCell.TopRight;
                if (v.Y >= UpperBound.Y && v.Y <= LowerBound.Y)
                    return SunderlandCell.CentreRight;
                if (v.Y > LowerBound.Y)
                    return SunderlandCell.BottomRight;
            }
            return SunderlandCell.Error;
        }

        public bool IsContained(Line line)
        {
            if (Contains(line.Start) && Contains(line.End))
                return true;
            else
                return false;
        }
        public bool Intersects(Line line)
        {            
            SunderlandCell A = CalculateSunderlandCell(line.Start);
            SunderlandCell B = CalculateSunderlandCell(line.End);

            if (A == B)
            {
                return false;
            }
            return true;
        }
    }
}
