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
    public class MailsController : Controller
    {
        private readonly IRepository<Mail> _repository;
        private readonly IEmailSender _emailSender;

        public MailsController(IRepository<Mail> repository, IEmailSender emailSender)
        {
           _repository = repository;
           _emailSender = emailSender;
        }        

        [HttpGet]
        public async Task<IEnumerable<Mail>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Mail> Get(string id)
        {
           return await _repository.Get(id);
        }

        [HttpPost]
        public async Task Post([FromBody]Mail mail)
        {
            if(mail==null) return;
            await _repository.Add(mail);
        }

        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody]Mail mail)
        {            
            return await _repository.Update(id, mail);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _repository.Remove(id);
        }

        [HttpPost]
        [Route("sendmails/{type}")]
        public async Task PostSendMails(string type, [FromBody]IList<Customer> customers)
        {
            var mailTemplate = await _repository.Get(type);
            await _emailSender.SendEmailAsync(mailTemplate, customers.Select(x=>x.Email).ToList());
        }        
    }
}
