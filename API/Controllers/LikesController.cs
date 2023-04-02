using API.Contracts.Services;
using API.Models.DB;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesSerivce;
        public LikesController(ILikesService likesService)
        {
            _likesSerivce = likesService;
        }

        [HttpPost]
        public IActionResult Create(Like like)
        {
            _likesSerivce.Create(like);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int likerId, int likeeId)
        {
            _likesSerivce.Delete(likerId, likeeId);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetMyLikes(int userId)
        {
            _likesSerivce.GetMyLikes(userId);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetMyLikees(int userId)
        {
            _likesSerivce.GetMyLikees(userId);
            return Ok();
        }


    }
}
