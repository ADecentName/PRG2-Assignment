﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10268226K
// Student Name	: Vuong Gia Van
// Partner Name	: Nate Ng
//==========================================================

//Done by Van
namespace PRG2_Assignment
{

    internal class Airline
    {
        public  string name { get; set; }
        public string code { get; set; }
        public Dictionary<string, Flight> flights {  get; set; }
        public bool AddFlight(Flight Flight)
        {
            return true;
        }
        public double CalculateFees()
        {
            return 1.0;
        }
        public bool RemoveFlight(Flight Flight)
        {
            return false;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
