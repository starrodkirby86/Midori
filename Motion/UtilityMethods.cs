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
                public static Vector2 Rotate(Vector2 p, double t, Vector2 origin)
                {
                    p -= origin;
                    var rotatedP = new Vector2((float)(p.X * Math.Cos(t) - p.Y * Math.Sin(t)), (float)(p.X * Math.Sin(t) + p.Y * Math.Cos(t)));
                    return rotatedP + origin;
                }

                public static Vector2 Rotate(Vector2 p, double t) => Rotate(p, t, Vector2.Zero);

                public static Vector2 Mirror(Vector2 p) => Rotate(p, MathHelper.Pi);
                
                public static Vector2 MirrorToScreen(Vector2 p) => Rotate(p, MathHelper.Pi, new Vector2(320, 240));

                public static Vector2 MirrorHorizontal(Vector2 p)
                {
                    var rotation = MirrorToScreen(p);
                    return new Vector2(rotation.X, p.Y);
                }

                public static Vector2 MirrorVertical(Vector2 p)
                {
                    var rotation = MirrorToScreen(p);
                    return new Vector2(p.X, rotation.Y);
                }

                public static Vector2 XRange { get { return new Vector2(-107, 747); } }

                public static Vector2 YRange { get { return new Vector2(0, 480); } }
            }
        }
    }
}