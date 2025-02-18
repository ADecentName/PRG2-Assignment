﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number    : S10268226
// Student Name      : Vuong Gia Van
// Partner Name      : Nate Ng
//==========================================================


//Done by Nate
namespace PRG2_Assignment
{
    abstract class Flight : IComparable<Flight>
    {
        public string flightNumber {  get; set; }
        public string Origin { get; set; }
        public string destination { get; set; }
        public DateTime expectedTime { get; set; }
        public string status { get; set; }
        public Flight(string flightNumber, string Origin, string destination, DateTime expectedTime, string status)
        {
            this.flightNumber = flightNumber;
            this.Origin = Origin;
            this.destination = destination;
            this.expectedTime = expectedTime;
            this.status = status;
        }
        public abstract double CalculateFees();
        public override string ToString()
        {
            return base.ToString();
        }
        public int CompareTo(Flight other)
        {
            return this.expectedTime.CompareTo(other.expectedTime);
        }
    }
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string Origin, string destination, DateTime expectedTime, string status) : base(flightNumber, Origin, destination, expectedTime, status) { }
        public override double CalculateFees()
        {
            return 0.0;
        }
        public override string ToString()
        {
            return "NORM";
        }
    }
    class LWTTFlight : Flight
    {
        public double requestFee { get; set; }
        public LWTTFlight(string flightNumber, string Origin, string destination, DateTime expectedTime, string status) : base(flightNumber, Origin, destination, expectedTime, status)
        {
        }
        public override string ToString()
        {
            return "LWTTF";
        }
        public override double CalculateFees()
        {
            return 0.0;
        }
    }
    class DDJBFlight : Flight
    {
        public double requestFee {  get; set; }
        public DDJBFlight(string flightNumber,string Origin, string destination, DateTime expectedTime, string status) : base(flightNumber, Origin, destination, expectedTime, status)
        {
            this.requestFee = requestFee;
        }
        public override double CalculateFees()
        {
            return 0.0;
        }
        public override string ToString()
        {
            return "DDJB";
        }
    }
    class CFFTFlight : Flight
    {
        public double requestFee { get; set; }
        public CFFTFlight(string flightNumber, string Origin, string destination, DateTime expectedTime, string status) : base(flightNumber, Origin, destination, expectedTime, status)
        {
            this.requestFee = requestFee;
        }
        public override double CalculateFees()
        {
            return 0.0;
        }
        public override string ToString()
        {
            return "CFFT";
        }
    }
}
