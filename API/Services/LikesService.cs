using API.Contracts.Services;
using API.Models.DB;

namespace API.Services
{
    public class LikesService : ILikesService
    {
        public void Create(Like like)
        {
            var query = @"INSERT INTO Likes 
                (LikerId, LikeeId)
                VALUES
                (@LikerId,@LikeeId)";

            //using (var connection = )
            //{

            //}
        }

        public void Delete(int likerId, int likeeId)
        {
            throw new NotImplementedException();
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
