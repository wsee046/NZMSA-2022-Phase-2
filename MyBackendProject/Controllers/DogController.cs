using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using MyBackendProject.Models;
using Newtonsoft.Json;

/// <summary>
/// This is the dog controller
/// </summary>
namespace MyBackendProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {
        private readonly HttpClient _client;

        public DogController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("dog");
        }

        /// <summary>
        /// This endpoint gets a random dog
        /// </summary>
        [HttpGet]
        [Route("getRandomDog")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetRandomDog()
        {
            var res = await _client.GetAsync("/v1/images/search");
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }

        /// <summary>
        /// This endpoint gets an array of breeds containing the search keyword
        /// </summary>
        /// <param name="breed">The breed to search for. Can enter part of the name</param>
        /// <returns>An array of breeds which match the query parameter passed in. Each element of the array represents a breed and contains information about the breed</returns>
        [HttpGet]
        [Route("getBreed")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBreed(String breed)
        {
            var res = await _client.GetAsync("/v1/breeds/search?q=" + breed);
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }

        /// <summary>
        /// Sends an object in the body containing the "image_id" you want to Vote on, and "value":1 to Up Vote, or "value":0 to Down Vote
        /// </summary>
        [HttpPost]
        [Route("createVote")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> postVote(Vote vote)
        { 
            var myContent = JsonConvert.SerializeObject(vote);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var result = await _client.PostAsync("/v1/votes", byteContent);
            string content = result.Content.ReadAsStringAsync().Result;
            return Ok(content);

        }

        /// <summary>
        ///  Gets a specified vote by id
        /// </summary>
        /// <param name="id">Id of the vote to search for</param>
        [HttpGet]
        [Route("getVote")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetById(int id) {
            var res = await _client.GetAsync("/v1/votes/" + id);
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }


        /// <summary>
        /// Deletes a vote specified by the input id
        /// </summary>
        /// <param name="voteId">Id of the vote to delete</param>
        [HttpDelete]
        [Route("deleteVote")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteVoteById(string voteId) 
        {
                var res = await _client.DeleteAsync("/v1/votes/" + voteId);
                var content = await res.Content.ReadAsStringAsync();
                return Ok(content);
        }
    }
}
