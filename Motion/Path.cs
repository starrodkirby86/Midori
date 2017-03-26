using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    namespace Midori
    {
        namespace Motion
        {
            /// <summary>
            /// Manages a trajectory of motion by generating a list of
            /// points for an arbitrary Function.
            /// </summary>
            public class Path
            {
                private List<Vector2> points = new List<Vector2>();
                public List<Vector2> Points => points;

                public Path(List<Vector2> p)
                {
                    points = p;
                }

                public Path(int pointCount, float startT, float endT, Func<float, float> x, Func<float, float> y)
                {
                    Generate(pointCount, startT, endT, x, y);
                }

                public Path(int pointCount, float startT, float endT, Func<float, float> x, Func<float, float> y, PathTransform p)
                {
                    Generate(pointCount, startT, endT, x, y);
                    Transform(p);
                }

                public Path(int pointCount, ParametricEquations eq, float a = 1, float b = 0, float c = 0)
                {
                    Generate(pointCount, eq, a, b, c);
                }

                public Path(int pointCount, ParametricEquations eq, PathTransform p, float a = 1, float b = 0, float c = 0)
                {
                    Generate(pointCount, eq, a, b, c);
                    Transform(p);
                }

                public void Generate(int pointCount, float startT, float endT, Func<float, float> x, Func<float, float> y)
                {
                    points.Clear();
                    var step = (endT - startT) / pointCount;
                    for (var t = startT; t < endT; t += step)
                        points.Add(new Vector2(x(t), y(t)));
                }

                public void Generate(int pointCount, ParametricEquations eq, float a = 1, float b = 0, float c = 0)
                {
                    points.Clear();
                    var step = MathHelper.TwoPi / pointCount;
                    for (var t = 0f; t < MathHelper.TwoPi; t += step)
                        points.Add(new Vector2((float)eq.X(a, b, c, t), (float)eq.Y(a, b, c, t)));
                }

                public void ForceRealNumbers() => ForceRealNumbers(MotionHelper.XRange, MotionHelper.YRange);

                public void ForceRealNumbers(Vector2 xBounds, Vector2 yBounds)
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        var point = points[i];

                        if (float.IsNaN(point.X))
                        {
                            if (float.IsNegativeInfinity(point.X)) point.X = xBounds.X;
                            else if (float.IsPositiveInfinity(point.X)) point.X = xBounds.Y;
                            else point.X = 0;
                        }

                        if (float.IsNaN(point.Y))
                        {
                            if (float.IsNegativeInfinity(point.Y)) point.Y = yBounds.X;
                            else if (float.IsPositiveInfinity(point.Y)) point.Y = yBounds.Y;
                            else point.Y = 0;
                        }

                        points[i] = point;
                    }
                }

                public void Clamp() => Clamp(MotionHelper.XRange, MotionHelper.YRange);

                public void Clamp(Vector2 xBounds, Vector2 yBounds)
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        var point = points[i];

                        if (point.X < xBounds.X) point.X = xBounds.X;
                        else if (point.X > xBounds.Y) point.X = xBounds.Y;

                        if (point.Y < yBounds.X) point.Y = yBounds.X;
                        else if (point.Y > yBounds.Y) point.Y = yBounds.Y;

                        points[i] = point;
                    }                    
                }

                public void Transform(PathTransform p)
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i] *= p.Scale;
                        points[i] += p.Translation;
                        points[i] = MotionHelper.Rotate(points[i], p.Rotation, p.RotationOrigin);
                    }
                }

                public static Path operator +(Path p1, Path p2)
                {
                    var newPathPoints = p1.Points;
                    var pathPointsToAdd = p2.Points.Skip(1);
                    var p1Offset = p1.Points.Last();
                    var p2Offset = p2.Points.First();
                    foreach(var p in pathPointsToAdd)
                    {
                        newPathPoints.Add(p - p2Offset + p1Offset);
                    }
                    return new Path(newPathPoints);
                }

                public static Path operator -(Path p1, Path p2) => p2 + p1;
            }

            /// <summary>
            /// Stores transformation information for a path. A path is transformed by its scale, then its translation, then its rotation by the given origin.
            /// </summary>
            public class PathTransform
            {

                public Vector2 Scale = new Vector2(1, 1);
                public Vector2 Translation = Vector2.Zero;
                public float Rotation = 0;
                public Vector2 RotationOrigin = Vector2.Zero;
            }
        }
    }
}