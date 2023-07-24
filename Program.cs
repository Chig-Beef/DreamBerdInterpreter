namespace DreamBerdInterp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Nice pretty title, better ideas are wanted
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== DreamBerdScript ====");
            Console.ForegroundColor = ConsoleColor.White;

            bool running = true;
            string command;
            Parser parser;

            while (running)
            {
                Console.Write(">>> ");
                command = Console.ReadLine();

                if (command == "EXIT")
                {
                    running = false;
                    break;
                }

                // Should I keep the newlines around the output of a command?
                Console.WriteLine();

                Logic.clearTokens();
                parser = new Parser();
                parser.runCmd(command);

                Console.WriteLine();
            }
        }
    }
}