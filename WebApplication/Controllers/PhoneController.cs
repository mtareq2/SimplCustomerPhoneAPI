/*
 Simple API
 Sep 27, 2018
 by Muhammad Tariq
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {


        // This customers list acts as our customers data in the DB
        private List<Customer> customers = new List<Customer>()
        {
            new Customer(){ ID=0, Name = "customer1", phones = new List<Phone>(){new Phone() { PhoneNumber = "123"} }},
            new Customer(){ ID=1, Name = "customer2", phones = new List<Phone>(){new Phone() { PhoneNumber = "456", IsActive=true}, new Phone() { PhoneNumber = "789", IsActive=true}  }},
        };

        public PhoneController()
        {

        }

        public void SetCustomersData(List<Customer> customers) {
            this.customers = customers;
        }
        //public PhoneController(List<Customer> customers)
        //{
        //    this.customers = customers;
        //}

        // GET api/phones
        [HttpGet]
        public IEnumerable<Phone> Get()
        {            
            return customers.SelectMany(a => a.phones).ToList();
        }

        // GET api/phone/5
        [HttpGet("{id}")]
        public IEnumerable<Phone> Get(int id)
        {
            return customers.Where(c => c.ID == id).SelectMany(a => a.phones).ToList();
        }


        // PUT api/phone/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, string value)
        {
            // Assuming that phone is unique, so I'll use the phone number as the id
            Customer customer = customers.Where(c => c.phones.Where(p => p.PhoneNumber == id.ToString()).SingleOrDefault() != null).FirstOrDefault();
            if(customer != null)
            {
                Phone phoneToActivate = customer.phones.Where(p => p.PhoneNumber == id.ToString()).SingleOrDefault();

                if(phoneToActivate != null)
                {
                    phoneToActivate.IsActive = true;
                    return Ok();
                }
            }
            return NotFound();

        }

    }
}
