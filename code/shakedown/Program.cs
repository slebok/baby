using System;
using System.Collections.Generic;

namespace shakedown
{
    class Program
    {
        private static CondensedTest _top;
        private static readonly List<CondensedTest> Plans = new List<CondensedTest>();
        static void Main(string[] args)
        {
            Console.WriteLine("shakedown v1.0.0");
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
                if (_top == null)
                    _top = new CondensedTest(arg);
                else
                    Plans.Add(new CondensedTest(arg));
            }
            if (_top == null)
                return;

            Console.WriteLine(_top.Expand());

            foreach (var plan in Plans)
            {
                Console.WriteLine(plan.Expand());
                _top.RefreshSubstitutions(plan.Substitutions);
                Console.WriteLine(_top.Expand());
            }
        }
    }
}