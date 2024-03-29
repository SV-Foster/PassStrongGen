/***

Copyright 2023, SV Foster. All rights reserved.

License:
    This program is free for personal, educational and/or non-profit usage    

Revision History:

***/

using System.Text;
using System.Security.Cryptography;
using System.Numerics;


namespace DataGeneratorRandom
{
    public class TDataGeneratorRandom
    {
        public const string USASCIILettersUp = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string USASCIILettersDown = @"abcdefghijklmnopqrstuvwxyz";
        public const string USASCIIDigits = @"0123456789";
        public const string USASCIISpecialChars = @" !""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";
        public const string USASCIIPrintable = USASCIILettersUp + USASCIILettersDown + USASCIIDigits + USASCIISpecialChars;
        public const string USASCIIBrackets = @"()[]{}";
        public const string USASCIIMath = @"%*+-/<=>^~";
        public const string USASCIIPunctuation = @"!""',.:;?`";


        public static string StringByABCGen(in int length, in string ABC)
        {
            var StrBuilder = new StringBuilder();
            byte[] data = new byte[length + 1];


            using (var RandGenerator = new RNGCryptoServiceProvider()) // actually CryptGenRandom() WinAPI call
            {
                RandGenerator.GetBytes(data);
            }
            data[length] = 0; // make sure the number is positive
            var Bignumber = new BigInteger(data);

            for (int i = length; i > 0; i--)
            {
                int idx = (int)(Bignumber % ABC.Length);
                Bignumber /= ABC.Length;

                StrBuilder.Append(ABC[idx]);
            }

            return StrBuilder.ToString();
        }

        public static string StringBytesHEXGet(in int length)
        {
            var StrBuilder = new StringBuilder();
            byte[] data = new byte[length];


            using (var RandGenerator = new RNGCryptoServiceProvider()) // actually CryptGenRandom() WinAPI call
            {
                RandGenerator.GetBytes(data); 
            }

            foreach (var n in data)
                StrBuilder.Append(n.ToString("X2"));

            return StrBuilder.ToString();
        }

    }
}
