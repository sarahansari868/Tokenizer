using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tokenizer.TokenOps
{
    public class TokenDefinition
    {
        private Regex _regex;
        private readonly TokenType tokenType;

        public TokenDefinition(TokenType tokenType, string regex)
        {
            this._regex = new Regex(regex); // keeping our language case sensitive
            this.tokenType = tokenType;
        }

        public TokenMatch MatchToken(string input)
        {
            var matchToken = this._regex.Match(input);

            if(matchToken.Success)
            {
                var token = new TokenMatch()
                {
                    IsMatch = true,
                    TokenType = this.tokenType,
                    Value = matchToken.Value
                };

                return token;
            }
            else
            {
                return new TokenMatch() { IsMatch = false };
            }
        }
    }

}
