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

            public class ImageFilters
            {
                public static string Sharpen(string sourcePath, string outputPathFolder = "fx", string suffix = "sharpen")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.Sharpen, outputPathFolder, suffix);

                public static string BoxBlur(string sourcePath, string outputPathFolder = "fx", string suffix = "blur")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.BoxBlur, outputPathFolder, suffix);

                public static string GaussianBlur3x3(string sourcePath, string outputPathFolder = "fx", string suffix = "gb")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.GaussianBlur3x3, outputPathFolder, suffix);

                public static string GaussianBlur5x5(string sourcePath, string outputPathFolder = "fx", string suffix = "gb")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.GaussianBlur5x5, outputPathFolder, suffix);

                public static string BottomSobel(string sourcePath, string outputPathFolder = "fx", string suffix = "bsobel")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.BottomSobel, outputPathFolder, suffix);

                public static string Emboss(string sourcePath, string outputPathFolder = "fx", string suffix = "emboss")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.Emboss, outputPathFolder, suffix);

                public static string LeftSobel(string sourcePath, string outputPathFolder = "fx", string suffix = "lsobel")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.LeftSobel, outputPathFolder, suffix);

                public static string Outline(string sourcePath, string outputPathFolder = "fx", string suffix = "outline")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.Outline, outputPathFolder, suffix);

                public static string RightSobel(string sourcePath, string outputPathFolder = "fx", string suffix = "rsobel")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.RightSobel, outputPathFolder, suffix);

                public static string TopSobel(string sourcePath, string outputPathFolder = "fx", string suffix = "tsobel")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.TopSobel, outputPathFolder, suffix);

                public static string EdgeDetect(string sourcePath, string outputPathFolder = "fx", string suffix = "edge")
                    => ApplyImageFilter(sourcePath, ConvolutionMatrices.EdgeDetect, outputPathFolder, suffix);

                public static string ApplyImageFilter(string sourcePath, double[,] kernel, string outputPathFolder = "fx", string suffix = "fx")
                {
                    var bitmap = StoryboardObjectGenerator.Current.GetMapsetBitmap(sourcePath);
                    var output = Convolution.Convolve(bitmap, kernel);

                    // Save the output into a file:
                    if (!FileHelper.DirectoryExists(outputPathFolder))
                        FileHelper.CreateDirectory(outputPathFolder);

                    var filename = $"{Path.GetFileNameWithoutExtension(sourcePath)}_{suffix}.png";
                    var savePath = Path.Combine(outputPathFolder, filename);

                    FileHelper.SaveBitmap(output, savePath);

                    return savePath;
                }
            }
        }
    }
}