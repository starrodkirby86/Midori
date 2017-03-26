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
        namespace UnitTest
        {
            // TODO: Develop test suites?

            /// A unit testing class for the library methods. Add test cases into the unit test, then
            /// run the Execute method. All unit testing methods should use storybrew's Assert method.
            public class UnitTest
            {
                private List<Action> testCases;
                
                public UnitTest()
                {
                    testCases = new List<Action>();
                }

                /// <summary>
                /// Adds a new test case in the suite.
                /// </summary>
                public void AddCase(Action a) => testCases.Add(a);

                /// <summary>
                /// Executes all test cases in the suite.
                /// Will output a log denoting cases that have passed or failed.
                /// Utilizes try-catching, so exceptions don't fail all the cases.
                /// </summary>
                public void Execute()
                {
                    var flags = new List<bool>();
                    var errors = new Queue<Exception>();

                    foreach(var a in testCases)
                    {
                        try
                        {
                            a.Invoke();
                            flags.Add(true);
                        }
                        catch (Exception e)
                        {
                            errors.Enqueue(e);
                            flags.Add(false);
                        }
                    }

                    string logMessage = "";

                    for(int i = 0; i < testCases.Count; i++)
                    {
                        logMessage += $"Test case {i} ... ";
                        logMessage += flags[i] ? "OK" : "FAILED";
                        logMessage += "\n";
                        
                        if (!flags[i])
                        {
                            logMessage += "<<< error log >>>\n";
                            var e = errors.Dequeue();
                            //logMessage += $">> {e.Message}\n";
                            logMessage += e.ToString();
                            logMessage += "\n<<< _______ >>>\n";
                        }
                    }

                    StoryboardObjectGenerator.Current.Log($"=== UNIT TESTING ===\n{logMessage}=== UNIT TESTING ===");
                }
            }
        }
    }
}