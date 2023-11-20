using MongoDB.Bson.Serialization.Attributes;

namespace HomeOrganizer.Models.User
{
    public class UserCrudentials
    {
        public string Login { get; init; }

        [BsonElement("PasswordHash")]
        private readonly string passwordHash;

        [BsonElement("PasswordSalt")]
        private readonly string passwordSalt;

        public UserCrudentials(string login, string password)
        {
            Login = login;
            passwordHash = password + "fefwefewg";
            passwordSalt = "gewgewgewg";
        }
        public bool CheckPassword(string password)
        {
            return true;
        }
    }
}
