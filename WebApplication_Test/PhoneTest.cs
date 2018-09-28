using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Controllers;
using WebApplication.Model;

namespace WebApplication_Test
{
    [TestClass]
    public class PhoneTest
    {

        private List<Customer> GetTestCustomers()
        {
            List<Customer> customers = new List<Customer>()
        {
            new Customer(){ ID=0, Name = "customer1", phones = new List<Phone>(){new Phone() { PhoneNumber = "123"} }},
            new Customer(){ ID=1, Name = "customer2", phones = new List<Phone>(){new Phone() { PhoneNumber = "456"}, new Phone() { PhoneNumber = "789", IsActive=true}  }},
        };

            return customers;
        }


        [TestMethod]
        public void Get_ShouldReturnAllCustomersPhones()
        {
            var testCustomers = GetTestCustomers();
            var controller = new PhoneController();
            controller.SetCustomersData(testCustomers);

            var result = controller.Get() as List<Phone>;
            Assert.AreEqual(testCustomers.SelectMany(a => a.phones).Count(), result.Count);
        }

        [TestMethod]
        public void Get_ShouldReturnCorrectCustomerPhoneNumbers()
        {
            var testCustomers = GetTestCustomers();

            var controller = new PhoneController();
            controller.SetCustomersData(testCustomers);

            var result = controller.Get(1) as List<Phone>;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, testCustomers[1].phones.Count);
        }

        [TestMethod]
        public void Put_ShouldSetIsActiveToTrueForThePhone()
        {
            var testCustomers = GetTestCustomers();

            var controller = new PhoneController();
            controller.SetCustomersData(testCustomers);

            var result = controller.Put(456, "true") as IActionResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(testCustomers[1].phones[0].IsActive, true);
        }

        [TestMethod]
        public void Get_ShouldNotFindCustomer()
        {
            var testCustomers = GetTestCustomers();

            var controller = new PhoneController();
            controller.SetCustomersData(testCustomers);

            var result = controller.Get(555) as List<Phone>;
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void Put_ShouldNotFindPhoneAndReturnsNotFound()
        {
            var testCustomers = GetTestCustomers();

            var controller = new PhoneController();
            controller.SetCustomersData(testCustomers);

            var result = controller.Put(555, "true") as IActionResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
