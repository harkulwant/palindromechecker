using Harkulwant.PalindromeChecker.Model.Repository;
using Harkulwant.PalindromeChecker.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Service
{
    public class RepositoryService : IRepositoryService
    {

        private readonly IPalindromeRepository _palindromeRepository;

        public RepositoryService(IPalindromeRepository palindromeRepository)
        {
            _palindromeRepository = palindromeRepository;
        }


        public async Task<List<Palindrome>> GetPalindromesAsync()
        {
            return await _palindromeRepository.GetAllAsync();
        }

        public async Task<Palindrome> InsertPalindromeAsync(Palindrome palindrome)
        {
            return await _palindromeRepository.InsertAsync(palindrome);
        }
    }
}
