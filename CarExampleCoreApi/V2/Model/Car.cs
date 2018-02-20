using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarExampleCoreApi.V2.Model
{
    public class Car
    {
        public int ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Horsepower { get; set; }
    }
}
