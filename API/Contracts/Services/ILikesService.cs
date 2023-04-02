using API.Models.DB;

namespace API.Contracts.Services
{
    public interface ILikesService
    {
        void Create(Like like);
        void Delete(int likerId, int likeeId);

        IList<Like> GetMyLikes(int userId);

        IList<Like> GetMyLikees(int userId);
    }
}
