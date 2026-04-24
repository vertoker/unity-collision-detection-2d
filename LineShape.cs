namespace vertoker.CollisionDetection2D
{
    public struct LineShape : IShape
    {
        public float X1;
        public float Y1;
        public float X2;
        public float Y2;
        public float Buf;

        public LineShape(float x1, float y1, float x2, float y2, float buf)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Buf = buf;
        }

        public bool CollisionDetection(IShape shape)
        {
            return shape.CollisionDetectionLine(this);
        }
        public bool CollisionDetectionPoint(PointShape pointShape)
        {
            return CollisionDetectionStatic.PointLine(pointShape.X, pointShape.Y, X1, Y1, X2, Y2, Buf);
        }
        public bool CollisionDetectionCircle(CircleShape circleShape)
        {
            return CollisionDetectionStatic.CircleLine(circleShape.X, circleShape.Y, circleShape.R, X1, Y1, X2, Y2, Buf);
        }
        public bool CollisionDetectionRectangle(RectangleShape rect)
        {
            return CollisionDetectionStatic.RectangleLine(rect.X, rect.Y, rect.W, rect.H, X1, Y1, X2, Y2);
        }
        public bool CollisionDetectionLine(LineShape lineShape)
        {
            return CollisionDetectionStatic.LineLine(lineShape.X1, lineShape.Y1, lineShape.X2, lineShape.Y2, X1, Y1, X2, Y2);
        }
        public bool CollisionDetectionPolygon(PolygonShape polygonShape)
        {
            return CollisionDetectionStatic.LinePolygon(X1, Y1, X2, Y2, polygonShape.Vertices);
        }
        public bool CollisionDetectionTriangle(TriangleShape triangleShape)
        {
            return CollisionDetectionStatic.LineTriangle(X1, Y1, X2, Y2, triangleShape.X1, triangleShape.Y1, triangleShape.X2, triangleShape.Y2, triangleShape.X3, triangleShape.Y3);
        }
    }
}