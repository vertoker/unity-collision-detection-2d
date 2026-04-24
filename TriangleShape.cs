namespace vertoker.CollisionDetection2D
{
    public struct TriangleShape : IShape
    {
        public float X1;
        public float Y1;
        public float X2;
        public float Y2;
        public float X3;
        public float Y3;

        public TriangleShape(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
        }

        public bool CollisionDetection(IShape shape)
        {
            return shape.CollisionDetectionTriangle(this);
        }
        public bool CollisionDetectionPoint(PointShape pointShape)
        {
            return CollisionDetectionStatic.PointTriangle(pointShape.X, pointShape.Y, X1, Y1, X2, Y2, X3, Y3);
        }
        public bool CollisionDetectionCircle(CircleShape circleShape)
        {
            return CollisionDetectionStatic.CircleTriangle(circleShape.X, circleShape.Y, circleShape.R, X1, Y1, X2, Y2, X3, Y3);
        }
        public bool CollisionDetectionRectangle(RectangleShape rect)
        {
            return CollisionDetectionStatic.RectangleTriangle(rect.X, rect.Y, rect.W, rect.H, X1, Y1, X2, Y2, X3, Y3);
        }
        public bool CollisionDetectionLine(LineShape lineShape)
        {
            return CollisionDetectionStatic.LineTriangle(lineShape.X1, lineShape.Y1, lineShape.X2, lineShape.Y2, X1, Y1, X2, Y2, X3, Y3);
        }
        public bool CollisionDetectionPolygon(PolygonShape polygonShape)
        {
            return CollisionDetectionStatic.PolygonTriangle(polygonShape.Vertices, X1, Y1, X2, Y2, X3, Y3);
        }
        public bool CollisionDetectionTriangle(TriangleShape trian)
        {
            return CollisionDetectionStatic.TriangleTriangle(trian.X1, trian.Y1, trian.X2, trian.Y2, trian.X3, trian.Y3, X1, Y1, X2, Y2, X3, Y3);
        }
    }
}