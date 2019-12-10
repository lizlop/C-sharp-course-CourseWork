using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace CourseWork.Models
{
    public class Encoder : IValidatableObject
    {
        private string alphabet;
        public string Alphabet {
            get 
            { return alphabet ?? (alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"); }
            set
            { alphabet = CorrectAlphabet(value.ToLower()); }
        }
        public enum Mode { ENCRYPT, DECRYPT};
        private string key;

        [Required(ErrorMessage = "A key is required for this cipher")]
        public string Key {
            get
            { return key; }
            set
            { key = value.ToLower(); } 
        } 
        public string Text { get; set; }
        public IFormFile File { get; set; }
        private string CorrectAlphabet(string str)
        {
            StringBuilder alphabet = new StringBuilder();
            foreach (var c in str) if (!alphabet.ToString().Contains(c)) alphabet.Append(c);
            return alphabet.ToString();
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!CheckKey())
            {
                yield return new ValidationResult("The key should consist only of alphabet characters: \"" + Alphabet + "\"", new List<string> { "Key" });
            }
            if ((Text == null || Text == "") && (File == null || File.Length == 0))
            {
                yield return new ValidationResult("You should enter the text or upload a text file", new List<string> { "Text", "File" });
            }
        }
        private bool CheckKey()
        {
            foreach (var c in Key) if (!Alphabet.Contains(c)) return false;
            return true;
        }
        private bool CheckEncoding(string line)
        {
            foreach (char ch in line) if (ch == 65533) return false;
            return true;
        }
        public string Vigener(Mode mode)
        {
            if (Text == null)
            {
                if (File == null || File.Length == 0) throw new Exception("There is no text to encode");
                else return VigenerFromFile(mode, File);
            }
            else return VigenerFromString(mode, Text);
        }
        public string VigenerFromFile(Mode mode, IFormFile File)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (File.FileName.Contains(".txt"))
            {
                bool isUTF8 = true;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var stream = new StreamReader(File.OpenReadStream()))
                {
                    string line = stream.ReadToEnd();
                    if ((isUTF8 = CheckEncoding(line))) stringBuilder.AppendLine(line);
                }
                if (!isUTF8) 
                    using (var stream = new StreamReader(File.OpenReadStream(), Encoding.GetEncoding("windows-1251")))
                        stringBuilder.Append(stream.ReadToEnd());
                stringBuilder = new StringBuilder(VigenerFromString(mode, stringBuilder.ToString()));
                using var fileStream = new StreamWriter("wwwroot\\uploads\\document.txt");
                fileStream.WriteLine(stringBuilder.ToString());
            }
            else if (File.FileName.Contains(".docx"))
            {
                using (var reader = File.OpenReadStream())
                using (var fileStream = new FileStream("wwwroot\\uploads\\document.docx", FileMode.Create))
                    reader.CopyTo(fileStream);
                using (var fileStream = new FileStream("wwwroot\\uploads\\document.docx", FileMode.Open))
                using (WordprocessingDocument document = WordprocessingDocument.Open(fileStream, true))
                {
                    var paragraphs = document.MainDocumentPart.Document.Body.Elements<Paragraph>();
                    foreach (var para in paragraphs)
                        foreach (var run in para.Elements<Run>())
                            foreach (var text in run.Elements<Text>())
                                stringBuilder.AppendLine(text.Text = VigenerFromString(mode, text.Text));
                }
            }
            else throw new Exception("Invalid file extention");
            return stringBuilder.ToString();
        }
        public string VigenerFromString(Mode mode, string Text)
        {
            if (!CheckKey()) throw new Exception("Incorrect key value");
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
