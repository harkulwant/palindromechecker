using Harkulwant.PalindromeChecker.Model.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Service
{
    public interface IRepositoryService
    {
        Task<List<Palindrome>> GetPalindromesAsync();
        Task<Palindrome> InsertPalindromeAsync(Palindrome palindrome);
    }
}
