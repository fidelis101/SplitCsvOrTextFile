// See https://aka.ms/new-console-template for more information
using Splitfiles;


var filePath = args[0];
int size = 0;

int.TryParse(args[1], out size);
var hasHeader = args[2].ToUpper().Equals("YES");
var appendHeaderToFiles = args[3].ToUpper().Equals("YES");

Console.WriteLine($"\nFile Path: {filePath}");
Console.WriteLine($"\nSplitting files into {size}");
Console.WriteLine($"\nDoc Has Header: {hasHeader}");
Console.WriteLine($"\nAppend Header in each copy: {appendHeaderToFiles}");

var splitter = new Form();

splitter.SplitFileByLines(filePath, Convert.ToInt32(size),hasHeader,appendHeaderToFiles);
//splitter.SplitFile3(@"C:\Users\Prime\Courses\ZIB_Autopay_Report_2022_07_02_211526.csv", Convert.ToInt32(4));