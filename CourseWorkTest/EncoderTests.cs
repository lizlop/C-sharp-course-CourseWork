using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseWork.Models;
using System;

namespace CourseWorkTest
{
        [TestClass]
        public class EncoderTests
        {
            [TestMethod]
            public void EncryptRussian()
            {
                Encoder encoder = new Encoder();
                encoder.Key = "скорпион";
                StringAssert.Equals(encoder.VigenerFromString(Encoder.Mode.ENCRYPT, "поздравляю"), "бщцфаирщри");
                encoder.Key = "лимон";
                StringAssert.Equals(encoder.VigenerFromString(Encoder.Mode.ENCRYPT, "Привет мир"), "Ыщхртю ххя");
                encoder.Key = "кларнет";
                StringAssert.Equals(encoder.VigenerFromString(Encoder.Mode.ENCRYPT, "Карл у Клары украл кораллы"), "Хлрь б Пюкьы дшхтц цобнрюё");
            }
            private string GenerateString()
            {
                Random random = new Random();
                var str = new System.Text.StringBuilder();
                int length = random.Next(1, 250);
                for (int i = 0; i < length; i++) str.Append((char)random.Next(0, 'я' + 1));
                return str.ToString();
            }
            private string GenerateRussianString()
            {
                Random random = new Random();
                var str = new System.Text.StringBuilder();
                int length = random.Next(1, 250);
                for (int i = 0; i < length; i++) str.Append((char)random.Next('А', 'я' + 1));
                return str.ToString();
            }
            private string GenerateKey(string alphabet)
            {
                Random random = new Random();
                var str = new System.Text.StringBuilder();
                int length = random.Next(1, 250);
                for (int i = 0; i < length; i++) str.Append(alphabet[random.Next(0, alphabet.Length)]);
                return str.ToString();
            }
            [TestMethod]
            public void EncryptDecryptRussian()
            {
                Encoder encoder = new Encoder();
                for (int i = 0; i < 5; i++)
                {
                    encoder.Key = GenerateRussianString();
                    string str = GenerateRussianString();
                    StringAssert.Equals(str, encoder.VigenerFromString(Encoder.Mode.DECRYPT, encoder.VigenerFromString(Encoder.Mode.ENCRYPT, str)));
                }
            }
            [TestMethod]
            public void EncryptDecryptAll()
            {
                Encoder encoder = new Encoder();
                for (int i = 0; i < 5; i++)
                {
                    encoder.Key = GenerateRussianString();
                    string str = GenerateString();
                    StringAssert.Equals(str, encoder.VigenerFromString(Encoder.Mode.DECRYPT, encoder.VigenerFromString(Encoder.Mode.ENCRYPT, str)));
                }
            }
            [TestMethod]
            public void EncryptDecryptWithAlphabet()
            {
                Encoder encoder = new Encoder();
                for (int i = 0; i < 5; i++)
                {
                    encoder.Alphabet = GenerateString();
                    encoder.Key = GenerateKey(encoder.Alphabet);
                    string str = GenerateString();
                    StringAssert.Equals(str, encoder.VigenerFromString(Encoder.Mode.DECRYPT, encoder.VigenerFromString(Encoder.Mode.ENCRYPT, str)));
                }
            }
        }
}
