using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlogValidator
{
    // Maira suggested these rules:
    //
    // TODO: Image sizes
    // TODO: Avoid non-English words(like i.e., e.g., etc.)
    //
    // Meenal suggested these rules:
    //
    // TODO: Minimum length for a post should be 300 words

    internal static class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("error: must specify an input path");
                return 1;
            }

            var directory = Path.GetFullPath(args[0]);
            if (!Directory.Exists(directory))
            {
                Console.Error.WriteLine($"error: directory '{directory}' does not exist");
                return 1;
            }

            try
            {
                return Run(directory) ? 0 : 1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return 1;
            }
        }

        private static bool Run(string directory)
        {
            var files = FindMarkdownFiles(directory);

            var diagnostics = Validate(files);

            var isInsideGitHubAction = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

            foreach (var d in diagnostics)
            {
                var path = Path.GetRelativePath(directory, d.FileName);
                var severity = d.IsWarning ? "warning" : "error";

                if (isInsideGitHubAction)
                {
                    var line = d.LinePositionSpan.Start.Line + 1;
                    var col = d.LinePositionSpan.Start.Column + 1;
                    Console.WriteLine($"::{severity} file={path},line={line},col={col}::{d.Id} {d.Message}");
                }
                else
                {
                    if (d.IsWarning)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"{path}({d.LinePositionSpan}): {severity}: {d.Id}: {d.Message}");

                    Console.ResetColor();
                }
            }

            return diagnostics.Any(d => !d.IsWarning);
        }

        private static IEnumerable<string> FindMarkdownFiles(string directory)
        {
            return Directory.GetFiles(directory, "*.md", SearchOption.AllDirectories)
                                 .Where(p => IsIncluded(directory, p));
        }

        private static bool IsIncluded(string repoPath, string path)
        {
            var relativePath = Path.GetRelativePath(repoPath, path);
            var segments = GetSegments(relativePath);

            if (segments.Length < 2 || segments[1].Length < 2)
                return false;

            if (!int.TryParse(segments[0], out var year))
                return false;

            if (!int.TryParse(segments[1].Substring(0, 2), out var month))
                return false;

            if (year < 2020 || year == 2020 && month < 9)
                return false;

            if (year == 2020 && month == 9)
            {
                if (segments.Length >= 3)
                {
                    var grandfathered = new[]
                    {
                        "announcing-entity-framework-5.0-rc1",
                        "Arm64PerfInNet5",
                        "debug-dotnet-in-wsl",
                        "dotnet5rc1",
                        "netstandard-update",
                    };

                    var isGrandfathered = grandfathered.Contains(segments[2], StringComparer.Ordinal);
                    if (isGrandfathered)
                        return false;
                }
            }

            return true;
        }

        private static string[] GetSegments(string path)
        {
            var list = new List<string>();
            while (path.Length > 0)
            {
                var last = Path.GetFileName(path);
                list.Add(last);
                path = Path.GetDirectoryName(path);
            }

            list.Reverse();

            return list.ToArray();
        }

        private static IReadOnlyList<Diagnostic> Validate(IEnumerable<string> fileNames)
        {
            var validator = new Validator();
            var result = new List<Diagnostic>();

            foreach (var fileName in fileNames)
            {
                var diagnostics = validator.Validate(fileName);
                result.AddRange(diagnostics);
            }

            return result;
        }
    }
}
