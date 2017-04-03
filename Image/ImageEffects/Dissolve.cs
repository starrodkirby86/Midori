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
        namespace Image
        {
            public partial class ImageEffects
            {
                /// <summary>
                /// Provides a 2D array of dissolve pixels that can be used as information to generate
                /// a set of OsbSprites used in a dissolve effect. Make sure to set the pixelSize value
                /// to a reasonable value, as the amount of sprites used is the image area divided by pixelSize.
                /// </summary>
                public static SpriteDescription[,] Dissolve(string baseImagePath, string spritePath, float baseImageScale, int pixelSize)
                {
                    var baseImage = StoryboardObjectGenerator.Current.GetMapsetBitmap(baseImagePath);
                    var spriteScale = ImageHelper.GetScaleRatio(spritePath, pixelSize) * baseImageScale;
                    var xMax = baseImage.Width / pixelSize;
                    var yMax = baseImage.Height / pixelSize;

                    var dissolvePixels = new SpriteDescription[xMax, yMax];

                    for (int i = 0; i < xMax; i++)
                    {
                        for (int j = 0; j < yMax; j++)
                        {
                            var location = new Vector2(i * pixelSize * baseImageScale, j * pixelSize * baseImageScale) + new Vector2(-107, 0);
                            var color = (Color4) baseImage.GetPixel(i * pixelSize, j * pixelSize);
                            dissolvePixels[i, j] =  new SpriteDescription {
                                spritePath = spritePath,
                                location = location,
                                scale = spriteScale,
                                color = color
                            };
                        }
                    }

                    return dissolvePixels;
                }
            }
        }
    }
}