using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public interface IVehicle
    {
        double FuelQuantity { get; }
        double FuelConsumption { get; }
        double TankCapacity { get; }
        bool Drive(double distance);
        bool Refuel(double liters);
    }
}
