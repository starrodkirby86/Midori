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

            public partial class MotionEffects
            {
                public static void Shake(OsbSprite sprite, int startTime, int endTime, Vector2 variance, int points = 20)
                {
                    var step = (endTime - startTime) / (float)points;
                    for(float i = startTime; i < endTime; i += step)
                    {
                        var pointB = (Vector2)sprite.PositionAt(startTime);
                        var pointA = pointB + new Vector2((float)(StoryboardObjectGenerator.Current.Random(0, variance.X) - variance.X / 2), (float)(StoryboardObjectGenerator.Current.Random(0, variance.Y) - variance.Y / 2));
                        sprite.Move(i, i + step, pointA, pointB);
                    }
                }

                // Goal for this one is to create that sort of camera-moving effect.
                // Maybe do something with extremely subtle easings and light movements.
                public static void ShakeSubtle(OsbSprite sprite, int startTime, int endTime, Vector2 variance, int points = 20)
                {
                    var step = (endTime - startTime) / (float)points;
                    for(float i = startTime; i < endTime; i += step)
                    {
                        var pointB = (Vector2)sprite.PositionAt(startTime);
                        var pointA = pointB + new Vector2((float)(StoryboardObjectGenerator.Current.Random(0, variance.X) - variance.X / 2), (float)(StoryboardObjectGenerator.Current.Random(0, variance.Y) - variance.Y / 2));
                        sprite.Move((OsbEasing)StoryboardObjectGenerator.Current.Random(0, Enum.GetNames(typeof(OsbEasing)).Length), i, i + step, pointA, pointB);
                    }
                }
            }
        }
    }
}