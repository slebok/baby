using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shakedown
{
    public abstract class Line
    {
        public abstract string Expand(Dictionary<string, string> substitutions);
    }

    public class SimpleLine : Line
    {
        public readonly string Text;

        public SimpleLine(string text)
        {
            Text = text;
        }

        public override string Expand(Dictionary<string, string> substitutions)
            => substitutions
                .Keys
                .Aggregate(Text, (current, meta) => current.Replace($"${meta}$", substitutions[meta]));
    }

    public class ConditionalLine : Line
    {
        public readonly string Condition;
        public readonly List<Line> Block = new List<Line>();

        public ConditionalLine(string condition)
        {
            Condition = condition;
        }

        public void Add(Line line)
            => Block.Add(line);

        public override string Expand(Dictionary<string, string> substitutions)
        {
            if (!substitutions.ContainsKey(Condition))
                return String.Empty;
            StringBuilder result = new StringBuilder();
            foreach (var line in Block)
            {
                result.Append(line.Expand(substitutions));
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }
    }

    public class IterationLine : Line
    {
        public readonly string Iterator;
        public readonly List<Line> Block = new List<Line>();

        public IterationLine(string iterator)
        {
            Iterator = iterator;
        }

        public void Add(Line line)
            => Block.Add(line);

        public override string Expand(Dictionary<string, string> substitutions)
        {
            if (!substitutions.ContainsKey(Iterator))
                return String.Empty;
            var allValues = substitutions[Iterator].Split(";;;");
            StringBuilder result = new StringBuilder();
            foreach (var value in allValues)
            {
                if (value.Contains(":="))
                {
                    var lhs = value.Split(":=")[0].Trim();
                    var rhs = value.Split(":=")[1].Trim();
                    foreach (var line in Block)
                        result.Append(line.Expand(substitutions)
                            .Replace("$LHS$", lhs)
                            .Replace("$RHS$", rhs));
                }
                else
                    foreach (var line in Block)
                        result.Append(line.Expand(substitutions)
                            .Replace("$LINE$", value));

                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }
    }
}