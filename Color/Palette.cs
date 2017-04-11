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
            /// A list providing an interface for managing multiple colors.
            /// </summary>
            public class Palette : List<Color4>
            {

                /// <summary>
                /// Load palette from an existing .ACO file (Adobe Photoshop Color Palette file)
                /// Code adapted from KDE Krita's method. Only works for RGB and HSB colorspace palettes.
                /// </summary>
                public static Palette LoadAco(string file)
                {
                    var aco = StoryboardObjectGenerator.Current.OpenProjectFile(file);

                    // First, read the ACO file version and number of colors in the header.
                    var bufferSize = 2;
                    var buffer = new byte[bufferSize];

                    var version = ReadShort(ref aco);
                    var numColors = ReadShort(ref aco);

                    var colors = new Palette();

                    // If version 2, read the v2 colors instead
                    if (version == 1 && aco.Length > 4 + numColors * 10)
                    {
                        aco.Seek(4 + numColors * 10, 0);
                        version = ReadShort(ref aco);
                        numColors = ReadShort(ref aco);
                    }

                    for (int i = 0; i < numColors && aco.Position != aco.Length; i++)
                    {
                        var colorSpace = (ushort)ReadShort(ref aco);
                        var ch1 = (ushort)ReadShort(ref aco);
                        var ch2 = (ushort)ReadShort(ref aco);
                        var ch3 = (ushort)ReadShort(ref aco);
                        var ch4 = (ushort)ReadShort(ref aco);

                        if (colorSpace == 0) // RGB
                        {
                            var color = new Color4((byte)(ch1 >> 8), (byte)(ch2 >> 8), (byte)(ch3 >> 8), 255);
                            colors.Add(color);
                        }
                        else if (colorSpace == 1) // HSB
                        {
                            // Untested.
                            var color = ColorHelper.FromHSB(new Vector3(ch1 / 65536f, ch2 / 65536f, ch3 / 65536f));
                            colors.Add(color);
                        }
                        else
                        {
                            StoryboardObjectGenerator.Current.Log($"Warning: Unsupported colorspace in palette {file}");
                        }

                        // In a v2 aco structure, there's extra markers that are not useful for us.
                        if (version == 2)
                        {
                            // Extra gunk needs to be skipped.
                            ReadShort(ref aco); // v2 marker
                            var size = ReadShort(ref aco) - 1; // get size
                            if (size > 0)
                            {
                                var ba = new byte[size * 2];
                                aco.Read(ba, 0, size * 2);
                            }
                            ReadShort(ref aco); // then skip the end marker.
                        }
                    }

                    return colors;
                }

                /// <summary>
                /// Reads a short from a stream, pushing the stream.
                /// </summary>
                public static Int16 ReadShort(ref Stream s, bool useBigEndian = true)
                {
                    var buffer = new byte[2];
                    s.Read(buffer, 0, 2);
                    return useBigEndian ? SwapEndian(buffer) : ToShort(buffer);
                }

                /// <summary>
                /// Converts a 2-byte array to Int16
                /// </summary>
                public static Int16 ToShort(byte[] a) => (Int16)((a[1] << 8) + a[0]);

                /// <summary>
                /// Swaps endianness of a 16-bit value.
                /// </summary>
                public static Int16 SwapEndian(byte[] a) => (Int16)((a[0] << 8) + a[1]);
            }
        }
    }
}