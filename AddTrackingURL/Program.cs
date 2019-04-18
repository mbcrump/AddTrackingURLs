using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AddTrackingURL
{
    class Program
    {
        static void Main(string[] args)
        {
            RenameOriginalFiles();
        }

        static void RenameOriginalFiles()
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
                Console.WriteLine("Error " + ex.ToString());
            }

            Console.ReadLine();
        }

        static void fixUrlFromPath(string filePath)
        {
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
           
            string[] lines = File.ReadAllLines(filePath);

            int count = 0;

            foreach (string line in lines)
            {
                count++;

                foreach (Match m in linkParser.Matches(line))
                {
                    //Console.WriteLine(m.Value);
                    if (m.Value.Contains("azure.microsoft.com") || m.Value.Contains("azuremarketplace.microsoft.com"))
                    { 
                        ReplaceText(filePath, m.Value, m.Value + "?WT.mc_id=azure-azuretipsandtricks-micrum");
                    }
                    else if(m.Value.Contains("docs.microsoft.com"))
                    {
                        ReplaceText(filePath, m.Value, m.Value + "?WT.mc_id=docs-azuretipsandtricks-micrum");
                    }
                    else if (m.Value.Contains("github.com"))
                    {
                        ReplaceText(filePath, m.Value, m.Value + "?WT.mc_id=github-azuretipsandtricks-micrum");
                    }
                    else if (m.Value.Contains("channel9.msdn.com"))
                    {
                        ReplaceText(filePath, m.Value, m.Value + "?WT.mc_id=ch9-azuretipsandtricks-micrum");
                    }
                    else if (m.Value.Contains("aka.ms"))
                    {
                        ReplaceText(filePath, m.Value, m.Value + "?WT.mc_id=akams-azuretipsandtricks-micrum");
                    }
                    else if (m.Value.Contains("nuget.org"))
                    {
                        ReplaceText(filePath, m.Value, m.Value + "?WT.mc_id=nuget-azuretipsandtricks-micrum");
                    }
                    else if (m.Value.Contains("twitter.com"))
                    {
                        ReplaceText(filePath, m.Value, m.Value + "?WT.mc_id=twitter-azuretipsandtricks-micrum");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Not Found: " + m.Value);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.ResetColor();
                    }

                }
            }
        }

        static void ReplaceText(string filePath, string oldText, string newText)
        {
            string text = File.ReadAllText(filePath);
            text = text.Replace(oldText, newText);
            Console.WriteLine("oldText = " + oldText + Environment.NewLine + "newText = " + newText + Environment.NewLine);
            File.WriteAllText(filePath, text);
        }

    }
}
