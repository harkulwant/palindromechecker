using Harkulwant.PalindromeChecker.Model.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Repository
{
    public interface IPalindromeRepository
    {
        Task<List<Palindrome>> GetAllAsync();
        Task<Palindrome> InsertAsync(Palindrome palindrome);
    }
}
