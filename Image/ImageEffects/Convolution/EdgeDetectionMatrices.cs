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
            public partial class ConvolutionMatrices
            {
                public static double[,] BottomSobel
                {
                    get
                    {
                        return new double[3, 3] {{-1, -2, -1},
                                                 {0, 0, 0},
                                                 {1, 2, 1}};
                    }
                }

                public static double[,] Emboss
                {
                    get
                    {
                        return new double[3, 3] {{-2, -1, 0},
                                                 {-1, 1, 1},
                                                 {0, 1, 2}};
                    }
                }

                public static double[,] LeftSobel
                {
                    get
                    {
                        return new double[3, 3] {{1, 0, -1},
                                                 {2, 0, -2},
                                                 {1, 0, -1}};
                    }
                }

                public static double[,] Outline
                {
                    get
                    {
                        return new double[3, 3] {{-1, -1, -1},
                                                 {-1, 8, -1},
                                                 {-1, -1, -1}};
                    }
                }

                public static double[,] RightSobel
                {
                    get
                    {
                        return new double[3, 3] {{-1, 0, 1},
                                                 {-2, 0, 2},
                                                 {-1, 0, 1}};
                    }
                }

                public static double[,] TopSobel
                {
                    get
                    {
                        return new double[3, 3] {{1, 2, 1},
                                                 {0, 0, 0},
                                                 {-1, -2, -1}};
                    }
                }

                public static double[,] EdgeDetect
                {
                    get
                    {
                        return new double[3, 3] {{0, 1, 0},
                                                 {1, -4, 1},
                                                 {0, 1, 0}};
                    }
                }
            }
        }
    }
}