using Microsoft.VisualStudio.TestTools.UnitTesting;
using controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using management;

namespace controllers.Tests
{
    [TestClass()]
    public class TctlProductsTests
    {


        [TestMethod()]
        public void AUpdateListTest1()
        {
            var products = new TctlProducts();
            Assert.IsNotNull(products);

            var prod = new Product();
            Assert.IsNotNull(prod);

            prod.ID = 1;
            prod.Name = "Product";

            List<String> aL = new List<String>();
            aL.Add("Item 1");
            aL.Add("Item 2");

            List<String> iL = new List<string>();
            iL.Add("Item 1");
            iL.Add("Item 2");

            prod.Allergens = aL;

            prod.Ingredients = iL;

            prod.Alcohol = false;
            prod.Price = 2.20;

            products.UpdateList(prod);

        }

        [TestMethod()]
        public void BGetProductsTest2()
        {
            var products = new TctlProducts();
            Assert.IsNotNull(products);

            var p = products.GetProducts();

            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void CGetByIDTest3()
        {
            var products = new TctlProducts();
            Assert.IsNotNull(products);

            var p = products.GetByID(1);

            Assert.IsNotNull(p);

        }

        [TestMethod()]
        public void DIncrementIDTest4()
        {
            var products = new TctlProducts();
            Assert.IsNotNull(products);

            var id = products.IncrementID();

            Assert.AreNotEqual(-1, id);

        }

        [TestMethod()]
        public void EGetFromDateTest5()
        {
            var products = new TctlProducts();
            Assert.IsNotNull(products);

            var p = products.GetFromDate(DateTime.Now, DateTime.Now.AddDays(365));

            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void FGetProductsByThemeIDTest6()
        {
            var products = new TctlProducts();
            Assert.IsNotNull(products);

            var p = products.GetProductsByThemeID(1);

            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void GDeleteTest7()
        {
            var products = new TctlProducts();
            Assert.IsNotNull(products);

            bool d = products.Delete(1);
            Assert.IsTrue(d);

        }
    }
}