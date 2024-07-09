using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        private const string InvalidRefuelArgument = "Fuel must be a positive number";
        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            FuelConsumption = fuelConsumption;
            TankCapacity = tankCapacity;
            FuelQuantity = InitializeFuelQuantity(fuelQuantity);
        }

        public double FuelQuantity { get; private set; }

        public virtual double FuelConsumption { get; private set; }

        public double TankCapacity { get; private set; }

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

        public virtual bool Refuel(double liters)
        {
            if (liters <= 0)
            {
                throw new ArgumentException(InvalidRefuelArgument);
            }

            double fuelAfterRefuel = FuelQuantity + liters;

            if (TankCapacity >= fuelAfterRefuel)
            {
                FuelQuantity = fuelAfterRefuel;
                return true;
            }

            return false;
        }
        private double InitializeFuelQuantity(double fuelQuantity)
        {
            if (fuelQuantity > TankCapacity)
            {
                return 0;
            }

            return fuelQuantity;
        }
    }
}
