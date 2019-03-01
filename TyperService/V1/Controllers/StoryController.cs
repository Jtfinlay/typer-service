using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TyperService.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string GetStory()
        {
            return _configuration["BestAnimal"];
        }

        // POST api/story
        [HttpPost]
        public void CreateStory([FromBody] string value)
        {

        }
    }
}
