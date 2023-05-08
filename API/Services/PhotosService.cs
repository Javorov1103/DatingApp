using API.Contracts.Services;
using API.Models.DB;
using Dapper;
using System.Data.SqlClient;

namespace API.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly string _dbConnectionString;

        public PhotosService(IConfiguration config)
        {
            _dbConnectionString = config.GetConnectionString("DefaultConnection");
        }
        public bool CreatePhoto(Photo photo)
        {
            throw new NotImplementedException();
        }

        public void DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhoto(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Photo> GetPhotos(int userId)
        {
            var query = @"
                SELECT [Id]
                      ,[Url]
                      ,[Description]
                      ,[DateAdded]
                      ,[IsMain]
                      ,[PublicId]
                      ,[UserId]
                  FROM [Photos]
                  WHERE UserId = @UserId
            ";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                return connection.Query<Photo>(query, new { UserId = userId}).ToList();
            }
        }

        public IList<Photo> GetPhotos()
        {
            throw new NotImplementedException();
        }

        public void UpdatePhoto(int id, Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
