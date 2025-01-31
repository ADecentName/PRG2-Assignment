using PRG2_Assignment;
using System.Runtime.InteropServices;
//==========================================================
// Student Number	: s10268454
// Student Name	: Nate Ng
// Partner Name	: Vuong Gia Van
//==========================================================

Dictionary<string, Airline> LoadAirlines()
{
    string airlinesFileName = "airlines.csv";
    StreamReader sr = new StreamReader(airlinesFileName);
    string header = sr.ReadLine();
    Dictionary<string, Airline> airlinesDict = new Dictionary<string, Airline>();
    string? s;
    while ((s = sr.ReadLine()) != null)
    {
        string[] parts = s.Split(',');
        airlinesDict.Add(parts[1], new Airline(parts[0], parts[1]));
    }
    foreach(KeyValuePair<string,Airline> kvp in airlinesDict)
    {
        Console.WriteLine(kvp.Value.ToString());
    }
    return airlinesDict;
}
Dictionary<string,BoardingGate> LoadBoardingGates()
{
    string boardingGatesFileName = "boardinggates.csv";
    StreamReader sr = new StreamReader (boardingGatesFileName);
    string header = sr.ReadLine();
    Dictionary<string, BoardingGate> boardingGatesDict = new Dictionary<string, BoardingGate>();
    string? s;
    while ((s = sr.ReadLine()) != null)
    {
        string[] parts = s.Split(',');
        bool DDJB = Convert.ToBoolean(parts[1]);
        bool CFFT = Convert.ToBoolean(parts[2]);
        bool LWTT = Convert.ToBoolean(parts[3]);
        boardingGatesDict.Add(parts[0], new BoardingGate(parts[0], DDJB, CFFT, LWTT));
    }
    return boardingGatesDict;
}



//Done By Nate
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

void ListGates()
{

}