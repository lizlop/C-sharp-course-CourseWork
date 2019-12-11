# Vigenere Encoder
ASP.NET Core web application to encode and decode text using Vigenere cipher. This app will help you encrypt or decrypt text or text files such as .txt and .docx
## Built With
* ASP.NET Core Razor Pages
* [Open XML SDK](https://github.com/OfficeDev/Open-XML-SDK)
* [Encoding.CodePages](https://github.com/dotnet/corefx)
### Prerequisites
* IIS Express
## Getting Started
#### 1. Put your text into the text area.

Note: if you want to use text from the text area, make sure there is no file uploaded, or that file will be encrypted/decrypted instead.

#### OR

#### 1. Upload a text file

The app accept only files with .txt or .docx extensions.

#### 2. Write down the key for your cipher.

A key is required for Vigenere cipher. The key should consist only of your alphabet characters.

#### 3. Choose an action you want to perform with the text

#### 4. (optional) Use your own alphabet

The Cyrillic alphabet (абвгдеёжзийклмнопрстуфхцчшщъыьэюя) is used by default. To use your own alphabet make sure you ticked the checkbox. 
Alphabet may consist of any symbols. Note that case of the letters in alphabet doesn't matter - the resulting text preserve case of the original text.

#### 5. Press the button "Get the result!"

The resulting text has only alphabet characters being encrypted/decrypted, other symbols stay unchanged. 
If you putted the original text in the text area, the result would be shown in the text area below the button. 
In case of a file, after pressing the button you will get the result file to download.

## Project structure

* CourseWork:
  * Models:
    * [Encoder.cs](https://github.com/lizlop/C-sharp-course-CourseWork/blob/master/CourseWork/Models/Encoder.cs) - class that implements algorithm of the cipher. It is model for the `Index` page.
  * Pages:
    * [Index.cshtml.cs](https://github.com/lizlop/C-sharp-course-CourseWork/blob/master/CourseWork/Pages/Index.cshtml.cs) - class that contains logic of binding properties and request implementaion.
* CourseWorkTest:
  * [EncoderTests.cs](https://github.com/lizlop/C-sharp-course-CourseWork/blob/master/CourseWorkTest/EncoderTests.cs) - class that contains tests for the `Encoder` class to check correctness of cipher implementation and getting result on different input parameters. 
