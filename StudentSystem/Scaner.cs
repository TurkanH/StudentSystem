using System;
using System.Globalization;

namespace StudentSystem
{
    internal class Scaner
    {
        internal static int ReadInteger(string caption)
        {
        l1:
            Console.Write(caption);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            goto l1;
        }

        internal static string ReadString(string caption)
        {
        l1:
            Console.Write(caption);
            string value = Console.ReadLine();    
            if (string.IsNullOrEmpty(value))    
            goto l1;

          return value;
        }

        internal static DateTime ReadDateTime(string caption)
        {
        l1:
            Console.Write($"{caption} [yyyy.MM.dd] ");
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy.MM.dd",null, DateTimeStyles.None,out DateTime date))
            {
                return date;
            }
            goto l1;
        }



        internal static int ReadInteger()
        {
            throw new NotImplementedException();
        }
    }    
}
