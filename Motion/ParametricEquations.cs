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
            /// A helper class storing a pair of parametric equations.
            /// Integrated with SineParametricCurve, you can also generate your own.
            /// </summary>
            public class ParametricEquations
            {
                #region Main

                // Can take a maximum of a, b, c, and t.
                public Func<double, double, double, double, double> X;
                public Func<double, double, double, double, double> Y;

                public ParametricEquations(Func<double, double, double, double, double> X, Func<double, double, double, double, double> Y)
                {
                    this.X = X;
                    this.Y = Y;
                }

                #endregion

                #region Constants
                public static ParametricEquations Astroid
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * Math.Pow(Math.Cos(t), 3),
                        (a, b, c, t) => a * Math.Pow(Math.Sin(t), 3)
                        );
                    }
                }

                public static ParametricEquations ArchimedesSpiral
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * t * Math.Cos(t),
                        (a, b, c, t) => a * t * Math.Sin(t)
                        );
                    }
                }
                public static ParametricEquations Bifoliate
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => (8 * a * Math.Pow(Math.Sin(t), 2) * Math.Pow(Math.Cos(t), 2)) / (Math.Cos(4 * t) + 3),
                        (a, b, c, t) => (8 * a * Math.Pow(Math.Sin(t), 3) * Math.Cos(t)) / (Math.Cos(4 * t) + 3)
                        );
                    }
                }
                public static ParametricEquations Cardioid
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * (1 - Math.Cos(t)) * Math.Cos(t),
                        (a, b, c, t) => a * Math.Sin(t) * (1 - Math.Cos(t))
                        );
                    }
                }
                public static ParametricEquations Cycloid
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * (t - Math.Sin(t)),
                        (a, b, c, t) => a * (1 - Math.Cos(t))
                        );
                    }
                }
                public static ParametricEquations CycloidOfCeva
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * Math.Cos(t) * (2 * Math.Cos(2 * t + 1)),
                        (a, b, c, t) => a * Math.Sin(t) * (2 * Math.Cos(2 * t + 1))
                        );
                    }
                }
                public static ParametricEquations EightCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * Math.Sin(t),
                        (a, b, c, t) => a * Math.Sin(t) * Math.Cos(t)
                        );
                    }
                }
                public static ParametricEquations FishCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * (Math.Cos(t) - (Math.Pow(Math.Sin(t), 2) / Math.Sqrt(2))),
                        (a, b, c, t) => a * Math.Sin(t) * Math.Cos(t)
                        );
                    }
                }
                public static ParametricEquations Folium
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => Math.Pow(Math.Cos(t), 2) * (4 * a * Math.Pow(Math.Sin(t), 2) - b),
                        (a, b, c, t) => Math.Sin(t) * Math.Cos(t) * (4 * a * Math.Pow(Math.Sin(t), 2) - b)
                        );
                    }
                }
                public static ParametricEquations GarfieldCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * t * Math.Pow(Math.Cos(t), 2),
                        (a, b, c, t) => a * t * Math.Sin(t) * Math.Cos(t)
                        );
                    }
                }
                public static ParametricEquations Hypocycloid
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => (a - b) * Math.Cos(t) + b * Math.Cos((t * (a - b)) / b),
                        (a, b, c, t) => (a - b) * Math.Sin(t) + b * Math.Sin((t * (a - b)) / b)
                        );
                    }
                }
                public static ParametricEquations ConchoidOfNicomedes
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => Math.Cos(t) * (a * (1 / Math.Cos(t)) + b),
                        (a, b, c, t) => Math.Sin(t) * (a * (1 / Math.Cos(t)) + b)
                        );
                    }
                }
                public static ParametricEquations PiriformCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * (Math.Sin(t) + 1),
                        (a, b, c, t) => b * (Math.Sin(t) + 1) * Math.Cos(t)
                        );
                    }
                }
                public static ParametricEquations Quadrifolium
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * Math.Sin(2 * t) * Math.Cos(t),
                        (a, b, c, t) => a * Math.Sin(t) * Math.Sin(2 * t)
                        );
                    }
                }
                public static ParametricEquations Ranunculoid
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * (6 * Math.Cos(t) - Math.Cos(6 * t)),
                        (a, b, c, t) => a * (6 * Math.Sin(t) - Math.Sin(6 * t))
                        );
                    }
                }
                public static ParametricEquations SerpentineCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * (1 / Math.Tan(t)),
                        (a, b, c, t) => b * Math.Sin(t) * Math.Cos(t)
                        );
                    }
                }
                public static ParametricEquations SwastikaCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => Math.Cos(t) * Math.Sqrt(-(1 / Math.Sin(4 * t))) * Math.Abs(Math.Sin(2 * t)),
                        (a, b, c, t) => Math.Sin(t) * Math.Sqrt(-(1 / Math.Sin(4 * t))) * Math.Abs(Math.Sin(2 * t))
                        );
                    }
                }
                public static ParametricEquations TalbotCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => (Math.Cos(t) * (Math.Pow(a, 2) - Math.Pow(b, 2)) * Math.Pow(Math.Sin(t), 2) + Math.Pow(a, 2)) / a,
                        (a, b, c, t) => (Math.Sin(t) * (Math.Pow(a, 2) - Math.Pow(b, 2)) * Math.Pow(Math.Sin(t), 2) - 2 * (Math.Pow(a, 2) - Math.Pow(b, 2)) + Math.Pow(a, 2)) / b
                        );
                    }
                }
                public static ParametricEquations TeardropCurve
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => Math.Cos(t),
                        (a, b, c, t) => Math.Sin(t) * Math.Pow(Math.Sin(t / 2), a)
                        );
                    }
                }
                public static ParametricEquations Trifolium
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => -a * Math.Cos(t) * Math.Cos(3 * t),
                        (a, b, c, t) => -a * Math.Sin(t) * Math.Cos(3 * t)
                        );
                    }
                }
                public static ParametricEquations Circle
                {
                    get
                    {
                        return new ParametricEquations(
                        (a, b, c, t) => a * Math.Sin(t),
                        (a, b, c, t) => a * Math.Cos(t)
                        );
                    }
                }
                #endregion
            }

        }
    }
}