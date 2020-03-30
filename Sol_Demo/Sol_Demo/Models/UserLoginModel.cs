using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Models
{
    public class UserLoginModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
