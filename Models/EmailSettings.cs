using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HtHInAction.Models
{
    public class EmailSettings : BaseItem
    {
        public bool Default {get;set;}

        public string Domain { get; set; }

        public int Port { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}