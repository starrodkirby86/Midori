using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    namespace Midori
    {
        namespace Image
        {
            /// <summary>
            /// A helper class storing a pair of convolution matrices as double [,].
            /// </summary>
            public partial class ConvolutionMatrices
            {

                public static double[,] Identity
                {
                    get
                    {
                        return new double[3, 3] {{0, 0, 0},
                                                 {0, 1, 0},
                                                 {0, 0, 0}};
                    }
                }

                public static double[,] Sharpen
                {
                    get
                    {
                        return new double[3, 3] {{0, -1, 0},
                                                 {-1, 5, -1},
                                                 {0, -1, 0}};
                    }
                }

                public static double[,] BoxBlur
                {
                    get
                    {
                        return new double[3, 3] {{1 / 9.0, 1 / 9.0, 1 / 9.0},
                                                 {1 / 9.0, 1 / 9.0, 1 / 9.0},
                                                 {1 / 9.0, 1 / 9.0, 1 / 9.0}};
                    }
                }                

                public static double[,] GaussianBlur3x3
                {
                    get
                    {
                        return new double[3, 3] {{1 / 16.0, 2 / 16.0, 1 / 16.0},
                                                 {2 / 16.0, 4 / 16.0, 2 / 16.0},
                                                 {1 / 16.0, 2 / 16.0, 1 / 16.0}};
                    }
                }                  

                public static double[,] GaussianBlur5x5
                {
                    get
                    {
                        return new double[5, 5] {{1 / 256.0, 4 / 256.0, 6 / 256.0, 4 / 256.0, 1 / 256.0},
                                                 {4 / 256.0, 16 / 256.0, 24 / 256.0, 16 / 256.0, 4 / 256.0},
                                                 {6 / 256.0, 24 / 256.0, 36 / 256.0, 24 / 256.0, 6 / 256.0},
                                                 {4 / 256.0, 16 / 256.0, 24 / 256.0, 16 / 256.0, 4 / 256.0},
                                                 {1 / 256.0, 4 / 256.0, 6 / 256.0, 4 / 256.0, 1 / 256.0}};
                    }
                }
            }
        }
    }
}