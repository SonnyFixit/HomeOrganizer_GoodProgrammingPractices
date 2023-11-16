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
            //BsonClassMap.RegisterClassMap<IFeature>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.SetIsRootClass(true);
            //});

            // Dodaj niestandardowe mapowanie dla klasy UserData
            //BsonClassMap.RegisterClassMap<UserData>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.MapMember(p => p.Features).SetElementName("features");
            //});

            db = new MongoClient(connectionString).GetDatabase(databaseName);

            //BsonClassMap.RegisterClassMap<Introduction>(cm =>
            //{
            //    cm.SetDiscriminator("Introduction");
            //    cm.AutoMap();
            //    cm.SetIgnoreExtraElements(true);
            //});

            //BsonClassMap.RegisterClassMap<CarStatus>(cm =>
            //{
            //    cm.SetDiscriminator("CarStatus");
            //    cm.AutoMap();
            //    cm.SetIgnoreExtraElements(true);
            //});

            //BsonClassMap.RegisterClassMap<MediaSubscriptions>(cm =>
            //{
            //    cm.SetDiscriminator("MediaSubscriptions");
            //    cm.AutoMap();
            //    cm.SetIgnoreExtraElements(true);
            //});

            //BsonClassMap.RegisterClassMap<CustomFeature>(cm =>
            //{
            //    cm.SetDiscriminator("CustomFeature");
            //    cm.AutoMap();
            //    cm.SetIgnoreExtraElements(true);
            //});

            //BsonClassMap.RegisterClassMap<HouseholdBills>(cm =>
            //{
            //    cm.SetDiscriminator("HouseholdBills");
            //    cm.AutoMap();
            //    cm.SetIgnoreExtraElements(true);
            //});

            //BsonClassMap.RegisterClassMap<PetStatus>(cm =>
            //{
            //    cm.SetDiscriminator("PetStatus");
            //    cm.AutoMap();
            //    cm.SetIgnoreExtraElements(true);
            //});
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
