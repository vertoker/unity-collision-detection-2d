using System;
using Unity.Mathematics;
using vertoker.CollisionDetection2D.Interfaces;

namespace vertoker.CollisionDetection2D
{
    public static class CollisionDetectionStatic
    {
        #region Static
        public const float Epsilon = 0.0001f;
        public const float DefaultBuf = 0.1f;

        public static float Distance<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            float distX = p2.X - p1.X, distY = p2.Y - p1.Y;
            return math.sqrt(distX * distX + distY * distY);
        }
        public static float Dot<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }
        #endregion
        
        #region Conversions
        public static PointShape ToPoint<T>(this T point) where T : unmanaged, IPoint
        {
            return new PointShape(point.X, point.Y);
        }
        public static CircleShape ToCircle<T>(this T circle) where T : unmanaged, ICircle
        {
            return new CircleShape(circle.X, circle.Y, circle.R);
        }
        public static LineShape ToLine<T>(this T line) where T : unmanaged, ILine<PointShape>
        {
            return new LineShape(line.P1, line.P2);
        }
        public static RectangleShape ToRectangle<T>(this T rectangle) where T : unmanaged, IRectangle
        {
            return new RectangleShape(rectangle.X, rectangle.Y, rectangle.W, rectangle.H);
        }
        public static TriangleShape ToTriangle<T>(this T triangle) where T : unmanaged, ITriangle<PointShape>
        {
            return new TriangleShape(triangle.P1, triangle.P2, triangle.P3);
        }
        public static PolygonShape ToPolygon<T>(this T polygon) where T : unmanaged, IPolygon<PointShape>
        {
            return new PolygonShape(polygon.Vertices);
        }
        #endregion
        
        #region PointShape
        public static bool PointPoint<TPoint1, TPoint2>(TPoint1 p1, TPoint2 p2) 
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return Math.Abs(p1.X - p2.X) < Epsilon && Math.Abs(p1.Y - p2.Y) < Epsilon;
        }
        public static bool PointCircle<TPoint1, TCircle2>(TPoint1 p, TCircle2 c)
            where TPoint1 : unmanaged, IPoint where TCircle2 : unmanaged, ICircle
        {
            return Distance(p, c) <= c.R;
        }
        public static bool PointRectangle<TPoint1, TRectangle2>(TPoint1 p, TRectangle2 r)
            where TPoint1 : unmanaged, IPoint where TRectangle2 : unmanaged, IRectangle
        {
            float w2 = r.W / 2, h2 = r.H / 2;
            return p.X >= r.X - w2 && p.X <= r.X + w2 && p.Y >= r.Y - h2 && p.Y <= r.Y + h2;
        }
        public static bool PointLine<TPoint1, TLine2>(TPoint1 p, TLine2 l, float buf = DefaultBuf)
            where TPoint1 : unmanaged, IPoint where TLine2 : unmanaged, ILine<PointShape>
        {
            var d1 = Distance(p, l.P1);
            var d2 = Distance(p, l.P2);
            var lineLen = Distance(l.P1, l.P2);
            return d1 + d2 >= lineLen - buf && d1 + d2 <= lineLen + buf;
        }
        public static bool PointPolygon<TPoint1, TPolygon2>(TPoint1 p, TPolygon2 pol)
            where TPoint1 : unmanaged, IPoint where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var collision = false;
            for (var current = 0; current < pol.Vertices.Length; current++)
            {
                var next = current + 1;
                if (next == pol.Vertices.Length)
                    next = 0;

                var vc = pol.Vertices[current];
                var vn = pol.Vertices[next];
                
                if (((vc.y >= p.Y && vn.y < p.Y) || (vc.y < p.Y && vn.y >= p.Y)) &&
                     (p.X < (vn.x - vc.x) * (p.Y - vc.y) / (vn.y - vc.y) + vc.x))
                    collision = !collision;
            }
            return collision;
        }
        public static bool PointTriangle<TPoint1, TTriangle2>(TPoint1 p, TTriangle2 t)
            where TPoint1 : unmanaged, IPoint where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            var areaOrig = math.abs((t.P2.x - t.P1.x) * (t.P3.y - t.P1.y) - (t.P3.x - t.P1.x) * (t.P2.y - t.P1.y));
            var area1 = math.abs((t.P1.x - p.X) * (t.P2.y - p.Y) - (t.P2.x - p.X) * (t.P1.y - p.Y));
            var area2 = math.abs((t.P2.x - p.X) * (t.P3.y - p.Y) - (t.P3.x - p.X) * (t.P2.y - p.Y));
            var area3 = math.abs((t.P3.x - p.X) * (t.P1.y - p.Y) - (t.P1.x - p.X) * (t.P3.y - p.Y));
            return math.abs(area1 + area2 + area3 - areaOrig) < Epsilon;
        }
        #endregion

        #region CircleShape
        public static bool CircleCircle<TCircle1, TCircle2>(TCircle1 c1, TCircle2 c2)
            where TCircle1 : unmanaged, ICircle where TCircle2 : unmanaged, ICircle
        {
            return Distance(c1, c2) <= c1.R + c2.R;
        }
        public static bool CircleRectangle<TCircle1, TRectangle2>(TCircle1 c, TRectangle2 r)
            where TCircle1 : unmanaged, ICircle where TRectangle2 : unmanaged, IRectangle
        {
            var mixX = r.X - r.W / 2;
            var maxX = r.X + r.W / 2;
            var mixY = r.Y - r.H / 2;
            var maxY = r.Y + r.H / 2;

            var p = c.ToPoint();
            
            if (c.X < mixX) p.x = mixX;
            else if (c.X > maxX) p.x = maxX;
            
            if (c.Y < mixY) p.y = mixY;
            else if (c.Y > maxY) p.y = maxY;

            return Distance(c, p) <= c.R;
        }
        public static bool CircleLine<TCircle1, TLine2>(TCircle1 c, TLine2 l)
            where TCircle1 : unmanaged, ICircle where TLine2 : unmanaged, ILine<PointShape>
        {
            var inside1 = PointCircle(l.P1, c);
            var inside2 = PointCircle(l.P2, c);
            if (inside1 || inside2)
                return true;

            var len = Distance(l.P1, l.P2);
            var dot = Dot(c.ToPoint() - l.P1, l.P2 - l.P1) / (len * len);
            // var dot = ((cx - lx1) * (lx2 - lx1) + (cy - ly1) * (ly2 - ly1)) / (len * len);
            var closest = l.P1 + dot * (l.P2 - l.P1);

            if (!PointLine(closest, l))
                return false;

            return Distance(c, closest) <= c.R;
        }
        public static bool CirclePolygon<TCircle1, TPolygon2>(TCircle1 c, TPolygon2 pol)
            where TCircle1 : unmanaged, ICircle where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var inPolygon = PointPolygon(c, pol);
            if (inPolygon) return true;

            var tempLine = new LineShape();
            for (var current = 0; current < pol.Vertices.Length; current++)
            {
                var next = current + 1;
                if (next == pol.Vertices.Length)
                    next = 0;
                
                tempLine.p1 = pol.Vertices[current];
                tempLine.p2 = pol.Vertices[next];
                if (CircleLine(c, tempLine)) return true;
            }
            return false;
        }
        public static bool CircleTriangle<TCircle1, TTriangle2>(TCircle1 c, TTriangle2 t)
            where TCircle1 : unmanaged, ICircle where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            if (PointTriangle(c, t)) return true;

            var p1 = Distance(c, t.P1);
            if (p1 <= c.R) return true;

            var p2 = Distance(c, t.P2);
            if (p2 <= c.R) return true;

            var p3 = Distance(c, t.P3);
            if (p3 <= c.R) return true;

            var dist = Distance(t.P1, t.P2);
            var s = (dist + p1 + p2) / 2f;
            if (2f * math.sqrt(s * (s - dist) * (s - p1) * (s - p2)) / dist <= c.R)
                return true;

            dist = Distance(t.P2, t.P3);
            s = (dist + p2 + p3) / 2f;
            if (2f * math.sqrt(s * (s - dist) * (s - p2) * (s - p3)) / dist <= c.R)
                return true;

            dist = Distance(t.P1, t.P3);
            s = (dist + p1 + p3) / 2f;
            return 2f * math.sqrt(s * (s - dist) * (s - p1) * (s - p3)) / dist <= c.R;
        }
        #endregion

        #region RectangleShape
        public static bool RectangleRectangle<TRectangle1, TRectangle2>(TRectangle1 r1, TRectangle2 r2)
            where TRectangle1 : unmanaged, IRectangle where TRectangle2 : unmanaged, IRectangle
        {
            float w1H = r1.W / 2f, h1H = r1.H / 2f, w2H = r2.W / 2f, h2H = r2.H / 2f;
            return r1.X + w1H >= r2.X - w2H
                   && r1.X - w1H <= r2.X + w2H
                   && r1.Y + h1H >= r2.Y - h2H
                   && r1.Y - h1H <= r2.Y + h2H;
        }
        public static bool RectangleLine<TRectangle1, TLine2>(TRectangle1 r, TLine2 l)
            where TRectangle1 : unmanaged, IRectangle where TLine2 : unmanaged, ILine<PointShape>
        {
            float minX = r.X - r.W / 2f, maxX = r.X + r.W / 2f;
            float minY = r.Y - r.H / 2f, maxY = r.Y + r.H / 2f;
            float x1 = l.P1.x, y1 = l.P1.y, x2 = l.P2.x, y2 = l.P2.y;
            var left = LineLine(x1, y1, x2, y2, minX, minY, minX, maxY);
            var right = LineLine(x1, y1, x2, y2, maxX, minY, maxX, maxY);
            var top = LineLine(x1, y1, x2, y2, minX, minY, maxX, minY);
            var bottom = LineLine(x1, y1, x2, y2, minX, maxY, maxX, maxY);
            return left || right || top || bottom;
        }
        public static bool RectanglePolygon<TRectangle1, TPolygon2>(TRectangle1 r, TPolygon2 pol)
            where TRectangle1 : unmanaged, IRectangle where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var inPolygon = PointPolygon(r, pol);
            if (inPolygon) return true;

            var tempLine = new LineShape();
            for (var current = 0; current < pol.Vertices.Length; current++)
            {
                var next = current + 1;
                if (next == pol.Vertices.Length)
                    next = 0;

                tempLine.p1 = pol.Vertices[current];
                tempLine.p2 = pol.Vertices[next];
                if (RectangleLine(r, tempLine)) return true;
            }
            return false;
        }
        public static bool RectangleTriangle<TRectangle1, TTriangle2>(TRectangle1 r, TTriangle2 t)
            where TRectangle1 : unmanaged, IRectangle where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            if (PointRectangle(t.P1, r) || 
                PointRectangle(t.P2, r) || 
                PointRectangle(t.P3, r))
                return true;

            var minX = r.X - r.W / 2;
            var minY = r.Y - r.H / 2;
            if (PointTriangle(new PointShape(minX, minY), t)) return true;

            var maxX = r.X + r.W / 2;
            if (PointTriangle(new PointShape(maxX, minY), t)) return true;

            var maxY = r.Y + r.H / 2;
            return PointTriangle(new PointShape(minX, maxY), t) || PointTriangle(new PointShape(maxX, maxY), t);
        }
        #endregion

        #region LineShape
        private static bool LineLine(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            var uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            var uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            return uA is >= 0 and <= 1 && uB is >= 0 and <= 1;
        }
        public static bool LineLine<TLine1, TPoint2>(TLine1 l1, TPoint2 p1, TPoint2 p2)
            where TLine1 : unmanaged, ILine<PointShape> where TPoint2 : unmanaged, IPoint
        {
            float x1 = l1.P1.x, x2 = l1.P2.x, x3 = p1.X, x4 = p2.X;
            float y1 = l1.P1.y, y2 = l1.P2.y, y3 = p1.Y, y4 = p2.Y;
            return LineLine(x1, y1, x2, y2, x3, y3, x4, y4);
        }
        public static bool LineLine<TLine1, TLine2>(TLine1 l1, TLine2 l2)
            where TLine1 : unmanaged, ILine<PointShape> where TLine2 : unmanaged, ILine<PointShape>
        {
            float x1 = l1.P1.x, x2 = l1.P2.x, x3 = l2.P1.x, x4 = l2.P2.x;
            float y1 = l1.P1.y, y2 = l1.P2.y, y3 = l2.P1.y, y4 = l2.P2.y;
            return LineLine(x1, y1, x2, y2, x3, y3, x4, y4);
        }
        public static bool LinePolygon<TLine1, TPolygon2>(TLine1 l, TPolygon2 pol)
            where TLine1 : unmanaged, ILine<PointShape> where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var inPolygon = PointPolygon(l.P1, pol) || PointPolygon(l.P2, pol);
            if (inPolygon) return true;

            var tempLine = new LineShape();
            for (var current = 0; current < pol.Vertices.Length; current++)
            {
                var next = current + 1;
                if (next == pol.Vertices.Length)
                    next = 0;

                tempLine.p1 = pol.Vertices[current];
                tempLine.p2 = pol.Vertices[next];
                if (LineLine(l, tempLine)) return true;
            }
            return false;
        }
        public static bool LineTriangle<TLine1, TTriangle2>(TLine1 l, TTriangle2 t)
            where TLine1 : unmanaged, ILine<PointShape> where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            return PointTriangle(l.P1, t) || PointTriangle(l.P2, t) ||
                LineLine(l, new LineShape(t.P1, t.P2)) ||
                LineLine(l, new LineShape(t.P1, t.P3)) ||
                LineLine(l, new LineShape(t.P2, t.P3));
        }
        #endregion

        #region PolygonShape
        public static bool PolygonPolygon<TPolygon1, TPolygon2>(TPolygon1 pol1, TPolygon2 pol2)
            where TPolygon1 : unmanaged, IPolygon<PointShape> where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var inPolygon = PointPolygon(pol2.Vertices[0], pol1);
            if (inPolygon) return true;

            var tempLine = new LineShape();
            for (var current = 0; current < pol1.Vertices.Length; current++)
            {
                var next = current + 1;
                if (next == pol1.Vertices.Length)
                    next = 0;

                tempLine.p1 = pol1.Vertices[current];
                tempLine.p2 = pol2.Vertices[next];
                if (LinePolygon(tempLine, pol2)) return true;
            }
            return false;
        }
        public static bool PolygonTriangle<TPolygon1, TTriangle2>(TPolygon1 pol, TTriangle2 t)
            where TPolygon1 : unmanaged, IPolygon<PointShape> where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            var inPolygon = PointPolygon(t.P1, pol) || PointPolygon(t.P2, pol) || PointPolygon(t.P3, pol);
            if (inPolygon) return true;

            var tempLine = new LineShape();
            for (var current = 0; current < pol.Vertices.Length; current++)
            {
                var next = current + 1;
                if (next == pol.Vertices.Length)
                    next = 0;

                tempLine.p1 = pol.Vertices[current];
                tempLine.p2 = pol.Vertices[next];
                if (LineTriangle(tempLine, t)) return true;
            }
            return false;
        }
        #endregion

        #region TriangleShape
        private static bool TriangleTriangle(float x1, float y1, float x2, float y2, float x3, float y3,
            float x4, float y4, float x5, float y5, float x6, float y6)
        {
            var areaOrig1 = math.abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));
            if (PointTriangleOrig(x4, y4, x1, y1, x2, y2, x3, y3, areaOrig1) ||
                PointTriangleOrig(x5, y5, x1, y1, x2, y2, x3, y3, areaOrig1) ||
                PointTriangleOrig(x6, y6, x1, y1, x2, y2, x3, y3, areaOrig1))
                return true;

            var areaOrig2 = math.abs((x5 - x4) * (y6 - y4) - (x6 - x4) * (y5 - y4));
            return PointTriangleOrig(x1, y1, x4, y4, x5, y5, x6, y6, areaOrig2) ||
                   PointTriangleOrig(x2, y2, x4, y4, x5, y5, x6, y6, areaOrig2) ||
                   PointTriangleOrig(x3, y3, x4, y4, x5, y5, x6, y6, areaOrig2) ||
                   LineLine(x1, y1, x2, y2, x4, y4, x5, y5) ||
                   LineLine(x1, y1, x2, y2, x4, y4, x6, y6) ||
                   LineLine(x1, y1, x2, y2, x5, y5, x6, y6) ||
                   LineLine(x1, y1, x3, y3, x4, y4, x5, y5) ||
                   LineLine(x1, y1, x3, y3, x4, y4, x6, y6) ||
                   LineLine(x1, y1, x3, y3, x5, y5, x6, y6) ||
                   LineLine(x2, y2, x3, y3, x4, y4, x5, y5) ||
                   LineLine(x2, y2, x3, y3, x4, y4, x6, y6) ||
                   LineLine(x2, y2, x3, y3, x5, y5, x6, y6);

            static bool PointTriangleOrig(float px, float py, float x1, float y1, float x2, float y2, float x3, float y3, float areaOrig)
            {
                var area1 = math.abs((x1 - px) * (y2 - py) - (x2 - px) * (y1 - py));
                var area2 = math.abs((x2 - px) * (y3 - py) - (x3 - px) * (y2 - py));
                var area3 = math.abs((x3 - px) * (y1 - py) - (x1 - px) * (y3 - py));
                return Math.Abs(area1 + area2 + area3 - areaOrig) < Epsilon;
            }
        }
        public static bool TriangleTriangle<TTriangle1, TTriangle2>(TTriangle1 t1, TTriangle2 t2)
            where TTriangle1 : unmanaged, ITriangle<PointShape> where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            float x1 = t1.P1.x, x2 = t1.P2.x, x3 = t1.P3.x, x4 = t2.P1.x, x5 = t2.P2.x, x6 = t2.P3.x;
            float y1 = t1.P1.y, y2 = t1.P2.y, y3 = t1.P3.y, y4 = t2.P1.y, y5 = t2.P2.y, y6 = t2.P3.y;
            return TriangleTriangle(x1, y1, x2, y2, x3, y3, x4, y4, x5, y5, x6, y6);
        }
        #endregion
    }
}
