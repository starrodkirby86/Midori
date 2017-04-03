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
        namespace File
        {
            class FileHelper
            {
                public static string FullPath(string path, bool useMapset = true) => Path.Combine(useMapset ? StoryboardObjectGenerator.Current.MapsetPath : StoryboardObjectGenerator.Current.ProjectPath, path);
                public static bool DirectoryExists(string path, bool useMapset = true) => System.IO.Directory.Exists(FullPath(path, useMapset));
                public static bool FileExists(string path, bool useMapset = true) => System.IO.File.Exists(FullPath(path, useMapset));

                public static DirectoryInfo CreateDirectory(string path, bool useMapset = true) => System.IO.Directory.CreateDirectory(FullPath(path, useMapset));

                public static void SaveBitmap(Bitmap bmp, string path) => bmp.Save(FullPath(path));
                public static void SaveBitmap(Bitmap bmp, string path, System.Drawing.Imaging.ImageFormat format) => bmp.Save(FullPath(path), format);

                public static void CleanDirectory(string path, bool useMapset = true)
                {
                    string fullPath = FullPath(path);
                    var directory = new DirectoryInfo(fullPath);
                    directory.Delete(true);
                    CreateDirectory(path, useMapset);
                }
            }
        }
    }
}