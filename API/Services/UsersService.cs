using API.Contracts.Services;
using API.Models.DB;
using System.Data.SqlClient;
using Dapper;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        private readonly string _dbConnectionString;
        public UsersService(IConfiguration config)
        {
            _dbConnectionString = config.GetConnectionString("DefaultConnection");
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

        public User GetUserById(int id)
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
                      FROM [Users]
                      WHERE Id = @Id
                    ";

                var user = connection.QueryFirstOrDefault<User>(query, new { Id = id });

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

                var users = connection.Query<User>(query).ToList();

                return users;
            }
        }
    }
}
