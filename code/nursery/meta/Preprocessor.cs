using nursery.ast;
using System;
using System.Collections.Generic;
using System.Text;

namespace nursery.meta
{
    internal static class Preprocessor
    {
        internal static List<LineOfCode> Preprocess(List<string> input)
        {
            // this will store the slowly accumulating processed lines
            List<LineOfCode> lines = new List<LineOfCode>();
            // this will hold the last line's contents
            StringBuilder current = new StringBuilder();
            // this will remember whether the last line was Zone A or Zone B
            bool currentIsA = true;

            foreach (var line in input)
            {
                // empty line or line too short to have a status
                if (String.IsNullOrEmpty(line) || line.Length < 7)
                {
                    PossiblyAddCurrent(lines, current, currentIsA);
                    continue;
                }
                // decide based on the indicator
                switch (line[6])
                {
                    case '*':
                    case '/':
                        // comment line
                        continue;
                    case ' ':
                        // executable code
                        PossiblyAddCurrent(lines, current, currentIsA);
                        var bare = CutOut(line);
                        if (bare.Length > 4 && bare[0] == ' ' && bare[1] == ' ' && bare[2] == ' ' && bare[3] == ' ')
                            currentIsA = false;
                        else
                            currentIsA = true;
                        current.Append(bare.Trim());
                        break;

                    default:
                        // continuation line
                        current.Append(CutOut(line));
                        break;
                }
            }
            PossiblyAddCurrent(lines, current, currentIsA);
            return lines;
        }

        private static void PossiblyAddCurrent(List<LineOfCode> lines, StringBuilder current, bool a)
        {
            if (current.Length == 0)
                return;
            if (a)
                lines.Add(new LineZoneA(current.ToString()));
            else
                lines.Add(new LineZoneB(current.ToString()));
            current.Clear();
        }

        private static string CutOut(string line)
        {
            if (line.Length > 72)
                line = line.Substring(0, 72);
            var v= line.Substring(7);
            Console.WriteLine($"[{v}]");
            return line.Substring(7);
        }
    }
}