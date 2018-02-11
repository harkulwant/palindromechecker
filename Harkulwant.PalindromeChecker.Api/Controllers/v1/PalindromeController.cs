using Harkulwant.PalindromeChecker.Model;
using Harkulwant.PalindromeChecker.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Api.Controllers.v1
{
    /// <summary>
    /// Controller to manage all palindrome actions
    /// </summary>
    [Route("api/v1/[controller]")]
    public class PalindromeController : Controller
    {
        private readonly IPalindromeService _palindromeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palindromeSerivce"></param>
        public PalindromeController(IPalindromeService palindromeSerivce)
        {
            _palindromeService = palindromeSerivce;
        }

        /// <summary>
        /// Get all palindromes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<string>> Get()
        {
            return await _palindromeService.GetAsync();
        }


        /// <summary>
        /// Post the palindrome and save if ok
        /// </summary>
        /// <param name="request"></param>
        /// <returns>http status code</returns>
        /// <response code="201"></response>
        /// <response code="400"></response>   
        [HttpPost]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AcceptedResult), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> Post([FromBody]Request request)
        {
            try
            {
                var response = await _palindromeService.PostAsync(request);

                if (response.IsValid)
                    return Accepted();

            }
            catch (System.Exception ex)
            {
                // todo: exception logging & handling
            }

            return new BadRequestResult();
        }
    }
}
