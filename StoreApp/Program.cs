using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Example data
            Food testFood=new Food("apples","Brand A",1.50,new DateTime(2021,06,14));
            Beverage testBeverage=new Beverage("milk","BrandM",0.99,new DateTime(2022,02,02));
            Clothes testClothes=new Clothes("T-shirt","BrandT",15.99,Sizes.M,"violet");
            Applience testApplience=new Applience("laptop","BrandL",2345,"ModelL",new DateTime(2021,03,03),1.125);

            DateTime purchaseDate=new DateTime(2021,06,14,12,34,56);

            CartItem[] cart={
            new CartItem(testFood,2.45,"kg"),
            new CartItem(testBeverage,3,"bottles"),
            new CartItem(testClothes,2,""),
            new CartItem(testApplience,1,"")};
            
            Cashier cashier=new Cashier();
            cashier.Print(cart,purchaseDate);
        }
    }
    class Product{
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public Product(string name,string brand,double price)
        {
            Name = name;
            Brand = brand;
            Price = price;
        }
    }
    class Perishables : Product
    {
        public DateTime ExpirationDate { get; set; }
        public Perishables(string name, string brand, double price, DateTime expirationDate) : base(name, brand, price)
        {
            ExpirationDate = expirationDate;
        }
    }
    class Food : Perishables
    {
        public Food(string name, string brand, double price, DateTime expirationDate) : base(name, brand, price, expirationDate) { }
    }
    class Beverage : Perishables
    {
        public Beverage(string name, string brand, double price, DateTime expirationDate) : base(name, brand, price, expirationDate) { }
    }
    public enum Sizes{ XS, S, M, L, XL}
    class Clothes : Product
    {        
        public Sizes Size { get; set; }
        public string Color { get; set; }
        public Clothes(string name, string brand, double price, Sizes size,string color) : base(name, brand, price)
        {
            Size = size;
            Color = color;
        }
    }
    class Applience : Product
    {
        public string Model { get; set; }
        public DateTime ProductionDate { get; set; }
        public double WeightInKg { get; set; }
        public Applience(string name, string brand, double price, string model,DateTime productionDate,double weightInKg) : base(name, brand, price)
        {
            Model = model;
            ProductionDate = productionDate;
            WeightInKg = weightInKg;
        }
    }
    class CartItem
    {
        public Product Product { get; set; }
        public double Ammount { get; set; }
        public string Unit { get; set; }
        public CartItem (Product product,double ammount,string unit)
	    {   
            Product=product;
            Ammount=ammount;
            Unit=unit;
	    }
    }
    class Cashier
    {
        public void Print(CartItem[] cart,DateTime purchaseDate)
        {
            double totalCost=0;
            double totalDiscount=0;
            StringBuilder receipt=new StringBuilder();
            receipt.AppendLine("Date: "+purchaseDate.ToString("yyyy-mm-dd hh:mm:ss")+"\n");
            receipt.AppendLine("---Products---\n\n");
            foreach (CartItem cartItem in cart)
	        {
                if (cartItem.Product is Perishables)
                {
                    Perishables perishable=(Perishables)cartItem.Product;
                    /// In the given format there is no "-" but the example data had a "-" for specifically food or apples in particular.
                    /// I have decided to follow the format as it was set, but it is no issue to add an exception in the format for food items or apples.
                    receipt.AppendLine(perishable.Name+" "+perishable.Brand+"\n");
                    receipt.AppendLine(cartItem.Ammount+" x $"+perishable.Price+" = $"+Math.Round(cartItem.Ammount*perishable.Price, 2)+"\n");
                    totalCost=Math.Round(totalCost+cartItem.Ammount*perishable.Price,2);
                    if((perishable.ExpirationDate - purchaseDate).TotalDays<1)
                    {
                        receipt.AppendLine("#discount 50% -$"+Math.Round(cartItem.Ammount*perishable.Price*0.5, 2)+"\n");
                        totalDiscount=totalDiscount+cartItem.Ammount*perishable.Price*0.5f;
                    }else if ((perishable.ExpirationDate - purchaseDate).TotalDays<6)
	                {
                        receipt.AppendLine("#discount 10% -$"+Math.Round(cartItem.Ammount*perishable.Price*0.1, 2)+"\n");
                        totalDiscount=totalDiscount+cartItem.Ammount*perishable.Price*0.1f;
	                }
                }
                if(cartItem.Product is Clothes)
                {  
                    Clothes clothes=(Clothes)cartItem.Product;
                    receipt.AppendLine(clothes.Name+" "+clothes.Brand+" "+clothes.Size+" "+clothes.Color+"\n");
                    receipt.AppendLine(cartItem.Ammount+" x $"+clothes.Price+" = $"+Math.Round(cartItem.Ammount*clothes.Price, 2)+"\n");
                    totalCost=Math.Round(totalCost+cartItem.Ammount*clothes.Price,2);
                    if ((purchaseDate.DayOfWeek != DayOfWeek.Saturday) && (purchaseDate.DayOfWeek != DayOfWeek.Sunday))
                    {
                        receipt.AppendLine("#discount 10% -$"+Math.Round(cartItem.Ammount*clothes.Price*0.1, 2)+"\n");
                        totalDiscount=totalDiscount+cartItem.Ammount*clothes.Price*0.1;
                    }
                }
                if(cartItem.Product is Applience)
                {
                    Applience applience=(Applience)cartItem.Product;
                    receipt.AppendLine(applience.Name+" "+applience.Brand+" "+applience.Model+"\n");
                    receipt.AppendLine(cartItem.Ammount+" x $"+applience.Price+" = $"+Math.Round(cartItem.Ammount*applience.Price, 2)+"\n");
                    totalCost=Math.Round(totalCost+cartItem.Ammount*applience.Price,2);
                    if(((purchaseDate.DayOfWeek == DayOfWeek.Saturday) || (purchaseDate.DayOfWeek == DayOfWeek.Sunday)) && applience.Price>999)
                    {
                        receipt.AppendLine("#discount 5% -$"+Math.Round(cartItem.Ammount*applience.Price*0.05, 2)+"\n");
                        totalDiscount=totalDiscount+cartItem.Ammount*applience.Price*0.05;
                    }
                }
                receipt.AppendLine("\n");
	        }
            receipt.AppendLine("------------------------------------------------------------\n");
            receipt.AppendLine("SUBTOTAL: $"+Math.Round(totalCost, 2));
            receipt.AppendLine("DISCOUNT: -$"+Math.Round(totalDiscount, 2)+"\n");
            receipt.AppendLine("TOTAL: $"+Math.Round(totalCost-totalDiscount, 2));
            Console.WriteLine(receipt);
        }
    }
}
