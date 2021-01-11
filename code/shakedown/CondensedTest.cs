using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace shakedown
{
    public class CondensedTest
    {
        private string _extension;
        private string _expected;
        private string[] _lines;
        private int _pos;
        private string _line => _lines[_pos];
        private int _max => _lines.Length;
        private List<Line> TryBlock = new List<Line>();

        public readonly Dictionary<string, string> Substitutions = new Dictionary<string, string>();

        public CondensedTest(string filename)
        {
            RefreshSubstitutions();
            FromFile(filename);
        }

        public void RefreshSubstitutions(Dictionary<string, string> _external = null)
        {
            Substitutions.Clear();
            Substitutions.Add("YEAR", DateTime.Today.Year.ToString());
            Substitutions.Add("MONTH", DateTime.Today.Month.ToString("D2"));
            Substitutions.Add("DAY", DateTime.Today.Day.ToString("D2"));
            if (_external == null || _external.Count == 0)
                return;
            foreach (var kvp in _external)
                Substitutions[kvp.Key] = kvp.Value;
        }

        private void ReportSubstitutions()
        {
            Console.WriteLine(
                $"[?] Subst: ({String.Join("; ", Substitutions.Select(kvp => $"{kvp.Key} -> {kvp.Value}"))})");
        }

        public string Expand()
        {
            ReportSubstitutions();
            
            StringBuilder result = new StringBuilder();
            foreach (var line in TryBlock)
            {
                string text = line.Expand(Substitutions);
                if (String.IsNullOrEmpty(text))
                    continue;
                result.Append(line.Expand(Substitutions));
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }
        
        private void FromFile(string name)
        {
            _lines = File.ReadAllLines(name);
            _pos = -1;
            while (_pos < _max) 
                ParseTop();
        }

        private void ReportParseError()
        {
            Console.WriteLine($"[!] Parse error on line {_pos}:");
            if (_pos < _max) Console.WriteLine(_line);
            throw new Exception("Abend!");
        }
        
        private void ReportParseProgress(string message)
        {
            Console.WriteLine($"[?] Parse progress on line {_pos}: {message}");
        }

        private void SkipEmptyLines()
        {
            while (_pos < _max && String.IsNullOrEmpty(_line))
                _pos++;
        }

        private void ParseTop()
        {
            _pos++;
            SkipEmptyLines();
            if (_pos >= _max) 
                return;
            if (_line[0] == '#')
            {
                if (_line.StartsWith("#ext"))
                    _extension = _line.Substring("#ext".Length).Trim();
                else if (_line.StartsWith("#get"))
                    _expected = _line.Substring("#get".Length).Trim();
                else if (_line.StartsWith("#try"))
                    ParseTry();
                else if (_line.StartsWith("#use"))
                    ParseUse();
                else if (_line.StartsWith("#def"))
                    ParseDef();
                else if (_line.StartsWith("#run"))
                    ParseRun();
                else
                    ReportParseError();
            }
            else
                ReportParseError();
        }

        private void ParseTry()
        {
            _pos++;
            SkipEmptyLines();
            while (_pos < _max)
            {
                if (_line[0] == '#')
                {
                    if (_line.StartsWith("#if"))
                        TryBlock.Add(ParseIf());
                    else if (_line.StartsWith("#for"))
                        TryBlock.Add(ParseFor());
                    else if (_line.StartsWith("#end"))
                        return;
                    else
                        ReportParseError();
                }
                else
                    TryBlock.Add(new SimpleLine(_line));

                _pos++;
            }
        }

        private Line ParseFor()
        {
            var forLine = new IterationLine(_line.Substring("#for ".Length));
            _pos++;
            SkipEmptyLines();
            while (_pos < _max)
            {
                if (_line[0] == '#')
                {
                    if (_line.StartsWith("#if"))
                        forLine.Add(ParseIf());
                    else if (_line.StartsWith("#for"))
                        forLine.Add(ParseFor());
                    else if (_line.StartsWith("#end"))
                        return forLine;
                    else
                        ReportParseError();
                }
                else
                    forLine.Add(new SimpleLine(_line));

                _pos++;
            }

            return forLine;
        }

        private void ParseUse()
        {
            ReportParseProgress("use!");
            var text = _line.Substring("#use ".Length).Trim();
            if (Substitutions.ContainsKey("use"))
                Substitutions["use"] += ";;;" + text;
            else
                Substitutions["use"] = text;
            ReportSubstitutions();
        }
        
        private void ParseDef()
        {
            ReportParseProgress("def!");
            var text = _line.Substring("#def ".Length).Trim();
            if (Substitutions.ContainsKey("def"))
                Substitutions["def"] += ";;;" + text;
            else
                Substitutions["def"] = text;
            ReportSubstitutions();
        }
        
        private void ParseRun()
        {
            ReportParseProgress("run!");
            var text = _line.Substring("#run ".Length).Trim();
            if (Substitutions.ContainsKey("run"))
                Substitutions["run"] += ";;;" + text;
            else
                Substitutions["run"] = text;
            ReportSubstitutions();
        }

        private Line ParseIf()
        {
            var ifLine = new ConditionalLine(_line.Substring("#if ".Length));
            _pos++;
            SkipEmptyLines();
            while (_pos < _max)
            {
                if (_line[0] == '#')
                {
                    if (_line.StartsWith("#if"))
                        ifLine.Add(ParseIf());
                    else if (_line.StartsWith("#for"))
                        ifLine.Add(ParseFor());
                    else if (_line.StartsWith("#end"))
                        return ifLine;
                    else
                        ReportParseError();
                }
                else
                    ifLine.Add(new SimpleLine(_line));

                _pos++;
            }

            return ifLine;
        }
    }
}