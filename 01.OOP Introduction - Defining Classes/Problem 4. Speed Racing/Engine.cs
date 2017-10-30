using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_4.Speed_Racing
{
    public class Engine
    {
        private bool IsRunning = true;
        public Engine()
        {
            
        }
        public void Run()
        {
            var n = int.Parse(Console.ReadLine());
            var allCars = new Dictionary<string,Car>();
            for (int i = 0; i < n; i++)
            {
                var currentCarcharacteristics = Console.ReadLine()
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var model = currentCarcharacteristics[0];
                var fuelAmount = double.Parse(currentCarcharacteristics[1]);
                var consumptionFor1Km = double.Parse(currentCarcharacteristics[2]);
                var currentCar = new Car(model,fuelAmount,consumptionFor1Km);
                allCars.Add(model,currentCar);
            }
            while (true)
            {
                var currentDriveSession = Console.ReadLine();
                if (currentDriveSession == "End")
                {
                    break;
                    IsRunning = false;
                }
                var drivingSessionArg = currentDriveSession.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var carModel = drivingSessionArg[1];
                var amountOfKm = double.Parse(drivingSessionArg[2]);
                try
                {
                    allCars[carModel].Drive(amountOfKm);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            foreach (var currentCar in allCars)
            {
                Console.WriteLine($"{currentCar.Value.Model} {currentCar.Value.FuelAmount:F2} {currentCar.Value.DistanceTraveled}");
            }
        }
    }
}
