using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    abstract class Flight
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
        public double CalculateFees()
        {
            double fees = 0;
            return 0.0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
