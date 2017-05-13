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
                    var oldImage = new LockedBitmap(bmp);
                    var newImage = new LockedBitmap(bmp);
                    var midpoint = (int)System.Math.Floor(kernel.GetLength(0) / 2.0);
                    for (int i = 0; i < oldImage.Width; i++)
                    {
                        for (int j = 0; j < oldImage.Height; j++)
                        {
                            var accumulator = new Vector3d();
                            for (int x = 0; x < kernel.GetLength(0); x++)
                            {
                                var z = Clamp<int>(i + (x - midpoint), oldImage.Width - 1, 0);
                                for (int y = 0; y < kernel.GetLength(1); y++)
                                {
                                    var w = Clamp<int>(j + (y - midpoint), oldImage.Height - 1, 0);

                                    accumulator.X += kernel[x, y] * (byte)(oldImage[z, w].R * 255);
                                    accumulator.Y += kernel[x, y] * (byte)(oldImage[z, w].G * 255);
                                    accumulator.Z += kernel[x, y] * (byte)(oldImage[z, w].B * 255);
                                }
                            }
                            accumulator.X = Math.Ceiling(Clamp<double>(accumulator.X, 255, 0));
                            accumulator.Y = Math.Ceiling(Clamp<double>(accumulator.Y, 255, 0));
                            accumulator.Z = Math.Ceiling(Clamp<double>(accumulator.Z, 255, 0));
                            newImage[i, j] = new Color4((byte)accumulator.X, (byte)accumulator.Y, (byte)accumulator.Z, (byte)(oldImage[i, j].A * 255));
                        }
                    }
                    return newImage.ExportBitmap();
                }

                [ObsoleteAttribute]
                public static Bitmap ConvolveWithDebug(Bitmap bmp, double[,] kernel, string logPath)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(logPath, false))
                    {
                        var oldImage = new LockedBitmap(bmp);
                        var newImage = new LockedBitmap(bmp);
                        var midpoint = (int)System.Math.Floor(kernel.GetLength(0) / 2.0);
                        for (int i = 0; i < oldImage.Width; i++)
                        {
                            for (int j = 0; j < oldImage.Height; j++)
                            {
                                file.Write($"({i}, {j}) | ");
                                var accumulator = new Vector3d();
                                for (int x = 0; x < kernel.GetLength(0); x++)
                                {
                                    if (i - midpoint < 0 || i + midpoint >= oldImage.Width) continue;

                                    var z = i + (x - midpoint);
                                    for (int y = 0; y < kernel.GetLength(1); y++)
                                    {
                                        if (j - midpoint < 0 || j + midpoint >= oldImage.Height) continue;
                                        var w = j + (y - midpoint);

                                        // if (z < 0 || z >= oldImage.Width || w < 0 || w >= oldImage.Height) continue;

                                        //file.Write($"| {kernel[x, y] * (byte)(oldImage[z, w].R * 255)} (<{z}, {w}> {oldImage[z, w].R * 255} * {kernel[x, y]}) | \n");

                                        accumulator.X += kernel[x, y] * (byte)(oldImage[z, w].R * 255);
                                        accumulator.Y += kernel[x, y] * (byte)(oldImage[z, w].G * 255);
                                        accumulator.Z += kernel[x, y] * (byte)(oldImage[z, w].B * 255);
                                    }
                                }
                                accumulator.X = Math.Ceiling(Clamp<double>(accumulator.X, 255, 0));
                                accumulator.Y = Math.Ceiling(Clamp<double>(accumulator.Y, 255, 0));
                                accumulator.Z = Math.Ceiling(Clamp<double>(accumulator.Z, 255, 0));
                                file.Write($"= {accumulator.X}\n");
                                newImage[i, j] = new Color4((byte)accumulator.X, (byte)accumulator.Y, (byte)accumulator.Z, (byte)(oldImage[i, j].A * 255));
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
