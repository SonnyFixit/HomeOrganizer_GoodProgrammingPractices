using HomeOrganizer.Database;
using HomeOrganizer.Models.Bases;
using HomeOrganizer.Models.Communication;
using HomeOrganizer.Models.User;

using MongoDB.Driver;

namespace MyWebsiteBlazor.Data.Database
{
    /// <summary>
    /// Contains extension methods for MongoDB operations.
    /// </summary>
    public static class ExtendMongo
    {

        /// <summary>
        /// Converts an IAsyncCursor to an array.
        /// </summary>
        /// <typeparam name="T">The type of elements in the cursor.</typeparam>
        /// <param name="asyncCursor">The cursor to convert.</param>
        /// <returns>An array of elements, or an empty array if the cursor is null.</returns>
        public static T[] ToArray<T>(this IAsyncCursor<T>? asyncCursor)
        {
            if (asyncCursor is null) return Array.Empty<T>();
            return asyncCursor.ToEnumerable().ToArray();
        }
    }

    /// <summary>
    /// Provides functionality for database operations in the Home Organizer application.
    /// This includes user management, feature management, password reset, and email change functionalities.
    public static class DbHandler
    {
        private static readonly string connectionString = DatabaseCredentials.ConnectionString;
        private static readonly string databaseName = DatabaseCredentials.DatabaseName;
        private static readonly IMongoDatabase? db = null;

        // Static constructor to initialize the database connection.
        static DbHandler()
        {
            db = new MongoClient(connectionString).GetDatabase(databaseName);
        }

        /// <summary>
        /// Clears a collection in the database.
        /// </summary>
        /// <param name="collectionName">Name of the collection to clear.</param>
        public static async Task ClearCollectionAsync(string collectionName)
        {
            await db.DropCollectionAsync(collectionName);
            await db.CreateCollectionAsync(collectionName);
        }

        #region UserData

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="newUser">The user data to add.</param>
        /// <returns>A response indicating success or failure with a message.</returns>
        public static async Task<Response> CreateUser(UserData newUser)
        {
            if (await GetUserByLogin(newUser.Credentials.Login) != null)
            {
                return new Response(false, $"User with login: {newUser.Credentials.Login} already exists!");
            }

            if (await GetUserByEmail(newUser.Email) != null)
            {
                return new Response(false, $"User with email: {newUser.Email} already exists!");
            }

            try
            {
                await db.GetCollection<UserData>("Users").InsertOneAsync(newUser);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                await Console.Out.WriteLineAsync(e.ToString());
                return new Response(true, $"Database error! Please, contact us!");
            }
            return new Response(true, $"User {newUser.Credentials.Login} added to database!");
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="updatedUser">The updated user data.</param>
        /// <returns>A response indicating success or failure with a message.</returns>
        public static async Task<Response> UpdateUser(UserData updatedUser)
        {
            try
            {
                await db.GetCollection<UserData>("Users").ReplaceOneAsync(p => p.Credentials.Login == updatedUser.Credentials.Login, updatedUser);
                return new Response(true, $"User {updatedUser.Credentials.Login} succesfully updated!");
            }
            catch (Exception e)
            {
                if (updatedUser == null)
                {
                    return new Response(false, $"Can't update user, it is null! Error: {e.Message}");

                }
                else
                {
                    return new Response(false, $"Can't update {updatedUser.Credentials.Login}! Error: {e.Message}");
                }
            }
        }

        /// <summary>
        /// Retrieves a user by their login.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <returns>The user data if found, null otherwise.</returns>
        public static async Task<UserData?> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            var users = await db.GetCollection<UserData>("Users").FindAsync(d => d.Credentials.Login == login);
            var registeredUser = await users.FirstOrDefaultAsync();

            return registeredUser;
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user data if found, null otherwise.</returns>
        public static async Task<UserData?> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var users = await db.GetCollection<UserData>("Users").FindAsync(d => d.Email == email);
            var registeredUser = await users.FirstOrDefaultAsync();

            return registeredUser;
        }

        /// <summary>
        /// Retrieves a list of features associated with a user's login.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <returns>A list of FeatureBase objects associated with the user.</returns>
        public static async Task<List<FeatureBase>> GetUsersFeatures(string login)
        {
            var user = await GetUserByLogin(login);
            if (user == null) return new List<FeatureBase>();
            else return user.Features;
        }

        /// <summary>
        /// Checks if a user exists in the database based on login.
        /// </summary>
        /// <param name="login">The login of the user to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static async Task<bool> UserExists(string login)
        {
            UserData? registeredUser = await GetUserByLogin(login);
            return registeredUser != null;
        }

        #endregion

        #region Reset password

        /// <summary>
        /// Adds reset password data for a user to the database.
        /// </summary>
        /// <param name="resetData">The reset password data to add.</param>
        /// <returns>A response indicating success or failure with a message.</returns>
        public static async Task<Response> AddResetPasswordData(ResetPasswordData resetData)
        {
            try
            {
                await db.GetCollection<ResetPasswordData>("ResetPasswordDatas").InsertOneAsync(resetData);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                await Console.Out.WriteLineAsync(e.ToString());
                return new Response(false, $"Error while adding password reset data!");
            }
            return new Response(true, $"Created password reset data!");
        }

        /// <summary>
        /// Retrieves reset password data for a specific user.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <returns>ResetPasswordData if found, null otherwise.</returns>
        public static async Task<ResetPasswordData?> GetResetPasswordData(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            var resetPasswordDatas = await db.GetCollection<ResetPasswordData>("ResetPasswordDatas").FindAsync(d => d.UserLogin == login);

            var resetPwdDatas = resetPasswordDatas.ToArray();
            var newestData = resetPwdDatas.OrderBy(d => d.CreationTime).Last();

            return newestData;
        }

        #endregion

        #region Change email

        /// <summary>
        /// Adds change email data for a user to the database.
        /// </summary>
        /// <param name="changeData">The change email data to add.</param>
        /// <returns>A response indicating success or failure with a message.</returns>
        public static async Task<Response> AddChangeEmailData(ChangeEmailData changeData)
        {
            try
            {
                await db.GetCollection<ChangeEmailData>("ChangeEmailDatas").InsertOneAsync(changeData);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                await Console.Out.WriteLineAsync(e.ToString());
                return new Response(false, $"Error while adding email change data!");
            }
            return new Response(true, $"Created change email data!");
        }

        /// <summary>
        /// Retrieves change email data for a specific user.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <returns>ChangeEmailData if found, null otherwise.</returns>
        public static async Task<ChangeEmailData?> GetChangeEmailData(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            var changeEmailDatas = await db.GetCollection<ChangeEmailData>("ChangeEmailDatas").FindAsync(d => d.UserLogin == login);

            var changeEmailDatasArray = changeEmailDatas.ToArray();
            var newestData = changeEmailDatasArray.OrderBy(d => d.CreationTime).Last();

            return newestData;
        }
        #endregion
    }
}
