using System.Runtime.InteropServices;

string filePath = "flights.csv";

StreamReader sr = new StreamReader(filePath);
string header = sr.ReadLine();
Console.WriteLine(header);

string? line;
while ((line = sr.ReadLine()) != null)
    Console.WriteLine(line);