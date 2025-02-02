using PRG2_Assignment;
using System.Runtime.InteropServices;
//==========================================================
// Student Number	: s10268454
// Student Name	: Nate Ng
// Partner Name	: Vuong Gia Van
//==========================================================
//Done by Van
//Feature 1
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
//Done by Van
//Feature 1
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



//Feature 2 [ Done by Nate ] 
void LoadFlights(string fileName, Dictionary<string, Flight> flights)
{
    bool firstLine = true;
    foreach (var line in File.ReadLines(fileName))
    {
        if (firstLine)
        {
            firstLine = false;
        }
        else if (line != null)
        {
            var flightdetails = line.Split(',');
            string flightNum = flightdetails[0];
            string origin = flightdetails[1];
            string destination = flightdetails[2];
            DateTime expectedTime = DateTime.Parse(flightdetails[3]);
            string status = "On Time";
            Flight flight = null;

            if (status == null)
            {
                flight = new NORMFlight(flightNum, origin, destination, expectedTime, status);
            }
            else if (status.ToUpper() == "CFFT")
            {
                flight = new CFFTFlight(10.0, flightNum, origin, destination, expectedTime, status);
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
//Done by Van
//Feature 4
void ListGates(Dictionary<string,BoardingGate> boardingGatesDict)
{
    Console.WriteLine("=============================================\r\n\r\n");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5\r\n\r\n");
    Console.WriteLine("=============================================\r\n\r\n");
    Console.Write($"{"Gate name",-5}{"DDJB",-5}{"CFFT",-5}{"LWTT",-5}");
    foreach(KeyValuePair<string, BoardingGate> kvp in boardingGatesDict)
    {
        Console.Write($"{kvp.Key,-5}{kvp.Value.supportsDDJB,-5}{kvp.Value.supportsCFFT,-5}{kvp.Value.supportsLWTT,-5}");
    }

}

//Done by Van
//Feature 7
Airline SelectAirlines(Dictionary<string,Airline> Airlines)
{
    Console.WriteLine("=============================================\r\n\r\n");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5\r\n\r\n");
    Console.WriteLine("=============================================\r\n\r\n");
    Console.WriteLine($"{"Airline Code",-20}{"Airline Name",-10}");
    foreach (KeyValuePair<string, Airline> kvp in Airlines)
    {
        Console.WriteLine($"{kvp.Key,-20}{kvp.Value.name,-10}");

    }
    Console.Write("Enter Airline Code: ");
    string choice = Console.ReadLine();
    Airline chosen = Airlines[choice];
    return chosen;
}
//Done by Van
//Feature 7
void DisplayFlights(Airline airline)
{
    Console.WriteLine("=============================================\r\n\r\n");
    Console.WriteLine($"List of Flights for {airline}\r\n\r\n");
    Console.WriteLine("=============================================\r\n\r\n");
    Console.WriteLine("Flight Number Airline Name Origin Destination Expected Departure/Arrival Time\r\n\r\n");
    foreach (KeyValuePair<string, Flight> kvp in airline.flights)
    {
        Console.WriteLine($"{kvp.Value.flightNumber} {airline} {kvp.Value.Origin} {kvp.Value.destination} {kvp.Value.expectedTime}");
    }
}
//Done by Van
//Feature 7
void DisplayFlightDetails(Dictionary<string,Airline> Airlines)
{
    Airline airlineSelected = SelectAirlines(Airlines);
    DisplayFlights(airlineSelected);
}


//Done by Van
//Feature 8
void ModifyFlightDetails(Dictionary<string, Airline> Airlines) { }

//Feature 3 [ Done by Nate ]
void displayflights(Dictionary<string, Flight> flights, Dictionary<string, string> airlines)
{
    Console.WriteLine($"{"Flight Number",-20}{"Airline Name",-23}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival",-20}");
    foreach (var flight in flights)
    {
        string Airline = new string(flight.Key.Substring(0, 2));
        Console.WriteLine("{0,-20}{1,-23}{2,-20}{3,-20}{4,-20}", flight.Key, airlines[Airline], flight.Value.destination, flight.Value.Origin, (flight.Value.expectedTime).ToString());
    }
}
//Feature 5 [ Done by Nate ]
static string GetSpecialCodeRequest(string flightNumber, string filePath)
{
    try
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var data = line.Split(',');
            if (data[0] == flightNumber)
            {
                if (data[4] != null)
                {
                    return data[4];
                }
                else
                {
                    return "None";
                }
            }
        }
    }
    catch (Exception ex) { }
    return null;
}
void AssignBoardingGate(Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> AssignedBoardingGates)
{
    string flightNum;

    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");

    while (true)
    {
        Console.Write("Enter Flight Number: ");
        flightNum = Console.ReadLine();
        if (flights.ContainsKey(flightNum))
        {
            break;
        }
        else
        {
            Console.WriteLine($"Flight {flightNum} not found. Please try again.");
        }
    }
    BoardingGate selectedGate;
    string boardingGateName;
    while (true)
    {
        Console.Write("Enter Boarding Gate Name: ");
        boardingGateName = Console.ReadLine();
        if (AssignedBoardingGates.TryGetValue(boardingGateName, out selectedGate))
        {
            if (selectedGate.Flights == null)
            {
                selectedGate.Flights = flights[flightNum];
                Console.WriteLine($"Boarding Gate {boardingGateName} has been assigned to Flight {flightNum}.");
                break;
            }
            else
            {
                Console.WriteLine($"Boarding Gate {boardingGateName} is already assigned to another flight. Please choose a different gate.");
            }
        }
        else
        {
            Console.WriteLine($"Boarding Gate {boardingGateName} does not exist. Please try again.");
        }
    }
    Console.WriteLine($"Flight Number: {flightNum}");
    Console.WriteLine($"Origin: {flights[flightNum].Origin}");
    Console.WriteLine($"Destination: {flights[flightNum].destination}");
    Console.WriteLine($"Expected Time: {flights[flightNum].expectedTime}");
    string specialCode = GetSpecialCodeRequest(flightNum, "flights.csv");
    Console.WriteLine($"Special Request Code: {specialCode}");
    Console.WriteLine($"Boarding Gate Name: {boardingGateName}");
    Console.WriteLine($"Supports DDJB: {selectedGate.supportsDDJB}");
    Console.WriteLine($"Supports CFFT: {selectedGate.supportsCFFT}");
    Console.WriteLine($"Supports LWTT: {selectedGate.supportsLWTT}");
    Console.WriteLine("Would you like to update the status of the flight (Y/N)");
    string option = Console.ReadLine();
    if (option.ToUpper() == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.Write("Please select the new status of the flight: ");
        option = Console.ReadLine();
        if (option == "1")
        {
            flights[flightNum].status = "Delayed";
        }
        else if (option == "2")
        {
            flights[flightNum].status = "Boarding";
        }
        else if (option == "3")
        {
            flights[flightNum].status = "On Time";
        }
    }
    else
    {
        flights[flightNum].status = "On Time";
    }
    Console.WriteLine($"Flight {flightNum} has been assigned to Boarding Gate {boardingGateName}");
}
//Feature 6 [ Done by Nate ]
void createFlight(Dictionary<string,Flight>flights)
{
    string flightNum;
    string origin;
    string destination;
    DateTime expectedTime;
    string specialCode;
    string option;
    Flight flight = null;
    while (true)
    {
        while (true)
        {
            Console.Write("Enter Flight Number: ");
            flightNum = Console.ReadLine();
            if (flights.ContainsKey(flightNum))
            {
                Console.WriteLine("Flight Number already exist. Please try again.");
                continue;
            }
            else
            {
                break;
            }
        }
        Console.Write("Enter Origin: ");
        origin = Console.ReadLine();
        Console.Write("Enter Destination: ");
        destination = Console.ReadLine();
        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm) : ");
        expectedTime = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None) : ");
        specialCode = Console.ReadLine();
        if (specialCode.ToUpper() == "NONE")
        {
            flight = new NORMFlight(flightNum, origin, destination, expectedTime, null);
        }
        else if (specialCode.ToUpper() == "CFFT")
        {
            flight = new CFFTFlight(10.0, flightNum, origin, destination, expectedTime, specialCode);
        }
        else if (specialCode.ToUpper() == "LWTT")
        {
            flight = new LWTTFlight(10.0, flightNum, origin, destination, expectedTime, specialCode);
        }
        else if (specialCode.ToUpper() == "DDJB")
        {
            flight = new DDJBFlight(10.0, flightNum, origin, destination, expectedTime, specialCode);
        }
        flights[flightNum] = flight;
        Console.WriteLine($"Flight {flightNum} has been added!");
        Console.WriteLine("Would you like to add another flight? (Y/N)");
        option = Console.ReadLine();
        if (option.ToUpper() == "Y")
        {
            continue;
        }
        else
        {
            break;
        }
    }
}
//Feature 9 [ Done By Nate ]
void DisplaySortedFlights(Dictionary<string,Flight> flights, Dictionary <string, BoardingGate> AssignedBoardingGates, Dictionary<string,string> AssignedSpecialCode, Dictionary<string,string> airlines)  
{  
    
    List<Flight> flightsList = flights.Values.ToList();  
    for (int i = 0; i < flights.Count - 1; i++)  
    {  
        for (int j = 0; j < flights.Count - i - 1; j++)  
        {  
            if (flightsList[j].CompareTo(flightsList[j + 1]) > 0)   
            {  
                var temp = flightsList[j];  
                flightsList[j] = flightsList[j + 1];  
                flightsList[j + 1] = temp;  
            }  
        }  
    }
    Console.WriteLine($"{"Flight Number",-20}{"Airline Name",-23}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival",-20}{"Special Request Code",25}{"Boarding Gate",20}");
    foreach (var flight in flightsList)  
    {
        string Airline = new string(flight.flightNumber.Substring(0, 2));
        if (AssignedBoardingGates.ContainsKey(flight.flightNumber) == false && AssignedSpecialCode.ContainsKey(flight.flightNumber) == false)
        {
            Console.WriteLine($"{flight.flightNumber,-20}{airlines[Airline],-23}{flight.Origin,-20}{flight.destination,-20}{flight.expectedTime,-20}{"None",25}{"Unassigned",20}");
        }
        else if (AssignedBoardingGates.ContainsKey(flight.flightNumber) == true && AssignedSpecialCode.ContainsKey(flight.flightNumber) == false)
        {
            Console.WriteLine($"{flight.flightNumber,-20}{airlines[Airline],-23}{flight.Origin,-20}{flight.destination,-20}{flight.expectedTime,-20}{"None",25}{AssignedBoardingGates[flight.flightNumber],20}");
        }
        else if (AssignedBoardingGates.ContainsKey(flight.flightNumber) == false && AssignedSpecialCode.ContainsKey(flight.flightNumber) == true)
        {
            Console.WriteLine($"{flight.flightNumber,-20}{airlines[Airline],-23}{flight.Origin,-20}{flight.destination,-20}{flight.expectedTime,-20}{AssignedSpecialCode[flight.flightNumber],25}{"Unassigned",20}");
        }
        else if (AssignedBoardingGates.ContainsKey(flight.flightNumber) == true && AssignedSpecialCode.ContainsKey(flight.flightNumber) == true)
        {
            Console.WriteLine($"{flight.flightNumber,-20}{airlines[Airline],-23}{flight.Origin,-20}{flight.destination,-20}{flight.expectedTime,-20}{AssignedSpecialCode[flight.flightNumber],25}{AssignedBoardingGates[flight.flightNumber],20}");
        }
    }  
}  
void modifyFlights()

{
    Airline airlineSelected = SelectAirlines(Airlines);
    DisplayFlights(airlineSelected);
    Console.WriteLine("Choose an existing Flight to modify or delete:");
    string flightcode = Console.ReadLine();
    Flight flightChosen = airlineSelected.flights[flightcode];
    Console.WriteLine("1. Modify flight");
    Console.WriteLine("2. Delete flight");
    Console.WriteLine("Choose an option:");
    int opt = Convert.ToInt16(Console.ReadLine());
    while(true)
    {
        if (opt == 2)
        {
            airlineSelected.flights.Remove(flightcode);
            Console.WriteLine("Flight removed successfully");
            break;
        }
        else if (opt == 1)
        {
            Console.WriteLine("1. Modify Basic Information\r\n\r\n2. Modify Status\r\n\r\n3. Modify Special Request Code\r\n\r\n4. Modify Boarding Gate");
            Console.WriteLine("Select an option");
            opt = Convert.ToInt16(Console.ReadLine());
            break;
        }
    }
    while(true)
    {
        if(opt == 1)
        {
            Console.Write("Enter new origin: ");
            Console.Write("Enter new destination: ");
            Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            Console.WriteLine("Flight updated!");
            Console.WriteLine($"Flight number: {flightChosen.flightNumber}");
            Console.WriteLine($"Airline Name: {airlineSelected}");
            Console.WriteLine($"Origin: {flightChosen.Origin}");
            Console.WriteLine($"Destination: {flightChosen.destination}");
            Console.WriteLine($"Expected Departure/Arrival Time: {flightChosen.expectedTime}");
            Console.WriteLine($"Status: {flightChosen.status}");
            Console.WriteLine("Special request code: ");
            Console.WriteLine("Boarding Gate: ");
        }

    }

}
