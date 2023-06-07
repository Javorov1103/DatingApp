using API.Contracts.Services;
using API.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotosService _photosService;

        public PhotosController(IPhotosService photosService)
        {
            _photosService = photosService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var photos = this._photosService.GetPhotos();

            if (photos.Count > 30)
            {
                return BadRequest("Too much photos");
            }

            return Ok(photos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Wrong id provided!");
            }

            var photo = this._photosService.GetPhoto(id);

            if(photo == null)
            {
                return NotFound("Photo with the provided id was not found");
            }

            return Ok(photo);
        }

        [HttpGet("users-photos/{userId:int}")]
        public IActionResult GetUsersPhoto(int userId)
        {
            var photos = this._photosService.GetPhotos(userId);

            return Ok(photos);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdatePhoto(int id, [FromBody]Photo model)
        {
            this._photosService.UpdatePhoto(id, model);

            return Ok();
        }
    }
}
