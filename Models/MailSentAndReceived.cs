using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HtHInAction.Models
{
    public class MailSentAndReceived : BaseItem
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }

    }
}