using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deltas
{
    class InstrumentSample
    {
        //public int id;
        public Dictionary<int, Dictionary<double, double>> samples;

        public InstrumentSample()
        {
            Dictionary<double, double> deltas = new Dictionary<double,double>() 
            {
                {0, .8},
                {-.9, -.3}
            };

            samples = new Dictionary<int, Dictionary<double, double>>();
            samples.Add(654, deltas);
            samples.Add(444, deltas);
        }
    }
}
