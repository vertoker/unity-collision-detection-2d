using System.Runtime.CompilerServices;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct LineShape : ILine<PointShape>
    {
        public PointShape p1;
        public PointShape p2;

        PointShape ILine<PointShape>.P1
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => p1;
        }
        PointShape ILine<PointShape>.P2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => p2;
        }

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
        public bool Intersect<TShape>(TShape shape) where TShape : unmanaged, IShape
            => shape.IntersectLine(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPoint<TPoint>(TPoint point) where TPoint : unmanaged, IPoint
            => CollisionDetection.PointLine(point, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectCircle<TCircle>(TCircle circle) where TCircle : unmanaged, ICircle
            => CollisionDetection.CircleLine(circle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectRectangle<TRectangle>(TRectangle rectangle) where TRectangle : unmanaged, IRectangle
            => CollisionDetection.RectangleLine(rectangle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectLine<TLine>(TLine line) where TLine : unmanaged, ILine<PointShape>
            => CollisionDetection.LineLine(line, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPolygon<TPolygon>(TPolygon polygon) where TPolygon : unmanaged, IPolygon<PointShape>
            => CollisionDetection.LinePolygon(this, polygon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectTriangle<TTriangle>(TTriangle triangle) where TTriangle : unmanaged, ITriangle<PointShape>
            => CollisionDetection.LineTriangle(this, triangle);
    }
}