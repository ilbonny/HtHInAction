using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HtHInAction.Models
{
    public class MailSentAndReceived : BaseItem
    {
        public Mail Mail {get;set;}
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}