using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HomeOrganizer.Models.User
{
    public class ChangeEmailData
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string UserLogin { get; init; }

        [BsonIgnore]
        public string Token { get; init; }
        public string HashedToken { get; init; }
        public string Salt { get; init; }
        public string NewEmail { get; init; }
        public DateTime CreationTime { get; init; }
    }
}
