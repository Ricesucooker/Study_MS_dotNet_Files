﻿using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "SalesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir,"totals.txt"),$"{salesTotal}{Environment.NewLine}");

foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}
Console.WriteLine($"Total sales is: {salesTotal}");

IEnumerable<string> FindFiles(string folderName)
{
   List<string> salesFiles = new List<string>();
   var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);


   foreach (var file in foundFiles)
   {
    var extension = Path.GetExtension(file);
     if (extension == ".json")
     {
        salesFiles.Add(file);
     }
   }
   return salesFiles;
}



double CalculateSalesTotal(IEnumerable<string> salesFile)
{
    double salesTotal =0;
    
    foreach(var file in salesFiles){
        string salesJson = File.ReadAllText(file);

        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        salesTotal += data?.Total ?? 0;
    }
    return salesTotal;


}
record SalesData (double Total);


