using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Xml.Serialization;

namespace SimpleClassConsole
{
    class Product
    {
        public string Name;
        private double _price;
        private Curency _cost;
        private string _producer;
        private int _quantity;
        private double _weight;
        private double _expirationDate;
        public void setName(string val) { Name = val; }
        public string getName() { return Name; }

        public void setPrice(double val) { _price = val; }
        public double getPrice() { return _price; }

        public void setCost(Curency val) { _cost = val; }
        public Curency getCost() { return _cost; }

        public void setProducer(string val) { _producer = val; }
        public string getProducer() { return _producer; }

        public void setQuantity(int val) { _quantity = val; }
        public int getQuantity() { return _quantity; }

        public void setWeight(double val) { _weight = val; }
        public double getWeight() { return _weight; }


      
        public Product(string name = "unknown", double price = 0, string producer = "unknown", int quantity = 0, double weight = 0)
        {
            Name = name;
            _price = price;
            _producer = producer;
            _quantity = quantity;
            _weight = weight;

        }

        public Product(Product other)
        {
            Name = other.Name;
            _price = other._price;
            _cost = other._cost;
            _producer = other._producer;
            _quantity = other._quantity;
            _weight = other._weight;
        }

        public double GetPriceUAN()
        {
            return _price;
        }

        public double GetTotalPriceUAN(Product[] products)
        {
            double res = 0;
            for (int i = 0; i < products.Length; i++)
            {
                res += products[i]._price * products[i]._quantity;
            }

            return res;
        }

        public double GetTotalWeight(Product[] products)
        {
            double res = 0;
            for (int i = 0; i < products.Length; i++)
            {
                res += products[i]._weight * products[i]._quantity;
            }
            return res;
        }

    }
    class Curency
    {
        private string Name { get; set; }
        private double ExRate { get; set; }

        public void setName(string val) { Name = val; }
        public string getName() { return Name; }

        public void setExRate(double val) { ExRate = val; }
        public double getExRate() { return ExRate; }

        public Curency(string name = "unknown", double exRate = 0)
        {
            Name = name;
            ExRate = exRate;
        }
    }
    class Program
    {
        static Product[] ReadProductsArray(Product[] products)
        {

            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"\n\nЗапис №{i + 1}\n");
                products[i] = new Product();
                Console.WriteLine($"Введіть назву для {i + 1} товару");
                products[i].setName(Console.ReadLine());
                Console.WriteLine($"Введіть ціну для {i + 1} товару");
                products[i].setPrice(Convert.ToDouble(Console.ReadLine()));
                Console.WriteLine($"Введіть назву виробника для {i + 1} товару");
                products[i].setProducer(Console.ReadLine());
                Console.WriteLine($"Введіть кількість {i + 1} товару");
                products[i].setQuantity(Convert.ToInt32(Console.ReadLine()));
                Console.WriteLine($"Введіть вагу для {i + 1} товару");
                products[i].setWeight(Convert.ToDouble(Console.ReadLine()));

            }
            return products;
        }

        static void PrintProduct(Product product)
        {
            Console.WriteLine($"Назва товару: {product.getName()}");
            Console.WriteLine($"Ціна товару: {product.getPrice()}");
            Console.WriteLine($"Виробник товару: {product.getProducer()}");
            Console.WriteLine($"Кількість товару на складі{product.getQuantity()}");
            Console.WriteLine($"Вага товару: {product.getWeight()}");
        }

        static void PrintProducts(Product[] product)
        {
            for (int i = 0; i < product.Length; i++)
            {
                Console.WriteLine($"\nТовар № {i + 1}");
                Console.WriteLine($"Назва товару: {product[i].getName()}");
                Console.WriteLine($"Ціна товару: {product[i].getPrice()}");
                Console.WriteLine($"Виробник товару: {product[i].getProducer()}");
                Console.WriteLine($"Кількість товару на складі{product[i].getQuantity()}");
                Console.WriteLine($"Вага товару: {product[i].getWeight()}");
            }
        }

        static void GetProductInfo(Product[] products, out Product max, out Product min)
        {
            max = min = products[0];
            for (int i = 0; i < products.Length; i++)
            {
                if (max.getPrice() <= products[i].getPrice())
                    max = products[i];
                if (min.getPrice() >= products[i].getPrice())
                    min = products[i];

            }
        }
        static void Swap(ref Product e1, ref Product e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        static Product[] SortProductsByPrice(Product[] products)
        {
            for (int i = 1; i < products.Length; i++)
            {
                for (int j = 0; j < products.Length - i; j++)
                {
                    if (products[j].getPrice() > products[j + 1].getPrice())
                    {
                        Swap(ref products[j], ref products[j + 1]);
                    }
                }
            }

            return products;
        }

        static Product[] SortProductsByCount(Product[] products)
        {
            for (int i = 1; i < products.Length; i++)
            {
                for (int j = 0; j < products.Length - i; j++)
                {
                    if (products[j].getQuantity() > products[j + 1].getQuantity())
                    {
                        Swap(ref products[j], ref products[j + 1]);
                    }
                }
            }

            return products;
        }

        static void Main(string[] args)
        {
            Curency curency = new Curency();
            Product obj1 = new Product();

            Console.WriteLine("Введіть кількість записів :");
            int n = Convert.ToInt32(Console.ReadLine());
            Product[] products = new Product[n];
            Product[] mas = new Product[n];
            mas = ReadProductsArray(products);
            PrintProducts(mas);
            Console.WriteLine($"\n\nЗагальна ціна всіх продуктів {obj1.GetTotalPriceUAN(mas)}\n");
            Console.WriteLine($"\n\nЗагальна вага всіх продуктів - {obj1.GetTotalWeight(mas)}\n");
            Product max, min;
            GetProductInfo(mas, out max, out min);
            Console.WriteLine($"\n\nНайдорожчий товар - {max.getName()}. Його ціна - {max.getPrice()}");
            Console.WriteLine($"\n\nНайдевший товар - {min.getName()}. Його ціна - {min.getPrice()}");
            Console.WriteLine("\n\n\nМасив відсортований по ціні:");
            SortProductsByPrice(mas);
            PrintProducts(mas);
            SortProductsByCount(mas);
            Console.WriteLine("\n\n\nМасив відсортований по кількості товару на  складі:");
            PrintProducts(mas);
        }
    }
}
