﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Terminal
    {
        public string terminalName {  get; set; }
        public Dictionary<string, Airline> airlines { get; set; }
        public Dictionary<string, Flights> flights { get; set; }
        public Dictionary<string, BoardingGate> boardingGates { get; set; }
        public Dictionary<string, double> gateFees { get; set; }
        public bool AddAirline(Airline airline)
        {
            while (true)
            {

            }
        }
        public bool AddBoardingGate(BoardingGate b)
        {
            return true;
        }
        public Airline GetAirlineFromFlight(Flight)
        {
            Airline a = null;
            return a;
        }
        public void PrintAirlineFees()
        {

        }
        public override string ToString()
        {
            return terminalName;
        }
    }
}
