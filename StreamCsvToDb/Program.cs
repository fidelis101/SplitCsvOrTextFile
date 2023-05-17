using System;

namespace StreamCsvToDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter filepath: ");


            //var filePath = args[0];
            int size = 0;
            var filePath = args[0];
            var connectionString = args[1];

            Console.WriteLine($"\nFile Path: {filePath}");

            var splitter = new Splitter();

            splitter.FilterOutFees(filePath, connectionString);
            //splitter.SplitFileByLines("./test.csv", Convert.ToInt32(size),hasHeader,appendHeaderToFiles);

            Console.ReadKey();
        }
    }
}
