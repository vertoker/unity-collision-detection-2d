using System.Runtime.CompilerServices;

namespace vertoker.CollisionDetection2D
{
    public struct CircleShape : IShape
    {
        public float X;
        public float Y;
        public float R;

        public CircleShape(float x, float y, float r)
        {
            X = x;
            Y = y;
            R = r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetection(IShape shape) => shape.CollisionDetectionCircle(this);
        public bool CollisionDetectionPoint(PointShape pointShape)
        {
            return CollisionDetectionStatic.PointCircle(pointShape.X, pointShape.Y, X, Y, R);
        }
        public bool CollisionDetectionCircle(CircleShape circleShape)
        {
            return CollisionDetectionStatic.CircleCircle(X, Y, R, circleShape.X, circleShape.Y, circleShape.R);
        }
        public bool CollisionDetectionRectangle(RectangleShape rect)
        {
            return CollisionDetectionStatic.CircleRectangle(X, Y, R, rect.X, rect.Y, rect.W, rect.H);
        }
        public bool CollisionDetectionLine(LineShape lineShape)
        {
            return CollisionDetectionStatic.CircleLine(X, Y, R, lineShape.X1, lineShape.Y1, lineShape.X2, lineShape.Y2, lineShape.Buf);
        }
        public bool CollisionDetectionPolygon(PolygonShape polygonShape)
        {
            return CollisionDetectionStatic.CirclePolygon(X, Y, R, polygonShape.Vertices);
        }
        public bool CollisionDetectionTriangle(TriangleShape triangleShape)
        {
            return CollisionDetectionStatic.CircleTriangle(X, Y, R, triangleShape.X1, triangleShape.Y1, triangleShape.X2, triangleShape.Y2, triangleShape.X3, triangleShape.Y3);
        }
    }
}
