using Harkulwant.PalindromeChecker.Model;
using Harkulwant.PalindromeChecker.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PalindromeRepoObj = Harkulwant.PalindromeChecker.Model.Repository.Palindrome;

namespace Harkulwant.PalindromeChecker.Test.Unit
{
    [TestClass]
    public class PalindromeServiceTests
    {
        private IPalindromeService _palindromeService;

        [TestInitialize]
        public void Initialize()
        {
            var _repositoryService = Substitute.For<IRepositoryService>();
            _repositoryService.InsertPalindromeAsync(Arg.Any<PalindromeRepoObj>()).ReturnsForAnyArgs(new PalindromeRepoObj() { Id = 99, Value = "Was it a cat I saw?" });
            _repositoryService.GetPalindromesAsync().ReturnsForAnyArgs(new List<PalindromeRepoObj> { new PalindromeRepoObj() { Value = "Was it a cat I saw?" },
                new PalindromeRepoObj() { Value = "Don't nod." }, new PalindromeRepoObj() { Value = "Radar" }});

            _palindromeService = Substitute.ForPartsOf<PalindromeService>(_repositoryService);
        }

        [TestMethod]
        public async Task PalindromeService_GetAsync_GetsAllStoredPalindromes()
        {
            var response = await _palindromeService.GetAsync();

            Assert.AreEqual(3, response.Palindromes.Count);
        }

        [TestMethod]
        public async Task PalindromeService_ValidPalindromeRequest_ReturnsResponseWithNewPalindromeId()
        {
            var response = await _palindromeService.PostAsync(new Request() { Value= "Was it a cat I saw?" });

            Assert.IsTrue(response.IsValid);
            Assert.AreEqual(99, response?.Palindromes?.FirstOrDefault()?.Id);
        }

        [TestMethod]
        public async Task PalindromeService_InValidPalindromeRequest_ReturnsResponseWithErrors()
        {
            var response = await _palindromeService.PostAsync(new Request() { Value = "Was it a cat I saw? Nope" });

            Assert.IsFalse(response.IsValid);
            Assert.AreEqual(0, response?.Palindromes?.Count);
        }
    }
}
