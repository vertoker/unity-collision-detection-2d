namespace vertoker.CollisionDetection2D
{
    public interface IShape
    {
        public bool CollisionDetection(IShape shape);
        public bool CollisionDetectionPoint(PointShape pointShape);
        public bool CollisionDetectionCircle(CircleShape circleShape);
        public bool CollisionDetectionRectangle(RectangleShape rect);
        public bool CollisionDetectionLine(LineShape lineShape);
        public bool CollisionDetectionPolygon(PolygonShape polygonShape);
        public bool CollisionDetectionTriangle(TriangleShape triangleShape);
    }
}
