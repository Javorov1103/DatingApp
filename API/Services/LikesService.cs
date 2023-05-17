using API.Contracts.Services;
using API.Models.DB;
using Dapper;
using System.Data.SqlClient;

namespace API.Services
{
    public class LikesService : ILikesService
    {
        private readonly string _dbConnectionString;

        public LikesService(IConfiguration config)
        {
            _dbConnectionString = config.GetConnectionString("DefaultConnection");
        }
        public void Create(Like like)
        {
            var query = @"INSERT INTO Likes 
                (LikerId, LikeeId)
                VALUES
                (@LikerId,@LikeeId)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(query, like);
            }
        }

        public void Delete(int likerId, int likeeId)
        {
            var query = @"DELETE FROM Likes
            WHERE LikerId = @LikerId AND LikeeId = @LikeeId";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(query, new {LikerId = likerId, LikeeId = likeeId });
            }
        }

        public IList<Like> GetMyLikees(int userId)
        {
            throw new NotImplementedException();
        }

        public IList<Like> GetMyLikes(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
