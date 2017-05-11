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
        namespace Image
        {
            public class Convolution
            {
                public static Bitmap Convolve(Bitmap bmp, double[,] kernel)
                {
                    var image = new LockedBitmap(bmp);
                    for (int i = 0; i < image.Width; i++)
                    {
                        for (int j = 0; j < image.Height; j++)
                        {
                            var accumulator = new Vector3d();
                            for (int x = 0; x < kernel.GetLength(0); x++)
                            {
                                var z = (i + x) >= image.Width ? image.Width - 1 : i + x;
                                for (int y = 0; y < kernel.GetLength(1); y++)
                                {
                                    var w = (j + y) >= image.Height ? image.Height - 1 : j + y;
                                    accumulator.X += kernel[x, y] * (byte)(image[z, w].R * 255);
                                    accumulator.Y += kernel[x, y] * (byte)(image[z, w].G * 255);
                                    accumulator.Z += kernel[x, y] * (byte)(image[z, w].B * 255);
                                }
                            }
                            accumulator.X = Clamp<double>(accumulator.X, 255, 0);
                            accumulator.Y = Clamp<double>(accumulator.Y, 255, 0);
                            accumulator.Z = Clamp<double>(accumulator.Z, 255, 0);
                            image[i, j] = new Color4((float)accumulator.X, (float)accumulator.Y, (float)accumulator.Z, image[i, j].A);
                        }
                    }
                    return image.ExportBitmap();
                }

                /*
                    I'm multiplying the wrong indices on the kernel.
                 */
                public static Bitmap ConvolveWithDebug(Bitmap bmp, double[,] kernel, string logPath)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(logPath, false))
                    {
                        var oldImage = new LockedBitmap(bmp);
                        var newImage = new LockedBitmap(bmp);
                        for (int i = 0; i < oldImage.Width; i++)
                        {
                            for (int j = 0; j < oldImage.Height; j++)
                            {
                                file.Write($"({i}, {j}) | ");
                                var accumulator = new Vector3d();
                                for (int x = 0; x < kernel.GetLength(0); x++)
                                {
                                    var midpoint = x / 2;
                                    if (i - midpoint < 0 || i + midpoint >= oldImage.Width) continue;

                                    var z = i + (x - midpoint);
                                    // var z = (i + x) >= oldImage.Width ? oldImage.Width - 1 : i + x;
                                    for (int y = 0; y < kernel.GetLength(1); y++)
                                    {
                                        if (j - midpoint < 0 || j + midpoint >= oldImage.Height) continue;
                                        var w = j + (y - midpoint);

                                        // var w = (j + y) >= oldImage.Height ? oldImage.Height - 1 : j + y;

                                        if (z < 0 || z >= oldImage.Width || w < 0 || w >= oldImage.Height) continue;

                                        file.Write($" {kernel[x, y] * (byte)(oldImage[z, w].R * 255)} ");

                                        accumulator.X += kernel[x, y] * (byte)(oldImage[z, w].R * 255);
                                        accumulator.Y += kernel[x, y] * (byte)(oldImage[z, w].G * 255);
                                        accumulator.Z += kernel[x, y] * (byte)(oldImage[z, w].B * 255);
                                    }
                                }
                                accumulator.X = Clamp<double>(accumulator.X, 255, 0);
                                accumulator.Y = Clamp<double>(accumulator.Y, 255, 0);
                                accumulator.Z = Clamp<double>(accumulator.Z, 255, 0);
                                file.Write($"= {accumulator.X}\n");
                                newImage[i, j] = new Color4((float)accumulator.X, (float)accumulator.Y, (float)accumulator.Z, oldImage[i, j].A);
                            }
                        }
                        return newImage.ExportBitmap();
                    }

                }

                public static T Clamp<T>(T value, T max, T min) where T : System.IComparable<T>
                    => (value.CompareTo(max) > 0) ? max : (value.CompareTo(min) < 0) ? min : value;
            }
        }
    }
}
