using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number    : S10268226
// Student Name      : Vuong Gia Van
// Partner Name      : Nate Ng
//==========================================================


//Done by Van
namespace PRG2_Assignment
{

    internal class BoardingGate
    {
        public string gateName {  get; set; }
        public bool supportsCFFT { get; set; }
        public bool supportsDDJB { get; set; }
        public bool supportsLWTT { get; set; }
        public Flight Flights { get; set; }

        public BoardingGate(string GateName, bool DDJB, bool CFFT, bool LWTT)
        {
            gateName = GateName;
            supportsCFFT = CFFT;
            supportsDDJB = DDJB;
            supportsLWTT = LWTT;
        }


        public double CalculateFees()
        {
            return 1.0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
