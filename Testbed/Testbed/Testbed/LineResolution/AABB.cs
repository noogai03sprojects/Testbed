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
            //SunderlandCell A = CalculateSunderlandCell(line.Start);
            //SunderlandCell B = CalculateSunderlandCell(line.End);

            //if (A == B)
            //{
            //    return false;
            //}
            //int cellTotal = (int)A + (int)B;
            //if (cellTotal < 4)            
            //    return false;
            //if (cellTotal > 9)
            //    return false;
            
            
            //return true;
            return SegmentIntersectRectangle(UpperBound.X, UpperBound.Y, LowerBound.X, LowerBound.Y, line.Start.X, line.Start.Y, line.End.X, line.End.Y);
            }

        static bool SegmentIntersectRectangle(int a_rectangleMinX,
                                 int a_rectangleMinY,
                                 int a_rectangleMaxX,
                                 int a_rectangleMaxY,
                                 int a_p1x,
                                 int a_p1y,
                                 int a_p2x,
                                 int a_p2y)
        {
            // Find min and max X for the segment

            int minX = a_p1x;
            int maxX = a_p2x;

            if (a_p1x > a_p2x)
            {
                minX = a_p2x;
                maxX = a_p1x;
            }

            // Find the intersection of the segment's and rectangle's x-projections

            if (maxX > a_rectangleMaxX)
            {
                maxX = a_rectangleMaxX;
            }

            if (minX < a_rectangleMinX)
            {
                minX = a_rectangleMinX;
            }

            if (minX > maxX) // If their projections do not intersect return false
            {
                return false;
            }

            // Find corresponding min and max Y for min and max X we found before

            int minY = a_p1y;
            int maxY = a_p2y;

            int dx = a_p2x - a_p1x;

            if (dx == 0)//Math.Abs(dx) > 0.0000001)
            {
                int a = (a_p2y - a_p1y) / dx;
                int b = a_p1y - a * a_p1x;
                minY = a * minX + b;
                maxY = a * maxX + b;
            }

            if (minY > maxY)
            {
                int tmp = maxY;
                maxY = minY;
                minY = tmp;
            }

            // Find the intersection of the segment's and rectangle's y-projections

            if (maxY > a_rectangleMaxY)
            {
                maxY = a_rectangleMaxY;
            }

            if (minY < a_rectangleMinY)
            {
                minY = a_rectangleMinY;
            }

            if (minY > maxY) // If Y-projections do not intersect return false
            {
                return false;
            }

            return true;
        }
    }
}
