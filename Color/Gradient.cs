using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StorybrewScripts
{
    namespace Midori
    {
        namespace Color
        {
            /// <summary>
            /// Handles management of multi-colored transitioning between colors.
            /// </summary>
            public class Gradient
            {
                private List<Tuple<Double, Color4>> points = new List<Tuple<Double, Color4>>();

                public Gradient()
                {
                    points.Add(new Tuple<Double, Color4>(0, Color4.White));
                }

                public Gradient(Color4 color)
                {
                    points.Add(new Tuple<Double, Color4>(0, color));
                }

                public Gradient(params Color4[] colors)
                {
                    if (colors.Length == 0) new Gradient();
                    for (int i = 0; i < colors.Length; i++)
                        points.Add(new Tuple<Double, Color4>((float)i / (colors.Length - 1), colors[i]));
                }

                public Gradient(List<Color4> colors)
                {
                    if (colors.Count == 0) new Gradient();
                    for (int i = 0; i < colors.Count; i++)
                        points.Add(new Tuple<Double, Color4>((float)i / (colors.Count - 1), colors[i]));
                }

                /// <summary>
                /// Gets the interpolated color of the gradient at a certain point.
                /// </summary>
                public Color4 GetColor(double t)
                {
                    StoryboardObjectGenerator.Current.Assert(t >= 0 && t <= 1, "Value must be between 0-1.");
                    StoryboardObjectGenerator.Current.Assert(points.Count != 0, "Please add additional color points in the gradient.");

                    if (points.Count == 1)
                        return points[0].Item2;
                    else if (points.Count == 2)
                        return ColorHelper.Lerp(points[0].Item2, points[1].Item2, (float)t);

                    // Find the element or the closest elements
                    var low = 0;
                    var high = points.Count - 1;

                    while (low <= high)
                    {
                        var mid = (low + high) / 2;
                        if (points[mid].Item1 > t)
                            high = mid - 1;
                        else if (points[mid].Item1 < t)
                            low = mid + 1;
                        else
                            return points[mid].Item2;
                    }

                    return ColorHelper.Lerp(points[high].Item2, points[low].Item2, (float)((t - points[high].Item1) / (points[low].Item1 - points[high].Item1)));
                }

                /// <summary>
                /// Adds a new point in the gradient at a certain point.
                /// If a point already exists, the color there is overwritten.
                /// </summary>
                public void Add(Color4 color, double t)
                {
                    StoryboardObjectGenerator.Current.Assert(t >= 0 && t <= 1, "Value must be between 0-1.");
                    var newColor = new Tuple<double, Color4>(t, color);
                    if (points.Count == 0) points.Add(newColor);
                    for (int i = 0; i < points.Count; i++)
                    {
                        if (points[i].Item1 == t)
                        {
                            points[i] = newColor;
                            return;
                        }
                        else if (points[i].Item1 > t)
                        {
                            points.Insert(i, newColor);
                            return;
                        }
                    }
                    points.Add(newColor);
                }

                /// <summary>
                /// Removes a point in the gradient based on the color.
                /// </summary>
                public void Remove(Color4 color)
                {
                    var index = points.FindIndex(0, (x) => x.Item2 == color);
                    if (index >= 0) points.RemoveAt(index);
                }

                /// <summary>
                /// Distributes all the points in the gradient evenly.
                /// </summary>
                public void DistributePoints()
                {
                    for (int i = 0; i < points.Count; i++)
                        points[i] = new Tuple<Double, Color4>((float)i / (points.Count - 1), points[i].Item2);
                }
            }
        }
    }
}