using System.Diagnostics;
using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    [DebuggerDisplay("p1 = ({p1}), p2 = ({p2}), p3 = ({p3})")]
    public struct TriangleShape : ITriangle<PointShape>
    {
        public PointShape p1;
        public PointShape p2;
        public PointShape p3;

        PointShape ITriangle<PointShape>.P1
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => p1;
        }
        PointShape ITriangle<PointShape>.P2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => p2;
        }
        PointShape ITriangle<PointShape>.P3
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => p3;
        }

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
        public bool Intersect<TShape>(TShape shape) where TShape : unmanaged, IShape
            => shape.IntersectTriangle(this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPoint<TPoint>(TPoint point) where TPoint : unmanaged, IPoint
            => CollisionDetection.PointTriangle(point, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectCircle<TCircle>(TCircle circle) where TCircle : unmanaged, ICircle
            => CollisionDetection.CircleTriangle(circle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectRectangle<TRectangle>(TRectangle rectangle) where TRectangle : unmanaged, IRectangle
            => CollisionDetection.RectangleTriangle(rectangle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectLine<TLine>(TLine line) where TLine : unmanaged, ILine<PointShape>
            => CollisionDetection.LineTriangle(line, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPolygon<TPolygon>(TPolygon polygon) where TPolygon : unmanaged, IPolygon<PointShape>
            => CollisionDetection.PolygonTriangle(polygon, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectTriangle<TTriangle>(TTriangle triangle) where TTriangle : unmanaged, ITriangle<PointShape>
            => CollisionDetection.TriangleTriangle(this, triangle);
    }
}