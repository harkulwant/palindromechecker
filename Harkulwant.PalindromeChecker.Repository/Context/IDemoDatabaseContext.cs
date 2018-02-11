using Harkulwant.PalindromeChecker.Model.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Repository.Context
{
    // todo: implement repository pattern to use database context more efficiently
    public interface IDemoDatabaseContext
    {
        DbSet<Palindrome> Palindromes { get; set; }
        /// <summary>
        /// Exposing database-context SaveAsync via interface to allow for IOC implementation
        /// todo: implement repo-pattern
        /// </summary>
        /// <typeparam name="T">TEntity to save</typeparam>
        /// <param name="palindrome"></param>
        /// <returns></returns>
        Task<T> SaveAsync<T>(T entity) where T: class;
    }
}
