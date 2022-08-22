// See https://aka.ms/new-console-template for more information
using Splitfiles;

Console.WriteLine("Zenith File Splitter!");

Console.WriteLine("\nPlease enter file path:");
var filePath =Console.ReadLine();
int size = 0;

Console.WriteLine("\nPlease enter file split size");
int.TryParse(Console.ReadLine(),out size);

Console.WriteLine("\nDoc Has Header (yes/no) ?");
var hasHeader = Console.ReadLine().ToUpper().Equals("YES");

Console.WriteLine("\nAppend Header in each copy (yes/no) ?");
var appendHeaderToFiles = Console.ReadLine().ToUpper().Equals("YES");


var splitter = new Form();

splitter.SplitFileByLines(filePath, Convert.ToInt32(size),hasHeader,appendHeaderToFiles);
//splitter.SplitFile3(@"C:\Users\Prime\Courses\ZIB_Autopay_Report_2022_07_02_211526.csv", Convert.ToInt32(4));