using System;
using System.Linq;
using Cars.Data;
using Cars.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new CarsDbContext();
            ResetDatabase(context);

            var cars = context
                .Cars
                .Include(c => c.Engine)
                .Include(c => c.Make)
                .Include(c => c.LicensePlate)
                .Include(c => c.CarDealerships)
                .ThenInclude(cd => cd.Dealership)
                .OrderBy(c => c.ProductionYear)
                .ToArray();

            Console.WriteLine();
        }

        private static void ResetDatabase(CarsDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            Seed(context);
        }

        private static void Seed(CarsDbContext context)
        {
            var makes = new[]
            {
                new Make {Name = "Ford"},
                new Make {Name = "Mercedes"},
                new Make {Name = "Audi"},
                new Make {Name = "BMW"},
            };
            var engines = new[]
            {
                new Engine {Capacity = 1.6, Cyllinders = 4, FuelType = FuelType.Petrol, HorsePower = 95},
                new Engine {Capacity = 3.0, Cyllinders = 8, FuelType = FuelType.Diesel, HorsePower = 350},
                new Engine {Capacity = 1.6, Cyllinders = 3, FuelType = FuelType.Gas, HorsePower = 50},

            };
            var cars = new[]
            {
                 new Car {Engine = engines[0],
                     Make = makes[0],
                     Doors = 5,
                     Model = "Fiesta",
                     ProductionYear = new DateTime(2001,1,1),
                     Transmission = Transmission.Manual
                 },
                new Car {Engine = engines[1],
                    Make = makes[1],
                    Doors = 5,
                    Model = "RS6",
                    ProductionYear = new DateTime(2015,1,1),
                    Transmission = Transmission.Automatic
                },
                new Car {Engine = engines[2],
                    Make = makes[2],
                    Doors = 2,
                    Model = "e36",
                    ProductionYear = new DateTime(2006,1,1),
                    Transmission = Transmission.Manual
                }
            };
            context.Cars.AddRange(cars);

            var dealerships = new[]
            {
                new Dealership {Name = "SoftUniAuto"},
                new Dealership {Name = "FastAndFuriousAuto"}
            };

            context.Dealerships.AddRange(dealerships);

            var carDealersShips = new[]
            {
                new CarDealership {Car = cars[0],Dealership = dealerships[0]},
                new CarDealership {Car = cars[1],Dealership = dealerships[1]},
                new CarDealership {Car = cars[2],Dealership = dealerships[0]},

            };

            context.CarDealerships.AddRange(carDealersShips);
            var licensePlates = new[]
            {
                new LicensePlate {Number = "KH5203AB"},
                new LicensePlate {Number = "KH7273AB"},
                new LicensePlate {Number = "KH1797BA"},

            };
            context.LicensePlates.AddRange(licensePlates);

            context.SaveChanges();
        }

    }
}
