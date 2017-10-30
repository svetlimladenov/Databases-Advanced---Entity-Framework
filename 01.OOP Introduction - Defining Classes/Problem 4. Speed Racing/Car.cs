using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_4.Speed_Racing
{
    public class Car
    {
        private string model;
        private double fuelAmount;
        private double consumptionFor1Km;
        private double distanceTraveled;

        public Car(string model, double fuelAmount, double consumptionFor1Km)
        {
            this.model = model;
            this.fuelAmount = fuelAmount;
            this.consumptionFor1Km = consumptionFor1Km;
            this.distanceTraveled = 0;
        }

        public void Drive(double km)
        {
            var neededFuel = this.consumptionFor1Km * km;
            if (this.FuelAmount < neededFuel)
            {
                throw new ArgumentException("Insufficient fuel for the drive");
            }
            else
            {
                this.FuelAmount -= neededFuel;
                this.DistanceTraveled += km;
            }
        }
        public string Model
        {
            get { return this.model; }
            set { this.model = value; }
        }
        public double DistanceTraveled
        {
            get { return this.distanceTraveled; }
            set { this.distanceTraveled = value; }
        }

        public double ConsumptionFor1Km
        {
            get { return this.consumptionFor1Km; }
            set { this.consumptionFor1Km = value; }
        }

        public double FuelAmount
        {
            get { return this.fuelAmount; }
            set { this.fuelAmount = value; }
        }

        public string MyProperty
        {
            get { return this.model; }
            set { this.model = value; }
        }

    }
}
