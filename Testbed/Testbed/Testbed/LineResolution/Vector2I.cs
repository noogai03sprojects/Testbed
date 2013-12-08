using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testbed
{
    struct Vector2I
    {
        public int X, Y;

        public static Vector2I operator +(Vector2I v1, Vector2I v2)
        {
            return new Vector2I(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector2I operator -(Vector2I v1, Vector2I v2)
        {
            return new Vector2I(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vector2I operator *(Vector2I v, int scalar)
        {
            return new Vector2I(v.X * scalar, v.Y * scalar);
        }

        /// <summary>
        /// Note: will cast to int if not a whole number divisor.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2I operator /(Vector2I v, int scalar)
        {
            return new Vector2I((int)(v.X / scalar), (int)(v.Y / scalar));
        }

        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
        }
        public float LengthSquared
        {
            get
            {
                return X * X + Y * Y;
            }
        }


        public Vector2 Normalized
        {
            get
            {
                float length = Length;
                //Vector2.
                return new Vector2((float)X / length, (float)Y / length);
            }
        }
        public Vector2 Normal
        {
            get
            {
                return new Vector2(Y, -1 * X);
            }
        }

        //public static Vector2 Normal(Vector2I vect)
        //{
        //    return new Vector2(vect.Y, -1 * vect.X);
        //}

        static public explicit operator Vector2I(Vector2 vect)
        {
            return new Vector2I((int)vect.X, (int)vect.Y);
        }
        static public implicit operator Vector2(Vector2I vect)
        {
            return new Vector2(vect.X, vect.Y);
        }

        public static float Dot(Vector2I A, Vector2I B)
        {
            return (A.X * A.Y) + (B.X * B.Y);
        }

        public static Vector2I Zero = new Vector2I(0, 0);
    }
}
