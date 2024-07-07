using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Car : Vehicle
    {
        private double FuelConsumptionIncrease = 0.9;
        public Car(double fuelQuantity, double fuelConsumption)
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
            FuelQuantity += liters;
        }
    }
}
