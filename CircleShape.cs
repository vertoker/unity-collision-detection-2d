using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct CircleShape : ICircle
    {
        public PointShape p;
        public float r;

        float IPoint.X => p.x;
        float IPoint.Y => p.y;
        float ICircle.R => r;
        
        public CircleShape(float x, float y, float r)
        {
            p = new PointShape(x, y);
            this.r = r;
        }
        public CircleShape(PointShape p, float r)
        {
            this.p = p;
            this.r = r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetection<TShape>(TShape shape) where TShape : unmanaged, IShape => shape.CollisionDetectionCircle(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPoint(PointShape point) => CollisionDetectionStatic.PointCircle(point, this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionCircle(CircleShape circle) => CollisionDetectionStatic.CircleCircle(this, circle);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionRectangle(RectangleShape rectangle) => CollisionDetectionStatic.CircleRectangle(this, rectangle);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionLine(LineShape line) => CollisionDetectionStatic.CircleLine(this, line);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPolygon(PolygonShape polygon) => CollisionDetectionStatic.CirclePolygon(this, polygon);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionTriangle(TriangleShape triangle) => CollisionDetectionStatic.CircleTriangle(this, triangle);
    }
}
