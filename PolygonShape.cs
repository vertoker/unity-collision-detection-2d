namespace vertoker.CollisionDetection2D
{
    public struct PolygonShape : IShape
    {
        public PointShape[] Vertices;

        public PolygonShape(PointShape[] vertices)
        {
            Vertices = vertices;
        }

        public bool CollisionDetection(IShape shape)
        {
            return shape.CollisionDetectionPolygon(this);
        }
        public bool CollisionDetectionPoint(PointShape pointShape)
        {
            return CollisionDetectionStatic.PointPolygon(pointShape.X, pointShape.Y, Vertices);
        }
        public bool CollisionDetectionCircle(CircleShape circleShape)
        {
            return CollisionDetectionStatic.CirclePolygon(circleShape.X, circleShape.Y, circleShape.R, Vertices);
        }
        public bool CollisionDetectionRectangle(RectangleShape rect)
        {
            return CollisionDetectionStatic.RectanglePolygon(rect.X, rect.Y, rect.W, rect.H, Vertices);
        }
        public bool CollisionDetectionLine(LineShape lineShape)
        {
            return CollisionDetectionStatic.LinePolygon(lineShape.X1, lineShape.Y1, lineShape.X2, lineShape.Y2, Vertices);
        }
        public bool CollisionDetectionPolygon(PolygonShape polygonShape)
        {
            return CollisionDetectionStatic.PolygonPolygon(Vertices, polygonShape.Vertices);
        }
        public bool CollisionDetectionTriangle(TriangleShape triangleShape)
        {
            return CollisionDetectionStatic.PolygonTriangle(Vertices, triangleShape.X1, triangleShape.Y1, triangleShape.X2, triangleShape.Y2, triangleShape.X3, triangleShape.Y3);
        }
    }
}
