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

    public static Dictionary<int, string> romanNumeralSubSpecialCases = new Dictionary<int, string>
    {
        { 4, "IV" },
        { 9, "IX" },
        { 40, "XL" },
        { 90, "XC" },
        { 400, "CD" },
        { 900, "CM" }
    };

    public static Dictionary<int, char> romanNumeralValues = new Dictionary<int, char>
    {
        { 1, 'I' },
        { 5, 'V' },
        { 10, 'X' },
        { 50, 'L' },
        { 100, 'C' },
        { 500, 'D' },
        { 1000, 'M' }
    };

    public static string IntToRoman(int num)
    {
        var base10Components = Base10Components(num);

        StringBuilder str = new StringBuilder();

        for (int i = 0; i < base10Components.Count; i++)

        {
            int component = base10Components[i];

            //Check if you need to perform Subtractive Form
            if (romanNumeralSubSpecialCases.ContainsKey(component))
            {
                str.Append(SubtractiveForm(component));
                continue; // Skip to the next component
            }

            //Perform normal conversions
            char currentSymbol = ' ';
            int consecutiveCount = 0;

            while (component > 0)
            {
                char symbolToAppend = ' ';
                int valueToSubtract = 0;

                if (component >= 1000)
                {
                    symbolToAppend = 'M';
                    valueToSubtract = 1000;
                }
                else if (component >= 500)
                {
                    symbolToAppend = 'D';
                    valueToSubtract = 500;
                }
                else if (component >= 100)
                {
                    symbolToAppend = 'C';
                    valueToSubtract = 100;
                }
                else if (component >= 50)
                {
                    symbolToAppend = 'L';
                    valueToSubtract = 50;
                }
                else if (component >= 10)
                {
                    symbolToAppend = 'X';
                    valueToSubtract = 10;
                }
                else if (component >= 5)
                {
                    symbolToAppend = 'V';
                    valueToSubtract = 5;
                }
                else
                {
                    symbolToAppend = 'I';
                    valueToSubtract = 1;
                }

                if (symbolToAppend == currentSymbol)
                {
                    consecutiveCount++;
                    if (consecutiveCount == 4)
                    {
                        // We're about to append the same symbol for the 4th time
                        // Instead, use the subtractive form
                        str.Append(SubtractiveForm(component));
                        break;
                    }
                }
                else
                {
                    currentSymbol = symbolToAppend;
                    consecutiveCount = 1;
                }

                str.Append(symbolToAppend);
                component -= valueToSubtract;
            }
        }

        return str.ToString();
    }

    public static string SubtractiveForm(int num)
    {
        return romanNumeralSubSpecialCases[num];
    }

    public static List<int> Base10Components(int number)
    {
        List<int> components = new List<int>();
        int placeValue = 1;

        while (number > 0)
        {
            int component = (number % 10) * placeValue;
            if (component != 0)
            {
                components.Insert(0, component);
            }
            number /= 10;
            placeValue *= 10;
        }

        return components;
    }

    static void Main(string[] args)
    {
        var t = IntToRoman(1984);
    }
}