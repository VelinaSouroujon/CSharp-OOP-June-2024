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
        private Dictionary<string, IVehicle> vehicles;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;

            vehicles = new Dictionary<string, IVehicle>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void Run()
        {
            ReadVehicles();
            ReadCommands();

            foreach (var (vehicleType, vehicle) in vehicles)
            {
                writer.WriteLine($"{vehicleType}: {vehicle.FuelQuantity.ToString("f2")}");
            }
        }
        private void ReadVehicles()
        {
            for (int i = 0; i < 3; i++)
            {
                string[] vehicleInfo = reader.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string type = vehicleInfo[0];
                double fuelQuantity = double.Parse(vehicleInfo[1]);
                double fuelConsumptionPerKm = double.Parse(vehicleInfo[2]);
                double tankCapacity = double.Parse(vehicleInfo[3]);

                switch (type.ToLower())
                {
                    case "car":
                        vehicles.Add(type, new Car(fuelQuantity, fuelConsumptionPerKm, tankCapacity));
                        break;

                    case "truck":
                        vehicles.Add(type, new Truck(fuelQuantity, fuelConsumptionPerKm, tankCapacity));
                        break;

                    case "bus":
                        vehicles.Add(type, new Bus(fuelQuantity, fuelConsumptionPerKm, tankCapacity));
                        break;

                    default:
                        throw new ArgumentException();
                }
            }
        }
        private void ReadCommands()
        {
            int n = int.Parse(reader.ReadLine());

            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] cmdArgs = reader.ReadLine()
                        .ToLower()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    string command = cmdArgs[0];
                    string vehicleType = cmdArgs[1];

                    switch (command)
                    {
                        case "drive":
                        case "driveempty":
                            double distance = double.Parse(cmdArgs[2]);

                            if (command == "driveempty")
                            {
                                Bus bus = vehicles[vehicleType] as Bus;
                                if (bus != null)
                                {
                                    bus.IsEmpty = true;
                                }
                            }

                            VehicleDrive(vehicles[vehicleType], distance);
                            break;

                        case "refuel":
                            double liters = double.Parse(cmdArgs[2]);

                            if (!vehicles[vehicleType].Refuel(liters))
                            {
                                writer.WriteLine($"Cannot fit {liters} fuel in the tank");
                            }

                            break;

                        default:
                            throw new InvalidOperationException();
                    }
                }
                catch(Exception ex)
                {
                    writer.WriteLine(ex.Message);
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
