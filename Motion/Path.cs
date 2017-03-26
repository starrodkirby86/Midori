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
                    }
                }
            }
        }
    }
}