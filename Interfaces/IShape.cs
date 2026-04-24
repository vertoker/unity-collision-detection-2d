namespace vertoker.CollisionDetection2D.Interfaces
{
    public interface IShape
    {
        public bool CollisionDetection<TShape>(TShape shape) where TShape : unmanaged, IShape;
        public bool CollisionDetectionPoint(PointShape point);
        public bool CollisionDetectionCircle(CircleShape circle);
        public bool CollisionDetectionRectangle(RectangleShape rectangle);
        public bool CollisionDetectionLine(LineShape line);
        public bool CollisionDetectionPolygon(PolygonShape polygon);
        public bool CollisionDetectionTriangle(TriangleShape triangle);
    }
}
