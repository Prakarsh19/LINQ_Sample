using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Task.Data;
using Task.Helpers.ResizerApp.Helpers;

namespace Task
{
    public class LinqSamples
    {
        private readonly DataSource _dataSource = new DataSource();
        public void Linq1()
        {
            MessageHelper.InfoMessage("Linq1. Enter the amount to X");
            decimal X = Convert.ToDecimal(Console.ReadLine());
            MessageHelper.InfoMessage("All customers whose total turnover (the sum of all orders) exceeds the amount X");
            var cutomers =
                from c in _dataSource.Customers
                group c by c.CustomerName into Group
                select new
                {
                    CustomerName = Group.Key,
                    TotalOrdersSum = Group.Sum(a => a.Orders.Sum(b => b.Total))
                };
            foreach (var customer in cutomers)
            {
                if (customer.TotalOrdersSum > X)
                    Console.WriteLine(customer);
            }
            Console.WriteLine();
        }

        public void Linq2()
        {
            MessageHelper.InfoMessage("Linq2. Customer and Supplier Having Same Cities");
            var Customer_Supplier = from c in _dataSource.Customers
                             from s in _dataSource.Suppliers
                             where c.City == s.City && s.Country == s.Country
                             select new { CustomerName = c.CustomerName, SupplierName = s.SupplierName };
            foreach (var c in Customer_Supplier)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine();
        }   
        public void Linq3()
        {
            MessageHelper.InfoMessage("Linq3. Enter the amount to X");
            int X = Convert.ToInt32(Console.ReadLine());
            MessageHelper.InfoMessage("All customers whose no. of orders exceeds the amount X");

            var customer =
                from c in _dataSource.Customers
                where c.Orders.Length > X
                select c;
            foreach (var c in customer)
            {
                Console.WriteLine(c.CustomerName);
            }
        }
        public void Linq4()
        {
            MessageHelper.InfoMessage("Linq4. Customer became clients");
            var customer =
                from c in _dataSource.Customers
                group c by c.CustomerName into g
                select new
                {
                    CustomerName = g.Key,
                    JoinDate = g.Min(a => a.Orders.Length==0? new DateTime():  a.Orders.Min(b => b.OrderDate)),
                };
            foreach (var c in customer)
            {
                Console.WriteLine(c);
            }
        }
        public void Linq5()
        {
            MessageHelper.InfoMessage("Linq5. Customer became clients");
            var entities =
                from c in _dataSource.Customers
                group c by c.CustomerName into g
                let Date = g.Min(a => a.Orders.Length == 0 ? new DateTime() : a.Orders.Min(b => b.OrderDate))
                let JoinYear = Date.Year
                let JoinMonth = Date.Month
                let TotalTurnOver = g.Sum(a => a.Orders.Sum(b => b.Total))
                orderby JoinMonth descending, JoinYear descending, TotalTurnOver descending
                select new
                {
                    CustomerName = g.Key,                   
                };
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        public void Linq6()
        {
            MessageHelper.InfoMessage("Linq6. Customers having Postal code is non-numeric or the Region is empty or the phone does not have a operator code");
            var customers =
                from c in _dataSource.Customers
                let no = c.Phone.Split()
                where int.TryParse(c.PostalCode, out int id) == true ||
                c.Region is null || 
                no[0] == null 
                select c;

            foreach(var customer in customers)
            {
                Console.WriteLine(customer.CustomerName);
            }
        }

        public void Linq7()
        {
            MessageHelper.InfoMessage("Linq4. Group products into groups cheap, moderate, expensive");
            var Productbyrates = 
                from p in _dataSource.Products
                group p by new
                {
                    Product = p.ProductName,
                    Expensive = p.UnitPrice >= 100,
                    Moderate = p.UnitPrice >= 50 && p.UnitPrice<100,
                    Cheap = p.UnitPrice < 50,
                }  into g
                select g;
           foreach (var product in Productbyrates)
            {
                Console.WriteLine(product.Key);
            }
        }

    }
}