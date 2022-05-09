using Microsoft.VisualStudio.TestTools.UnitTesting;
using Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    [TestClass()]
    public class TctlLoginTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            // User must be able to login with admin account
            // Username = admin
            // Password = admin
            TctlLogin login = new TctlLogin();

            string username1 = "admin";
            string password1 = "admin";

            bool res1 = login.Login(username1, password1);

            Assert.IsTrue(res1);

            string username2 = "THIS IS NOT A VALID USERNAME";
            string password2 = "admin";

            bool res2 = login.Login(username2, password2);

            Assert.IsFalse(res2);
        }
    }
}