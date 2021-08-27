using System;

namespace Lesson10
{
    class Program
    {
        static void Main(string[] args)
        {
            //Задача 2
            Transport transport = new Transport(100, false);
            transport.speed = 40;
            transport.ShowInfo();
            Car car = new Car(270, false, true);
            car.speed = 130;
            car.ShowInfo();
            PassengerCar passengerCar = new PassengerCar(220, false, 5);
            passengerCar.speed = 80;
            passengerCar.ShowInfo();
            CargoCar cargoCar = new CargoCar(140, true, 100);
            cargoCar.speed = 60;
            cargoCar.ShowInfo();
            Train train = new Train(360, 200, 700, 15);
            train.speed = 180;
            train.ShowInfo();
            Airplane airplane = new Airplane(700, true, 2000);
            airplane.speed = 480;
            airplane.heigth = 300;
            airplane.ShowInfo();
            PassengerPlane passengerPlane = new PassengerPlane(1200, 1800, 80);
            passengerPlane.speed = 900;
            passengerPlane.heigth = 700;
            passengerPlane.ShowInfo();
            CargoPlane cargoPlane = new CargoPlane(1000, 1600, 1200);
            cargoPlane.speed = 600;
            cargoPlane.heigth = 900;
            cargoPlane.ShowInfo();
            //Трудности: Изза того что условия были слегка не конкретными , пришлось додумавать в некоторых местах
        }
    }
    public enum TypeTransport
    {
        BaseTransport,
        Car,
        PassengerCar,
        CargoCar,
        AirPlane,
        PassengerPlane,
        CargoPlane,
        Train
    }
    class Transport
    {
        public TypeTransport type { get; }
        public bool canFly { get; }
        private double _speed;
        public double speed { get => _speed; set => _speed = value > maxSpeed ? maxSpeed : value; }
        public double maxSpeed { get; }
        public Transport(double maxSpeed, bool canFly) : this(TypeTransport.BaseTransport, maxSpeed, canFly) { }
        protected Transport(TypeTransport type, double maxSpeed, bool canFly)
        {
            this.type = type;
            this.canFly = canFly;
            this.maxSpeed = maxSpeed;
            speed = 0;
        }
        public void ShowInfo()
        {
            string res = $"TypeOfTransport: {type}\tCanFly:{canFly}\tCurrentSpeed:{speed} km/h\tMaxSpeed:{maxSpeed} km/h";
            res += type switch
            {
                TypeTransport.Train when (this is Train x) => $"\nCapasity:{x.capasity} people\tLoadCapasity{x.loadCapasity} kg\tCount of Wagons:{x.countWagon}",
                TypeTransport.Car when (this is Car x) => $"\nIs Cargo:{x.isCargo}\tIs Mechanic:{x.isMechanic}",
                TypeTransport.PassengerCar when (this is PassengerCar x) => $"\nIs Cargo:{x.isCargo}\tIs Mechanic:{x.isMechanic}\tCapasity:{x.capasity} people",
                TypeTransport.CargoCar when (this is CargoCar x) => $"\nIs Cargo:{x.isCargo}\tIs Mechanic:{x.isMechanic}\tLoad Capasity:{x.loadCapasity} kg",
                TypeTransport.AirPlane when (this is Airplane x) => $"\nIs Cargo:{x.isCargo}\tCurrent Heigth:{x.heigth} m\tMax Heigth:{x.maxHeigth} m",
                TypeTransport.PassengerPlane when (this is PassengerPlane x) => $"\nIs Cargo:{x.isCargo}\tCurrent Heigth:{x.heigth} m\tMax Heigth:{x.maxHeigth} m\tCapasity:{x.capasity} people",
                TypeTransport.CargoPlane when (this is CargoPlane x) => $"\nIs Cargo:{x.isCargo}\tCurrent Heigth:{x.heigth} m\tMax Heigth:{x.maxHeigth} m\tLoad Capasity:{x.loadCapasity} kg",
                _ => ""
            };
            System.Console.WriteLine(res + "\n");
        }
    }
    class Train : Transport
    {
        public int capasity { get; }
        public double loadCapasity { get; }
        public int countWagon { get; }
        public Train(double maxSpeed, int capasity, double loadCapasity, int countWagon) : base(TypeTransport.Train, maxSpeed, false)
        {
            this.capasity = capasity;
            this.loadCapasity = loadCapasity;
            this.countWagon = countWagon;
        }
    }
    class Car : Transport
    {
        public bool isCargo { get; }
        public bool isMechanic { get; }
        public Car(double maxSpeed, bool isCargo, bool isMechanic) : this(TypeTransport.Car, maxSpeed, isCargo, isMechanic) { }
        protected Car(TypeTransport type, double maxSpeed, bool isCargo, bool isMechanic) : base(type, maxSpeed, false)
        {
            this.isCargo = isCargo;
            this.isMechanic = isMechanic;
        }
    }
    class PassengerCar : Car
    {
        public int capasity { get; }
        public PassengerCar(double maxSpeed, bool isMechanic, int capasity) : base(TypeTransport.PassengerCar, maxSpeed, false, isMechanic)
        {
            this.capasity = capasity;
        }
    }
    class CargoCar : Car
    {
        public double loadCapasity { get; }
        public CargoCar(double maxSpeed, bool isMechanic, double loadCapasity) : base(TypeTransport.CargoCar, maxSpeed, true, isMechanic)
        {
            this.loadCapasity = loadCapasity;
        }
    }
    class Airplane : Transport
    {
        public bool isCargo { get; }
        private double _heigth;
        public double heigth { get => _heigth; set => _heigth = value > maxHeigth ? maxHeigth : value; }
        public double maxHeigth { get; }
        public Airplane(double maxSpeed, bool isCargo, double maxHeigth) : this(TypeTransport.AirPlane, maxSpeed, isCargo, maxHeigth) { }
        protected Airplane(TypeTransport type, double maxSpeed, bool isCargo, double maxHeigth) : base(type, maxSpeed, true)
        {
            this.isCargo = isCargo;
            this.maxHeigth = maxHeigth;
            heigth = 0;
        }
    }
    class PassengerPlane : Airplane
    {
        public int capasity { get; }
        public PassengerPlane(double maxSpeed, double maxHeigth, int capasity) : base(TypeTransport.PassengerPlane, maxSpeed, false, maxHeigth)
        {
            this.capasity = capasity;
        }
    }
    class CargoPlane : Airplane
    {
        public double loadCapasity { get; }
        public CargoPlane(double maxSpeed, double maxHeigth, double loadCapasity) : base(TypeTransport.CargoPlane, maxSpeed, true, maxHeigth)
        {
            this.loadCapasity = loadCapasity;
        }
    }
}
