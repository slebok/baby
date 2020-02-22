using nursery.ast;
using System;
using System.Collections.Generic;

namespace nursery.meta
{
    internal static class Parser
    {
        internal static BcProgram Parse(List<LineOfCode> lines)
        {
            BcProgram prg = new BcProgram();
            if (lines == null || lines.Count == 0)
                return prg;
            int index = 0;
            // IDENTIFICATION DIVISION
            if (IsZoneAExact(lines[index], "IDENTIFICATION DIVISION."))
            {
                // parse the entire division as key-value pairs
                index++;
                int dot1, dot2;
                string key, val;
                while (index < lines.Count && IsZoneB(lines[index]))
                {
                    dot1 = lines[index].Content.IndexOf('.');
                    dot2 = lines[index].Content.LastIndexOf('.');
                    if (dot1 < 0 || (dot1 != dot2 && dot2 != lines[index].Content.Length - 1))
                        Logger.ErrorIdDivClauseBug(lines[index].Line, lines[index].Content);
                    key = lines[index].Content.Substring(0, dot1).Trim();
                    if (dot1 == dot2)
                        val = String.Empty;
                    else
                        val = lines[index].Content.Substring(dot1 + 1, dot2 - dot1 - 1).Trim();
                    if (prg.Identifications.ContainsKey(key))
                        Logger.ErrorIdDivClauseDup(lines[index].Line, lines[index].Content);
                    prg.Identifications[key] = val;
                    index++;
                }
            }
            // DATA DIVISION
            if (index < lines.Count && IsZoneAExact(lines[index], "DATA DIVISION."))
            {
                index++;
                // there is a data division
            }

            // PROCEDURE DIVISION
            if (index < lines.Count && IsZoneAExact(lines[index], "PROCEDURE DIVISION."))
            {
                index++;
                // there is a procedure division
            }
            return prg;
        }

        private static bool IsZoneAExact(LineOfCode line, string content)
        {
            LineZoneA code = line as LineZoneA;
            return code != null && code.Content == content;
        }

        private static bool IsZoneB(LineOfCode line)
        {
            LineZoneB code = line as LineZoneB;
            return code != null;
        }

        private static bool IsZoneBExact(LineOfCode line, string content)
        {
            LineZoneB code = line as LineZoneB;
            return code != null && code.Content == content;
        }
    }
}