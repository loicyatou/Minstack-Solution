using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using Microsoft.AspNetCore.Mvc;

class TestClass
{
    public static int MyAtoi(string s)
    {
        var cleanString = s.TrimStart();

        bool isNegative = cleanString.StartsWith("-");

        cleanString = isNegative ? cleanString.Remove(0, 1) : cleanString.StartsWith("+") ? cleanString.Remove(0, 1) : cleanString;

        cleanString = cleanString.TrimStart('0');

        cleanString = CheckForAlphaChar(cleanString);

        if (cleanString.Length == 0)
        {
            return 0;
        }

        int convertedString;

        try
        {
            convertedString = checked(Convert.ToInt32(cleanString));

            return isNegative ? convertedString * -1 : convertedString;
        }
        catch (OverflowException ex)
        {
            return isNegative ? int.MinValue : int.MaxValue;
        }
    }

    public static string CheckForAlphaChar(string str)
    {
        StringBuilder cleanedString = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            if (Char.IsDigit(str[i]))
            {
                cleanedString.Append(str[i]);
            }
            else
            {
                break;
            }
        }

        return cleanedString.ToString();
    }


    static void Main(string[] args)
    {
        var t = MyAtoi("+1");
    }
}