using Harkulwant.PalindromeChecker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Service
{
    public interface IPalindromeService
    {
        Task<Response> GetAsync();
        Task<Response> PostAsync(Request request);
    }
}
