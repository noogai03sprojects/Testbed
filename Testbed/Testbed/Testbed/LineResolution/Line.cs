using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testbed.Physics
{
    struct Line
    {
        public Vector2I Start, End;

        public Line(int X1, int Y1, int X2, int Y2)
        {
            Start = new Vector2I(X1, Y1);
            End = new Vector2I(X2, Y2);
        }
        public Line(Vector2I topleft, Vector2I bottomright)
        {
            Start = topleft;
            End = bottomright;
        }

        public Vector2 Midpoint
        {
            get
            {
                return new Vector2(((Start.X + End.X) / 2), ((Start.Y + End.Y) / 2));
            }
        }

        public float Gradient
        {
            get
            {
                return ((float)Start.Y - (float)End.Y) / ((float)Start.X - (float)End.X);
            }
        }

        public Vector2I StartToEnd
        {
            get
            {
                return End - Start;
            }
        }

        public float Dot(Vector2I Vector)
        {
            return Vector2I.Dot(StartToEnd, Vector);
        }

        public float DotFromPoint(Vector2 point)
        {
            Vector2 normal = StartToEnd.Normal;
            normal.Normalize();
            Vector2 distance = (Vector2)Start - point;
            distance.Normalize();
            return Vector2.Dot(normal, distance);            
        }

        public static bool Intersects(Line A, Line B, out Vector2 intersection)
        {
            intersection = Vector2I.Zero;
            Vector2 b = A.StartToEnd;
            Vector2 d = B.StartToEnd;

            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return false;

            Vector2 c = B.Start - A.Start;
            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
                return false;

            intersection = A.Start + t * b;

            return true;
        }        
    }
}
