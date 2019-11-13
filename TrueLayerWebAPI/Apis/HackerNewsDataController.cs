using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrueLayerWebAPI.Models;
using TrueLayerWebAPI.Repositories;
using TrueLayerWebAPI.Infrastructure;

namespace TrueLayerWebAPI.Controllers
{   
    /// <summary>
    /// API controller for Hacker news data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsDataController : ControllerBase
    {
        INewsDataRepository _newsDataRepository;
        ILogger _logger;

        public HackerNewsDataController(INewsDataRepository newsRepo, ILoggerFactory loggerFactory)
        {
            _newsDataRepository = newsRepo;
            _logger = loggerFactory.CreateLogger(nameof(HackerNewsDataController));
        }

        /// <summary>
        /// Get method to return all hacker news(max : 100)
        /// </summary>
        /// <returns></returns>
        // GET api/HackerNewsData
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Post>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var posts = await _newsDataRepository.GetAllNewsDataAsync();
                return Ok(posts);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        /// <summary>
        /// Get method to return hacker news posts for the number of posts requested
        /// </summary>
        /// <param name="numberofpost"></param>
        /// <returns></returns>
        // GET api/HackerNewsData/5
        [HttpGet("{numberofpost}")]
        [NoCache]
        [ProducesResponseType(typeof(List<Post>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Get(int numberofpost)
        {
            try
            {
                var posts = await _newsDataRepository.GetNewsDataAsync(numberofpost);
                return Ok(posts);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

       
    }
}
