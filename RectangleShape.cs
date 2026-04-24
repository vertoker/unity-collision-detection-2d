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

        float IPoint.X => p.x;
        float IPoint.Y => p.y;
        float IRectangle.W => w;
        float IRectangle.H => h;
        
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
        public bool CollisionDetection<TShape>(TShape shape) where TShape : unmanaged, IShape => shape.CollisionDetectionRectangle(this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPoint(PointShape point) => CollisionDetectionStatic.PointRectangle(point, this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionCircle(CircleShape circle) => CollisionDetectionStatic.CircleRectangle(circle, this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionRectangle(RectangleShape rectangle) => CollisionDetectionStatic.RectangleRectangle(this, rectangle);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionLine(LineShape line) => CollisionDetectionStatic.RectangleLine(this, line);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionPolygon(PolygonShape polygon) => CollisionDetectionStatic.RectanglePolygon(this, polygon);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CollisionDetectionTriangle(TriangleShape triangle) => CollisionDetectionStatic.RectangleTriangle(this, triangle);
    }
}