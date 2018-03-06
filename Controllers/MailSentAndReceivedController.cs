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
    public class MailSentAndReceivedController : Controller
    {
        private readonly IRepository<MailSentAndReceived> _repository;

        public MailSentAndReceivedController(IRepository<MailSentAndReceived> repository)
        {
            _repository = repository;
        }

        [HttpGet("sent")]
        public async Task<IEnumerable<MailSentAndReceived>> GetMailSent()
        {
            return await _repository.Find(x => x.Type == "Sent");
        }

        [HttpGet("received/{email}")]
        public async Task<IEnumerable<MailSentAndReceived>> GetMailReceived(string email)
        {
            return await _repository.Find(x => x.Type == "Received" && x.From == email);
        }
    }
}
