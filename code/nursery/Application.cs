using System;

namespace nursery
{
    internal class Application
    {
        private static void Main(string[] args)
        {
            if (args == null || args.Length != 3)
            {
                Console.WriteLine("nursery is a reference software language processor for BabyCobol");
                Console.WriteLine("Run nursery with two arguments:");
                Console.WriteLine("\tthe first one to say what to do with the input");
                Console.WriteLine("\tthe second one to point to the input file");
                Console.WriteLine("Supported actions:");
                Console.WriteLine("\t-p      parse [WIP]");
                Console.WriteLine("\t-u      parse and unparse [WIP]");
                Console.WriteLine("\t-f      parse, fix and unparse [WIP]");
                Console.WriteLine("\t-c      parse and compile [WIP]");
                Console.WriteLine("\t-x      parse and execute [WIP]");
                return;
            }
            switch (args[1])
            {
                case "-p":
                    Console.WriteLine("Parsing not supported yet");
                    return;

                case "-u":
                    Console.WriteLine("Unparsing not supported yet");
                    return;

                case "-f":
                    Console.WriteLine("Fixing not supported yet");
                    return;

                case "-c":
                    Console.WriteLine("Compilation not supported yet");
                    return;

                case "-x":
                    Console.WriteLine("Execution not supported yet");
                    return;

                default:
                    Console.WriteLine("Unsupported action: " + args[1]);
                    return;
            }
        }
    }
}