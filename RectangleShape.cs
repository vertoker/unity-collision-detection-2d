using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    /// <summary>
    /// This is not Rect. x and y denote center of rectangle, not a left top corner
    /// </summary>
    public struct RectangleShape : IRectangle
    {
        public PointShape p;
        public float w;
        public float h;

        float IPoint.X
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => p.x;
        }
        float IPoint.Y
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => p.y;
        }
        float IRectangle.W
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => w;
        }
        float IRectangle.H
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => h;
        }
        
        public RectangleShape(float x, float y, float w, float h)
        {
            p = new PointShape(x, y);
            this.w = w;
            this.h = h;
        }
        public RectangleShape(PointShape p, float w, float h)
        {
            this.p = p;
            this.w = w;
            this.h = h;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersect<TShape>(TShape shape) where TShape : unmanaged, IShape
            => shape.IntersectRectangle(this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPoint<TPoint>(TPoint point) where TPoint : unmanaged, IPoint
            => CollisionDetection.PointRectangle(point, this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectCircle<TCircle>(TCircle circle) where TCircle : unmanaged, ICircle
            => CollisionDetection.CircleRectangle(circle, this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectRectangle<TRectangle>(TRectangle rectangle) where TRectangle : unmanaged, IRectangle
            => CollisionDetection.RectangleRectangle(this, rectangle);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectLine<TLine>(TLine line) where TLine : unmanaged, ILine<PointShape>
            => CollisionDetection.RectangleLine(this, line);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPolygon<TPolygon>(TPolygon polygon) where TPolygon : unmanaged, IPolygon<PointShape>
            => CollisionDetection.RectanglePolygon(this, polygon);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectTriangle<TTriangle>(TTriangle triangle) where TTriangle : unmanaged, ITriangle<PointShape>
            => CollisionDetection.RectangleTriangle(this, triangle);
    }
}