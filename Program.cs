using PRG2_Assignment;
using System.Runtime.InteropServices;

string filePath = "flights.csv";

StreamReader sr = new StreamReader(filePath);
string header = sr.ReadLine();
Console.WriteLine(header);

string? line;
while ((line = sr.ReadLine()) != null)
    Console.WriteLine(line);
void LoadFlights(string fileName, Dictionary<string, Flight> flights)
{
    var lines = File.ReadLines(fileName).Skip(1);
    foreach(var line in File.ReadLines(fileName))
    {
        if (line != null)
        {
            var flightdetails = line.Split(',');
            string flightNum = flightdetails[0];
            string origin = flightdetails[1];
            string destination = flightdetails[2];
            DateTime expectedTime = DateTime.Parse(flightdetails[3]);
            string status = flightdetails[4];
            Flight flight = null;

            if (status == null)
            {
                flight = new NORMFlight(flightNum, origin, destination, expectedTime, status);
            }
            else if (status.ToUpper() == "CFFT")
            {
                flight = new CFFTFlight(10.0,flightNum, origin, destination, expectedTime, status); 
            }
            else if (status.ToUpper() == "LWTT")
            {
                flight = new LWTTFlight(10.0, flightNum, origin, destination, expectedTime, status);  
            }
            else if (status.ToUpper() == "DDJB")
            {
                flight = new DDJBFlight(10.0, flightNum, origin, destination, expectedTime, status);   
            }

            flights[flightNum] = flight;
        }
    }
}