using HomeOrganizer.Database;

using MongoDB.Driver;

using HomeOrganizer.Models;
using HomeOrganizer.Models.Communication;
using HomeOrganizer.Models.User;
using MongoDB.Bson.Serialization;
using HomeOrganizer.Models.Features;
using HomeOrganizer.Models.Interfaces;

namespace MyWebsiteBlazor.Data.Database
{
    public static class ExtendMongo
    {
        public static T[] ToArray<T>(this IAsyncCursor<T>? asyncCursor)
        {
            if (asyncCursor is null) return Array.Empty<T>();
            return asyncCursor.ToEnumerable().ToArray();
        }
    }

    public static class DatabaseHandlerMongoDB
    {
        private static readonly string connectionString = DatabaseCrudentials.ConnectionString;
        private static readonly string databaseName = DatabaseCrudentials.DatabaseName;
        private static readonly IMongoDatabase? db = null;

        static DatabaseHandlerMongoDB()
        {
            db = new MongoClient(connectionString).GetDatabase(databaseName);
        }


        public static async Task ClearCollectionAsync(string collectionName)
        {
            await db.DropCollectionAsync(collectionName);
            await db.CreateCollectionAsync(collectionName);
        }

        #region UserData

        public static async Task<Response> CreateUser(UserData newUser)
        {
            if (await GetUser(newUser.Login) != null)
            {
                return new Response(false, $"User {newUser.Login} already exists!");
            }

            try
            {
                await db.GetCollection<UserData>("Users").InsertOneAsync(newUser);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                await Console.Out.WriteLineAsync(e.ToString());
            }
            return new Response(true, $"User {newUser.Login} added to database!");
        }

        public static async Task<Response> UpdateUser(UserData updatedUser)
        {
            try
            {
                await db.GetCollection<UserData>("Users").ReplaceOneAsync(p => p.Login == updatedUser.Login, updatedUser);
                return new Response(true, $"User {updatedUser.Login} succesfully updated!");
            }
            catch (Exception e)
            {
                return new Response(false, $"Can't update {updatedUser.Login}! Error: {e.Message}");
            }
        }

        public static async Task<UserData?> GetUser(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            var users = await db.GetCollection<UserData>("Users").FindAsync(d => d.Login == login);
            var registeredUser = await users.FirstOrDefaultAsync();

            return registeredUser;
        }

        public static async Task<bool> UserExists(string login)
        {
            UserData? registeredUser = await GetUser(login);
            return registeredUser != null;
        }

        #endregion
    }
}
