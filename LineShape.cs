using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct LineShape : ILine<PointShape>
    {
        public PointShape p1;
        public PointShape p2;

        PointShape ILine<PointShape>.P1 => p1;
        PointShape ILine<PointShape>.P2 => p2;

        public LineShape(float x1, float y1, float x2, float y2)
        {
            p1 = new PointShape(x1, y1);
            p2 = new PointShape(x2, y2);
        }
        public LineShape(PointShape p1, PointShape p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetection<TShape>(TShape shape) where TShape : unmanaged, IShape => shape.CollisionDetectionLine(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPoint(PointShape point) => CollisionDetectionStatic.PointLine(point, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionCircle(CircleShape circle) => CollisionDetectionStatic.CircleLine(circle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionRectangle(RectangleShape rectangle) => CollisionDetectionStatic.RectangleLine(rectangle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionLine(LineShape line) => CollisionDetectionStatic.LineLine(line, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPolygon(PolygonShape polygon) => CollisionDetectionStatic.LinePolygon(this, polygon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionTriangle(TriangleShape triangle) => CollisionDetectionStatic.LineTriangle(this, triangle);
    }
}