using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Models
{
    public class UsersModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public int Age { get; set; }

        [BsonIgnoreIfNull]
        public UserLoginModel UserLoginModel { get; set; }
    }
}
