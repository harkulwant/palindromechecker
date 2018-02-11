using System;
using System.Collections.Generic;

namespace Harkulwant.PalindromeChecker.Model
{
    public class Response
    {
        public Response(Guid contextId)
        {
            ContextId = contextId;
            Errors = new List<string>();
            Palindromes = new List<Palindrome>();
        }

        public Guid ContextId { get; private set; }
        public IList<Palindrome> Palindromes { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid
        {
            get
            {
                return Errors.Count == 0;
            }
        }
    }
}
