using PRG2_Assignment;
using System;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

//==========================================================
// Student Number    : s10268454
// Student Name      : Nate Ng
// Partner Number    : S10268226K
// Partner Name      : Vuong Gia Van
//==========================================================



// Done by Van
// Feature 1

void LoadAirlines(string airlinesFileName, Dictionary<string, Airline> airlinesDict)
{
    Console.WriteLine("Loading Airlines...");
    StreamReader sr = new StreamReader(airlinesFileName);
    string header = sr.ReadLine();
    string? s;
    while ((s = sr.ReadLine()) != null)
    {
        string[] parts = s.Split(',');
        airlinesDict.Add(parts[1], new Airline(parts[0], parts[1]));
    }
    Console.WriteLine($"{airlinesDict.Count} airlines loaded.");
}

// Done by Van
// Feature 1
void LoadBoardingGates(string boardingGatesFileName, Dictionary<string, BoardingGate> boardingGatesDict)
{
    Console.WriteLine("Loading boarding gates...");
    StreamReader sr = new StreamReader(boardingGatesFileName);
    string header = sr.ReadLine();
    string? s;
    while ((s = sr.ReadLine()) != null)
    {
        string[] parts = s.Split(',');
        bool DDJB = Convert.ToBoolean(parts[1]);
        bool CFFT = Convert.ToBoolean(parts[2]);
        bool LWTT = Convert.ToBoolean(parts[3]);
        boardingGatesDict.Add(parts[0], new BoardingGate(parts[0], DDJB, CFFT, LWTT));
    }
    Console.WriteLine($"{boardingGatesDict.Count} boarding gates loaded.");
}
//Done by Van
void AssignFlightsToAirline(Dictionary<string, Flight> flightsDict, Dictionary<string,Airline> airlinesDict)
{
    foreach(KeyValuePair<string, Flight> flights in flightsDict)
    {
        string airlineCode = flights.Value.flightNumber.Substring(0, 2);
        Airline airlineChosen = airlinesDict[airlineCode];
        airlineChosen.flights.Add(flights.Value.flightNumber, flights.Value);
    }
}

string fileName = "flights.csv";
// Feature 2 [ Done by Nate ]
void LoadFlights(string fileName, Dictionary<string, Flight> flights, Dictionary<string,string> AssignedSpecialCode)
{
    Console.WriteLine("Loading flights...");
    bool firstLine = true;
    foreach (var line in File.ReadLines(fileName))
    {
        if (firstLine)
        {
            firstLine = false;
        }
        else
        {
            var flightdetails = line.Split(',');
            string flightNum = flightdetails[0];
            string origin = flightdetails[1];
            string destination = flightdetails[2];
            DateTime expectedTime = DateTime.Parse(flightdetails[3]);
            string status = "On Time";
            string specialCodeRequest = flightdetails[4];
            Flight flight = null;
            if (specialCodeRequest.ToUpper() == "CFFT")
            {
                flight = new CFFTFlight(flightNum, origin, destination, expectedTime, status);
                AssignedSpecialCode[flightNum] = specialCodeRequest;
            }
            else if (specialCodeRequest.ToUpper() == "LWTT")
            {
                flight = new LWTTFlight(flightNum, origin, destination, expectedTime, status);
                AssignedSpecialCode[flightNum] = specialCodeRequest;
            }
            else if (specialCodeRequest.ToUpper() == "DDJB")
            {
                flight = new DDJBFlight(flightNum, origin, destination, expectedTime, status);
                AssignedSpecialCode[flightNum] = specialCodeRequest;
            }
            else
            {
                flight = new NORMFlight(flightNum, origin, destination, expectedTime, status);
            }
            flights.Add(flightNum, flight);
        }
    }
    Console.WriteLine($"{flights.Count} flights added.");
}

// Done by Van
int InputParsing()
{
    while (true)
    {
        try
        {
            Console.WriteLine("Choose an option: ");
            int choice = Convert.ToInt16(Console.ReadLine());
            return choice;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid option.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Please enter a valid option.");
        }
    }
}
//Done by Van
string StringValidation(string input)
{
    while (true)
    {
        if (!string.IsNullOrEmpty(input))
        {
            return input;
        }
        else
        {
            Console.WriteLine("Input cannot be empty. Please enter a valid input:");
            input = Console.ReadLine();
        }
    }
}


// Done by Van
// Feature 4
void ListGates(Dictionary<string, BoardingGate> boardingGatesDict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.Write($"{"Gate name",-15}{"DDJB",-15}{"CFFT",-15}{"LWTT",-15}\n");
    foreach (KeyValuePair<string, BoardingGate> kvp in boardingGatesDict)
    {
        Console.WriteLine($"{kvp.Key,-15}{kvp.Value.supportsDDJB,-15}{kvp.Value.supportsCFFT,-15}{kvp.Value.supportsLWTT,-15}");
    }
}


// Done by Van
// Feature 7
Airline SelectAirlines(Dictionary<string, Airline> Airlines)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-20}{"Airline Name",-10}");
    foreach (KeyValuePair<string, Airline> kvp in Airlines)
    {
        Console.WriteLine($"{kvp.Key,-20}{kvp.Value.name,-10}");
    }

    Airline airlineChosen = null;
    while (airlineChosen == null)
    {
        Console.Write("Enter Airline Code: ");
        string choice = Console.ReadLine();
        try
        {
            airlineChosen = Airlines[choice];
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("Airline not found, please try again.");
        }
    }
    return airlineChosen;
}


// Done by Van
// Feature 7
void DisplayAllFlights(Dictionary<string,Airline> airlinesDict)
{
    Console.WriteLine($"{"Flight Number",-10} {"Airline Name",-20} {"Origin",-25} {"Destination",-25} {"Expected/Departure Arrival Time",-20}");
    foreach (KeyValuePair<string, Airline> kvp in airlinesDict)
    {
        foreach(KeyValuePair<string, Flight> flightNumberpair in kvp.Value.flights)
        {
            Console.WriteLine($"{flightNumberpair.Value.flightNumber,-13} {kvp.Value.name,-20} {flightNumberpair.Value.Origin,-25} {flightNumberpair.Value.destination,-25} {flightNumberpair.Value.expectedTime,-20}");

        }

    }
}
//Done by Van
void DisplayFlightsByAirline(Airline airline)
{
    Console.WriteLine("=============================================");
    Console.WriteLine($"List of Flights for {airline}");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Number Airline Name Origin Destination Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Flight> kvp in airline.flights)
    {
        Console.WriteLine($"{kvp.Value.flightNumber} {airline} {kvp.Value.Origin} {kvp.Value.destination} {kvp.Value.expectedTime}");
    }
}

// Done by Van
// Feature 7
void DisplayFlightDetails(Dictionary<string, Airline> Airlines)
{
    Airline airlineSelected = SelectAirlines(Airlines);
    if (airlineSelected != null )
    {
        DisplayFlightsByAirline(airlineSelected);
    }
    else
    {
        return;
    }
}

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
//Done by Nate
void AssignBoardingGate(
    Dictionary<string, Flight> flights,
    Dictionary<string, BoardingGate> AssignedBoardingGates,
    Dictionary<string, BoardingGate> boardingGatesDict,
    Dictionary<string, string> AssignedSpecialCode)  // Special request codes
{
    string flightNum;
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");

    // Validate Flight Number
    while (true)
    {
        Console.Write("Enter Flight Number: ");
        flightNum = Console.ReadLine();
        if (flights.ContainsKey(flightNum))
            break;
        Console.WriteLine($"Flight {flightNum} not found. Please try again.");
    }

    // Get the special request code of the flight
    string specialCode = AssignedSpecialCode.ContainsKey(flightNum) ? AssignedSpecialCode[flightNum] : "None";

    // Validate Boarding Gate
    BoardingGate selectedGate;
    string boardingGateName;
    while (true)
    {
        Console.Write("Enter Boarding Gate Name: ");
        boardingGateName = Console.ReadLine();

        if (boardingGatesDict.TryGetValue(boardingGateName, out selectedGate))  // Check all available gates
        {
            if (!AssignedBoardingGates.ContainsKey(boardingGateName))  // Ensure gate is not already assigned
            {
                // Check if the gate supports the flight's special request
                if (specialCode == "None" ||
                    (specialCode == "DDJB" && selectedGate.supportsDDJB) ||
                    (specialCode == "CFFT" && selectedGate.supportsCFFT) ||
                    (specialCode == "LWTT" && selectedGate.supportsLWTT))
                {
                    selectedGate.Flights = flights[flightNum];
                    AssignedBoardingGates[boardingGateName] = selectedGate; // Assign the gate
                    Console.WriteLine($"Boarding Gate {boardingGateName} has been assigned to Flight {flightNum}.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Boarding Gate {boardingGateName} does not support the special request '{specialCode}'. Please choose another gate.");
                }
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

    // Display Flight Details
    Flight assignedFlight = flights[flightNum];
    Console.WriteLine($"Flight Number: {flightNum}");
    Console.WriteLine($"Origin: {assignedFlight.Origin}");
    Console.WriteLine($"Destination: {assignedFlight.destination}");
    Console.WriteLine($"Expected Time: {assignedFlight.expectedTime}");
    Console.WriteLine($"Special Request Code: {specialCode}");
    Console.WriteLine($"Boarding Gate Name: {boardingGateName}");
    Console.WriteLine($"Supports DDJB: {selectedGate.supportsDDJB}");
    Console.WriteLine($"Supports CFFT: {selectedGate.supportsCFFT}");
    Console.WriteLine($"Supports LWTT: {selectedGate.supportsLWTT}");

    // Update Flight Status
    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
    string option = StringValidation(Console.ReadLine());

    if (option.ToUpper() == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.Write("Please select the new status of the flight: ");
        option = Console.ReadLine();

        switch (option)
        {
            case "1":
                assignedFlight.status = "Delayed";
                break;
            case "2":
                assignedFlight.status = "Boarding";
                break;
            case "3":
                assignedFlight.status = "On Time";
                break;
            default:
                Console.WriteLine("Invalid input. Status remains unchanged.");
                break;
        }
    }

    Console.WriteLine($"Flight {flightNum} has been successfully assigned to Boarding Gate {boardingGateName}.");
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
            flight = new CFFTFlight(flightNum, origin, destination, expectedTime, specialCode);
        }
        else if (specialCode.ToUpper() == "LWTT")
        {
            flight = new LWTTFlight(flightNum, origin, destination, expectedTime, specialCode);
        }
        else if (specialCode.ToUpper() == "DDJB")
        {
            flight = new DDJBFlight(flightNum, origin, destination, expectedTime, specialCode);
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
void DisplaySortedFlights(
    Dictionary<string, Flight> flights,
    Dictionary<string, BoardingGate> assignedBoardingGates,
    Dictionary<string, string> assignedSpecialCode,
    Dictionary<string, Airline> airlines)
{
    List<Flight> flightsList = flights.Values.ToList();
    flightsList.Sort();
    Console.WriteLine($"{"Flight Number",-20}{"Airline Name",-23}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival",-30}{"Boarding Gate",-20}");
    foreach (var flight in flightsList)
    {
        string airlineCode = flight.flightNumber.Substring(0, 2);
        string airlineName = airlines.ContainsKey(airlineCode) ? airlines[airlineCode].name : "Unknown";
        string boardingGate = assignedBoardingGates.ContainsKey(flight.flightNumber) ? assignedBoardingGates[flight.flightNumber].gateName : "Unassigned";
        string specialCode = assignedSpecialCode.ContainsKey(flight.flightNumber) ? assignedSpecialCode[flight.flightNumber] : "None";
        Console.WriteLine($"{flight.flightNumber,-20}{airlineName,-23}{flight.Origin,-20}{flight.destination,-20}{flight.expectedTime,-30}{boardingGate,-20}");
        Console.WriteLine($"{"Special Request Code:",-30}{specialCode}\n");
    }
}


//Feature 8
//Done by Van
//Handles modifying flight information for DisplayModifyOptions() to improve code readability
void ModifyFlightDetails(Flight flightChosen, Airline airlineChosen)
{
    Console.WriteLine("1. Modify Basic Information\r\n\r\n2. Modify Status\r\n\r\n3. Modify Special Request Code\r\n\r\n4. Modify Boarding Gate");
    int opt = InputParsing();

    if (opt == 1)
    {
        Console.Write("Enter new origin: ");
        flightChosen.Origin = StringValidation(Console.ReadLine());
        Console.Write("Enter new destination: ");
        flightChosen.destination = StringValidation(Console.ReadLine());
        Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
        while (true)
        {
            string input = Console.ReadLine();
            try
            {
                DateTime expectedDateTime = DateTime.ParseExact(input, "dd/MM/yyyy HH:mm", null);
                Console.WriteLine("Valid date and time entered: " + expectedDateTime.ToString());
                flightChosen.expectedTime = expectedDateTime;
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date and time format. Please enter in the format 'dd/MM/yyyy HH:mm'.");
            }
        }
        Console.WriteLine("Flight updated");
        Console.WriteLine($"Flight number: {flightChosen.flightNumber}");
        Console.WriteLine($"Airline name: {airlineChosen.name}");
        Console.Write($"Origin: {flightChosen.Origin}");
        Console.WriteLine($"Destination: {flightChosen.destination}");
        Console.WriteLine($"Expected arrival time: {flightChosen.expectedTime}");
        Console.WriteLine($"Status: {flightChosen.status}");
    }
    else if (opt == 2)
    {
        Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
        string choice = Console.ReadLine().ToUpper();
        if (choice == "Y")
        {
            Console.WriteLine("1. Delayed\r\n\r\n2. Boarding\r\n\r\n3. On Time");
            Console.WriteLine("Please select the new status of the flight:\r\n\r\n");
            int newStatus = InputParsing();
            while (true)
            {
                if (newStatus == 1)
                {
                    flightChosen.status = "Delayed";
                    Console.WriteLine("Flight status updated.");
                    return;
                }
                else if (newStatus == 2)
                {
                    flightChosen.status = "Boarding";
                    Console.WriteLine("Flight status updated.");

                    return;
                }
                else if (newStatus == 3)
                {
                    flightChosen.status = ("On Time");
                    Console.WriteLine("Flight status updated.");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
            }

        }
        else if (choice == "N")
        {
            Console.WriteLine("Operation cancelled");
            return;
        }

    }
    else if (opt == 3)
    {
        Console.WriteLine("Enter new special request code: (CFFT/DDJB/LWTT/NORM: ");
        string specialReqCode = StringValidation(Console.ReadLine()).ToUpper();
        if (specialReqCode == "CFFT")
        {
            flightChosen = ConvertToCFFT(flightChosen);
            return;
        }
        else if (specialReqCode == "DDJB")
        {
            flightChosen = ConvertToDDJB(flightChosen);
            return;
        }
        else if (specialReqCode == "LWTT")
        {
            flightChosen = ConvertToLWTTF(flightChosen);
            return;
        }
        else if (specialReqCode == "NORM")
        {
            flightChosen = ConvertToNORM(flightChosen);
            return;
        }
        else
        {
            Console.WriteLine("Invalid special request code");
            return;
        }
    }
    else if (opt == 4)
    {
        return;
    }
}
//Feature 8
//Done by Van
void DisplayModifyOptions(Dictionary<string,Airline> airlinesDict)
{
    Airline airlineSelected = SelectAirlines(airlinesDict);
    DisplayFlightsByAirline(airlineSelected);
    Console.WriteLine("Choose an existing Flight to modify or delete:");
    string flightcode = Console.ReadLine();
    Flight flightChosen = airlineSelected.flights[flightcode];
    Console.WriteLine("1. Modify flight");
    Console.WriteLine("2. Delete flight");
    int opt = InputParsing();
    while (true)
    {
        if (opt == 2)
        {
            Console.Write("Are you sure you would like to delete this flight? (Y/N)");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "Y")
            {
                airlineSelected.flights.Remove(flightcode);
                Console.WriteLine("Flight removed successfully");
                return;
            }
            else if ((choice == "N"))
            {
                Console.WriteLine("Operation cancelled.");
                return;
            }

        }
        else if (opt == 1)
        {
            ModifyFlightDetails(flightChosen, airlineSelected);
            return;
        }
        else
        {
            Console.WriteLine("Invalid option, please try again.");
            return;
        }

    }
}

//Done by Van
NORMFlight ConvertToNORM(Flight flight)
{
    NORMFlight newFlight = new NORMFlight(flight.flightNumber, flight.Origin, flight.destination, flight.expectedTime, flight.status);
    return newFlight;
}
//Done by Van
CFFTFlight ConvertToCFFT(Flight flight)
{
    CFFTFlight newFlight = new CFFTFlight(flight.flightNumber, flight.Origin, flight.destination, flight.expectedTime, flight.status);
    return newFlight;
}
//Done by Van

LWTTFlight ConvertToLWTTF(Flight flight)
{
    LWTTFlight newFlight = new LWTTFlight(flight.flightNumber, flight.Origin, flight.destination, flight.expectedTime, flight.status);
    return newFlight;
}
//Done by Van
DDJBFlight ConvertToDDJB(Flight flight)
{
    DDJBFlight newFlight = new DDJBFlight(flight.flightNumber, flight.Origin, flight.destination, flight.expectedTime, flight.status);
    return newFlight;
}
//Done by Van
void DisplayMenu()
{
    Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================");
    Console.WriteLine("1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n8. Display Airline Fees\r\n0. Exit");
    Console.WriteLine();
}

//Advanced Features
//Part (a) [ Done By Nate ]

    //Done by Van
    //Part (b)
    void CalculateTotalFee(Dictionary<string, Airline> airlinesDict)
{
    foreach (KeyValuePair<string, Airline> keyAirlinesPair in airlinesDict)
    {
        foreach (KeyValuePair<string, Flight> numFlightPair in keyAirlinesPair.Value.flights)
        {
            if (numFlightPair.Value.Origin.EndsWith("(SIN)"))
            {
                keyAirlinesPair.Value.fees += 800;
                keyAirlinesPair.Value.fees += 300; //Base boarding fee

            }
            else if (numFlightPair.Value.destination.EndsWith("(SIN)"))
            {
                keyAirlinesPair.Value.fees += 500;
                keyAirlinesPair.Value.fees += 300; //Base boarding fee

            }
            if (keyAirlinesPair.Value.flights.Count > 3)
            {
                keyAirlinesPair.Value.discounts += Math.Floor(keyAirlinesPair.Value.flights.Count / 3.0) * 350;
            }
            if (numFlightPair.Value is DDJBFlight)
            {
                keyAirlinesPair.Value.fees += 300;
            }
            else if (numFlightPair.Value is CFFTFlight)
            {
                keyAirlinesPair.Value.fees += 150;
            }
            else if (numFlightPair.Value is LWTTFlight)
            {
                keyAirlinesPair.Value.fees += 500;
            }
            else
            {
                keyAirlinesPair.Value.discounts += 50;
            }


        }
        double eachMoreThan5 = Math.Floor(keyAirlinesPair.Value.flights.Count / 5.0);
        keyAirlinesPair.Value.discounts += keyAirlinesPair.Value.fees * 0.03 * eachMoreThan5;
    }
}
//Function to check if time is before 11AM or after 9PM
bool IsBefore11AMOrAfter9PM(DateTime dateTime)
{
    TimeSpan elevenAM = new TimeSpan(11, 0, 0);
    TimeSpan ninePM = new TimeSpan(21, 0, 0);
    return dateTime.TimeOfDay < elevenAM || dateTime.TimeOfDay > ninePM;
}

//Function to display airline fees
void DisplayFees(Dictionary<string,Airline> airlinesDict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine();
    Console.WriteLine($"{"Airline",-25}{"Fees",-15}{"Discount",-15}{"Subtotal",-15}");
    foreach(KeyValuePair<string,Airline> keyAirlinePair in airlinesDict)
    {
        Console.WriteLine($"{keyAirlinePair.Value.name,-25}{keyAirlinePair.Value.fees,-15}{keyAirlinePair.Value.discounts,-15}{keyAirlinePair.Value.CalculateSubtotal(),-15}");
    }
}
void Main()
{
    string airlinesFileName = "airlines.csv";
    string boardingGatesFileName = "boardinggates.csv";
    Dictionary<string, Airline> airlinesDict = new Dictionary<string, Airline>();
    Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
    Dictionary<string, Flight> flightsDict = new Dictionary<string, Flight>();
    Dictionary<string,BoardingGate> assignedBoardingGates = new Dictionary<string, BoardingGate>();
    Dictionary<string, string> assignedSpecialCodes = new Dictionary<string, string>();
    LoadAirlines(airlinesFileName, airlinesDict);
    LoadBoardingGates(boardingGatesFileName, boardingGateDict);
    LoadFlights(fileName, flightsDict, assignedSpecialCodes);
    AssignFlightsToAirline(flightsDict, airlinesDict);
    CalculateTotalFee(airlinesDict);

    while(true)
    {
        DisplayMenu();
        int opt = InputParsing();
        if (opt == 1)
        {
            DisplayAllFlights(airlinesDict);
        }
        else if (opt == 2)
        {
            ListGates(boardingGateDict);
        }
        else if (opt == 3)
        {
            AssignBoardingGate(flightsDict, assignedBoardingGates,boardingGateDict, assignedSpecialCodes);
        }
        else if (opt == 4)
        {
            createFlight(flightsDict);
        }
        else if (opt == 5)
        {
            DisplayFlightDetails(airlinesDict);
        }
        else if (opt == 6)
        {
            DisplayModifyOptions(airlinesDict);
        }
        else if (opt == 7)
        {
            DisplaySortedFlights(flightsDict, assignedBoardingGates, assignedSpecialCodes, airlinesDict);
        }
        else if (opt == 8)
        {
            DisplayFees(airlinesDict);
        }
        else if (opt == 0)
        {
            Console.WriteLine("Goodbye!");
            break;
        }
        else
        {
            Console.WriteLine("Invalid option, please try again");
            continue;
        }
    }

}

Main();