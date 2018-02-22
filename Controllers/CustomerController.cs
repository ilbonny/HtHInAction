using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtHInAction.Models;
using HtHInAction.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HtHInAction.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> _repository;
        public CustomersController(IRepository<Customer> repository, IOptions<Settings> settings)
        {
           _repository = repository;
        }        

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Customer> Get(string id)
        {
           return await _repository.Get(id);
        }

        [HttpPost]
        public async Task Post([FromBody]Customer customer)
        {
            if(customer==null) return;
            await _repository.Add(customer);
        }

        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody]Customer customer)
        {            
            return await _repository.Update(id, customer);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _repository.Remove(id);
        }
    }
}
