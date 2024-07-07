using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            IVehicle car = GetVehicle();
            IVehicle truck = GetVehicle();
            ReadCommands(car, truck);

            writer.WriteLine($"Car: {car.FuelQuantity.ToString("f2")}");
            writer.WriteLine($"Truck: {truck.FuelQuantity.ToString("f2")}");
        }
        private IVehicle GetVehicle()
        {
            string[] vehicleInfo = reader.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string type = vehicleInfo[0].ToLower();
            double fuelQuantity = double.Parse(vehicleInfo[1]);
            double fuelConsumptionPerKm = double.Parse(vehicleInfo[2]);

            if(type == "car")
            {
                return new Car(fuelQuantity, fuelConsumptionPerKm);
            }
            if(type == "truck")
            {
                return new Truck(fuelQuantity, fuelConsumptionPerKm);
            }

            throw new ArgumentException();
        }
        private void ReadCommands(IVehicle car, IVehicle truck)
        {
            int n = int.Parse(reader.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] cmdArgs = reader.ReadLine()
                    .ToLower()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string command = cmdArgs[0];
                string vehicleType = cmdArgs[1];

                switch(command)
                {
                    case "drive":
                        double distance = double.Parse(cmdArgs[2]);
                        if (vehicleType == "car")
                        {
                            VehicleDrive(car, distance);
                        }
                        else if(vehicleType == "truck")
                        {
                            VehicleDrive(truck, distance);
                        }
                        break;

                    case "refuel":
                        double liters = double.Parse(cmdArgs[2]);
                        if(vehicleType == "car")
                        {
                            car.Refuel(liters);
                        }
                        else if (vehicleType == "truck")
                        {
                            truck.Refuel(liters);
                        }
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }
        }
        private void VehicleDrive(IVehicle vehicle, double distance)
        {
            string vehicleType = vehicle.GetType().Name;

            if (vehicle.Drive(distance))
            {
                writer.WriteLine($"{vehicleType} travelled {distance} km");
            }
            else
            {
                writer.WriteLine($"{vehicleType} needs refueling");
            }
        }
    }
}
