using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public abstract class Vehicle
    {
        public Vehicle(int horsePower, double fuel)
        {
            HorsePower = horsePower;
            Fuel = fuel;
        }
        protected virtual double DefaultFuelConsumption => 1.25;
        public double FuelConsumption => DefaultFuelConsumption;
        public double Fuel {  get; private set; }
        public int HorsePower { get; private set; }
        public void Drive(double kilometers)
        {
            double consumedFuel = FuelConsumption * kilometers;

            if(Fuel >= consumedFuel)
            {
                Fuel -= consumedFuel;
            }
        }
    }
}
