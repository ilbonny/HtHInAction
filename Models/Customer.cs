
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HtHInAction.Models
{
    public class Customer : BaseItem
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}