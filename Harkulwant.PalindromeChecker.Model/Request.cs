using System;

namespace Harkulwant.PalindromeChecker.Model
{
    public class Request
    {
        public Request()
        {
            ContextId = Guid.NewGuid();
        }

        public Guid ContextId { get; private set; }
        public string Value { get; set; }
    }
}
