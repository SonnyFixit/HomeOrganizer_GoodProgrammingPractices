﻿using HomeOrganizer.Database;
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

    public static class DbHandler
    {
        private static readonly string connectionString = DatabaseCredentials.ConnectionString;
        private static readonly string databaseName = DatabaseCredentials.DatabaseName;
        private static readonly IMongoDatabase? db = null;

        static DbHandler()
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

        public static async Task<UserData?> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            var users = await db.GetCollection<UserData>("Users").FindAsync(d => d.Credentials.Login == login);
            var registeredUser = await users.FirstOrDefaultAsync();

            return registeredUser;
        }
        public static async Task<UserData?> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var users = await db.GetCollection<UserData>("Users").FindAsync(d => d.Email == email);
            var registeredUser = await users.FirstOrDefaultAsync();

            return registeredUser;
        }

        public static async Task<List<FeatureBase>> GetUsersFeatures(string login)
        {
            var user = await GetUserByLogin(login);
            if (user == null) return new List<FeatureBase>();
            else return user.Features;
        }

        public static async Task<bool> UserExists(string login)
        {
            UserData? registeredUser = await GetUserByLogin(login);
            return registeredUser != null;
        }

        #endregion

        #region Reset password

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