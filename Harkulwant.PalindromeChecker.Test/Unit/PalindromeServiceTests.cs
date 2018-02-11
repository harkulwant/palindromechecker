using Harkulwant.PalindromeChecker.Model;
using Harkulwant.PalindromeChecker.Model.Repository;
using Harkulwant.PalindromeChecker.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            _repositoryService.InsertPalindromeAsync(Arg.Any<Palindrome>()).ReturnsForAnyArgs(new Palindrome() { Id = 99, Value = "Was it a cat I saw?" });
            _repositoryService.GetPalindromesAsync().ReturnsForAnyArgs(new List<Palindrome> { new Palindrome() { Value = "Was it a cat I saw?" },
                new Palindrome() { Value = "Don't nod." }, new Palindrome() { Value = "Radar" }});

            _palindromeService = Substitute.ForPartsOf<PalindromeService>(_repositoryService);
        }

        [TestMethod]
        public async Task PalindromeService_GetAsync_GetsAllStoredPalindromes()
        {
            var palindromes = await _palindromeService.GetAsync();

            Assert.AreEqual(3, palindromes.Count);
        }

        [TestMethod]
        public async Task PalindromeService_ValidPalindromeRequest_ReturnsResponseWithNewPalindromeId()
        {
            var response = await _palindromeService.PostAsync(new Request() { Value= "Was it a cat I saw?" });

            Assert.IsTrue(response.IsValid);
            Assert.AreEqual(99, response.PalindromeId);
        }

        [TestMethod]
        public async Task PalindromeService_InValidPalindromeRequest_ReturnsResponseWithErrors()
        {
            var response = await _palindromeService.PostAsync(new Request() { Value = "Was it a cat I saw? Nope" });

            Assert.IsFalse(response.IsValid);
            Assert.AreEqual(0, response.PalindromeId);
        }
    }
}
