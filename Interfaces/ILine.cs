namespace vertoker.CollisionDetection2D.Interfaces
{
    public interface ILine<out TPoint> : IShape where TPoint : unmanaged, IPoint
    {
        public TPoint P1 { get; }
        public TPoint P2 { get; }
    }
}