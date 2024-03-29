﻿using API.Contracts.Services;
using API.Models.DB;
using System.Data.SqlClient;
using Dapper;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        private readonly string _dbConnectionString;
        private readonly IPhotosService _photosService;
        public UsersService(IConfiguration config, IPhotosService photosService)
        {
            _dbConnectionString = config.GetConnectionString("DefaultConnection");
            this._photosService = photosService;
        }

        public bool CreateUser(User user)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                var query = @"
INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password]
           ,[Gender]
           ,[DateOfBirth]
           ,[KnownAs]
           ,[Created]
           ,[LastActive]
           ,[Introduction]
           ,[LookingFor]
           ,[IsActive]
           ,[Interests]
           ,[City]
           ,[Country])
     VALUES
           (@Username
           ,@Password 
           ,@Gender
           ,@DateOfBirth
           ,@KnownAs
           ,@Created 
           ,@LastActive 
           ,@Introduction 
           ,@LookingFor
           ,@IsActive 
           ,@Interests 
           ,@City
           ,@Country)

  SELECT cast(scope_identity() as int)
";

                var result = (int)connection.ExecuteScalar(query, user);

                if(result > 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        public User GetUserById(int id) //bool loadPhotos = false
        {
            //Get the user from db
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                var query = @"
                    SELECT  [Id]
                    ,[Username]
                    ,[Password]
                    ,[Gender]
                    ,[DateOfBirth]
                    ,[KnownAs]
                    ,[Created]
                    ,[LastActive]
                    ,[Introduction]
                    ,[LookingFor]
                    ,[IsActive]
                    ,[Interests]
                    ,[City]
                    ,[Country]
		            ,CASE WHEN Likes.LikeeId IS NULL THEN 'False' ELSE 'True' END as IsUserLiked
                FROM [Users]
	            LEFT JOIN Likes on LikerId = @UserId AND LikeeId = Users.Id
                WHERE Id = @Id
                    ";

                var user = connection.QueryFirstOrDefault<User>(query, new { Id = id, UserId = 4 });

                //if (loadPhotos)
                //{
                    user.Photos = this._photosService.GetPhotos(user.Id);
                //}
                

                return user;
            }
        }

        public IList<User> GetUsers()
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                var query = @"
                SELECT  [Id]
                      ,[Username]
                      ,[Password]
                      ,[Gender]
                      ,[DateOfBirth]
                      ,[KnownAs]
                      ,[Created]
                      ,[LastActive]
                      ,[Introduction]
                      ,[LookingFor]
                      ,[IsActive]
                      ,[Interests]
                      ,[City]
                      ,[Country]
                  FROM [Users]
                ";

                return new List<User>();
                var users = connection.Query<User>(query).ToList();

                return users;
            }
        }

        public void UpdateUser(int id, User user)
        {
            user.Id = id;

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                var query = @"
                        UPDATE [Users]
                           SET [Username] = @Username
                              ,[Password] = @Password
                              ,[Gender] = @Gender
                              ,[DateOfBirth] = @DateOfBirth
                              ,[KnownAs] = @KnownAs
                              ,[LastActive] = @LastActive
                              ,[Introduction] = @Introduction
                              ,[LookingFor] = @LookingFor
                              ,[IsActive] = @IsActive
                              ,[Interests] = @Interests
                              ,[City] = @City
                              ,[Country] = @Country
                         WHERE Id = @Id
                        ";

                connection.Execute(query, user);
            }
        }
    }
}
