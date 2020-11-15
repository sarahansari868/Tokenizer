using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tokenizer.TokenOps
{
    public class Tokenizer
    {
        private List<TokenDefinition> _tokenDefinitions;

        public Tokenizer()
        {
            this._tokenDefinitions = new List<TokenDefinition>();

            /*
             * perl identifier assumption: 
             * A Perl variable name starts with either $, @ or % followed 
             * by zero or more letters, underscores, and digits (0 to 9).
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.PlIdentifiers, "^(\\$|&|%)([a-zA-Z0-9_]*)$"));

            /*
             * Java String literal assumption:
             * Must start with a " and end with a "
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.JString, "^([a-zA-Z\\s])*$"));


            /*
             * C style integer literal assumption:
             * Must begin with a ' and end with a single quote and have 1 char from 0-9
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.CInteger, "^\'([0-9])\'$"));


            /*
             * C style char literal assumption:
             * Must begin with a ' and end with a single quote and have 1 char from a-z or A-Z
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.CChar, "^\'([a-zA-Z])\'$"));


            /*
             * C style floating point literal
             * 
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.CFloat, "^[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?$"));


            /*
             * semi colon (;) has been taken as line terminator in our language
             * 
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.LineTerminator, "^;$"));

            /*
             * Addition operator is denoted by _addtn_
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.AdditionOp, "^_addtn_$"));

            /*
             * Assignment operator is denoted by _assign_
             */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.AssignmentOp, "^_assign_$"));

            /*
            * Subtraction operator is denoted by _subtract_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.SubtractionOp, "^_subtract_$"));

            /*
            * Multiplication operator is denoted by _multiply_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.AssignmentOp, "^_multiply_$"));

            /*
            * Increment operator is denoted by _++_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.IncrementOp, "^_\\+\\+_$"));

            /*
            * Decrement operator is denoted by _--_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.DecrementOp, "^_--_$"));

            /*
            * Modulo operator is denoted by _mod_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.ModuloOp, "^_mod_$"));

            /*
            * Logical and operator is denoted by _&&_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.LAndOp, "^_&&_$"));

            /*
            * Logical or operator is denoted by _||_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.LOrOp, "^_\\|\\|_$"));

            /*
            * Logical not operator is denoted by _<>_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.LNot, "^_<>_$"));

            /*
            * Open code block is denoted by _{_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.OpenCodeBlock, "^_{_$"));

            /*
            * Close code block is denoted by _}_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.CloseCodeBlock, "^_}_$"));

            /*
            * Open function param is denoted by _(_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.OpenFnParam, "^_\\(_$"));

            /*
            * Close function param is denoted by _)_
            */
            _tokenDefinitions.Add(new TokenDefinition(TokenType.CloseFnParam, "^_\\)_$"));
        }

        public List<PlcToken> Tokenize(string input)
        {
            var tokens = new List<PlcToken>();

            List<string> symbols;

            using (MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(input)))
            {
                using (TextFieldParser tfp = new TextFieldParser(ms))
                {
                    tfp.Delimiters = new string[] { " ", ";" };
                    tfp.HasFieldsEnclosedInQuotes = true;
                    string[] output = tfp.ReadFields();
                    symbols = new List<string>(output);
                }
            }

            foreach(var symbol in symbols)
            {
                var match = FindMatch(symbol);
                if (match.IsMatch)
                {
                    tokens.Add(new PlcToken(match.TokenType, match.Value));
                }
            }

            tokens.Add(new PlcToken(TokenType.SequenceTerminator, string.Empty));

            return tokens;

        }

        private TokenMatch FindMatch(string input)
        {
            foreach (var tokenDefinition in _tokenDefinitions)
            {
                var match = tokenDefinition.MatchToken(input);
                if (match.IsMatch)
                    return match;
            }

            return new TokenMatch() { IsMatch = false };
        }


    }
}
