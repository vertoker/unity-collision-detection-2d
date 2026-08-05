using NUnit.Framework;
using Unity.Collections;
using Unity.Mathematics;

namespace vertoker.CollisionDetection2D.Tests
{
    [TestFixture]
    public class CollisionDetectionTests
    {
        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointPoint_SamePoint_ReturnsTrue()
        {
            var p1 = new PointShape(1f, 2f);
            var p2 = new PointShape(1f, 2f);
            Assert.IsTrue(CollisionDetection.PointPoint(p1, p2));
            Assert.IsTrue(p1.Intersect(p2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointPoint_DifferentPoints_ReturnsFalse()
        {
            var p1 = new PointShape(1f, 2f);
            var p2 = new PointShape(1.1f, 2f);
            Assert.IsFalse(CollisionDetection.PointPoint(p1, p2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointCircle_PointInsideCircle_ReturnsTrue()
        {
            var p = new PointShape(3f, 4f);
            var c = new CircleShape(3f, 4f, 1f); // point at center
            Assert.IsTrue(CollisionDetection.PointCircle(p, c));

            p = new PointShape(3.5f, 4f);
            c = new CircleShape(3f, 4f, 1f);
            Assert.IsTrue(CollisionDetection.PointCircle(p, c));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointCircle_PointOutsideCircle_ReturnsFalse()
        {
            var p = new PointShape(5f, 4f);
            var c = new CircleShape(3f, 4f, 1f);
            Assert.IsFalse(CollisionDetection.PointCircle(p, c));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointRectangle_PointInsideRect_ReturnsTrue()
        {
            var p = new PointShape(1f, 1f);
            var r = new RectangleShape(1f, 1f, 2f, 2f); // center 1,1 size 2x2 -> left 0, right 2, bottom 0, top 2
            Assert.IsTrue(CollisionDetection.PointRectangle(p, r));
            Assert.IsTrue(r.IntersectPoint(p));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointRectangle_PointOnEdge_ReturnsTrue()
        {
            var r = new RectangleShape(0f, 0f, 4f, 4f); // from -2 to 2
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(2f, 0f), r));
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(-2f, 0f), r));
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(0f, 2f), r));
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(0f, -2f), r));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointRectangle_PointOutsideRect_ReturnsFalse()
        {
            var r = new RectangleShape(0f, 0f, 4f, 4f);
            Assert.IsFalse(CollisionDetection.PointRectangle(new PointShape(2.1f, 0f), r));
            Assert.IsFalse(CollisionDetection.PointRectangle(new PointShape(0f, -2.1f), r));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointLine_PointOnSegment_ReturnsTrue()
        {
            var p = new PointShape(2f, 2f);
            var l = new LineShape(new PointShape(1f, 1f), new PointShape(3f, 3f));
            Assert.IsTrue(CollisionDetection.PointLine(p, l));
            Assert.IsTrue(l.IntersectPoint(p));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointLine_PointOffSegment_ReturnsFalse()
        {
            var l = new LineShape(new PointShape(0f, 0f), new PointShape(4f, 0f));
            Assert.IsFalse(CollisionDetection.PointLine(new PointShape(2f, 1f), l)); // above line, > default buf
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointTriangle_PointInside_ReturnsTrue()
        {
            var t = new TriangleShape(new PointShape(0f, 0f), new PointShape(4f, 0f), new PointShape(2f, 3f));
            var inside = new PointShape(2f, 1f);
            Assert.IsTrue(CollisionDetection.PointTriangle(inside, t));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointTriangle_PointOutside_ReturnsFalse()
        {
            var t = new TriangleShape(new PointShape(0f, 0f), new PointShape(4f, 0f), new PointShape(2f, 3f));
            var outside = new PointShape(5f, 1f);
            Assert.IsFalse(CollisionDetection.PointTriangle(outside, t));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointPolygon_PointInside_ReturnsTrue()
        {
            var arr = new NativeArray<PointShape>(4, Allocator.Temp);
            arr[0] = new PointShape(0, 0);
            arr[1] = new PointShape(4, 0);
            arr[2] = new PointShape(4, 3);
            arr[3] = new PointShape(0, 3);
            var pol = new PolygonShape(arr); // axis-aligned quad
            var p = new PointShape(2f, 1.5f);
            Assert.IsTrue(CollisionDetection.PointPolygon(p, pol));
            arr.Dispose();
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PointPolygon_PointOutside_ReturnsFalse()
        {
            var arr = new NativeArray<PointShape>(3, Allocator.Temp);
            arr[0] = new PointShape(0, 0);
            arr[1] = new PointShape(4, 0);
            arr[2] = new PointShape(2, 3);
            var pol = new PolygonShape(arr);
            var outside = new PointShape(5f, 1f);
            Assert.IsFalse(CollisionDetection.PointPolygon(outside, pol));
            arr.Dispose();
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleCircle_Overlapping_ReturnsTrue()
        {
            var c1 = new CircleShape(0f, 0f, 2f);
            var c2 = new CircleShape(3f, 0f, 2f); // centers distance 3, sum of radii 4 -> overlap
            Assert.IsTrue(CollisionDetection.CircleCircle(c1, c2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleCircle_Separate_ReturnsFalse()
        {
            var c1 = new CircleShape(0f, 0f, 2f);
            var c2 = new CircleShape(5f, 0f, 2f); // distance 5 > sum 4
            Assert.IsFalse(CollisionDetection.CircleCircle(c1, c2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleRectangle_Overlapping_ReturnsTrue()
        {
            var c = new CircleShape(0f, 0f, 1f);
            var r = new RectangleShape(0.5f, 0.5f, 1f, 1f); // small rect partially covering circle
            Assert.IsTrue(CollisionDetection.CircleRectangle(c, r));
            Assert.IsTrue(c.IntersectRectangle(r));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleRectangle_Separate_ReturnsFalse()
        {
            var c = new CircleShape(0f, 0f, 1f);
            var r = new RectangleShape(3f, 0f, 1f, 1f);
            Assert.IsFalse(CollisionDetection.CircleRectangle(c, r));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleLine_Intersects_ReturnsTrue()
        {
            var c = new CircleShape(0f, 0f, 1f);
            var l = new LineShape(new PointShape(-2f, 0f), new PointShape(2f, 0f)); // line through center
            Assert.IsTrue(CollisionDetection.CircleLine(c, l));
            Assert.IsTrue(l.IntersectCircle(c));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleLine_NoIntersect_ReturnsFalse()
        {
            var c = new CircleShape(0f, 0f, 1f);
            var l = new LineShape(new PointShape(-2f, 2f), new PointShape(2f, 2f)); // line above circle
            Assert.IsFalse(CollisionDetection.CircleLine(c, l));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CirclePolygon_Overlapping_ReturnsTrue()
        {
            var arr = new NativeArray<PointShape>(4, Allocator.Temp);
            arr[0] = new PointShape(0, 0);
            arr[1] = new PointShape(3, 0);
            arr[2] = new PointShape(3, 3);
            arr[3] = new PointShape(0, 3);
            var pol = new PolygonShape(arr);
            var c = new CircleShape(1.5f, 1.5f, 1f); // inside rect
            Assert.IsTrue(CollisionDetection.CirclePolygon(c, pol));
            arr.Dispose();
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleTriangle_Overlapping_ReturnsTrue()
        {
            var t = new TriangleShape(new PointShape(0,0), new PointShape(4,0), new PointShape(2,3));
            var c = new CircleShape(2f, 1f, 0.5f); // inside
            Assert.IsTrue(CollisionDetection.CircleTriangle(c, t));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void CircleTriangle_NoIntersect_ReturnsFalse()
        {
            var t = new TriangleShape(new PointShape(0,0), new PointShape(4,0), new PointShape(2,3));
            var c = new CircleShape(10f, 0f, 1f);
            Assert.IsFalse(CollisionDetection.CircleTriangle(c, t));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void RectangleRectangle_Overlapping_ReturnsTrue()
        {
            var r1 = new RectangleShape(0f, 0f, 2f, 2f);
            var r2 = new RectangleShape(1f, 1f, 2f, 2f);
            Assert.IsTrue(CollisionDetection.RectangleRectangle(r1, r2));
            Assert.IsTrue(r1.IntersectRectangle(r2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void RectangleRectangle_Separate_ReturnsFalse()
        {
            var r1 = new RectangleShape(0f, 0f, 2f, 2f);
            var r2 = new RectangleShape(3f, 3f, 2f, 2f);
            Assert.IsFalse(CollisionDetection.RectangleRectangle(r1, r2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void RectangleLine_Intersects_ReturnsTrue()
        {
            var r = new RectangleShape(0f, 0f, 2f, 2f);
            var l = new LineShape(new PointShape(-1f, 0f), new PointShape(1f, 0f)); // crosses left and right edges
            Assert.IsTrue(CollisionDetection.RectangleLine(r, l));
            Assert.IsTrue(r.IntersectLine(l));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void RectanglePolygon_Overlapping_ReturnsTrue()
        {
            var arr = new NativeArray<PointShape>(3, Allocator.Temp);
            arr[0] = new PointShape(0,0);
            arr[1] = new PointShape(3,0);
            arr[2] = new PointShape(1.5f, 3);
            var pol = new PolygonShape(arr);
            var r = new RectangleShape(1.5f, 1f, 1f, 1f); // inside triangle
            Assert.IsTrue(CollisionDetection.RectanglePolygon(r, pol));
            arr.Dispose();
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void RectangleTriangle_Overlapping_ReturnsTrue()
        {
            var r = new RectangleShape(0f, 0f, 2f, 2f);
            var t = new TriangleShape(new PointShape(-1, -1), new PointShape(1, -1), new PointShape(0, 1));
            Assert.IsTrue(CollisionDetection.RectangleTriangle(r, t));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void LineLine_Intersecting_ReturnsTrue()
        {
            var l1 = new LineShape(new PointShape(0,0), new PointShape(2,2));
            var l2 = new LineShape(new PointShape(0,2), new PointShape(2,0));
            Assert.IsTrue(CollisionDetection.LineLine(l1, l2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void LineLine_Parallel_ReturnsFalse()
        {
            var l1 = new LineShape(new PointShape(0,0), new PointShape(2,0));
            var l2 = new LineShape(new PointShape(0,1), new PointShape(2,1));
            Assert.IsFalse(CollisionDetection.LineLine(l1, l2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void LinePolygon_Intersects_ReturnsTrue()
        {
            var arr = new NativeArray<PointShape>(4, Allocator.Temp);
            arr[0] = new PointShape(0, 0);
            arr[1] = new PointShape(3, 0);
            arr[2] = new PointShape(3, 3);
            arr[3] = new PointShape(0, 3);
            var pol = new PolygonShape(arr);
            var l = new LineShape(new PointShape(-1, 1), new PointShape(4, 1));
            Assert.IsTrue(CollisionDetection.LinePolygon(l, pol));
            arr.Dispose();
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void LineTriangle_Intersects_ReturnsTrue()
        {
            var t = new TriangleShape(new PointShape(0,0), new PointShape(4,0), new PointShape(2,3));
            var l = new LineShape(new PointShape(1, -1), new PointShape(1, 4)); // vertical line through
            Assert.IsTrue(CollisionDetection.LineTriangle(l, t));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PolygonPolygon_Overlapping_ReturnsTrue()
        {
            var arr1 = new NativeArray<PointShape>(4, Allocator.Temp);
            arr1[0] = new PointShape(0, 0);
            arr1[1] = new PointShape(2, 0);
            arr1[2] = new PointShape(2, 2);
            arr1[3] = new PointShape(0, 2);
            var pol1 = new PolygonShape(arr1);

            var arr2 = new NativeArray<PointShape>(4, Allocator.Temp);
            arr2[0] = new PointShape(1, 1);
            arr2[1] = new PointShape(3, 1);
            arr2[2] = new PointShape(3, 3);
            arr2[3] = new PointShape(1, 3);
            var pol2 = new PolygonShape(arr2);

            Assert.IsTrue(CollisionDetection.PolygonPolygon(pol1, pol2));

            arr1.Dispose();
            arr2.Dispose();
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void PolygonTriangle_Overlapping_ReturnsTrue()
        {
            var arr = new NativeArray<PointShape>(3, Allocator.Temp);
            arr[0] = new PointShape(0, 0);
            arr[1] = new PointShape(4, 0);
            arr[2] = new PointShape(2, 4);
            var pol = new PolygonShape(arr);
            var t = new TriangleShape(new PointShape(1, 1), new PointShape(3, 1), new PointShape(2, 2));
            Assert.IsTrue(CollisionDetection.PolygonTriangle(pol, t));
            arr.Dispose();
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void TriangleTriangle_Overlapping_ReturnsTrue()
        {
            var t1 = new TriangleShape(new PointShape(0,0), new PointShape(2,0), new PointShape(1,2));
            var t2 = new TriangleShape(new PointShape(0.5f, 0.5f), new PointShape(1.5f, 0.5f), new PointShape(1, 1.5f));
            Assert.IsTrue(CollisionDetection.TriangleTriangle(t1, t2));
            Assert.IsTrue(t1.IntersectTriangle(t2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void TriangleTriangle_Separate_ReturnsFalse()
        {
            var t1 = new TriangleShape(new PointShape(0,0), new PointShape(1,0), new PointShape(0,1));
            var t2 = new TriangleShape(new PointShape(3,3), new PointShape(4,3), new PointShape(3,4));
            Assert.IsFalse(CollisionDetection.TriangleTriangle(t1, t2));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.Easy)]
        public void GenericIntersect_DispatchWorks()
        {
            var circle = new CircleShape(0, 0, 1);
            var pointInside = new PointShape(0.5f, 0);
            // Intersect<TShape> on point should call IntersectPoint, which returns true
            Assert.IsTrue(pointInside.Intersect(circle)); // point.Intersect<CircleShape>
            // Intersect<TShape> on circle should call IntersectCircle
            Assert.IsTrue(circle.Intersect(pointInside));
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.Easy)]
        public void Distance_SquaredDistance_Consistency()
        {
            var a = new PointShape(3, 4);
            var b = new PointShape(0, 0);
            float sqDist = CollisionDetection.SquaredDistance(a, b);
            Assert.AreEqual(25f, sqDist, 1e-5f);
            float dist = math.sqrt(sqDist);
            Assert.AreEqual(dist, CollisionDetection.Distance(a, b), 1e-5f);
        }

        [Test]
        [Author(Metadata.Author.Vertoker)]
        [Category(Metadata.Category.Self)]
        [Category(Metadata.Category.VeryEasy)]
        public void ApproxEqual_WithEpsilon()
        {
            Assert.IsTrue(CollisionDetection.Approximately(1.0000001f, 1.0f));
            Assert.IsFalse(CollisionDetection.Approximately(1.001f, 1.0f));
        }
    }
}