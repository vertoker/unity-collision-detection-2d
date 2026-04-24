using System;

namespace vertoker.CollisionDetection2D
{
    public static class CollisionDetectionStatic
    {
        #region Static

        private const float Tolerance = 0.0001f;

        private static float Sqrt(float x)
        {
            if (x <= 0)
                return 0;
            var root = x / 3;
            for (var i = 0; i < 32; i++)
                root = (root + x / root) / 2;
            return root;
        }
        private static float Abs(float x)
        {
            if (x < 0)
                return -x;
            return x;
        }
        private static float Distance(float x1, float y1, float x2, float y2)
        {
            float distX = x2 - x1, distY = y2 - y1;
            return Sqrt((distX * distX) + (distY * distY));
        }
        #endregion

        #region PointShape
        public static bool PointPoint(float x1, float y1, float x2, float y2)
        {
            return Math.Abs(x1 - x2) < Tolerance && Math.Abs(y1 - y2) < Tolerance;
        }
        public static bool PointCircle(float px, float py, float cx, float cy, float cr)
        {
            return Distance(px, py, cx, cy) <= cr;
        }
        public static bool PointRectangle(float px, float py, float rx, float ry, float rw, float rh)
        {
            float w2 = rw / 2, h2 = rh / 2;
            return px >= rx - w2 && px <= rx + w2 && py >= ry - h2 && py <= ry + h2;
        }
        public static bool PointLine(float px, float py, float lx1, float ly1, float lx2, float ly2, float buf = 0.1f)
        {
            var d1 = Distance(px, py, lx1, ly1);
            var d2 = Distance(px, py, lx2, ly2);
            var lineLen = Distance(lx1, ly1, lx2, ly2);
            return d1 + d2 >= lineLen - buf && d1 + d2 <= lineLen + buf;
        }
        public static bool PointPolygon(float px, float py, PointShape[] vertices)
        {
            var collision = false;
            for (int current = 0; current < vertices.Length; current++)
            {
                var next = current + 1;
                if (next == vertices.Length)
                    next = 0;

                var vc = vertices[current];
                var vn = vertices[next];

                if (((vc.Y >= py && vn.Y < py) || (vc.Y < py && vn.Y >= py)) &&
                     (px < (vn.X - vc.X) * (py - vc.Y) / (vn.Y - vc.Y) + vc.X))
                    collision = !collision;
            }
            return collision;
        }
        public static bool PointTriangle(float px, float py, float x1, float y1, float x2, float y2, float x3, float y3)
        {
            var areaOrig = Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));
            var area1 = Abs((x1 - px) * (y2 - py) - (x2 - px) * (y1 - py));
            var area2 = Abs((x2 - px) * (y3 - py) - (x3 - px) * (y2 - py));
            var area3 = Abs((x3 - px) * (y1 - py) - (x1 - px) * (y3 - py));
            return Math.Abs(area1 + area2 + area3 - areaOrig) < Tolerance;
        }
        #endregion

        #region CircleShape
        public static bool CircleCircle(float x1, float y1, float r1, float x2, float y2, float r2)
        {
            return Distance(x1, y1, x2, y2) <= r1 + r2;
        }
        public static bool CircleRectangle(float cx, float cy, float cr, float rx, float ry, float rw, float rh)
        {
            var mixX = rx - rw / 2;
            var maxX = rx + rw / 2;
            var mixY = ry - rh / 2;
            var maxY = ry + rh / 2;

            var testX = cx;
            var testY = cy;
            
            if (cx < mixX) testX = mixX;
            else if (cx > maxX) testX = maxX;
            
            if (cy < mixY) testY = mixY;
            else if (cy > maxY) testY = maxY;

            return Distance(cx, cy, testX, testY) <= cr;
        }
        public static bool CircleLine(float cx, float cy, float cr, float lx1, float ly1, float lx2, float ly2, float buf = 0.1f)
        {
            var inside1 = PointCircle(lx1, ly1, cx, cy, cr);
            var inside2 = PointCircle(lx2, ly2, cx, cy, cr);
            if (inside1 || inside2)
                return true;

            var len = Distance(lx1, ly1, lx2, ly2);
            var dot = (((cx - lx1) * (lx2 - lx1)) + ((cy - ly1) * (ly2 - ly1))) / (len * len);

            var closestX = lx1 + (dot * (lx2 - lx1));
            var closestY = ly1 + (dot * (ly2 - ly1));

            if (!PointLine(closestX, closestY, lx1, ly1, lx2, ly2))
                return false;

            return Distance(cx, cy, closestX, closestY) <= cr;
        }
        public static bool CirclePolygon(float cx, float cy, float cr, PointShape[] vertices)
        {
            var inPolygon = PointPolygon(cx, cy, vertices);
            if (inPolygon) return true;

            for (var current = 0; current < vertices.Length; current++)
            {
                var next = current + 1;
                if (next == vertices.Length)
                    next = 0;

                var vc = vertices[current];
                var vn = vertices[next];

                if (CircleLine(cx, cy, cr, vc.X, vc.Y, vn.X, vn.Y))
                    return true;
            }
            return false;
        }
        public static bool CircleTriangle(float cx, float cy, float cr, float x1, float y1, float x2, float y2, float x3, float y3)
        {
            if (PointTriangle(cx, cy, x1, y1, x2, y2, x3, y3))
                return true;

            var p1 = Distance(cx, cy, x1, y1);
            if (p1 <= cr)
                return true;

            var p2 = Distance(cx, cy, x2, y2);
            if (p2 <= cr)
                return true;

            var p3 = Distance(cx, cy, x3, y3);
            if (p3 <= cr)
                return true;

            var dist = Distance(x1, y1, x2, y2);
            var s = (dist + p1 + p2) / 2;
            if (2 * Sqrt(s * (s - dist) * (s - p1) * (s - p2)) / dist <= cr)
                return true;

            dist = Distance(x2, y2, x3, y3);
            s = (dist + p2 + p3) / 2;
            if (2 * Sqrt(s * (s - dist) * (s - p2) * (s - p3)) / dist <= cr)
                return true;

            dist = Distance(x1, y1, x3, y3);
            s = (dist + p1 + p3) / 2;
            return 2 * Sqrt(s * (s - dist) * (s - p1) * (s - p3)) / dist <= cr;
        }
        #endregion

        #region RectangleShape
        public static bool RectangleRectangle(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2)
        {
            float w1H = w1 / 2, h1H = h1 / 2, w2H = w2 / 2, h2H = h2 / 2;
            return x1 + w1H >= x2 - w2H && x1 - w1H <= x2 + w2H && y1 + h1H >= y2 - h2H && y1 - h1H <= y2 + h2H;
        }
        public static bool RectangleLine(float rx, float ry, float rw, float rh, float x1, float y1, float x2, float y2)
        {
            float minX = rx - rw / 2, maxX = rx + rw / 2;
            float mixY = ry - rh / 2, maxY = ry + rh / 2;
            var left = LineLine(x1, y1, x2, y2, minX, mixY, minX, maxY);
            var right = LineLine(x1, y1, x2, y2, maxX, mixY, maxX, maxY);
            var top = LineLine(x1, y1, x2, y2, minX, mixY, maxX, mixY);
            var bottom = LineLine(x1, y1, x2, y2, minX, maxY, maxX, maxY);
            return left || right || top || bottom;
        }
        public static bool RectanglePolygon(float rx, float ry, float rw, float rh, PointShape[] vertices)
        {
            var inPolygon = PointPolygon(rx, ry, vertices);
            if (inPolygon) return true;

            for (var current = 0; current < vertices.Length; current++)
            {
                var next = current + 1;
                if (next == vertices.Length)
                    next = 0;

                var vc = vertices[current];
                var vn = vertices[next];

                if (RectangleLine(rx, ry, rw, rh, vc.X, vc.Y, vn.X, vn.Y))
                    return true;
            }
            return false;
        }
        public static bool RectangleTriangle(float rx, float ry, float rw, float rh, float x1, float y1, float x2, float y2, float x3, float y3)
        {
            if (PointRectangle(x1, y1, rx, ry, rw, rh) || 
                PointRectangle(x2, y2, rx, ry, rw, rh) || 
                PointRectangle(x3, y3, rx, ry, rw, rh))
                return true;

            var minX = rx - rw / 2;
            var minY = ry - rh / 2;
            if (PointTriangle(minX, minY, x1, y1, x2, y2, x3, y3))
                return true;

            var maxX = rx + rw / 2;
            if (PointTriangle(maxX, minY, x1, y1, x2, y2, x3, y3))
                return true;

            var maxY = ry + rh / 2;
            return PointTriangle(minX, maxY, x1, y1, x2, y2, x3, y3) || 
                   PointTriangle(maxX, maxY, x1, y1, x2, y2, x3, y3);
        }
        #endregion

        #region LineShape
        public static bool LineLine(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            var uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            var uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
        }
        public static bool LinePolygon(float x1, float y1, float x2, float y2, PointShape[] vertices)
        {
            var inPolygon = PointPolygon(x1, y1, vertices) || PointPolygon(x2, y2, vertices);
            if (inPolygon) return true;

            for (var current = 0; current < vertices.Length; current++)
            {
                var next = current + 1;
                if (next == vertices.Length)
                    next = 0;

                if (LineLine(x1, y1, x2, y2, vertices[current].X, vertices[current].Y, vertices[next].X, vertices[next].Y))
                    return true;
            }
            return false;
        }
        public static bool LineTriangle(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float x5, float y5)
        {
            return PointTriangle(x1, y1, x3, y3, x4, y4, x5, y5) ||
                PointTriangle(x2, y2, x3, y3, x4, y4, x5, y5) ||
                LineLine(x1, y1, x2, y2, x3, y3, x4, y4) ||
                LineLine(x1, y1, x2, y2, x3, y3, x5, y5) ||
                LineLine(x1, y1, x2, y2, x4, y4, x5, y5);
        }
        #endregion

        #region PolygonShape
        public static bool PolygonPolygon(PointShape[] vertices1, PointShape[] vertices2)
        {
            var inPolygon = PointPolygon(vertices2[0].X, vertices2[0].Y, vertices1);
            if (inPolygon) return true;

            for (var current = 0; current < vertices1.Length; current++)
            {
                var next = current + 1;
                if (next == vertices1.Length)
                    next = 0;

                var vc = vertices1[current];
                var vn = vertices1[next];

                if (LinePolygon(vc.X, vc.Y, vn.X, vn.Y, vertices2))
                    return true;
            }
            return false;
        }
        public static bool PolygonTriangle(PointShape[] vertices, float x1, float y1, float x2, float y2, float x3, float y3)
        {
            var inPolygon = PointPolygon(x1, y1, vertices) || 
                            PointPolygon(x2, y2, vertices) ||
                            PointPolygon(x3, y3, vertices);
            if (inPolygon) return true;

            for (var current = 0; current < vertices.Length; current++)
            {
                var next = current + 1;
                if (next == vertices.Length)
                    next = 0;

                var vc = vertices[current];
                var vn = vertices[next];

                if (LineTriangle(vc.X, vc.Y, vn.X, vn.Y, x1, y1, x2, y2, x3, y3))
                    return true;
            }
            return false;
        }
        #endregion

        #region TriangleShape
        public static bool TriangleTriangle(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float x5, float y5, float x6, float y6)
        {
            var areaOrig1 = Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));
            if (PointTriangleOrig(x4, y4, x1, y1, x2, y2, x3, y3, areaOrig1) ||
                PointTriangleOrig(x5, y5, x1, y1, x2, y2, x3, y3, areaOrig1) ||
                PointTriangleOrig(x6, y6, x1, y1, x2, y2, x3, y3, areaOrig1))
                return true;

            var areaOrig2 = Abs((x5 - x4) * (y6 - y4) - (x6 - x4) * (y5 - y4));
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
                var area1 = Abs((x1 - px) * (y2 - py) - (x2 - px) * (y1 - py));
                var area2 = Abs((x2 - px) * (y3 - py) - (x3 - px) * (y2 - py));
                var area3 = Abs((x3 - px) * (y1 - py) - (x1 - px) * (y3 - py));
                return Math.Abs(area1 + area2 + area3 - areaOrig) < Tolerance;
            }
        }
        #endregion
    }
}
