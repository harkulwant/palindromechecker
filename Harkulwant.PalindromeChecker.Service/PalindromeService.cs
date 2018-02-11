using Harkulwant.PalindromeChecker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PalindromeRepoObj = Harkulwant.PalindromeChecker.Model.Repository.Palindrome;

namespace Harkulwant.PalindromeChecker.Service
{
    public class PalindromeService : IPalindromeService
    {

        private readonly IRepositoryService _repositoryService;

        public PalindromeService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<Response> GetAsync()
        {
            var dbPalindromes = await _repositoryService.GetPalindromesAsync();
            var response = new Response(Guid.NewGuid())
            {
                Palindromes = dbPalindromes?.Select(x => new Palindrome() { Id = x.Id, Value = x.Value }).ToList()
            };

            return response;
        }

        public async Task<Response> PostAsync(Request request)
        {
            var response = ProcessRequest(request);

            if (!response.IsValid)
                return response;

            // todo: implement automapper
            var palindrome = new PalindromeRepoObj()
            {
                Value = request.Value,
                Timestamp = DateTime.UtcNow
            };

            // stored palindrome returned with id
            var dbPalindrome = await _repositoryService.InsertPalindromeAsync(palindrome);
            response.Palindromes.Add(new Palindrome() { Id = dbPalindrome.Id, Value=dbPalindrome.Value });

            return response;
        }


        private Response ProcessRequest(Request request)
        {
            var response = new Response(request.ContextId);
            var palindromeObject = request.Value?.ToLower();

            // assuming that numbers are accepted to be part of the palindrome check
            var cleanupRegex = new Regex("[^a-zA-Z0-9]");
            palindromeObject = cleanupRegex.Replace(palindromeObject, string.Empty);

            if (string.IsNullOrWhiteSpace(palindromeObject))
                response.Errors.Add("invalid input");


            else if (!palindromeObject.SequenceEqual(palindromeObject.Reverse()))
                response.Errors.Add("not a palindrome");

            return response;
        }
    }
}
