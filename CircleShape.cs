using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct CircleShape : ICircle
    {
        public PointShape p;
        public float r;

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
        float ICircle.R
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => r;
        }
        
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
        public bool Intersect<TShape>(TShape shape) where TShape : unmanaged, IShape
            => shape.IntersectCircle(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPoint<TPoint>(TPoint point) where TPoint : unmanaged, IPoint 
            => CollisionDetection.PointCircle(point, this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectCircle<TCircle>(TCircle circle) where TCircle : unmanaged, ICircle 
            => CollisionDetection.CircleCircle(this, circle);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectRectangle<TRectangle>(TRectangle rectangle) where TRectangle : unmanaged, IRectangle 
            => CollisionDetection.CircleRectangle(this, rectangle);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectLine<TLine>(TLine line) where TLine : unmanaged, ILine<PointShape> 
            => CollisionDetection.CircleLine(this, line);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPolygon<TPolygon>(TPolygon polygon) where TPolygon : unmanaged, IPolygon<PointShape> 
            => CollisionDetection.CirclePolygon(this, polygon);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectTriangle<TTriangle>(TTriangle triangle) where TTriangle : unmanaged, ITriangle<PointShape> 
            => CollisionDetection.CircleTriangle(this, triangle);
    }
}
