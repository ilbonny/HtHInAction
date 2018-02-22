using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtHInAction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HtHInAction.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IMongoCollection<Customer> _collection;
        public CustomersController(IOptions<Settings> settings)
        {
           var client =  new MongoClient(settings.Value.ConnectionString);
           var database = client.GetDatabase(settings.Value.Database);
           _collection = database.GetCollection<Customer>("Customers"); 
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _collection.Find(_=> true).ToList();
        }

        [HttpGet("{id}")]
        public Customer Get(string id)
        {
           return _collection.Find( x => x.Id == ObjectId.Parse(id)).FirstOrDefault();
        }

        [HttpPost]
        public void Post([FromBody]Customer customer)
        {
            if(customer!=null)
                _collection.InsertOne(customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
