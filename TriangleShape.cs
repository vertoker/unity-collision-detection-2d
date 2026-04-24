using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct TriangleShape : ITriangle<PointShape>
    {
        public PointShape p1;
        public PointShape p2;
        public PointShape p3;

        PointShape ITriangle<PointShape>.P1 => p1;
        PointShape ITriangle<PointShape>.P2 => p2;
        PointShape ITriangle<PointShape>.P3 => p3;

        public TriangleShape(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            p1 = new PointShape(x1, y1);
            p2 = new PointShape(x2, y2);
            p3 = new PointShape(x3, y3);
        }
        public TriangleShape(PointShape p1, PointShape p2, PointShape p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetection<TShape>(TShape shape) where TShape : unmanaged, IShape => shape.CollisionDetectionTriangle(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPoint(PointShape point) => CollisionDetectionStatic.PointTriangle(point, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionCircle(CircleShape circle) => CollisionDetectionStatic.CircleTriangle(circle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionRectangle(RectangleShape rectangle) => CollisionDetectionStatic.RectangleTriangle(rectangle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionLine(LineShape line) => CollisionDetectionStatic.LineTriangle(line, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPolygon(PolygonShape polygon) => CollisionDetectionStatic.PolygonTriangle(polygon, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionTriangle(TriangleShape triangle) => CollisionDetectionStatic.TriangleTriangle(this, triangle);
    }
}