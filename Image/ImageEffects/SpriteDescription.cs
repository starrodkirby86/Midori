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
            /// <summary>
            /// Stores common information for a sprite for easy generation and utilization with effects.
            /// </summary>
            public class SpriteDescription
            {
                public string spritePath = "";
                public Vector2 location = Vector2.Zero;
                public float scale = 1f;
                public Color4 color = Color4.White;
            }
        }
    }
}