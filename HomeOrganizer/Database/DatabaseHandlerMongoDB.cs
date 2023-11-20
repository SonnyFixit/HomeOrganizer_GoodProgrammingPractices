using HomeOrganizer.Database;
using HomeOrganizer.Models.Bases;
using HomeOrganizer.Models.Communication;
using HomeOrganizer.Models.User;
using MongoDB.Driver;

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
            if (await GetUser(newUser.Crudentials.Login) != null)
            {
                return new Response(false, $"User {newUser.Crudentials.Login} already exists!");
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
            return new Response(true, $"User {newUser.Crudentials.Login} added to database!");
        }

        public static async Task<Response> UpdateUser(UserData updatedUser)
        {
            try
            {
                await db.GetCollection<UserData>("Users").ReplaceOneAsync(p => p.Crudentials.Login == updatedUser.Crudentials.Login, updatedUser);
                return new Response(true, $"User {updatedUser.Crudentials.Login} succesfully updated!");
            }
            catch (Exception e)
            {
                if (updatedUser == null)
                {
                    return new Response(false, $"Can't update user, it is null! Error: {e.Message}");

                }
                else
                {
                    return new Response(false, $"Can't update {updatedUser.Crudentials.Login}! Error: {e.Message}");
                }
            }
        }

        public static async Task<UserData?> GetUser(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            var users = await db.GetCollection<UserData>("Users").FindAsync(d => d.Crudentials.Login == login);
            var registeredUser = await users.FirstOrDefaultAsync();

            return registeredUser;
        }

        public static async Task<List<FeatureBase>> GetUsersFeatures(string login)
        {
            var user = await GetUser(login);
            if (user == null) return new List<FeatureBase>();
            else return user.Features;
        }

        public static async Task<bool> UserExists(string login)
        {
            UserData? registeredUser = await GetUser(login);
            return registeredUser != null;
        }

        #endregion
    }
}
