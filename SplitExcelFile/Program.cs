﻿using Splitfiles;

Console.WriteLine("Please enter filepath: ");
var filePath = Console.ReadLine();


//var filePath = args[0];
int size = 0;

int.TryParse(args[1], out size);
var hasHeader = args[2].ToUpper().Equals("YES");
var appendHeaderToFiles = args[3].ToUpper().Equals("YES");

Console.WriteLine($"\nFile Path: {filePath}");
Console.WriteLine($"\nSplitting files into {size}");
Console.WriteLine($"\nDoc Has Header: {hasHeader}");
Console.WriteLine($"\nAppend Header in each copy: {appendHeaderToFiles}");

var splitter = new Splitter();

splitter.FilterOutFees(filePath,"");
//splitter.SplitFileByLines("./test.csv", Convert.ToInt32(size),hasHeader,appendHeaderToFiles);

Console.ReadKey();