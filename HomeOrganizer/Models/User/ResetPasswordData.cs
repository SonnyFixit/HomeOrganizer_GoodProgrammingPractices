using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeOrganizer.Models.User
{
    public class ResetPasswordData
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string UserLogin { get; init; }

        [BsonIgnore]
        public string Token { get; init; }
        public string HashedToken { get; init; }
        public string Salt { get; init; }
        public DateTime CreationTime { get; init; }

    }
}
