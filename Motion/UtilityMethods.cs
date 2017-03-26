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
            class MotionHelper
            {

                public static Vector2 Rotate(Vector2 p, float t) => new Vector2((float)(p.X * Math.Cos(t) - p.Y * Math.Sin(t)), (float)(p.X * Math.Sin(t) + p.Y * Math.Cos(t)));

                public static Vector2 Rotate(Vector2 p, float t, Vector2 origin)
                {
                    p -= origin;
                    var rotatedP = new Vector2((float)(p.X * Math.Cos(t) - p.Y * Math.Sin(t)), (float)(p.X * Math.Sin(t) + p.Y * Math.Cos(t)));
                    return rotatedP + origin;
                }



                public static Vector2 XRange { get { return new Vector2(-107, 747); } }

                public static Vector2 YRange { get { return new Vector2(0, 480); } }
            }
        }
    }
}