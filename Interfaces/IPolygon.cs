using System;
using Unity.Collections;

namespace vertoker.CollisionDetection2D.Interfaces
{
    public interface IPolygon<TPoint> : IShape where TPoint : unmanaged, IPoint
    {
        public NativeSlice<TPoint> Vertices { get; }
    }
}