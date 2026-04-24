using System.Runtime.CompilerServices;
using Unity.Collections;
using vertoker.CollisionDetection2D.Interfaces;
// ReSharper disable InconsistentNaming

namespace vertoker.CollisionDetection2D
{
    public struct PolygonShape : IPolygon<PointShape>
    {
        public NativeSlice<PointShape> vertices;

        NativeSlice<PointShape> IPolygon<PointShape>.Vertices => vertices;
        
        public PolygonShape(NativeSlice<PointShape> vertices)
        {
            this.vertices = vertices;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetection<TShape>(TShape shape) where TShape : unmanaged, IShape => shape.CollisionDetectionPolygon(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPoint(PointShape point) => CollisionDetectionStatic.PointPolygon(point, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionCircle(CircleShape circle) => CollisionDetectionStatic.CirclePolygon(circle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionRectangle(RectangleShape rectangle) => CollisionDetectionStatic.RectanglePolygon(rectangle, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionLine(LineShape line) => CollisionDetectionStatic.LinePolygon(line, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPolygon(PolygonShape polygon) => CollisionDetectionStatic.PolygonPolygon(this, polygon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionTriangle(TriangleShape triangle) => CollisionDetectionStatic.PolygonTriangle(this, triangle);
    }
}
