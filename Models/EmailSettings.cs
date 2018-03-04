using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HtHInAction.Models
{
    public class EmailSettings : BaseItem
    {
        public bool Default { get; set; }

        public string DomainSmtp { get; set; }

        public int PortSmtp { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string DomainPop { get; set; }

        public int PortPop { get; set; }
    }
}