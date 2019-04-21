using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AddTrackingURL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RenameOriginalFiles();
        }

        private static void RenameOriginalFiles()
        {
            var rootDir = @"C:\Users\mbcrump\source\repos\AddTrackingURL\AddTrackingURL\Working";

            try
            {
                var fileNames = Directory.EnumerateFiles(rootDir);

                Console.WriteLine("Starting...");

                foreach (string path in fileNames)
                {
                    var dir = Path.GetDirectoryName(path);
                    var fileName = Path.GetFileName(path);


                    var newPath = Path.Combine(dir + "\\output\\", fileName);
                    File.Copy(path, newPath, true);

                    fixUrlFromPath(newPath);
                }
                Console.WriteLine("Finished!");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString() + Environment.NewLine + "Inner Exception " + ex.InnerException);
            }

            Console.ReadLine();
        }

        private static void fixUrlFromPath(string filePath)
        {
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

            // Open the file to read from.
            string readText = File.ReadAllText(filePath);

            foreach (Match m in linkParser.Matches(readText))
            {
                string finalURL = m.Value;

                if (finalURL.Contains("/azure-tips-and-tricks"))
                {
                    string resultString = Regex.Match(finalURL, @"\d+").Value;
                    int num1;
                    bool res = int.TryParse(resultString, out num1);
                    if (res == true)
                    {
                        ReplaceText(filePath, "(" + finalURL, "(https://microsoft.github.io/AzureTipsAndTricks/blog/tip" + num1 + ".html?WT.mc_id=github-azuredevtips-micrum");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Number not found in: " + finalURL);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.ResetColor();
                    }
                }
                else if (finalURL.Contains("azure.microsoft.com"))
                {
                    ReplaceText(filePath, "(" + finalURL , "(" + finalURL + "?WT.mc_id=azure-azuredevtips-micrum");
                }
                else if (finalURL.Contains("docs.microsoft.com"))
                {
                    ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=docs-azuredevtips-micrum)");
                }
                else if (finalURL.Contains("go.microsoft.com"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=go-azuredevtips-micrum");
                }
                else if (finalURL.Contains("support.microsoft.com"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=support-azuredevtips-micrum");
                }
                else if (finalURL.Contains("www.microsoft.com"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=microsoft-azuredevtips-micrum");
                }
                else if (finalURL.Contains("github.com"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=github-azuredevtips-micrum");
                }
                else if (finalURL.Contains("channel9.msdn.com"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=ch9-azuredevtips-micrum");
                }
                else if (finalURL.Contains("aka.ms"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=akams-azuredevtips-micrum");
                }
                else if (finalURL.Contains("nuget.org"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=nuget-azuredevtips-micrum");
                }
                else if (finalURL.Contains("twitter.com"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=twitter-azuredevtips-micrum");
                }
                else if (finalURL.Contains("youtube.com"))
                {
                   ReplaceText(filePath, "(" + finalURL + ")", "(" + finalURL + "?WT.mc_id=youtube-azuredevtips-micrum");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not Found: " + finalURL);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.ResetColor();
                }
            }
        }

        private static void ReplaceText(string filePath, string oldText, string newText)
        {
            string text = File.ReadAllText(filePath);
            text = text.Replace(oldText, newText);
            Console.WriteLine("filePath = " + filePath + Environment.NewLine + Environment.NewLine + "oldText = " + oldText + Environment.NewLine + "newText = " + newText + Environment.NewLine);
            File.WriteAllText(filePath, text);
        }

    }
}
