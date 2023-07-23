namespace DreamBerdInterp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== DreamBerdScript ====");
            Console.ForegroundColor = ConsoleColor.White;

            bool running = true;
            string command;
            Parser parser = new Parser();

            while (running)
            {
                Console.Write(">>> ");
                command = Console.ReadLine();

                Console.WriteLine();
                parser.runCmd(command);
                Console.WriteLine();
            }
        }
    }
}