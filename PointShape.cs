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
        public bool Intersect<TShape>(TShape shape) where TShape : unmanaged, IShape
            => shape.IntersectPoint(this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPoint<TPoint>(TPoint point) where TPoint : unmanaged, IPoint
            => CollisionDetection.PointPoint(this, point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectCircle<TCircle>(TCircle circle) where TCircle : unmanaged, ICircle
            => CollisionDetection.PointCircle(this, circle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectRectangle<TRectangle>(TRectangle rectangle) where TRectangle : unmanaged, IRectangle
            => CollisionDetection.PointRectangle(this, rectangle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectLine<TLine>(TLine line) where TLine : unmanaged, ILine<PointShape>
            => CollisionDetection.PointLine(this, line);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPolygon<TPolygon>(TPolygon polygon) where TPolygon : unmanaged, IPolygon<PointShape>
            => CollisionDetection.PointPolygon(this, polygon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectTriangle<TTriangle>(TTriangle triangle) where TTriangle : unmanaged, ITriangle<PointShape>
            => CollisionDetection.PointTriangle(this, triangle);
        
        
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
