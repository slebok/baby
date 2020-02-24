using nursery.ast;
using System;
using System.Collections.Generic;
using System.Text;

namespace nursery.meta
{
    internal static class Parser
    {
        private const string IDENTIFICATION_DIVISION = "IDENTIFICATION DIVISION.";
        private const string DATA_DIVISION = "DATA DIVISION.";
        private const string PROCEDURE_DIVISION = "PROCEDURE DIVISION.";
        private const string OCCURS = "OCCURS";
        private const string PICTURE = "PICTUREIS";
        private const string LIKE = "LIKE";
        private const string ACCEPT = "ACCEPT";

        internal static BcProgram Parse(List<LineOfCode> lines)
        {
            BcProgram prg = new BcProgram();
            if (lines == null || lines.Count == 0)
                return prg;
            int index = 0;
            // IDENTIFICATION DIVISION
            if (IsZoneAExact(lines[index], IDENTIFICATION_DIVISION))
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
            Dictionary<string, BcDataEntry> CachedData = new Dictionary<string, BcDataEntry>();
            HashSet<string> Duplicates = new HashSet<string>();

            if (index < lines.Count && IsZoneAExact(lines[index], DATA_DIVISION))
            {
                index++;
                // there is a data division
                while (index < lines.Count && !IsZoneAExact(lines[index], PROCEDURE_DIVISION))
                {
                    string val = lines[index].Content.Replace(" ", "");
                    if (val[val.Length - 1] == '.')
                        val = val.Substring(0, val.Length - 1);
                    uint lineNo = lines[index].Line;
                    int level = GetIntOrComplain(lineNo, val.Substring(0, 2), Logger.ErrorDataDivWrongLevel);
                    int occurs = 1;
                    // OCCURS
                    int occursPos = val.IndexOf(OCCURS);
                    if (occursPos >= 0)
                    {
                        occurs = GetIntOrComplain(lineNo, val.Substring(occursPos + OCCURS.Length), Logger.ErrorDataDivWrongOccurs);
                        val = val.Substring(0, occursPos);
                    }
                    // PICTURE
                    int picPos = val.LastIndexOf(PICTURE); // using last index to avoid problems where PICTURE is a (part of) the name
                    if (picPos >= 0)
                    {
                        string pat = val.Substring(picPos + PICTURE.Length);
                        BcDataEntry f = new BcDataField(level, val.Substring(2, picPos - 2), pat);
                        RememberFieldType(f, CachedData, Duplicates);
                        ConnectField(prg.Data, f, lineNo, level, occurs);
                        index++;
                        continue;
                    }
                    // LIKE
                    int lastLikePos = val.LastIndexOf(LIKE);
                    if (lastLikePos >= 0)
                    {
                        bool done = false;
                        int curLikePos = -LIKE.Length;
                        string s1, s2;
                        while (!done && curLikePos < lastLikePos)
                        {
                            curLikePos = val.IndexOf(LIKE, curLikePos + LIKE.Length);
                            s1 = val.Substring(2, curLikePos - 2);
                            s2 = val.Substring(curLikePos + LIKE.Length);
                            if (CachedData.ContainsKey(s2))
                            {
                                BcDataEntry f = CachedData[s2].Like();
                                f.Level = level;
                                f.Name = s1;
                                RememberFieldType(f, CachedData, Duplicates);
                                ConnectField(prg.Data, f, lineNo, level, occurs);
                                done = true;
                            }
                        }
                        if (done)
                        {
                            index++;
                            continue;
                        }
                    }
                    // nothing
                    var v = new BcDataView(level, val.Substring(2));
                    RememberFieldType(v, CachedData, Duplicates);
                    ConnectField(prg.Data, v, lineNo, level, occurs);
                    index++;
                }
            }

            // PROCEDURE DIVISION
            if (index < lines.Count && IsZoneAExact(lines[index], PROCEDURE_DIVISION))
            {
                index++;
                var currParaName = "";
                var currParaCode = new BcBlock();
                var currSentence = new BcSentence();
                // there is a procedure division
                while (index < lines.Count)
                {
                    var tokens = Tokenise(lines[index]);
                    for (int i = 0; i < tokens.Count; i++)
                    {
                        if (tokens[i] is EOPToken tokenP)
                        {
                            if (currSentence.Statements.Count > 0)
                            {
                                currParaCode.Sentences.Add(currSentence);
                                currSentence = new BcSentence();
                            }
                            if (currParaCode.Sentences.Count > 0)
                            {
                                prg.Paragraphs.Add(currParaName, currParaCode);
                                currParaCode = new BcBlock();
                                if (i + 2 < tokens.Count && tokens[i + 1] is UnquotedToken tokenPU && tokens[i + 2] is EOSToken)
                                {
                                    currParaName = tokenPU.Value;
                                    i += 2;
                                }
                                else
                                    currParaName = "";
                            }
                        }
                        else if (tokens[i] is EOSToken tokenS)
                        {
                            if (currSentence.Statements.Count > 0)
                            {
                                currParaCode.Sentences.Add(currSentence);
                                currSentence = new BcSentence();
                            }
                        }
                        else if (tokens[i] is QuotedToken tokenQ)
                            Logger.ErrorStrayQuoted(tokenQ);
                        else if (tokens[i] is UnquotedToken tokenU)
                        {
                            if (tokenU.Value.StartsWith(ACCEPT))
                            {
                                // TODO cover all possible arguments, now it accepts only one trivial identifier
                                var acc = new BcAccept();
                                var name = tokenU.Value.Substring(ACCEPT.Length);
                                acc.Accepted.Add(new BcIdRef(name));
                                currSentence.Statements.Add(acc);
                            }
                            else
                                Logger.ErrorNotImplementedYet(tokenU.Row, tokenU.Col, tokenU.Value);
                        }
                        else
                            Logger.ErrorUnrecognisedToken(tokens[i]);
                    }
                    index++;
                }
                // final touch
                if (currSentence.Statements.Count > 0)
                    currParaCode.Sentences.Add(currSentence);
                if (currParaCode.Sentences.Count > 0)
                    prg.Paragraphs.Add(currParaName, currParaCode);
            }
            return prg;
        }

        private enum ParserState { }

        private static void ConnectField(List<BcDataEntry> data, BcDataEntry f, uint lineno, int level, int occurs)
        {
            if (occurs != 1)
                f = new BcDataArray(f, occurs);
            if (data.Count == 0 || NotNested(level, data))
                data.Add(f);
            else
            {
                var parent = data[data.Count - 1] as BcDataView;
                if (parent == null)
                    Logger.ErrorDataDivNotAView(lineno, f.Name);
                else
                    parent.Inners.Add(f);
            }
        }

        private static bool NotNested(int level, List<BcDataEntry> fields)
        {
            if (fields == null || fields.Count == 0)
                return true;
            return level <= fields[fields.Count - 1].Level;
        }

        private static void RememberFieldType(BcDataEntry f, Dictionary<string, BcDataEntry> data, HashSet<string> dups)
        {
            if (dups.Contains(f.Name))
                return;
            if (data.ContainsKey(f.Name))
            {
                dups.Add(f.Name);
                data.Remove(f.Name);
                return;
            }
            data.Add(f.Name, f);
        }

        private static int GetIntOrComplain(uint line, string val, Action<uint, string> err)
        {
            if (Int32.TryParse(val, out int value))
                return value;
            else
                err(line, val);
            return 0;
        }

        //private static bool StartsWith(string s1, string s2)
        //{
        //}

        private static List<Token> Tokenise(LineOfCode line)
        {
            List<Token> res = new List<Token>();
            if (line is LineZoneA)
                res.Add(new EOPToken());
            string content = line.Content;
            int i = 0;
            StringBuilder sb = new StringBuilder();
            while (i < content.Length)
            {
                switch (content[i])
                {
                    case ' ':
                        i++;
                        continue;
                    case '.':
                        if (sb.Length > 0)
                        {
                            res.Add(new UnquotedToken(sb));
                            sb.Clear();
                        }
                        res.Add(new EOSToken());
                        i++;
                        continue;
                    case '"':
                        int j = content.IndexOf('"', i + 1);
                        if (j < i)
                            Logger.ErrorQuoteNotFound(line.Line, content);
                        else
                        {
                            if (sb.Length >= 0)
                                res.Add(new UnquotedToken(sb));
                            sb.Clear();
                            res.Add(new QuotedToken(content.Substring(i + 1, j - i)));
                            i = j + 1;
                        }
                        continue;
                    default:
                        sb.Append(content[i++]);
                        continue;
                }
            }
            if (sb.Length > 0)
                res.Add(new UnquotedToken(sb));

            return res;
        }

        private static char FindNonSpace(string val, ref int i)
        {
            while (val[i] == ' ' || val[i] == '\t' || val[i] == '\r' || val[i] == '\n')
                i++;
            return val[i++];
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