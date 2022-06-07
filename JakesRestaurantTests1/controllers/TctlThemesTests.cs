using Microsoft.VisualStudio.TestTools.UnitTesting;
using management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controllers;

namespace controllers.Tests
{

    // string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "themes.json");

    [TestClass()]
    public class TctlThemesTests
    {

        [TestMethod()]
        public void UpdateListTest()
        {
            var theme = new TctlThemes();
            Assert.IsNotNull(theme);

            var obj = new Theme();
            obj.ID = 1;
            obj.Name = "Test";
            obj.StartDate = DateTime.Now;
            obj.EndDate = DateTime.Now;
            theme.UpdateList(obj);
        }

        [TestMethod()]
        public void GetThemesTest()
        {
            var theme = new TctlThemes();
            Assert.IsNotNull(theme);

            var themesList = theme.GetThemes();
            Assert.IsNotNull(themesList);

        }
              

        [TestMethod()]
        public void GetByIDTest()
        {
            var theme = new TctlThemes();
            Assert.IsNotNull(theme);

            var obj = theme.GetByID(1);

            Assert.IsNull(obj);
        }

        [TestMethod()]
        public void IncrementIDTest()
        {
            var theme = new TctlThemes();
            Assert.IsNotNull(theme);

            var t = theme.IncrementID();
            Assert.AreNotEqual(1, t);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var theme = new TctlThemes();
            Assert.IsNotNull(theme);

            var d = theme.Delete(1);
            Assert.IsTrue(d);

        }

    }
}
