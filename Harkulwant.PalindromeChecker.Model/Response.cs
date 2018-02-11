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
        }

        public Guid ContextId { get; private set; }
        public int PalindromeId { get; set; }
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
