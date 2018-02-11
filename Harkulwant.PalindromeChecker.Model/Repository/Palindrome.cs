using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harkulwant.PalindromeChecker.Model.Repository
{
    public class Palindrome
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
