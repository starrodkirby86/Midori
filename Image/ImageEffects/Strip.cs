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
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    namespace Midori
    {
        using Midori.File;
        namespace Image
        {
            public partial class ImageEffects
            {
                /// <summary>
                /// Provides a 2D array of strips that can be used as information to generate
                /// a set of OsbSprites in a wipe transition. The image is split into strips based on
                /// a given size, with the strips being generated if the files don't exist yet.
                /// By default, regeneration of bitmaps is turned off for efficiency, but they can be forcefully
                /// turned on given the forceGeneration param set to true.
                /// </summary>
                public static SpriteDescription[,] Strip(string baseImagePath, string spriteDirectory, Vector2 stripSize, float baseImageScale, bool forceGeneration = false)
                {
                    var baseImage = StoryboardObjectGenerator.Current.GetMapsetBitmap(baseImagePath);
                    if (forceGeneration) FileHelper.CleanDirectory(spriteDirectory);
                    else FileHelper.CreateDirectory(spriteDirectory);
                    
                    var xMax = baseImage.Width / (int)stripSize.X;
                    var yMax = baseImage.Height / (int)stripSize.Y;

                    var strips = new SpriteDescription[xMax, yMax];

                    for (int i = 0; i < xMax; i++)
                    {
                        for (int j = 0; j < yMax; j++)
                        {
                            var location = new Vector2(i * (int)stripSize.X, j * (int)stripSize.Y);
                            var sizeX = (int)((stripSize.X + location.X) > baseImage.Width ? baseImage.Width - location.X : stripSize.X);
                            var sizeY = (int)((stripSize.Y + location.Y) > baseImage.Height ? baseImage.Height - location.Y : stripSize.Y);

                            var stripPath = Path.Combine(spriteDirectory, $"{i:X4}{j:X4}.png");

                            if (!FileHelper.FileExists(stripPath) || forceGeneration)
                            {
                                var rect = new Rectangle((int)location.X, (int)location.Y, sizeX, sizeY);
                                var strip = baseImage.Clone(rect, baseImage.PixelFormat);
                                FileHelper.SaveBitmap(strip, stripPath);
                                strip.Dispose();
                            }

                            strips[i, j] = new SpriteDescription
                            {
                                spritePath = stripPath,
                                location = location * baseImageScale + new Vector2(-107, 0),
                                scale = baseImageScale,
                            };
                        }
                    }

                    return strips;
                }

                /// <summary>
                /// Provides a 2D array of strips that can be used as information to generate
                /// a set of OsbSprites in a wipe transition. The strips are entirely vertical.
                /// By default, regeneration of bitmaps is turned off for efficiency, but they can be forcefully
                /// turned on given the forceGeneration param set to true.
                /// </summary>
                public static SpriteDescription[,] StripVerticalBars(string baseImagePath, string spriteDirectory, float gapSize, float baseImageScale, bool forceGeneration = false)
                {
                    var baseImage = StoryboardObjectGenerator.Current.GetMapsetBitmap(baseImagePath);
                    return Strip(baseImagePath, spriteDirectory, new Vector2(gapSize, baseImage.Height), baseImageScale, forceGeneration);
                }

                /// <summary>
                /// Provides a 2D array of strips that can be used as information to generate
                /// a set of OsbSprites in a wipe transition. The strips are entirely horizontal.
                /// By default, regeneration of bitmaps is turned off for efficiency, but they can be forcefully
                /// turned on given the forceGeneration param set to true.
                /// </summary>
                public static SpriteDescription[,] StripHorizontalBars(string baseImagePath, string spriteDirectory, float gapSize, float baseImageScale, bool forceGeneration = false)
                {
                    var baseImage = StoryboardObjectGenerator.Current.GetMapsetBitmap(baseImagePath);
                    return Strip(baseImagePath, spriteDirectory, new Vector2(baseImage.Width, gapSize), baseImageScale, forceGeneration);
                }
            }
        }
    }
}