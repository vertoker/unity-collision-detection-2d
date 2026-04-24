using NUnit.Framework;
using Unity.Collections;
using Unity.Mathematics;
using vertoker.CollisionDetection2D;
using vertoker.CollisionDetection2D.Interfaces;

namespace vertoker.CollisionDetection2D.Tests
{
    [TestFixture]
    public class CollisionDetectionEdgeCaseTests
    {
        // ---------------------------------------------------------------
        // Zero, negative and very large values
        // ---------------------------------------------------------------

        [Test]
        public void PointPoint_BothZeros_ReturnsTrue()
        {
            var a = new PointShape(0f, 0f);
            var b = new PointShape(0f, 0f);
            Assert.IsTrue(CollisionDetection.PointPoint(a, b));
        }

        [Test]
        public void PointPoint_ZeroVsEpsilon_ReturnsFalse()
        {
            var a = new PointShape(0f, 0f);
            var b = new PointShape(CollisionDetection.Epsilon * 2, 0f);
            Assert.IsFalse(CollisionDetection.PointPoint(a, b));
        }

        [Test]
        public void PointCircle_ZeroRadiusCircle_CenterOnly()
        {
            var p = new PointShape(1f, 0f);
            var c = new CircleShape(1f, 0f, 0f);
            Assert.IsTrue(CollisionDetection.PointCircle(p, c));

            p = new PointShape(1.0001f, 0f);
            Assert.IsFalse(CollisionDetection.PointCircle(p, c));
        }

        [Test]
        public void PointCircle_PointExactlyOnBoundary()
        {
            var c = new CircleShape(0f, 0f, 5f);
            var p = new PointShape(3f, 4f); // distance = 5 exactly
            Assert.IsTrue(CollisionDetection.PointCircle(p, c));
        }

        [Test]
        public void PointCircle_HugeCoordinates()
        {
            const float max = 1e7f;
            var c = new CircleShape(max, -max, 1f);
            
            var p = new PointShape(max + 0.5f, -max);
            Assert.IsTrue(CollisionDetection.PointCircle(p, c));

            p = new PointShape(max + 2f, -max);
            Assert.IsFalse(CollisionDetection.PointCircle(p, c));
        }

        [Test]
        public void PointRectangle_ZeroWidthRect()
        {
            // degenerate rectangle: zero width, height = 2 (vertical line segment)
            var r = new RectangleShape(0f, 0f, 0f, 2f);
            // point exactly at centre
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(0f, 0f), r));
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(0f, 1f), r));
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(0f, -1f), r));
            Assert.IsFalse(CollisionDetection.PointRectangle(new PointShape(0.001f, 0f), r));
        }

        [Test]
        public void PointRectangle_ZeroHeightRect()
        {
            var r = new RectangleShape(2f, 2f, 2f, 0f);
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(2f, 2f), r));
            Assert.IsFalse(CollisionDetection.PointRectangle(new PointShape(2f, 2.001f), r));
        }

        [Test]
        public void PointRectangle_NegativeCoordinates()
        {
            var r = new RectangleShape(-3f, -3f, 2f, 2f); // from -4 to -2
            Assert.IsTrue(CollisionDetection.PointRectangle(new PointShape(-3f, -3f), r));
            Assert.IsFalse(CollisionDetection.PointRectangle(new PointShape(0f, 0f), r));
        }

        // ---------------------------------------------------------------
        // Parallel and perpendicular lines, degenerate lines
        // ---------------------------------------------------------------

        [Test]
        public void PointLine_PointOnParallelLine_BeyondSegment()
        {
            var l = new LineShape(new PointShape(0f, 0f), new PointShape(4f, 0f)); // horizontal
            var p = new PointShape(5f, 0f); // collinear but beyond end
            // Default buf = 0.001, distance = 1.0 -> false
            Assert.IsFalse(CollisionDetection.PointLine(p, l));

            // point exactly at endpoint (buf covers)
            p = new PointShape(4f, 0f);
            Assert.IsTrue(CollisionDetection.PointLine(p, l));
        }

        [Test]
        public void PointLine_PointOnPerpendicularLine_IntersectionAtEndpoint()
        {
            var l = new LineShape(new PointShape(1f, 0f), new PointShape(1f, 3f)); // vertical
            var p = new PointShape(1f, 0f); // endpoint
            Assert.IsTrue(CollisionDetection.PointLine(p, l));
            p = new PointShape(1.001f, 0f); // just outside buf
            Assert.IsFalse(CollisionDetection.PointLine(p, l));
        }

        [Test]
        public void LineLine_ParallelHorizontal_NoIntersection()
        {
            var l1 = new LineShape(new PointShape(0f, 0f), new PointShape(2f, 0f));
            var l2 = new LineShape(new PointShape(1f, 1f), new PointShape(3f, 1f));
            Assert.IsFalse(CollisionDetection.LineLine(l1, l2));
        }

        [Test]
        public void LineLine_ParallelVertical_NoIntersection()
        {
            var l1 = new LineShape(new PointShape(0f, 0f), new PointShape(0f, 2f));
            var l2 = new LineShape(new PointShape(1f, 0f), new PointShape(1f, 2f));
            Assert.IsFalse(CollisionDetection.LineLine(l1, l2));
        }

        [Test]
        public void LineLine_Perpendicular_IntersectsAtMidpoint()
        {
            var l1 = new LineShape(new PointShape(-1f, 0f), new PointShape(1f, 0f)); // horizontal
            var l2 = new LineShape(new PointShape(0f, -1f), new PointShape(0f, 1f)); // vertical
            Assert.IsTrue(CollisionDetection.LineLine(l1, l2));
        }

        [Test]
        public void LineLine_Perpendicular_NoIntersection()
        {
            var l1 = new LineShape(new PointShape(-1f, 0f), new PointShape(1f, 0f));
            var l2 = new LineShape(new PointShape(2f, -1f), new PointShape(2f, 1f)); // vertical offset
            Assert.IsFalse(CollisionDetection.LineLine(l1, l2));
        }

        [Test]
        public void LineLine_DegeneratePointLine_NoCrash()
        {
            var p = new LineShape(new PointShape(1f, 1f), new PointShape(1f, 1f)); // zero length
            var l = new LineShape(new PointShape(0f, 0f), new PointShape(2f, 2f));
            Assert.IsFalse(CollisionDetection.LineLine(p, l));
        }

        [Test]
        public void LineLine_BothDegenerate_NoCrash()
        {
            var p1 = new LineShape(new PointShape(2f, 3f), new PointShape(2f, 3f));
            var p2 = new LineShape(new PointShape(2f, 3f), new PointShape(2f, 3f));
            Assert.IsFalse(CollisionDetection.LineLine(p1, p2));
        }

        // ---------------------------------------------------------------
        // Tangent and boundary touches (epsilon sensitive)
        // ---------------------------------------------------------------

        [Test]
        public void CircleCircle_TangentExternally()
        {
            var c1 = new CircleShape(0f, 0f, 2f);
            var c2 = new CircleShape(5f, 0f, 3f); // distance = 5 = 2+3
            Assert.IsTrue(CollisionDetection.CircleCircle(c1, c2));
        }

        [Test]
        public void CircleCircle_NearlyTangent_Outside()
        {
            var c1 = new CircleShape(0f, 0f, 2f);
            var c2 = new CircleShape(5.0001f, 0f, 3f); // distance just > 5
            // Due to epsilon in SquareDistance maybe false? We test robustness.
            bool result = CollisionDetection.CircleCircle(c1, c2);
            // Not heavily asserting, but we want no crash.
            Assert.IsFalse(result); // 5.0001 squared > 25.001 > 25
        }

        [Test]
        public void CircleRectangle_TangentFromOutside()
        {
            var r = new RectangleShape(0f, 0f, 4f, 4f); // from -2..2
            var c = new CircleShape(5f, 0f, 3f); // closest point on rect is (2,0), distance=3 exactly
            Assert.IsTrue(CollisionDetection.CircleRectangle(c, r));
        }

        [Test]
        public void CircleRectangle_CircleInsideRectangle_TangentToEdge()
        {
            var r = new RectangleShape(0f, 0f, 4f, 4f);
            var c = new CircleShape(0f, 0f, 2f); // circle radius 2, center at (0,0): touches all edges
            Assert.IsTrue(CollisionDetection.CircleRectangle(c, r));
        }

        // ---------------------------------------------------------------
        // Zero‑area triangles and polygons with few vertices
        // ---------------------------------------------------------------

        [Test]
        public void PointTriangle_DegenerateTriangle_ZeroArea()
        {
            // collinear points
            var t = new TriangleShape(new PointShape(0f, 0f), new PointShape(1f, 1f), new PointShape(2f, 2f));
            var p = new PointShape(0.5f, 0.5f); // on the line
            Assert.IsFalse(CollisionDetection.PointTriangle(p, t)); // areaOrig < Epsilon -> false
        }

        [Test]
        public void CircleTriangle_DegenerateTriangle_ReturnsFalse()
        {
            var t = new TriangleShape(new PointShape(0f, 0f), new PointShape(0f, 2f), new PointShape(0f, 4f));
            var c = new CircleShape(0f, 2f, 0.5f);
            Assert.IsFalse(CollisionDetection.CircleTriangle(c, t));
        }

        [Test]
        public void PolygonPolygon_OneWithLessThan3Vertices_ReturnsFalse()
        {
            var arr1 = new NativeArray<PointShape>(3, Allocator.Temp);
            arr1[0] = new PointShape(0, 0);
            arr1[1] = new PointShape(1, 0);
            arr1[2] = new PointShape(0, 1);
            var pol1 = new PolygonShape(arr1);

            var arr2 = new NativeArray<PointShape>(2, Allocator.Temp); // only 2 vertices
            arr2[0] = new PointShape(0.2f, 0.2f);
            arr2[1] = new PointShape(0.3f, 0.3f);
            var pol2 = new PolygonShape(arr2);

            Assert.IsFalse(CollisionDetection.PolygonPolygon(pol1, pol2));
            arr1.Dispose();
            arr2.Dispose();
        }

        [Test]
        public void TriangleTriangle_OneDegenerate_ReturnsFalse()
        {
            var good = new TriangleShape(new PointShape(0,0), new PointShape(2,0), new PointShape(1,2));
            var bad = new TriangleShape(new PointShape(0,0), new PointShape(2,0), new PointShape(1,0)); // flat
            Assert.IsFalse(CollisionDetection.TriangleTriangle(good, bad));
        }

        // ---------------------------------------------------------------
        // Very small shapes, epsilon touches
        // ---------------------------------------------------------------

        [Test]
        public void CircleRectangle_VerySmallRect_InsideCircle()
        {
            var c = new CircleShape(0f, 0f, 1f);
            var r = new RectangleShape(0f, 0f, 0.001f, 0.001f);
            Assert.IsTrue(CollisionDetection.CircleRectangle(c, r));
        }

        [Test]
        public void RectangleRectangle_ZeroSizeBoth_OverlapAtPoint()
        {
            var r1 = new RectangleShape(0f, 0f, 0f, 0f);
            var r2 = new RectangleShape(0f, 0f, 0f, 0f);
            Assert.IsTrue(CollisionDetection.RectangleRectangle(r1, r2));
        }

        [Test]
        public void RectangleRectangle_ZeroSize_Separate()
        {
            var r1 = new RectangleShape(0f, 0f, 0f, 0f);
            var r2 = new RectangleShape(1f, 0f, 0f, 0f);
            Assert.IsFalse(CollisionDetection.RectangleRectangle(r1, r2));
        }

        // ---------------------------------------------------------------
        // Conversions and operators (optional but nice)
        // ---------------------------------------------------------------

        [Test]
        public void PointShapeOperators_AdditionSubtraction()
        {
            var a = new PointShape(3f, -2f);
            var b = new PointShape(1f, 5f);
            var sum = a + b;
            Assert.AreEqual(4f, sum.x, 1e-6f);
            Assert.AreEqual(3f, sum.y, 1e-6f);

            var diff = a - b;
            Assert.AreEqual(2f, diff.x);
            Assert.AreEqual(-7f, diff.y);
        }

        [Test]
        public void Distance_SquaredDistance_NegativeCoordinates()
        {
            var a = new PointShape(-1f, -2f);
            var b = new PointShape(2f, 2f);
            float sqDist = CollisionDetection.SquaredDistance(a, b);
            Assert.AreEqual(25f, sqDist, 1e-5f);
        }

        [Test]
        public void Approximately_ZeroEpsilon()
        {
            Assert.IsTrue(CollisionDetection.Approximately(0f, 0f));
            Assert.IsTrue(CollisionDetection.Approximately(0f, CollisionDetection.Epsilon * 0.5f));
            Assert.IsFalse(CollisionDetection.Approximately(0f, CollisionDetection.Epsilon * 2f));
        }

        // ---------------------------------------------------------------
        // Mixed shape dispatch via generic Intersect
        // ---------------------------------------------------------------

        [Test]
        public void GenericDispatch_CircleVsRectangleTangent()
        {
            var circle = new CircleShape(5f, 0f, 3f);
            var rect = new RectangleShape(0f, 0f, 4f, 4f);
            Assert.IsTrue(circle.Intersect(rect));
            Assert.IsTrue(rect.Intersect(circle));
        }

        [Test]
        public void GenericDispatch_LineVsPolygon_NoIntersection()
        {
            var arr = new NativeArray<PointShape>(4, Allocator.Temp);
            arr[0] = new PointShape(0, 0);
            arr[1] = new PointShape(2, 0);
            arr[2] = new PointShape(2, 2);
            arr[3] = new PointShape(0, 2);
            var pol = new PolygonShape(arr);
            var line = new LineShape(new PointShape(3, 0), new PointShape(3, 2));
            Assert.IsFalse(line.Intersect(pol));
            arr.Dispose();
        }
    }
}