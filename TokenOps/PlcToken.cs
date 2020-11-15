using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer.TokenOps
{
    public class PlcToken
    {

        public TokenType TokenType { get; set; }
        public string Value { get; set; }

        public string TokenName { get; set; }

        public PlcToken(TokenType tokenType)
        {
            this.TokenType = tokenType;
            this.Value = string.Empty;
        }

        public PlcToken(TokenType tokenType, string value)
        {
            this.TokenType = tokenType;
            this.Value = value;
            this.TokenName = Enum.GetName(typeof(TokenType), TokenType);
        }

        public PlcToken Clone()
        {
            return new PlcToken(this.TokenType, this.Value);
        }
    }
}
