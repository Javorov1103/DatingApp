using API.Models.DB;

namespace API.Contracts.Services
{
    public interface IPhotosService
    {
        IList<Photo> GetPhotos(int userId);
        IList<Photo> GetPhotos();
        Photo GetPhoto(int id);

        void UpdatePhoto(int id,Photo photo);

        bool CreatePhoto(Photo photo);

        void DeletePhoto(int id);
    }
}
