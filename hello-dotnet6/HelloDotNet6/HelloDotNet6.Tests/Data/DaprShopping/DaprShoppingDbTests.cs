using System;
using HelloDotNet6.Data.Models;
using HelloDotNet6.Data.Models.DaprShopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Pomelo.EntityFrameworkCore.MySql;

namespace HelloDotNet6.Tests.Data.DaprShopping
{
    public class Tests
    {
        private ShoppingDatabaseContext _db;

        [SetUp]
        public void Setup()
        {
            _db = new ShoppingDatabaseContext();
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
        }

        [Test]
        public void SeedDatabase()
        {
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@dapr.io"
            };
            var product = new Product
            {
                Name = "Basketball",
                Description = "NBA Authorized Basket Ball",
                UnitCost = 11.99
            };
            var inventory = new Inventory
            {
                Product = product,
                Quantity = 100
            };
            _db.Customers.Add(customer);
            _db.Products.Add(product);
            _db.Inventory.Add(inventory);
            _db.SaveChanges();
            Assert.Pass();
        }
    }
}