using System.Runtime.CompilerServices;
using Unity.Collections;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct PolygonShape : IPolygon<PointShape>
    {
        public NativeSlice<PointShape> vertices;

        NativeSlice<PointShape> IPolygon<PointShape>.Vertices
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => vertices;
        }
        
        public PolygonShape(NativeSlice<PointShape> vertices)
        {
            this.vertices = vertices;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersect<TShape>(TShape shape) where TShape : unmanaged, IShape => shape.IntersectPolygon(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPoint<TPoint>(TPoint point) where TPoint : unmanaged, IPoint
            => CollisionDetection.PointPolygon(point, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectCircle<TCircle>(TCircle circle) where TCircle : unmanaged, ICircle
            => CollisionDetection.CirclePolygon(circle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectRectangle<TRectangle>(TRectangle rectangle) where TRectangle : unmanaged, IRectangle
            => CollisionDetection.RectanglePolygon(rectangle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectLine<TLine>(TLine line) where TLine : unmanaged, ILine<PointShape>
            => CollisionDetection.LinePolygon(line, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectPolygon<TPolygon>(TPolygon polygon) where TPolygon : unmanaged, IPolygon<PointShape>
            => CollisionDetection.PolygonPolygon(this, polygon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectTriangle<TTriangle>(TTriangle triangle) where TTriangle : unmanaged, ITriangle<PointShape>
            => CollisionDetection.PolygonTriangle(this, triangle);
    }
}
