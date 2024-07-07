using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Bus : Vehicle
    {
        private const double FuelConsumptionIncrease = 1.4;
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {

        }
        public override double FuelConsumption 
        {
            get => IsEmpty ? base.FuelConsumption : FuelConsumptionIncrease + base.FuelConsumption;
            protected set => base.FuelConsumption = value; 
        }
        public bool IsEmpty { get; set; }
    }
}
