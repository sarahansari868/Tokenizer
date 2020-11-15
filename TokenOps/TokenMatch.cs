using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer.TokenOps
{
    public class TokenMatch
    {
        public bool IsMatch { get; set; }
        public TokenType TokenType { get; set; }
        public string Value { get; set; }
    }
}
