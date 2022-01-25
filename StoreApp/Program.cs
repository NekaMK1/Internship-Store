using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }
    class Product{
        public string Name { get; set; }
        public string Brand { get; set; }
        public float Price { get; set; }
        public Product(string name,string brand,float price)
        {
            Name = name;
            Brand = brand;
            Price = price;
        }
    }
    class Perishables : Product
    {
        public DateTime ExpirationDate { get; set; }
        public Perishables(string name, string brand, float price, DateTime expirationDate) : base(name, brand, price)
        {
            ExpirationDate = expirationDate;
        }
    }
    class Food : Perishables
    {
        public Food(string name, string brand, float price, DateTime expirationDate) : base(name, brand, price, expirationDate) { }
    }
    class Beverage : Perishables
    {
        public Beverage(string name, string brand, float price, DateTime expirationDate) : base(name, brand, price, expirationDate) { }
    }
    class Clothes : Product
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public Clothes(string name, string brand, float price, string size,string color) : base(name, brand, price)
        {
            Size = size;
            Color = color;
        }
    }
    class Applience : Product
    {
        public string Model { get; set; }
        public DateTime ProductionDate { get; set; }
        public float Weight { get; set; }
        public Applience(string name, string brand, float price, string model,DateTime productionDate,float weight) : base(name, brand, price)
        {
            Model = model;
            ProductionDate = productionDate;
            Weight = weight;
        }
    }
}
