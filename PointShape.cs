namespace vertoker.CollisionDetection2D
{
    public struct PointShape : IShape
    {
        public float X;
        public float Y;

        public PointShape(float x, float y)
        {
            X = x;
            Y = y;
        }

        public bool CollisionDetection(IShape shape)
        {
            return shape.CollisionDetectionPoint(this);
        }
        public bool CollisionDetectionPoint(PointShape pointShape)
        {
            return CollisionDetectionStatic.PointPoint(X, Y, pointShape.X, pointShape.Y);
        }
        public bool CollisionDetectionCircle(CircleShape circleShape)
        {
            return CollisionDetectionStatic.PointCircle(X, Y, circleShape.X, circleShape.Y, circleShape.R);
        }
        public bool CollisionDetectionRectangle(RectangleShape rect)
        {
            return CollisionDetectionStatic.PointRectangle(X, Y, rect.X, rect.Y, rect.W, rect.H);
        }
        public bool CollisionDetectionLine(LineShape lineShape)
        {
            return CollisionDetectionStatic.PointLine(X, Y, lineShape.X1, lineShape.Y1, lineShape.X2, lineShape.Y2, lineShape.Buf);
        }
        public bool CollisionDetectionPolygon(PolygonShape polygonShape)
        {
            return CollisionDetectionStatic.PointPolygon(X, Y, polygonShape.Vertices);
        }
        public bool CollisionDetectionTriangle(TriangleShape triangleShape)
        {
            return CollisionDetectionStatic.PointTriangle(X, Y, triangleShape.X1, triangleShape.Y1, triangleShape.X2, triangleShape.Y2, triangleShape.X3, triangleShape.Y3);
        }
    }
}
