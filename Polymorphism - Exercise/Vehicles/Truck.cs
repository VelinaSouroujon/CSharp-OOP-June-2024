using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double FuelConsumptionIncrease = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption)
            : base(fuelQuantity, fuelConsumption)
        {

        }
        public override double FuelConsumption 
        {
            get => base.FuelConsumption + FuelConsumptionIncrease;
            protected set => base.FuelConsumption = value;
        }

        public override void Refuel(double liters)
        {
            FuelQuantity += 0.95 * liters;
        }
    }
}
