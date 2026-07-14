using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using vertoker.CollisionDetection2D.Interfaces;

namespace vertoker.CollisionDetection2D
{
    public static class CollisionDetection
    {
        #region Static
        
        public const float Epsilon = 0.0001f;
        public const float DefaultBuf = 0.001f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(float a, float b)
        {
            return math.abs(a - b) < Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SquaredDistance(float x1, float y1, float x2, float y2)
        {
            float distX = x2 - x1, distY = y2 - y1;
            return distX * distX + distY * distY;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SquaredDistance<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            float distX = p2.X - p1.X, distY = p2.Y - p1.Y;
            return distX * distX + distY * distY;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SquaredDistance(float x, float y)
        {
            return x * x + y * y;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SquaredDistance<TPoint>(this TPoint p)
            where TPoint : unmanaged, IPoint
        {
            return p.X * p.X + p.Y * p.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float distX = x2 - x1, distY = y2 - y1;
            return math.sqrt(distX * distX + distY * distY);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            float distX = p2.X - p1.X, distY = p2.Y - p1.Y;
            return math.sqrt(distX * distX + distY * distY);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(float x, float y)
        {
            return math.sqrt(x * x + y * y);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance<TPoint>(this TPoint p)
            where TPoint : unmanaged, IPoint
        {
            return math.sqrt(p.X * p.X + p.Y * p.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(float x1, float y1, float x2, float y2)
        {
            return x1 * x2 + y1 * y2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape Clamp(float valueX, float valueY, float minX, float minY, float maxX, float maxY)
        {
            return new PointShape(Clamp(valueX, minX, maxX), Clamp(valueY, minY, maxY));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape Plus<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return new PointShape(p1.X + p2.X, p1.Y + p2.Y);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape Minus<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return new PointShape(p1.X - p2.X, p1.Y - p2.Y);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape Multiply<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return new PointShape(p1.X * p2.X, p1.Y * p2.Y);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape Divide<TPoint1, TPoint2>(this TPoint1 p1, TPoint2 p2)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return new PointShape(p1.X / p2.X, p1.Y / p2.Y);
        }
        
        #endregion
        
        #region Conversions
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointShape ToPoint<TPoint>(this TPoint point)
            where TPoint : unmanaged, IPoint => new(point.X, point.Y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircleShape ToCircle<TCircle>(this TCircle circle)
            where TCircle : unmanaged, ICircle => new(circle.X, circle.Y, circle.R);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineShape ToLine<TLine>(this TLine line)
            where TLine : unmanaged, ILine<PointShape> => new(line.P1, line.P2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleShape ToRectangle<TRectangle>(this TRectangle rectangle)
            where TRectangle : unmanaged, IRectangle => new(rectangle.X, rectangle.Y, rectangle.W, rectangle.H);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TriangleShape ToTriangle<TTriangle>(this TTriangle triangle)
            where TTriangle : unmanaged, ITriangle<PointShape> => new(triangle.P1, triangle.P2, triangle.P3);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolygonShape ToPolygon<TPolygon>(this TPolygon polygon)
            where TPolygon : unmanaged, IPolygon<PointShape> => new(polygon.Vertices);
        
        #endregion
        
        #region Point
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointPoint(float x1, float y1, float x2, float y2)
        {
            return Approximately(x1, x2) && Approximately(y1, y2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointPoint<TPoint1, TPoint2>(TPoint1 p1, TPoint2 p2) 
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            return Approximately(p1.X, p2.X) && Approximately(p1.Y, p2.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointCircle(float px, float py, float cx, float cy, float cr)
        {
            return SquaredDistance(px, py, cx, cy) <= cr * cr;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointCircle<TPoint1, TCircle2>(TPoint1 p, TCircle2 c)
            where TPoint1 : unmanaged, IPoint where TCircle2 : unmanaged, ICircle
        {
            return SquaredDistance(p, c) <= c.R * c.R;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointRectangle(float px, float py, float rx, float ry, float rw, float rh)
        {
            float w2 = rw * 0.5f, h2 = rh * 0.5f;
            return px >= rx - w2
                   && px <= rx + w2
                   && py >= ry - h2
                   && py <= ry + h2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointRectangle<TPoint1, TRectangle2>(TPoint1 p, TRectangle2 r)
            where TPoint1 : unmanaged, IPoint where TRectangle2 : unmanaged, IRectangle
        {
            float w2 = r.W * 0.5f, h2 = r.H * 0.5f;
            return p.X >= r.X - w2
                   && p.X <= r.X + w2
                   && p.Y >= r.Y - h2
                   && p.Y <= r.Y + h2;
        }
        
        public static bool PointLine(float px, float py, float lx1, float ly1, float lx2, float ly2, float buf = DefaultBuf)
        {
            float vecX = lx2 - lx1, vecY = ly2 - ly1;
            float pVecX = px - lx1, pVecY = py - ly1;
            
            var lenSq = SquaredDistance(vecX, vecY);
            var t = Dot(pVecX, pVecY, vecX, vecY) / lenSq;
            t = math.clamp(t, 0f, 1f);
            
            float closestX = lx1 + t * vecX, closestY = ly1 + t * vecY;
            var distSq = SquaredDistance(px - closestX, py - closestY);
            
            return distSq <= buf * buf;
        }
        public static bool PointLine<TPoint1, TLine2>(TPoint1 p, TLine2 l, float buf = DefaultBuf)
            where TPoint1 : unmanaged, IPoint where TLine2 : unmanaged, ILine<PointShape>
        {
            var vec = l.P2 - l.P1;
            var pVec = p.Minus(l.P1);

            var lenSq = vec.SquaredDistance();
            var t = math.clamp(Dot(pVec, vec) / lenSq, 0f, 1f);
            
            var closest = l.P1 + t * vec;
            var distSq = p.Minus(closest).SquaredDistance();
            
            return distSq <= buf * buf;
        }
        
        public static bool PointPolygon(float px, float py, ReadOnlySpan<PointShape> vertices)
        {
            var length = vertices.Length;
            if (length < 3) return false;
            
            var collision = false;
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                var vc = vertices[current];
                var vn = vertices[next];
                if (((vc.y >= py && vn.y < py) || (vc.y < py && vn.y >= py)) &&
                     (px < (vn.x - vc.x) * (py - vc.y) / (vn.y - vc.y) + vc.x))
                    collision = !collision;
            }
            return collision;
        }
        public static bool PointPolygon<TPoint1, TPolygon2>(TPoint1 p, TPolygon2 pol)
            where TPoint1 : unmanaged, IPoint where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var vertices = pol.Vertices;
            var length = vertices.Length;
            if (length < 3) return false;
            
            var collision = false;
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                var vc = vertices[current];
                var vn = vertices[next];
                if (((vc.y >= p.Y && vn.y < p.Y) || (vc.y < p.Y && vn.y >= p.Y)) &&
                     (p.X < (vn.x - vc.x) * (p.Y - vc.y) / (vn.y - vc.y) + vc.x))
                    collision = !collision;
            }
            return collision;
        }
        
        public static bool PointTriangle(float px, float py, float tx1, float ty1, float tx2, float ty2, float tx3, float ty3)
        {
            var areaOrig = math.abs((tx2 - tx1) * (ty3 - ty1) - (tx3 - tx1) * (ty2 - ty1));
            if (areaOrig < Epsilon) return false;
            var area1 = math.abs((tx1 - px) * (ty2 - py) - (tx2 - px) * (ty1 - py));
            var area2 = math.abs((tx2 - px) * (ty3 - py) - (tx3 - px) * (ty2 - py));
            var area3 = math.abs((tx3 - px) * (ty1 - py) - (tx1 - px) * (ty3 - py));
            return Approximately(area1 + area2 + area3, areaOrig);
        }
        public static bool PointTriangle<TPoint1, TTriangle2>(TPoint1 p, TTriangle2 t)
            where TPoint1 : unmanaged, IPoint where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            var areaOrig = math.abs((t.P2.x - t.P1.x) * (t.P3.y - t.P1.y) - (t.P3.x - t.P1.x) * (t.P2.y - t.P1.y));
            if (areaOrig < Epsilon) return false;
            var area1 = math.abs((t.P1.x - p.X) * (t.P2.y - p.Y) - (t.P2.x - p.X) * (t.P1.y - p.Y));
            var area2 = math.abs((t.P2.x - p.X) * (t.P3.y - p.Y) - (t.P3.x - p.X) * (t.P2.y - p.Y));
            var area3 = math.abs((t.P3.x - p.X) * (t.P1.y - p.Y) - (t.P1.x - p.X) * (t.P3.y - p.Y));
            return Approximately(area1 + area2 + area3, areaOrig);
        }
        
        #endregion

        #region Circle
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CircleCircle(float cx1, float cy1, float cr1, float cx2, float cy2, float cr2)
        {
            var sumR = cr1 + cr2;
            return SquaredDistance(cx1, cy1, cx2, cy2) <= sumR * sumR;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CircleCircle<TCircle1, TCircle2>(TCircle1 c1, TCircle2 c2)
            where TCircle1 : unmanaged, ICircle where TCircle2 : unmanaged, ICircle
        {
            var sumR = c1.R + c2.R;
            return SquaredDistance(c1, c2) <= sumR * sumR;
        }
        
        public static bool CircleRectangle(float cx, float cy, float cr, float rx, float ry, float rw, float rh)
        {
            float halfW = rw * 0.5f, halfH = rh * 0.5f;
            float minX = rx - halfW, maxX = rx + halfW;
            float minY = ry - halfH, maxY = ry + halfH;

            float px = cx, py = cy;
            
            if (cx < minX) px = minX;
            else if (cx > maxX) px = maxX;
            
            if (cy < minY) py = minY;
            else if (cy > maxY) py = maxY;

            return SquaredDistance(cx, cy, px, py) <= cr * cr;
        }
        public static bool CircleRectangle<TCircle1, TRectangle2>(TCircle1 c, TRectangle2 r)
            where TCircle1 : unmanaged, ICircle where TRectangle2 : unmanaged, IRectangle
        {
            float halfW = r.W * 0.5f, halfH = r.H * 0.5f;
            float minX = r.X - halfW, maxX = r.X + halfW;
            float minY = r.Y - halfH, maxY = r.Y + halfH;

            var p = c.ToPoint();
            
            if (c.X < minX) p.x = minX;
            else if (c.X > maxX) p.x = maxX;
            
            if (c.Y < minY) p.y = minY;
            else if (c.Y > maxY) p.y = maxY;

            return SquaredDistance(c, p) <= c.R * c.R;
        }
        
        public static bool CircleLine(float cx, float cy, float cr, float lx1, float ly1, float lx2, float ly2)
        {
            if (PointCircle(lx1, ly1, cx, cy, cr) || PointCircle(lx2, ly2, cx, cy, cr)) return true;

            var lenSq = SquaredDistance(lx1, ly1, lx2, ly2);
            if (lenSq < Epsilon) return false;

            float diffX = lx2 - lx1, diffY = ly2 - ly1;
            var dot = Dot(cx - lx1, cy - ly1, diffX, diffY) / lenSq;
            if (dot is < 0f or > 1f) return false;
            
            float closestX = lx1 + dot * diffX, closestY = ly1 + dot * diffY;
            return SquaredDistance(cx, cy, closestX, closestY) <= cr * cr;
        }
        public static bool CircleLine<TCircle1, TLine2>(TCircle1 c, TLine2 l)
            where TCircle1 : unmanaged, ICircle where TLine2 : unmanaged, ILine<PointShape>
        {
            if (PointCircle(l.P1, c) || PointCircle(l.P2, c)) return true;

            var lenSq = SquaredDistance(l.P1, l.P2);
            if (lenSq < Epsilon) return false;
            
            var dot = Dot(c.Minus(l.P1), l.P2 - l.P1) / lenSq;
            if (dot is < 0f or > 1f) return false;
            
            var closest = l.P1 + dot * (l.P2 - l.P1);
            return SquaredDistance(c, closest) <= c.R * c.R;
        }
        
        public static bool CirclePolygon(float cx, float cy, float cr, ReadOnlySpan<PointShape> vertices)
        {
            var length = vertices.Length;
            if (length < 3) return false;
            
            if (PointPolygon(cx, cy, vertices)) return true;

            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                var p1 = vertices[current];
                var p2 = vertices[next];
                if (CircleLine(cx, cy, cr, p1.x, p1.y, p2.x, p2.y)) return true;
            }
            return false;
        }
        public static bool CirclePolygon<TCircle1, TPolygon2>(TCircle1 c, TPolygon2 pol)
            where TCircle1 : unmanaged, ICircle where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var vertices = pol.Vertices;
            var length = vertices.Length;
            if (length < 3) return false;
            
            if (PointPolygon(c, pol)) return true;

            var tempLine = new LineShape();
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                tempLine.p1 = vertices[current];
                tempLine.p2 = vertices[next];
                if (CircleLine(c, tempLine)) return true;
            }
            return false;
        }
        
        public static bool CircleTriangle(float cx, float cy, float cr, 
            float tx1, float ty1, float tx2, float ty2, float tx3, float ty3)
        {
            // case if S = 0
            // var cross = (t.P2.x - t.P1.x) * (t.P3.y - t.P1.y) - (t.P2.y - t.P1.y) * (t.P3.x - t.P1.x);
            // if (math.abs(cross) < Epsilon) return false;
            
            if (PointTriangle(cx, cy, tx1, ty1, tx2, ty2, tx3, ty3)) return true;

            var radiusSq = cr * cr;
            
            if (SquaredDistance(cx, cy, tx1, ty1) <= radiusSq) return true;
            if (SquaredDistance(cx, cy, tx2, ty2) <= radiusSq) return true;
            if (SquaredDistance(cx, cy, tx3, ty3) <= radiusSq) return true;
            
            if (CircleLineFast(tx1, ty1, tx2, ty2, cx, cy, radiusSq)) return true;
            if (CircleLineFast(tx2, ty2, tx3, ty3, cx, cy, radiusSq)) return true;
            if (CircleLineFast(tx1, ty1, tx3, ty3, cx, cy, radiusSq)) return true;
            
            return false;
        }
        public static bool CircleTriangle<TCircle1, TTriangle2>(TCircle1 c, TTriangle2 t)
            where TCircle1 : unmanaged, ICircle where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            // case if S = 0
            // var cross = (t.P2.x - t.P1.x) * (t.P3.y - t.P1.y) - (t.P2.y - t.P1.y) * (t.P3.x - t.P1.x);
            // if (math.abs(cross) < Epsilon) return false;
            
            if (PointTriangle(c, t)) return true;

            var radiusSq = c.R * c.R;
            
            if (SquaredDistance(c, t.P1) <= radiusSq) return true;
            if (SquaredDistance(c, t.P2) <= radiusSq) return true;
            if (SquaredDistance(c, t.P3) <= radiusSq) return true;
            
            if (CircleLineFast(c, t.P1, t.P2, radiusSq)) return true;
            if (CircleLineFast(c, t.P2, t.P3, radiusSq)) return true;
            if (CircleLineFast(c, t.P1, t.P3, radiusSq)) return true;
            
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CircleLineFast(float ax, float ay, float bx, float by, float cx, float cy, float radiusSq)
        {
            float dx = bx - ax, dy = by - ay; 
            var lenSq = SquaredDistance(dx, dy);
            if (lenSq < Epsilon) return false;

            float px = (cx - ax) * dx, py = (cy - ay) * dy;
            var t = (px + py) / lenSq;
            t = math.clamp(t, 0f, 1f);

            float closestX = ax + t * dx, closestY = ay + t * dy;
            var distSq = SquaredDistance(cx - closestX, cy - closestY);
            return distSq <= radiusSq;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CircleLineFast<TPoint>(TPoint c, PointShape a, PointShape b, float radiusSq)
            where TPoint : unmanaged, IPoint
        {
            var d = b - a;
            var lenSq = SquaredDistance(d);
            if (lenSq < Epsilon) return false;

            var p = c.Minus(a) * d;
            var t = (p.x + p.y) / lenSq;
            t = math.clamp(t, 0f, 1f);

            var closest = a + t * d;
            var distSq = c.Minus(closest).SquaredDistance();

            return distSq <= radiusSq;
        }
        
        #endregion

        #region Rectangle
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangle(float rx1, float ry1, float rw1, float rh1,
            float rx2, float ry2, float rw2, float rh2)
        {
            float w1H = rw1 * 0.5f, h1H = rh1 * 0.5f, w2H = rw2 * 0.5f, h2H = rh2 * 0.5f;
            return rx1 + w1H >= rx2 - w2H
                   && rx1 - w1H <= rx2 + w2H
                   && ry1 + h1H >= ry2 - h2H
                   && ry1 - h1H <= ry2 + h2H;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangle<TRectangle1, TRectangle2>(TRectangle1 r1, TRectangle2 r2)
            where TRectangle1 : unmanaged, IRectangle where TRectangle2 : unmanaged, IRectangle
        {
            float w1H = r1.W * 0.5f, h1H = r1.H * 0.5f, w2H = r2.W * 0.5f, h2H = r2.H * 0.5f;
            return r1.X + w1H >= r2.X - w2H
                   && r1.X - w1H <= r2.X + w2H
                   && r1.Y + h1H >= r2.Y - h2H
                   && r1.Y - h1H <= r2.Y + h2H;
        }
        
        public static bool RectangleLine(float rx, float ry, float rw, float rh, float lx1, float ly1, float lx2, float ly2)
        {
            float minX = rx - rw * 0.5f, maxX = rx + rw * 0.5f;
            float minY = ry - rh * 0.5f, maxY = ry + rh * 0.5f;
            
            var left = LineLine(lx1, ly1, lx2, ly2, minX, minY, minX, maxY);
            var right = LineLine(lx1, ly1, lx2, ly2, maxX, minY, maxX, maxY);
            var top = LineLine(lx1, ly1, lx2, ly2, minX, minY, maxX, minY);
            var bottom = LineLine(lx1, ly1, lx2, ly2, minX, maxY, maxX, maxY);
            return left || right || top || bottom;
        }
        public static bool RectangleLine<TRectangle1, TLine2>(TRectangle1 r, TLine2 l)
            where TRectangle1 : unmanaged, IRectangle where TLine2 : unmanaged, ILine<PointShape>
        {
            float minX = r.X - r.W * 0.5f, maxX = r.X + r.W * 0.5f;
            float minY = r.Y - r.H * 0.5f, maxY = r.Y + r.H * 0.5f;
            float x1 = l.P1.x, y1 = l.P1.y, x2 = l.P2.x, y2 = l.P2.y;
            
            var left = LineLine(x1, y1, x2, y2, minX, minY, minX, maxY);
            var right = LineLine(x1, y1, x2, y2, maxX, minY, maxX, maxY);
            var top = LineLine(x1, y1, x2, y2, minX, minY, maxX, minY);
            var bottom = LineLine(x1, y1, x2, y2, minX, maxY, maxX, maxY);
            return left || right || top || bottom;
        }
        
        public static bool RectanglePolygon(float rx, float ry, float rw, float rh, ReadOnlySpan<PointShape> vertices)
        {
            var inPolygon = PointPolygon(rx, ry, vertices);
            if (inPolygon) return true;

            var length = vertices.Length;
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                var p1 = vertices[current];
                var p2 = vertices[next];
                if (RectangleLine(rx, ry, rw, rh, p1.x, p1.y, p2.x, p2.y)) return true;
            }
            return false;
        }
        public static bool RectanglePolygon<TRectangle1, TPolygon2>(TRectangle1 r, TPolygon2 pol)
            where TRectangle1 : unmanaged, IRectangle where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var inPolygon = PointPolygon(r, pol);
            if (inPolygon) return true;

            var length = pol.Vertices.Length;
            var tempLine = new LineShape();
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                tempLine.p1 = pol.Vertices[current];
                tempLine.p2 = pol.Vertices[next];
                if (RectangleLine(r, tempLine)) return true;
            }
            return false;
        }
        
        public static bool RectangleTriangle(float rx, float ry, float rw, float rh,
            float tx1, float ty1, float tx2, float ty2, float tx3, float ty3)
        {
            if (PointRectangle(tx1, ty1, rx, ry, rw, rh)
                || PointRectangle(tx2, ty2, rx, ry, rw, rh)
                || PointRectangle(tx3, ty3, rx, ry, rw, rh)) return true;
            
            float hw = rw * 0.5f, hh = rh * 0.5f;
            float minX = rx - hw, minY = ry - hh;
            if (PointTriangle(minX, minY, tx1, ty1, tx2, ty2, tx3, ty3)) return true;

            var maxX = rx + hw;
            if (PointTriangle(maxX, minY, tx1, ty1, tx2, ty2, tx3, ty3)) return true;

            var maxY = ry + hh;
            return PointTriangle(minX, maxY, tx1, ty1, tx2, ty2, tx3, ty3)
                   || PointTriangle(maxX, maxY, tx1, ty1, tx2, ty2, tx3, ty3);
        }
        public static bool RectangleTriangle<TRectangle1, TTriangle2>(TRectangle1 r, TTriangle2 t)
            where TRectangle1 : unmanaged, IRectangle where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            if (PointRectangle(t.P1, r)
                || PointRectangle(t.P2, r)
                || PointRectangle(t.P3, r)) return true;

            float hw = r.W * 0.5f, hh = r.H * 0.5f;
            float minX = r.X - hw, minY = r.Y - hh;
            if (PointTriangle(new PointShape(minX, minY), t)) return true;

            var maxX = r.X + hw;
            if (PointTriangle(new PointShape(maxX, minY), t)) return true;

            var maxY = r.Y + hh;
            return PointTriangle(new PointShape(minX, maxY), t) || PointTriangle(new PointShape(maxX, maxY), t);
        }
        
        #endregion

        #region Line
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLine(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            var uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            var uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            return uA is >= 0 and <= 1 && uB is >= 0 and <= 1;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLine<TPoint1, TPoint2>(TPoint1 p1, TPoint1 p2, TPoint2 p3, TPoint2 p4)
            where TPoint1 : unmanaged, IPoint where TPoint2 : unmanaged, IPoint
        {
            float x1 = p1.X, x2 = p2.X, x3 = p3.X, x4 = p4.X;
            float y1 = p1.Y, y2 = p2.Y, y3 = p3.Y, y4 = p4.Y;
            return LineLine(x1, y1, x2, y2, x3, y3, x4, y4);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLine<TLine1, TLine2>(TLine1 l1, TLine2 l2)
            where TLine1 : unmanaged, ILine<PointShape> where TLine2 : unmanaged, ILine<PointShape>
        {
            float x1 = l1.P1.x, x2 = l1.P2.x, x3 = l2.P1.x, x4 = l2.P2.x;
            float y1 = l1.P1.y, y2 = l1.P2.y, y3 = l2.P1.y, y4 = l2.P2.y;
            return LineLine(x1, y1, x2, y2, x3, y3, x4, y4);
        }
        
        public static bool LinePolygon(float lx1, float ly1, float lx2, float ly2, ReadOnlySpan<PointShape> vertices)
        {
            var length = vertices.Length;
            if (length < 3) return false;
            
            if (PointPolygon(lx1, ly1, vertices) || PointPolygon(lx2, ly2, vertices)) return true;

            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                var p1 = vertices[current];
                var p2 = vertices[next];
                if (LineLine(lx1, ly1, lx2, ly2, p1.x, p1.y, p2.x, p2.y)) return true;
            }
            return false;
        }
        public static bool LinePolygon<TLine1, TPolygon2>(TLine1 l, TPolygon2 pol)
            where TLine1 : unmanaged, ILine<PointShape> where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var vertices = pol.Vertices;
            var length = vertices.Length;
            if (length < 3) return false;
            
            if (PointPolygon(l.P1, pol) || PointPolygon(l.P2, pol)) return true;

            var tempLine = new LineShape();
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                tempLine.p1 = pol.Vertices[current];
                tempLine.p2 = pol.Vertices[next];
                if (LineLine(l, tempLine)) return true;
            }
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineTriangle(float lx1, float ly1, float lx2, float ly2,
            float tx1, float ty1, float tx2, float ty2, float tx3, float ty3)
        {
            return PointTriangle(lx1, ly1, tx1, ty1, tx2, ty2, tx3, ty3)
                   || PointTriangle(lx2, ly2, tx1, ty1, tx2, ty2, tx3, ty3) ||
                LineLine(lx1, ly1, lx2, ly2, tx1, ty1, tx2, ty2) ||
                LineLine(lx1, ly1, lx2, ly2, tx1, ty1, tx3, ty3) ||
                LineLine(lx1, ly1, lx2, ly2, tx2, ty2, tx3, ty3);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineTriangle<TLine1, TTriangle2>(TLine1 l, TTriangle2 t)
            where TLine1 : unmanaged, ILine<PointShape> where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            return PointTriangle(l.P1, t) || PointTriangle(l.P2, t) ||
                LineLine(l, new LineShape(t.P1, t.P2)) ||
                LineLine(l, new LineShape(t.P1, t.P3)) ||
                LineLine(l, new LineShape(t.P2, t.P3));
        }
        
        #endregion

        #region Polygon
        
        public static bool PolygonPolygon(ReadOnlySpan<PointShape> vertices1, ReadOnlySpan<PointShape> vertices2)
        {
            var length1 = vertices1.Length;
            var length2 = vertices2.Length;
            if (length1 < 3 || length2 < 3) return false;

            var p01 = vertices1[0];
            if (PointPolygon(p01.x, p01.y, vertices2)) return true;
            
            var p02 = vertices2[0];
            if (PointPolygon(p02.x, p02.y, vertices1)) return true;
            
            for (var i = 0; i < length1; i++)
            {
                var a1 = vertices1[i];
                var a2 = vertices1[(i + 1) % length1];

                for (var j = 0; j < length2; j++)
                {
                    var b1 = vertices2[j];
                    var b2 = vertices2[(j + 1) % length2];

                    if (LineLine(a1, a2, b1, b2)) return true;
                }
            }
            return false;
        }
        public static bool PolygonPolygon<TPolygon1, TPolygon2>(TPolygon1 pol1, TPolygon2 pol2)
            where TPolygon1 : unmanaged, IPolygon<PointShape> where TPolygon2 : unmanaged, IPolygon<PointShape>
        {
            var vertices1 = pol1.Vertices;
            var vertices2 = pol2.Vertices;
            var length1 = vertices1.Length;
            var length2 = vertices2.Length;
            if (length1 < 3 || length2 < 3) return false;
            
            if (PointPolygon(vertices1[0], pol2) || PointPolygon(vertices2[0], pol1)) return true;
            
            for (var i = 0; i < length1; i++)
            {
                var a1 = vertices1[i];
                var a2 = vertices1[(i + 1) % length1];

                for (var j = 0; j < length2; j++)
                {
                    var b1 = vertices2[j];
                    var b2 = vertices2[(j + 1) % length2];

                    if (LineLine(a1, a2, b1, b2)) return true;
                }
            }
            return false;
        }
        
        public static bool PolygonTriangle(ReadOnlySpan<PointShape> vertices,
            float tx1, float ty1, float tx2, float ty2, float tx3, float ty3)
        {
            var length = vertices.Length;
            if (length < 3) return false;
            
            if (PointPolygon(tx1, ty1, vertices)
                || PointPolygon(tx2, ty2, vertices)
                || PointPolygon(tx3, ty3, vertices)) return true;

            var p0 = vertices[0];
            if (PointTriangle(p0.x, p0.y, tx1, ty1, tx2, ty2, tx3, ty3)) return true;
            
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                var a = vertices[current];
                var b = vertices[next];
                
                if (LineLine(a.x, a.y, b.x, b.y, tx1, ty1, tx2, ty2)) return true;
                if (LineLine(a.x, a.y, b.x, b.y, tx1, ty1, tx3, ty3)) return true;
                if (LineLine(a.x, a.y, b.x, b.y, tx2, ty2, tx3, ty3)) return true;
            }
            return false;
        }
        public static bool PolygonTriangle<TPolygon1, TTriangle2>(TPolygon1 pol, TTriangle2 t)
            where TPolygon1 : unmanaged, IPolygon<PointShape> where TTriangle2 : unmanaged, ITriangle<PointShape>
        {
            var vertices = pol.Vertices;
            var length = vertices.Length;
            if (length < 3) return false;
            
            if (PointPolygon(t.P1, pol) || PointPolygon(t.P2, pol) || PointPolygon(t.P3, pol)) return true;
            if (PointTriangle(vertices[0], t)) return true;
            
            for (var current = 0; current < length; current++)
            {
                var next = (current + 1) % length;
                var a = vertices[current];
                var b = vertices[next];
                
                if (LineLine(a, b, t.P1, t.P2)) return true;
                if (LineLine(a, b, t.P2, t.P3)) return true;
                if (LineLine(a, b, t.P3, t.P1)) return true;
            }
            return false;
        }
        
        #endregion

        #region Triangle
        
        public static bool TriangleTriangle(float x1, float y1, float x2, float y2, float x3, float y3,
            float x4, float y4, float x5, float y5, float x6, float y6)
        {
            var area1 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);
            var area2 = (x5 - x4) * (y6 - y4) - (x6 - x4) * (y5 - y4);
            float absArea1 = math.abs(area1), absArea2 = math.abs(area2);
            
            if (absArea1 < Epsilon || absArea2 < Epsilon) return false;
            
            if (PointInTriangle(x4, y4, x1, y1, x2, y2, x3, y3, absArea1) ||
                PointInTriangle(x5, y5, x1, y1, x2, y2, x3, y3, absArea1) ||
                PointInTriangle(x6, y6, x1, y1, x2, y2, x3, y3, absArea1))
                return true;
            
            return LineLine(x1, y1, x2, y2, x4, y4, x5, y5)
                   || LineLine(x1, y1, x2, y2, x4, y4, x6, y6)
                   || LineLine(x1, y1, x2, y2, x5, y5, x6, y6)
                   || LineLine(x2, y2, x3, y3, x4, y4, x5, y5)
                   || LineLine(x2, y2, x3, y3, x4, y4, x6, y6)
                   || LineLine(x2, y2, x3, y3, x5, y5, x6, y6)
                   || LineLine(x1, y1, x3, y3, x4, y4, x5, y5)
                   || LineLine(x1, y1, x3, y3, x4, y4, x6, y6)
                   || LineLine(x1, y1, x3, y3, x5, y5, x6, y6);
            
            static bool PointInTriangle(float px, float py,
                float tx1, float ty1, float tx2, float ty2, float tx3, float ty3,
                float areaOrig)
            {
                var a1 = math.abs((tx1 - px) * (ty2 - py) - (tx2 - px) * (ty1 - py));
                var a2 = math.abs((tx2 - px) * (ty3 - py) - (tx3 - px) * (ty2 - py));
                var a3 = math.abs((tx3 - px) * (ty1 - py) - (tx1 - px) * (ty3 - py));
                return Approximately(a1 + a2 + a3, areaOrig);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
