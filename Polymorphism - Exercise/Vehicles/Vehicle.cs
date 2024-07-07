using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        protected Vehicle(double fuelQuantity, double fuelConsumption)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
        }

        public double FuelQuantity { get; protected set; }

        public virtual double FuelConsumption { get; protected set; }

        public bool Drive(double distance)
        {
            double fuelConsumed = distance * FuelConsumption;

            if(FuelQuantity < fuelConsumed)
            {
                return false;
            }

            FuelQuantity -= fuelConsumed;
            return true;
        }

        public abstract void Refuel(double liters);
    }
}
