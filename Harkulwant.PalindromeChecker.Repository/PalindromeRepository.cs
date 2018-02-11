using Harkulwant.PalindromeChecker.Model.Repository;
using Harkulwant.PalindromeChecker.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Repository
{
    public class PalindromeRepository : IPalindromeRepository
    {

        private readonly IDemoDatabaseContext _demoDatabaseContext;

        public PalindromeRepository(IDemoDatabaseContext demoDatabaseContext)
        {
            _demoDatabaseContext = demoDatabaseContext;
        }

        public async Task<List<Palindrome>> GetAllAsync()
        {
            return await _demoDatabaseContext.Palindromes.ToListAsync();
        }

        public async Task<Palindrome> InsertAsync(Palindrome palindrome)
        {
            return await _demoDatabaseContext.SaveAsync<Palindrome>(palindrome);
        }
    }
}
