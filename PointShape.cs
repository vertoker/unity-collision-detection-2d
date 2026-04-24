using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct PointShape : IPoint
    {
        public float x;
        public float y;

        float IPoint.X => x;
        float IPoint.Y => y;

        public PointShape(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetection<TShape>(TShape shape) where TShape : unmanaged, IShape => shape.CollisionDetectionPoint(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPoint(PointShape point) => CollisionDetectionStatic.PointPoint(this, point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionCircle(CircleShape circle) => CollisionDetectionStatic.PointCircle(this, circle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionRectangle(RectangleShape rectangle) => CollisionDetectionStatic.PointRectangle(this, rectangle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionLine(LineShape line) => CollisionDetectionStatic.PointLine(this, line);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPolygon(PolygonShape polygon) => CollisionDetectionStatic.PointPolygon(this, polygon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionTriangle(TriangleShape triangle) => CollisionDetectionStatic.PointTriangle(this, triangle);
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator + (PointShape lhs, PointShape rhs) => new(lhs.x + rhs.x, lhs.y + rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator + (float lhs, PointShape rhs) => new(lhs + rhs.x, lhs + rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator + (PointShape lhs, float rhs) => new(lhs.x + rhs, lhs.y + rhs);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator - (PointShape lhs, PointShape rhs) => new(lhs.x - rhs.x, lhs.y - rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator - (float lhs, PointShape rhs) => new(lhs - rhs.x, lhs - rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator - (PointShape lhs, float rhs) => new(lhs.x - rhs, lhs.y - rhs);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator * (PointShape lhs, PointShape rhs) => new(lhs.x * rhs.x, lhs.y * rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator * (float lhs, PointShape rhs) => new(lhs * rhs.x, lhs * rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator * (PointShape lhs, float rhs) => new(lhs.x * rhs, lhs.y * rhs);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator / (PointShape lhs, PointShape rhs) => new(lhs.x / rhs.x, lhs.y / rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator / (float lhs, PointShape rhs) => new(lhs / rhs.x, lhs / rhs.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape operator / (PointShape lhs, float rhs) => new(lhs.x / rhs, lhs.y / rhs);
    }
}
