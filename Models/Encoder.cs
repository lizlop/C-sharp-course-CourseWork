using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Encoder : IValidatableObject
    {
        private string alphabet;
        public string Alphabet {
            get 
            { return alphabet ?? "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"; }
            set
            { alphabet = value; }
        }
        public enum Mode { ENCRYPT, DECRYPT};
        [Required(ErrorMessage = "A key is required for this cipher")]
        public string Key { get; set; }
        private string text;
        public string Text { 
            get 
            { return text ?? (text = FileParse()); } 
            set 
            { text = value; } 
        }
        public IFormFile File { get; set; }
        private string FileParse()
        {
            string str = "";
            return str;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((Text == null || Text == "") && File == null)
            {
                yield return new ValidationResult("You should enter the text or download a text file", new List<string> { "Text", "File" });
            }
        }
        private void CheckKey()
        {
            foreach (var c in Key) if (!Alphabet.Contains(c)) throw new Exception("Key contains non-alphabet characters");
        }
        public string Vigener(Mode mode, bool checkKey)
        {
            if (checkKey) CheckKey();
            int charIndex; // index in alphabet
            int msgIndex = 0; // index in message letters
            int index = 0; // index in all message chars
            int keyIndex; // index in key
            StringBuilder stringBuilder = new StringBuilder(Text);
            foreach (var character in Text)
            {
                if (!(Alphabet.Contains(Char.ToUpper(character)) || Alphabet.Contains(Char.ToLower(character)))) { index++; continue; }
                //shift of alphabet for encrypting and index of char in non-shifted alphabet for decrypting 
                charIndex = Alphabet.IndexOf(Char.ToLower(character));
                // index of column in shifted alphabet
                keyIndex = Alphabet.IndexOf(Key[msgIndex % Key.Length]);
                if (mode == Mode.ENCRYPT) charIndex = (keyIndex + charIndex) % Alphabet.Length;
                else charIndex = (charIndex - keyIndex + Alphabet.Length) % Alphabet.Length;
                stringBuilder.Replace(character, Char.IsUpper(character) ? Char.ToUpper(Alphabet[charIndex]) : Char.ToLower(Alphabet[charIndex]), index, 1);
                msgIndex++; index++;
            }
            return stringBuilder.ToString();
        }
    }
}
