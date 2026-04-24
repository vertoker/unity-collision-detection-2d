namespace vertoker.CollisionDetection2D.Interfaces
{
    public interface IShape
    {
        public bool Intersect<TShape>(TShape shape) where TShape : unmanaged, IShape;
        public bool IntersectPoint<TPoint>(TPoint point) where TPoint : unmanaged, IPoint;
        public bool IntersectCircle<TCircle>(TCircle circle) where TCircle : unmanaged, ICircle;
        public bool IntersectRectangle<TRectangle>(TRectangle rectangle) where TRectangle : unmanaged, IRectangle;
        public bool IntersectLine<TLine>(TLine line) where TLine : unmanaged, ILine<PointShape>;
        public bool IntersectPolygon<TPolygon>(TPolygon polygon) where TPolygon : unmanaged, IPolygon<PointShape>;
        public bool IntersectTriangle<TTriangle>(TTriangle triangle) where TTriangle : unmanaged, ITriangle<PointShape>;
    }
}
