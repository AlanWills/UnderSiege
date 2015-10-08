using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Maths.Primitives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths
{
    public class _2DGeometry
    {
        public static bool RectangleContainsPoint(Rectangle bounds, Vector2 point)
        {
            return bounds.Contains(new Point((int)point.X, (int)point.Y));
        }

        public static bool LineIntersectsCircle(Circle circle, Line line)
        {
            // line y = mx + c
            // circle centre (a, b) radius r
            // do discriminant on the equation you get from subbing in line equation into circle equation
            float a = line.Gradient * line.Gradient + 1;
            float b = 2 * (line.Gradient * (line.YIntercept - circle.Centre.Y) - circle.Centre.X);
            float c = (circle.Centre.X * circle.Centre.X + (line.YIntercept - circle.Centre.Y) * (line.YIntercept - circle.Centre.Y) - circle.Radius * circle.Radius);

            float discriminant = b * b - 4 * a * c;

            return discriminant >= 0;
        }

        // Finds the intersection point of the line with the circle and then checks to see if the intersection points are in the arc
        public static bool LineIntersectsArc(Arc arc, Line line)
        {
            Circle circle = new Circle(arc.Centre, arc.Radius, Color.White, 0);
            bool intersect = LineIntersectsCircle(circle, line);
            if (!intersect)
                return false;

            float a = line.Gradient * line.Gradient + 1;
            float b = 2 * (line.Gradient * (line.YIntercept - circle.Centre.Y) - circle.Centre.X);
            float c = (circle.Centre.X * circle.Centre.X + (line.YIntercept - circle.Centre.Y) * (line.YIntercept - circle.Centre.Y) - circle.Radius * circle.Radius);
            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                return false;

            float x_1 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);
            float x_2 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);
            float y_1 = line.Gradient * x_1 + line.YIntercept;
            float y_2 = line.Gradient * x_2 + line.YIntercept;

            float angle_1 = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(arc.Centre, new Vector2(x_1, y_1));
            if (MathUtils.FloatInRange(angle_1, arc.StartingAngle, arc.StartingAngle + arc.ArcWidth))
                return true;

            float angle_2 = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(arc.Centre, new Vector2(x_2, y_2));
            if (MathUtils.FloatInRange(angle_2, arc.StartingAngle, arc.ArcWidth))
                return true;

            return false;
        }

        public static bool RectangleIntersectsArc(Rectangle rectangle, Arc arc)
        {
            bool intersects = RectangleIntersectsCircle(rectangle, new Circle(arc.Centre, arc.Radius));
            if (!intersects)
                return false;

            intersects = LineIntersectsArc(arc, new Line(new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Left, rectangle.Bottom)));
            if (intersects)
                return true;

            intersects = LineIntersectsArc(arc, new Line(new Vector2(rectangle.Left, rectangle.Bottom), new Vector2(rectangle.Right, rectangle.Bottom)));
            if (intersects)
                return true;

            intersects = LineIntersectsArc(arc, new Line(new Vector2(rectangle.Right, rectangle.Bottom), new Vector2(rectangle.Right, rectangle.Top)));
            if (intersects)
                return true;

            intersects = LineIntersectsArc(arc, new Line(new Vector2(rectangle.Right, rectangle.Top), new Vector2(rectangle.Left, rectangle.Top)));
            if (intersects)
                return true;

            return false;
        }

        public static bool LineIntersectsRect(Line line, Rectangle r)
        {
            return LineIntersectsRect(new Point((int)line.StartPoint.X, (int)line.StartPoint.Y), new Point((int)line.EndPoint.X, (int)line.EndPoint.Y), r);
        }

        public static bool LineIntersectsRect(Point startPoint, Point endPoint, Rectangle r)
        {
            return LineIntersectsLine(startPoint, endPoint, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(startPoint, endPoint, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(startPoint, endPoint, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(startPoint, endPoint, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                   (r.Contains(startPoint) && r.Contains(endPoint));
        }

        private static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }

        public static bool RectangleIntersectsRectangle(Rectangle r1, Rectangle r2)
        {
            return r1.Intersects(r2);
        }

        public static Rectangle RectangleOverlap(Rectangle r1, Rectangle r2)
        {
            if (!RectangleIntersectsRectangle(r1, r2))
                return Rectangle.Empty;

            Vector2 position, size;
            
            // Workout the relationship between the positions of the two rectangles by using the screen centres
            Vector2 r1Centre = new Vector2(r1.Center.X, r1.Center.Y);
            Vector2 r2Centre = new Vector2(r2.Center.X, r2.Center.Y);

            if (r1Centre.X <= r2Centre.X)
            {
                position.X = r2.Left;
                size.X = Math.Min(r2.Width, r1.Right - position.X);
            }
            else
            {
                position.X = r1.Left;
                size.X = Math.Min(r1.Width, r2.Right - position.X);
            }

            if (r1Centre.Y <= r2Centre.Y)
            {
                position.Y = r2.Top;
                size.Y = Math.Min(r2.Height, r1.Bottom - position.Y);
            }
            else
            {
                position.Y = r1.Top;
                size.Y = Math.Min(r1.Height, r2.Bottom - position.Y);
            }

            if (size.X < 0)
                size.X = 0;
            if (size.Y < 0)
                size.Y = 0;

            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public static bool RectangleIntersectsCircle(Rectangle rectangle, Circle circle)
        {
            float circleDistanceX = Math.Abs(circle.Centre.X - rectangle.Center.X);
            float circleDistanceY = Math.Abs(circle.Centre.Y - rectangle.Center.Y);

            if (circleDistanceX > (rectangle.Width * 0.5f + circle.Radius)) { return false; }
            if (circleDistanceY > (rectangle.Height * 0.5f + circle.Radius)) { return false; }

            if (circleDistanceX <= (rectangle.Width * 0.5f)) { return true; }
            if (circleDistanceY <= (rectangle.Height * 0.5f)) { return true; }

            float cornerDistance_sq = (circleDistanceX - rectangle.Width * 0.5f) * (circleDistanceX - rectangle.Width * 0.5f) +
                                      (circleDistanceY - rectangle.Height * 0.5f) * (circleDistanceY - rectangle.Height * 0.5f);

            return (cornerDistance_sq <= (circle.Radius * circle.Radius));
        }

        public static bool CircleContainsPoint(Circle circle, Vector2 point)
        {
            return (circle.Centre - point).LengthSquared() <= circle.Radius * circle.Radius;
        }

        public static bool CircleIntersectsCircle(Circle circle1, Circle circle2)
        {
            return (circle1.Centre - circle2.Centre).LengthSquared() <= (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius);
        }

        public static bool CircleIntersectsArc(Circle circle, Arc arc)
        {
            Circle arcCircle = new Circle(arc.Centre, arc.Radius);

            bool intersects = CircleIntersectsCircle(circle, arcCircle);
            if (!intersects)
                return false;

            return true;
        }

        public static bool ArcContainsPoint(Arc arc, Vector2 point)
        {
            float angle = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(arc.Centre, point);
            return MathUtils.FloatInRange(angle, arc.StartingAngle, arc.StartingAngle + arc.ArcWidth);
        }
    }
}
