using Newtonsoft.Json;
using System;
using System.IO;

namespace Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            using (var sr = new StreamReader("Test_Cases/input.txt"))
            {
                input = sr.ReadToEnd();
            }

            if(input.Length > 0)
            {

                var tokenizer = new TokenOps.Tokenizer();
                var tokenSeq = tokenizer.Tokenize(input);


                var json = JsonConvert.SerializeObject(tokenSeq);

                Console.WriteLine("Input: \n" + input);

                Console.WriteLine("Output: \n" + json);
               

            }
        }
    }
}
