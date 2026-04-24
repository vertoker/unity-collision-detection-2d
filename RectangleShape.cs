namespace vertoker.CollisionDetection2D
{
    /// <summary>
    /// This is not Rect. x and y denote center of rectangle, not a left top corner
    /// </summary>
    public struct RectangleShape : IShape
    {
        public float X;
        public float Y;
        public float W;
        public float H;

        public RectangleShape(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public bool CollisionDetection(IShape shape)
        {
            return shape.CollisionDetectionRectangle(this);
        }
        public bool CollisionDetectionPoint(PointShape pointShape)
        {
            return CollisionDetectionStatic.PointRectangle(pointShape.X, pointShape.Y, X, Y, W, H);
        }
        public bool CollisionDetectionCircle(CircleShape circleShape)
        {
            return CollisionDetectionStatic.CircleRectangle(circleShape.X, circleShape.Y, circleShape.R, X, Y, W, H);
        }
        public bool CollisionDetectionRectangle(RectangleShape rect)
        {
            return CollisionDetectionStatic.RectangleRectangle(X, Y, W, H, rect.X, rect.Y, rect.W, rect.H);
        }
        public bool CollisionDetectionLine(LineShape lineShape)
        {
            return CollisionDetectionStatic.RectangleLine(X, Y, W, H, lineShape.X1, lineShape.Y1, lineShape.X2, lineShape.Y2);
        }
        public bool CollisionDetectionPolygon(PolygonShape polygonShape)
        {
            return CollisionDetectionStatic.RectanglePolygon(X, Y, W, H, polygonShape.Vertices);
        }
        public bool CollisionDetectionTriangle(TriangleShape triangleShape)
        {
            return CollisionDetectionStatic.RectangleTriangle(X, Y, W, H, triangleShape.X1, triangleShape.Y1, triangleShape.X2, triangleShape.Y2, triangleShape.X3, triangleShape.Y3);
        }
    }
}