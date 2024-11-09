using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Helpers
{
    public class StringHelper
    {

        private static Random random = new Random();

        public static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string NumberToLetter(int number)
        {
            // Chuyển đổi số thành mã ASCII và trừ đi mã ASCII của ký tự 'A' (65)
            return ((char)(number + 64)).ToString();
        }

        public static string GenerateCode(int length)
        {
            int asciiStart = 33;
            int asciiEnd = 126;
            char[] randomArray = new char[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int rd;
                do
                {
                    rd = random.Next(asciiStart, asciiEnd + 1);
                    randomArray[i] = (char)rd;
                } while (rd == 39);
            }
            string randomString = new string(randomArray);
            return randomString;
        }

        public static string GenerateCodeFormat(int length, string inputString)
        {
            //string currentDate = DateTime.Now.ToString("yyyyMMdd");

            int asciiStart = 33;
            int asciiEnd = 126;
            char[] randomArray = new char[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int rd;
                do
                {
                    rd = random.Next(asciiStart, asciiEnd + 1);
                    randomArray[i] = (char)rd;
                } while (rd == 39);
            }
            string randomString = new string(randomArray);

            //string result = $"{currentDate}-{inputString}-{randomString}";
            string result = $"{inputString}-{randomString}";


            return result;
        }
        public static string GenerateTrackingNumberFormat(int length, string inputString)
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomString= new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string result = $"{currentDate}{inputString}{randomString}";


            return result;
        }

        public static string GenerateCodeNumberFormat(int length, string inputString)
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");

            int asciiStart = 48; // Ký tự '0'
            int asciiEnd = 57;   // Ký tự '9'

            char[] randomArray = new char[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int rd;
                rd = random.Next(asciiStart, asciiEnd + 1);
                randomArray[i] = (char)rd;
            }
            string randomString = new string(randomArray);

            //string result = $"{currentDate}-{inputString}-{randomString}";
            string result = $"{inputString}-{randomString}";


            return result;
        }


        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            string inputlower = input.ToLower().Trim();
            string firstLetter = inputlower.Substring(0, 1);
            string restOfString = inputlower.Substring(1);

            string capitalizedString = firstLetter.ToUpper(CultureInfo.CurrentCulture) + restOfString;

            return capitalizedString;
        }
        public static string ConvertFirstLetterToUpper(string userName)
        {
            string[] words = userName.Split(' ');
            System.Text.StringBuilder formattedNameBuilder = new System.Text.StringBuilder();
            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    formattedNameBuilder.Append(char.ToUpper(word[0]) + word.ToLower().Substring(1)).Append(" ");
                }
            }
            return formattedNameBuilder.ToString().Trim();
        }
        public static string FormatWord(string userName)
        {
            string[] words = userName.Trim().Split(' ');

            System.Text.StringBuilder formattedNameBuilder = new System.Text.StringBuilder();
            if (words[0].Length == 0)
            {
                throw new ArgumentException("Does not exist char or text to FormatWord");
            }
            else if (words[0].Length == 1)
            {
                formattedNameBuilder.Append(char.ToUpper(words[0][0])).Append(" ");
            }
            else
            {
                formattedNameBuilder.Append(char.ToUpper(words[0][0]) + words[0].ToLower().Substring(1)).Append(" ");
            }
            for (int i = 1; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    formattedNameBuilder.Append(words[i].ToLower()).Append(" ");
                }
            }
            return formattedNameBuilder.ToString().Trim();
        }
        public static string RemoveDiacritics(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
