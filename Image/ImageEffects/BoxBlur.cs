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
            using Midori.File;
            public partial class ImageEffects
            {
                public static string BoxBlur(string SourcePath, string OutputPathFolder)
                {
                    var bitmap = StoryboardObjectGenerator.Current.GetMapsetBitmap(SourcePath);
                    /*var kernel = new double[3,3] {  {1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0},
                                                    {1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0},
                                                    {1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0} };
                                                    */
                    var kernel = new double[3, 3] { {0.0625, 0.125, 0.0625},
                                                    {0.125, 0.25, 0.125},
                                                    {0.0625, 0.125, 0.0625} };
                    var output = Convolution.ConvolveWithDebug(bitmap, kernel, FileHelper.FullPath(Path.Combine(OutputPathFolder, "debug.log")));

                    // Save the output into a file:
                    if (!FileHelper.DirectoryExists(OutputPathFolder))
                        FileHelper.CreateDirectory(OutputPathFolder);

                    var savePath = Path.Combine(OutputPathFolder, "blur.png");

                    FileHelper.SaveBitmap(output, savePath);

                    return savePath;
                }
            }
        }
    }
}
