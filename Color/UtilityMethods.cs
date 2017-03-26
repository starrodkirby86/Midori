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
using System.Linq;

namespace StorybrewScripts
{
    namespace Midori
    {
        namespace Color
        {
            /// A class containing helper methods for color manipulation.
            /// There exists: Color Adjustment, HSB/HSL conversion, and Math
            public class ColorHelper
            {

                #region Adjust Color

                /// <summary>
                /// Darkens a color by percentage t (0-1).
                /// </summary>
                public static Color4 Darken(Color4 a, float t) => new Color4(a.R * (1 - t), a.G * (1 - t), a.B * (1 - t), a.A);

                /// <summary>
                /// Lightens a color by percentage t (0-1).
                /// </summary>
                public static Color4 Lighten(Color4 a, float t) => new Color4(a.R * (1 + t), a.G * (1 + t), a.B * (1 + t), a.A);

                /// <summary>
                /// Desaturates a color by percentage t (0-1). Also includes a boolean to use HSB/HSL-based saturation. (default HSB)
                /// </summary>
                public static Color4 Desaturate(Color4 a, float t, bool useHSB = true)
                {
                    Vector3 colorInfo = useHSB ? GetHSB(a) : GetHSL(a);
                    colorInfo.Y *= (1 - t);
                    return useHSB ? FromHSB(colorInfo) : FromHSL(colorInfo);
                }

                /// <summary>
                /// Saturates a color by percentage t (0-1). Also includes a boolean to use HSB/HSL-based saturation. (default HSB)
                /// </summary>
                public static Color4 Saturate(Color4 a, float t, bool useHSB = true)
                {
                    Vector3 colorInfo = useHSB ? GetHSB(a) : GetHSL(a);
                    colorInfo.Y *= (1 + t);
                    return useHSB ? FromHSB(colorInfo) : FromHSL(colorInfo);
                }

                /// <summary>
                /// Makes a color grayscale. The equivalent of desaturating a color by 1.
                /// </summary>
                public static Color4 Grayscale(Color4 a) => Desaturate(a, 1);

                /// <summary>
                /// Rotates the color hue by given degrees. Domain is bounded to 360 degrees.
                /// </summary>
                public static Color4 AdjustHue(Color4 a, int degrees)
                {
                    Vector3 colorInfo = GetHSB(a);
                    colorInfo.X = (colorInfo.X + degrees) % 360;
                    return FromHSB(colorInfo);
                }

                /// <summary>
                /// Finds the complementary color. The equivalent of adjusting hue by 180 degrees.
                /// </summary>
                public static Color4 Complement(Color4 a) => AdjustHue(a, 180);

                /// <summary>
                /// Inverts the given color.
                /// </summary>
                public static Color4 Invert(Color4 a)
                {
                    int rgb = a.ToArgb();
                    rgb ^= 0xffffff;
                    return (Color4)System.Drawing.Color.FromArgb(rgb);
                }

                #endregion

                #region Conversions

                public static Vector3 GetHSB(Color4 a) => new Vector3(GetHue(a), GetSaturationHSB(a), GetBrightness(a));

                public static Vector3 GetHSL(Color4 a) => new Vector3(GetHue(a), GetSaturationHSL(a), GetLightness(a));

                public static float GetHue(Color4 a, float M, float m)
                {
                    if (M == m) return 0;

                    if (M == a.R) return (60 * (a.G - a.B) / (M - m)) % 360;
                    else if (M == a.G) return (60 * (a.B - a.R) / (M - m)) + 120;
                    else if (M == a.B) return (60 * (a.R - a.G) / (M - m)) + 240;
                    else return 0;
                }

                public static float GetHue(Color4 a)
                {
                    var colors = new[] { a.R, a.G, a.B };
                    return GetHue(a, colors.Max(), colors.Min());
                }

                public static float GetBrightness(Color4 a) => new[] { a.R, a.G, a.B }.Max();

                public static float GetLightness(Color4 a, float M, float m) => (M + m) / 2;

                public static float GetLightness(Color4 a)
                {
                    var colors = new[] { a.R, a.G, a.B };
                    return GetLightness(a, colors.Max(), colors.Min());
                }

                public static float GetSaturationHSB(Color4 a, float M, float m)
                {
                    if (M == 0) return 0;
                    return (M - m) / M;
                }

                public static float GetSaturationHSB(Color4 a)
                {
                    var colors = new[] { a.R, a.G, a.B };
                    return GetSaturationHSB(a, colors.Max(), colors.Min());
                }

                public static float GetSaturationHSL(Color4 a, float M, float m)
                {
                    if ((M - m) == 0) return 0;

                    return (M - m) / (1 - Math.Abs(2 * GetLightness(a, M, m) - 1));
                }

                public static float GetSaturationHSL(Color4 a)
                {
                    var colors = new[] { a.R, a.G, a.B };
                    return GetSaturationHSL(a, colors.Max(), colors.Min());
                }

                /// <summary>
                /// Creates a Color4 from an HSB Vector3.
                /// </summary>
                public static Color4 FromHSB(Vector3 a)
                {
                    var hue = a.X;
                    var saturation = a.Y;
                    var brightness = a.Z;

                    var hi = (int)Math.Floor(hue / 60) % 6;
                    var f = (hue / 60) - Math.Floor(hue / 60);

                    var v = brightness;
                    var p = brightness * (1 - saturation);
                    var q = (float)(brightness * (1 - (f * saturation)));
                    var t = (float)(brightness * (1 - ((1 - f) * saturation)));

                    if (hi == 0) return new Color4(v, t, p, 1);
                    if (hi == 1) return new Color4(q, v, p, 1);
                    if (hi == 2) return new Color4(p, v, t, 1);
                    if (hi == 3) return new Color4(p, q, v, 1);
                    if (hi == 4) return new Color4(t, p, v, 1);

                    return new Color4(v, p, q, 1);
                }

                /// <summary>
                /// Creates a Color4 from an HSL Vector3.
                /// </summary>
                public static Color4 FromHSL(Vector3 a)
                {
                    var hue = a.X;
                    var saturation = a.Y;
                    var lightness = a.Z;

                    var c = (1 - Math.Abs(2 * lightness - 1)) * saturation;
                    var x = c * (1 - Math.Abs((hue / 60) % 2) - 1);
                    var m = lightness - c / 2;

                    Vector3 rgbPrime;

                    if (hue >= 0 && hue < 60) rgbPrime = new Vector3(c, x, 0);
                    else if (hue >= 60 && hue < 120) rgbPrime = new Vector3(x, c, 0);
                    else if (hue >= 120 && hue < 180) rgbPrime = new Vector3(0, c, x);
                    else if (hue >= 180 && hue < 240) rgbPrime = new Vector3(0, x, c);
                    else if (hue >= 240 && hue < 300) rgbPrime = new Vector3(x, 0, c);
                    else rgbPrime = new Vector3(c, 0, x);

                    rgbPrime += new Vector3(m);
                    return new Color4(rgbPrime.X, rgbPrime.Y, rgbPrime.Z, 1f);
                }

                /// <summary>
                /// Creates a Color4 from a hex color code.
                /// </summary>
                public static Color4 FromHTML(string htmlColor) => (Color4)ColorTranslator.FromHtml(htmlColor);

                #endregion

                #region Math

                /// <summary>
                /// Finds the interpolation of two colors based on location of t (0-1).
                /// The equivalent of mixing two colors, where t is the weight given between the two colors.
                /// </summary>
                public static Color4 Lerp(Color4 a, Color4 b, float t)
                {
                    var colorA = new Vector4(a.R, a.G, a.B, a.A);
                    var colorB = new Vector4(b.R, b.G, b.B, b.A);
                    var v = Vector4.Lerp(colorA, colorB, t);
                    return new Color4(v.X, v.Y, v.Z, v.W);
                }

                #endregion

            }
        }
    }
}
